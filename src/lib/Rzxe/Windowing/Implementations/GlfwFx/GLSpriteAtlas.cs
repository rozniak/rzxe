using Newtonsoft.Json;
using Oddmatics.Rzxe.Windowing.Graphics;
using Oddmatics.Rzxe.Windowing.Graphics.Models;
using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    internal sealed class GLSpriteAtlas : ISpriteAtlas, IDisposable
    {
        public IReadOnlyDictionary<string, IBorderBoxResource> BorderBoxes
        {
            get;
            private set;
        }
        
        public Vector2 GlAtlasSize { get; private set; }
    
        public int GlTextureId { get; private set; }

        public string Name { get; private set; }

        public Size Size
        {
            get { return new Size((int) GlAtlasSize.X, (int) GlAtlasSize.Y); }
        }

        public IReadOnlyDictionary<string, ISprite> Sprites { get; private set; }


        private bool Disposing { get; set; }

        

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
            
            // GL Setup
            //
            GlAtlasSize = new Vector2(bitmap.Width, bitmap.Height);
            GlTextureId = GLUtility.LoadBitmapTexture(bitmap);
            
            bitmap.Dispose();
        }


        public void Dispose()
        {
            if (Disposing)
            {
                throw new ObjectDisposedException(Name);
            }

            Disposing = true;

            GL.DeleteTexture(GlTextureId);
        }


        internal static GLSpriteAtlas FromFileSet(string searchRoot, string name)
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
