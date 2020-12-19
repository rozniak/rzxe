/**
 * GLBorderBoxResource.cs - OpenGL Border Box Resource Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using Oddmatics.Rzxe.Windowing.Graphics.Models;
using Pencil.Gaming.MathUtils;
using System;
using System.Collections.Generic;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL implementation of the border box resource interface.
    /// </summary>
    internal sealed class GLBorderBoxResource : IBorderBoxResource
    {
        /// <inheritdoc />
        public string Name { get; private set; }
        
        /// <inheritdoc />
        public EdgeMetrics Slices { get; private set; }

        /// <summary>
        /// Gets the referenced sprite for the border box.
        /// </summary>
        public GLSprite Sprite { get; private set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLBorderBoxResource"/> class.
        /// </summary>
        /// <param name="atlas">
        /// The atlas that contains the sprite data.
        /// </param>
        /// <param name="model">
        /// The data model of the border box.
        /// </param>
        public GLBorderBoxResource(
            GLSpriteAtlas  atlas,
            BorderBoxModel model
        )
        {
            if (!atlas.Sprites.ContainsKey(model.SpriteName))
            {
                throw new KeyNotFoundException(
                    "Sprite for border box not present in atlas."
                );
            }
            
            if (model.Slices.Length != 4)
            {
                throw new ArgumentException(
                    "Invalid border box model - invalid slices."
                );
            }

            Name   = model.Name;
            Slices = new EdgeMetrics(
                         model.Slices[0],
                         model.Slices[1],
                         model.Slices[2],
                         model.Slices[3]
                     );
            Sprite = (GLSprite) atlas.Sprites[model.SpriteName];
        }
        
        
        /// <summary>
        /// Gets the UV region for a particular segment of the border box.
        /// </summary>
        /// <param name="segment">
        /// The border box segment.
        /// </param>
        /// <returns>
        /// The UV region for the segment.
        /// </returns>
        public Rectanglei GetRect(
            BorderBoxSegment segment
        )
        {
            switch (segment)
            {
                case BorderBoxSegment.BottomLeft:
                    return new Rectanglei(
                        Sprite.Bounds.Left,
                        Sprite.Bounds.Bottom - Slices.Bottom,
                        Slices.Left,
                        Slices.Bottom
                    );
                    
                case BorderBoxSegment.BottomMiddle:
                    return new Rectanglei(
                        Sprite.Bounds.Left + Slices.Left,
                        Sprite.Bounds.Bottom - Slices.Bottom,
                        Sprite.Bounds.Width - Slices.Left - Slices.Right,
                        Slices.Bottom
                    );
                    
                case BorderBoxSegment.BottomRight:
                    return new Rectanglei(
                        Sprite.Bounds.Right - Slices.Right,
                        Sprite.Bounds.Bottom - Slices.Bottom,
                        Slices.Right,
                        Slices.Bottom
                    );
                    
                case BorderBoxSegment.MiddleLeft:
                    return new Rectanglei(
                        Sprite.Bounds.Left,
                        Sprite.Bounds.Top + Slices.Top,
                        Slices.Left,
                        Sprite.Bounds.Height - Slices.Top - Slices.Bottom
                    );
                    
                case BorderBoxSegment.MiddleMiddle:
                    return new Rectanglei(
                        Sprite.Bounds.Left + Slices.Left,
                        Sprite.Bounds.Top + Slices.Top,
                        Sprite.Bounds.Width - Slices.Left - Slices.Right,
                        Sprite.Bounds.Height - Slices.Top - Slices.Bottom
                    );
                    
                case BorderBoxSegment.MiddleRight:
                    return new Rectanglei(
                        Sprite.Bounds.Right - Slices.Right,
                        Sprite.Bounds.Top + Slices.Top,
                        Slices.Right,
                        Sprite.Bounds.Height - Slices.Top - Slices.Bottom
                    );
                    
                case BorderBoxSegment.TopLeft:
                    return new Rectanglei(
                        Sprite.Bounds.Left,
                        Sprite.Bounds.Top,
                        Slices.Left,
                        Slices.Top
                    );
                    
                case BorderBoxSegment.TopMiddle:
                    return new Rectanglei(
                        Sprite.Bounds.Left + Slices.Left,
                        Sprite.Bounds.Top,
                        Sprite.Bounds.Width - Slices.Left - Slices.Right,
                        Slices.Top
                    );
                    
                case BorderBoxSegment.TopRight:
                    return new Rectanglei(
                        Sprite.Bounds.Right - Slices.Right,
                        Sprite.Bounds.Top,
                        Slices.Right,
                        Slices.Top
                    );
                    
                default:
                    throw new ArgumentException(
                        "Unknown segment."
                    );
            }
        }
    }
}
