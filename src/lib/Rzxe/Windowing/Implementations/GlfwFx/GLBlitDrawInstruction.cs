/**
 * GLBlitDrawInstruction.cs - OpenGL Blit Drawing Instruction Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL implementation of the general 'blitting' draw instruction 
    /// interface.
    /// </summary>
    internal abstract class GLBlitDrawInstruction : GLDrawInstruction,
                                                    IBlitDrawInstruction
    {
        /// <inheritdoc />
        public float Alpha
        {
            get { return _Alpha; }
            set
            {
                _Alpha = value;
                Invalidate();
            }
        }
        private float _Alpha;

        /// <inheritdoc />
        public Rectangle DestinationRectangle
        {
            get { return _DestinationRectangle; }
            set
            {
                SetPropertyValue(ref _DestinationRectangle, ref value);
            }
        }
        private Rectangle _DestinationRectangle;
        
        /// <inheritdoc />
        public override Point Location
        {
            get { return DestinationRectangle.Location; }
            set
            {
                if (DestinationRectangle.Location == value)
                {
                    return;
                }

                DestinationRectangle =
                    new Rectangle(
                        value,
                        DestinationRectangle.Size
                    );
            }
        }

        /// <inheritdoc />
        public Size Size
        {
            get { return DestinationRectangle.Size; }
            set
            {
                if (DestinationRectangle.Size == value)
                {
                    return;
                }

                DestinationRectangle =
                    new Rectangle(
                        DestinationRectangle.Location,
                        value
                    );
            }
        }

        /// <inheritdoc />
        public Color Tint
        {
            get { return _Tint; }
            set
            {
                SetPropertyValue(ref _Tint, ref value);
            }
        }
        private Color _Tint;


        /// <summary>
        /// Initializes a new instance of the <see cref="IBlitDrawInstruction"/>
        /// class.
        /// </summary>
        /// <param name="atlas">
        /// The sprite atlas to use.
        /// </param>
        public GLBlitDrawInstruction(
            ISpriteAtlas atlas
        ) : base(atlas)
        {
            Alpha = 1.0f;
            Tint  = Color.Transparent;
        }
    }
}
