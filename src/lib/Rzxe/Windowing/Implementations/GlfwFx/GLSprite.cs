/**
 * GLSprite.cs - OpenGL Sprite Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using Oddmatics.Rzxe.Windowing.Graphics.Models;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL implementation of the sprite interface.
    /// </summary>
    internal sealed class GLSprite : ISprite
    {
        /// <summary>
        /// Gets the bounds of the sprite on the atlas.
        /// </summary>
        public Rectangle Bounds { get; private set; }
        
        /// <inheritdoc />
        public string Name { get; private set; }
        
        /// <inheritdoc />
        public Size Size
        {
            get { return Bounds.Size; }
        }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLSprite"/> class.
        /// </summary>
        /// <param name="model">
        /// The data model of the sprite.
        /// </param>
        public GLSprite(
            SpriteMappingModel model
        )
        {
            Bounds = model.Bounds;
            Name   = model.Name.ToLower();
        }
    }
}
