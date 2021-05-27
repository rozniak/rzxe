/**
 * GLGraphicsController.cs - OpenGL Graphics Controller Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using Pencil.Gaming.Graphics;
using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL implementation of the graphics controller interface.
    /// </summary>
    internal sealed class GLGraphicsController : IGraphicsController
    {
        /// <inheritdoc />
        public Size TargetResolution { get; private set; }
        
        
        /// <summary>
        /// The resource cache for graphics objects.
        /// </summary>
        private GLResourceCache ResourceCache { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLGraphicsController"/> class.
        /// </summary>
        /// <param name="resourceCache">
        /// The resource cache for graphics objects.
        /// </param>
        /// <param name="targetResolution">
        /// The target resolution.
        /// </param>
        public GLGraphicsController(
            GLResourceCache resourceCache,
            Size            targetResolution
        )
        {
            ResourceCache    = resourceCache;
            TargetResolution = targetResolution;
        }
        
        
        /// <inheritdoc />
        public void ClearViewport(
            Color color
        )
        {
            GL.ClearColor(
                (float) color.R / 255,
                (float) color.G / 255,
                (float) color.B / 255,
                1.0f
            );

            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
        
        /// <inheritdoc />
        public ISpriteBatch CreateSpriteBatch(
            ISpriteAtlas         atlas,
            SpriteBatchUsageHint usage
        )
        {
            if (!(atlas is GLSpriteAtlas))
            {
                throw new ArgumentException("Invalid atlas.");
            }
            
            switch (usage)
            {
                case SpriteBatchUsageHint.Dynamic:
                    return new GLDynamicSpriteBatch(
                        this,
                        (GLSpriteAtlas) atlas,
                        ResourceCache
                    );
                    
                case SpriteBatchUsageHint.Static:
                    return new GLStaticSpriteBatch(
                        this,
                        (GLSpriteAtlas) atlas,
                        ResourceCache
                    );
                    
                case SpriteBatchUsageHint.Stream:
                    return new GLStreamSpriteBatch(
                        this,
                        (GLSpriteAtlas) atlas,
                        ResourceCache
                    );

                default:
                    throw new NotSupportedException(
                        "Unsupported sprite batch usage hint."
                    );
            }
        }
        
        /// <inheritdoc />
        public ISpriteAtlas GetSpriteAtlas(
            string name
        )
        {
            return ResourceCache.GetAtlas(name);
        }
    }
}
