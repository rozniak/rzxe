/**
 * GLSpriteDrawInstruction.cs - OpenGL Sprite Drawing Instruction Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL implementation of the sprite drawing instruction interface.
    /// </summary>
    internal sealed class GLSpriteDrawInstruction : GLBlitDrawInstruction,
                                                    ISpriteDrawInstruction
    {
        /// <inheritdoc />
        public DrawMode DrawMode
        {
            get { return _DrawMode; }
            set
            {
                SetPropertyValue(ref _DrawMode, ref value);
            }
        }
        private DrawMode _DrawMode;
        
        /// <inheritdoc />
        public Rectangle SourceRectangle
        {
            get { return _SourceRectangle; }
            set
            {
                if (_SourceRectangle == value)
                {
                    return;
                }
                
                if (!new Rectangle(Point.Empty, Atlas.Size).Contains(value))
                {
                    throw new ArgumentException(
                        "The source rectangle is not contained within the atlas."
                    );
                }

                _SourceRectangle = value;
                _Sprite          = null;
                Invalidate();
            }
        }
        private Rectangle _SourceRectangle;
        
        /// <inheritdoc />
        public ISprite Sprite
        {
            get { return _Sprite; }
            set
            {
                if (_Sprite == value)
                {
                    return;
                }
                
                if (!Atlas.Sprites.ContainsKey(value.Name))
                {
                    throw new ArgumentException(
                        "Sprite does not exist in the atlas."
                    );
                }

                _SourceRectangle = ((GLSprite) value).Bounds;
                _Sprite          = value;
                Invalidate();
            }
        }
        private ISprite _Sprite;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLSpriteDrawInstruction"/>
        /// class.
        /// </summary>
        /// <param name="atlas">
        /// The sprite atlas to use.
        /// </param>
        public GLSpriteDrawInstruction(
            GLSpriteAtlas atlas
        ) : base(atlas)
        {
            DrawMode = DrawMode.Stretch;
        }
        
        
        /// <inheritdoc />
        public override object Clone()
        {
            var cloned = new GLSpriteDrawInstruction((GLSpriteAtlas) Atlas)
                         {
                             Alpha                = Alpha,
                             DestinationRectangle = DestinationRectangle,
                             DrawMode             = DrawMode,
                             Tint                 = Tint
                         };
            
            if (Sprite != null)
            {
                cloned.Sprite = Sprite;
            }
            else
            {
                cloned.SourceRectangle = SourceRectangle;
            }

            return cloned;
        }

        /// <inheritdoc />
        public override GLVboData Compose()
        {
            var buffer = new GLVboData((GLSpriteAtlas) Atlas);

            buffer.Draw(
                new Pencil.Gaming.MathUtils.Rectanglei(
                    SourceRectangle.X,
                    SourceRectangle.Y,
                    SourceRectangle.Width,
                    SourceRectangle.Height
                ),
                new Pencil.Gaming.MathUtils.Rectanglei(
                    DestinationRectangle.X,
                    DestinationRectangle.Y,
                    DestinationRectangle.Width,
                    DestinationRectangle.Height
                ),
                DrawMode,
                Tint,
                Alpha
            );
            
            return buffer;
        }
    }
}
