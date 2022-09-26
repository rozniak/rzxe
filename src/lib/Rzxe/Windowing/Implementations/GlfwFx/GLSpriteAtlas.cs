/**
 * GLSpriteAtlas.cs - OpenGL Sprite Atlas Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Newtonsoft.Json;
using Oddmatics.Rzxe.Windowing.Graphics;
using Oddmatics.Rzxe.Windowing.Graphics.Models;
using Oddmatics.Rzxe.Util;
using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL implementation of the sprite atlas interface.
    /// </summary>
    internal sealed class GLSpriteAtlas : DisposableBase, ISpriteAtlas
    {
        /// <inheritdoc />
        public IReadOnlyDictionary<string, IBorderBoxResource> BorderBoxes
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets the size of the atlas.
        /// </summary>
        public Vector2 GlAtlasSize { get; private set; }
        
        /// <summary>
        /// Gets the ID of the texture in OpenGL.
        /// </summary>
        public int GlTextureId { get; private set; }
        
        /// <inheritdoc />
        public string Name { get; private set; }
        
        /// <inheritdoc />
        public Size Size
        {
            get { return new Size((int) GlAtlasSize.X, (int) GlAtlasSize.Y); }
        }
        
        /// <inheritdoc />
        public IReadOnlyDictionary<string, ISprite> Sprites { get; private set; }
        
        
        /// <summary>
        /// The available font models in the atlas.
        /// </summary>
        private IReadOnlyDictionary<string, FontModel> FontModels { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLSpriteAtlas"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the atlas.
        /// </param>
        /// <param name="model">
        /// The data model of the sprite atlas.
        /// </param>
        /// <param name="bitmap">
        /// The sprite atlas bitmap.
        /// </param>
        private GLSpriteAtlas(
            string     name,
            AtlasModel model,
            Bitmap     bitmap
        )
        {
            Disposing = false;
            Name      = name;
        
            // Model setup
            //
            var borderBoxes = new Dictionary<string, IBorderBoxResource>();
            var fontModels  = new Dictionary<string, FontModel>();
            var sprites     = new Dictionary<string, ISprite>();
            
            foreach (SpriteMappingModel spriteMapping in model.SpriteMappings)
            {
                sprites.Add(
                    spriteMapping.Name.ToLower(),
                    new GLSprite(spriteMapping)
                );
            }
            
            Sprites = new ReadOnlyDictionary<string, ISprite>(sprites);
            
            foreach (BorderBoxModel borderBox in model.BorderBoxes)
            {
                borderBoxes.Add(
                    borderBox.Name,
                    new GLBorderBoxResource(this, borderBox)
                );
            }
            
            BorderBoxes =
                new ReadOnlyDictionary<string, IBorderBoxResource>(
                    borderBoxes
                );
            
            foreach (FontModel fontModel in model.Fonts)
            {
                fontModels.Add(
                    fontModel.Name,
                    fontModel
                );
            }
            
            FontModels =
                new ReadOnlyDictionary<string, FontModel>(
                    fontModels
                );

            // GL Setup
            //
            GlAtlasSize = new Vector2(bitmap.Width, bitmap.Height);
            GlTextureId = GLUtility.LoadBitmapTexture(bitmap);
            
            bitmap.Dispose();
        }
        
        
        /// <inheritdoc />
        public override void Dispose()
        {
            base.Dispose();

            GL.DeleteTexture(GlTextureId);
        }
        
        /// <inheritdoc />
        public IFont GetSpriteFont(
            string name,
            int    scale = 1
        )
        {
            AssertNotDisposed();
        
            if (!FontModels.ContainsKey(name))
            {
                throw new KeyNotFoundException(
                    "Font not present in atlas."
                );
            }
            
            return new GLSpriteFont(this, FontModels[name], scale);
        }


        /// <summary>
        /// Creates a <see cref="GLSpriteAtlas"/> from the specified file set.
        /// </summary>
        /// <param name="searchRoot">
        /// The directory path of the root to search for the atlas file set.
        /// </param>
        /// <param name="name">
        /// The name of the atlas.
        /// </param>
        /// <returns>
        /// The <see cref="GLSpriteAtlas"/> this method creates.
        /// </returns>
        internal static GLSpriteAtlas FromFileSet(
            string searchRoot,
            string name
        )
        {
            // Read texture atlas information and bitmap data
            //
            string atlasBmpSrc  = Path.Combine(searchRoot, $"{name}-atlas.png");
            string atlasJsonSrc = Path.Combine(searchRoot, $"{name}-atlas.json");

            var json = File.ReadAllText(atlasJsonSrc);
            
            var bitmap  = (Bitmap) Image.FromFile(atlasBmpSrc);
            var model   = JsonConvert.DeserializeObject<AtlasModel>(json);
            
            return new GLSpriteAtlas(name, model, bitmap);
        }
    }
}
