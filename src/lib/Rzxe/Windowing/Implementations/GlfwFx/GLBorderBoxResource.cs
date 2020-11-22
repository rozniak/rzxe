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
        
        /// <summary>
        /// Gets the referenced sprite for the border box.
        /// </summary>
        public GLSprite Sprite { get; private set; }
        
        
        /// <summary>
        /// The offset for the bottom slice of the sprite.
        /// </summary>
        private int BottomSlice { get; set; }
        
        /// <summary>
        /// The offset for the left slice of the sprite.
        /// </summary>
        private int LeftSlice { get; set; }
        
        /// <summary>
        /// The offset for the right slice of the sprite.
        /// </summary>
        private int RightSlice { get; set; }
        
        /// <summary>
        /// The offset for the top slice of the sprite.
        /// </summary>
        private int TopSlice { get; set; }
        
        
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
            Sprite = (GLSprite) atlas.Sprites[model.SpriteName];
            
            TopSlice    = model.Slices[0];
            RightSlice  = model.Slices[1];
            BottomSlice = model.Slices[2];
            LeftSlice   = model.Slices[3];
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
                        Sprite.Bounds.Bottom - BottomSlice,
                        LeftSlice,
                        BottomSlice
                    );
                    
                case BorderBoxSegment.BottomMiddle:
                    return new Rectanglei(
                        Sprite.Bounds.Left + LeftSlice,
                        Sprite.Bounds.Bottom - BottomSlice,
                        Sprite.Bounds.Width - LeftSlice - RightSlice,
                        BottomSlice
                    );
                    
                case BorderBoxSegment.BottomRight:
                    return new Rectanglei(
                        Sprite.Bounds.Right - RightSlice,
                        Sprite.Bounds.Bottom - BottomSlice,
                        RightSlice,
                        BottomSlice
                    );
                    
                case BorderBoxSegment.MiddleLeft:
                    return new Rectanglei(
                        Sprite.Bounds.Left,
                        Sprite.Bounds.Top + TopSlice,
                        LeftSlice,
                        Sprite.Bounds.Height - TopSlice - BottomSlice
                    );
                    
                case BorderBoxSegment.MiddleMiddle:
                    return new Rectanglei(
                        Sprite.Bounds.Left + LeftSlice,
                        Sprite.Bounds.Top + TopSlice,
                        Sprite.Bounds.Width - LeftSlice - RightSlice,
                        Sprite.Bounds.Height - TopSlice - BottomSlice
                    );
                    
                case BorderBoxSegment.MiddleRight:
                    return new Rectanglei(
                        Sprite.Bounds.Right - RightSlice,
                        Sprite.Bounds.Top + TopSlice,
                        RightSlice,
                        Sprite.Bounds.Height - TopSlice - BottomSlice
                    );
                    
                case BorderBoxSegment.TopLeft:
                    return new Rectanglei(
                        Sprite.Bounds.Left,
                        Sprite.Bounds.Top,
                        LeftSlice,
                        TopSlice
                    );
                    
                case BorderBoxSegment.TopMiddle:
                    return new Rectanglei(
                        Sprite.Bounds.Left + LeftSlice,
                        Sprite.Bounds.Top,
                        Sprite.Bounds.Width - LeftSlice - RightSlice,
                        TopSlice
                    );
                    
                case BorderBoxSegment.TopRight:
                    return new Rectanglei(
                        Sprite.Bounds.Right - RightSlice,
                        Sprite.Bounds.Top,
                        RightSlice,
                        TopSlice
                    );
                    
                default:
                    throw new ArgumentException(
                        "Unknown segment."
                    );
            }
        }
    }
}
