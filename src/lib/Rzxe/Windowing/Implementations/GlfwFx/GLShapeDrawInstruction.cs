/**
 * GLShapeDrawInstruction.cs - OpenGL Shape Drawing Instruction Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Util.Shapes;
using Oddmatics.Rzxe.Windowing.Graphics;
using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// Represents a shape drawing instruction during a sprite batching process.
    /// </summary>
    internal sealed class GLShapeDrawInstruction : GLDrawInstruction,
                                                   IShapeDrawInstruction
    {
        /// <inheritdoc />
        public Color Color
        {
            get { return _Color; }
            set
            {
                SetPropertyValue(ref _Color, ref value);
            }
        }
        private Color _Color;

        /// <inheritdoc />
        public Shape Shape
        {
            get { return _Shape; }
            set
            {
                SetPropertyValue(ref _Shape, ref value);
            }
        }
        private Shape _Shape;


        /// <inheritdoc />
        protected override int BufferSizeRequired
        {
            get
            {
                if (Shape == null)
                {
                    return 0;
                }
                
                return Shape.Vertices.Length;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GLShapeDrawInstruction"/>
        /// class.
        /// </summary>
        /// <param name="atlas">
        /// The sprite atlas to draw.
        /// </param>
        public GLShapeDrawInstruction(
            GLSpriteAtlas atlas
        ) : base(atlas)
        {
            Color = Color.Black;
        }
        
        
        /// <inheritdoc />
        public override object Clone()
        {
            return new GLShapeDrawInstruction((GLSpriteAtlas) Atlas)
            {
                Color = Color,
                Shape = Shape
            };
        }
        
        /// <inheritdoc />
        public override GLVboData Compose()
        {
            var buffer = new GLVboData((GLSpriteAtlas) Atlas);

            buffer.Draw(
                Shape,
                new Pencil.Gaming.MathUtils.Vector2i(
                    Location.X,
                    Location.Y
                ),
                Color
            );
            
            return buffer;
        }
    }
}
