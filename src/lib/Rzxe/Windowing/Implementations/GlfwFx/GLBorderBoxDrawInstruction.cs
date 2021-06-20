/**
 * GLBorderBoxDrawInstruction - OpenGL Border Box Drawing Instruction Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using System;
using System.Collections.Generic;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL implementation of the border box drawing instruction interface.
    /// </summary>
    internal sealed class GLBorderBoxDrawInstruction : GLBlitDrawInstruction,
                                                       IBorderBoxDrawInstruction
    {
        /// <inheritdoc />
        public IBorderBoxResource BorderBox
        {
            get { return _BorderBox; }
            set
            {
                SetPropertyValue(ref _BorderBox, ref value);
            }
        }
        private IBorderBoxResource _BorderBox;
        
        
        /// <inheritdoc />
        protected override int BufferSizeRequired
        {
            // 9 sprites make up the box (6 vertices per sprite * 9 sprites)
            //
            get { return 42; }
        }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLBorderBoxDrawInstruction"/>
        /// class.
        /// </summary>
        /// <param name="atlas">
        /// The sprite atlas to use.
        /// </param>
        public GLBorderBoxDrawInstruction(
            ISpriteAtlas atlas
        ) : base(atlas) { }
        
        
        /// <inheritdoc />
        public override object Clone()
        {
            return new GLBorderBoxDrawInstruction((GLSpriteAtlas) Atlas)
            {
                Alpha                = Alpha,
                BorderBox            = BorderBox,
                DestinationRectangle = DestinationRectangle,
                Tint                 = Tint
            };
        }
        
        /// <inheritdoc />
        public override GLVboData Compose()
        {
            var buffer      = new GLVboData((GLSpriteAtlas) Atlas);
            var glBorderBox = (GLBorderBoxResource) BorderBox;
            
            var tl = glBorderBox.GetRect(BorderBoxSegment.TopLeft);
            var tm = glBorderBox.GetRect(BorderBoxSegment.TopMiddle);
            var tr = glBorderBox.GetRect(BorderBoxSegment.TopRight);
            var ml = glBorderBox.GetRect(BorderBoxSegment.MiddleLeft);
            var mm = glBorderBox.GetRect(BorderBoxSegment.MiddleMiddle);
            var mr = glBorderBox.GetRect(BorderBoxSegment.MiddleRight);
            var bl = glBorderBox.GetRect(BorderBoxSegment.BottomLeft);
            var bm = glBorderBox.GetRect(BorderBoxSegment.BottomMiddle);
            var br = glBorderBox.GetRect(BorderBoxSegment.BottomRight);

            // Top section
            //
            buffer.Draw(
                tl,
                new Pencil.Gaming.MathUtils.Rectanglei(
                    DestinationRectangle.X,
                    DestinationRectangle.Y,
                    tl.Width,
                    tl.Height
                ),
                DrawMode.Stretch,
                Tint,
                Alpha
            );

            buffer.Draw(
                tm,
                new Pencil.Gaming.MathUtils.Rectanglei(
                    DestinationRectangle.X + tl.Width,
                    DestinationRectangle.Y,
                    DestinationRectangle.Width - tl.Width - tr.Width,
                    tm.Height
                ),
                DrawMode.Tiled,
                Tint,
                Alpha
            );

            buffer.Draw(
                tr,
                new Pencil.Gaming.MathUtils.Rectanglei(
                    DestinationRectangle.Right - tr.Width,
                    DestinationRectangle.Y,
                    tr.Width,
                    tr.Height
                ),
                DrawMode.Stretch,
                Tint,
                Alpha
            );

            // Middle section
            //
            buffer.Draw(
                ml,
                new Pencil.Gaming.MathUtils.Rectanglei(
                    DestinationRectangle.X,
                    DestinationRectangle.Y + tl.Height,
                    ml.Width,
                    DestinationRectangle.Height - tl.Height - bl.Height
                ),
                DrawMode.Tiled,
                Tint,
                Alpha
            );

            buffer.Draw(
                mm,
                new Pencil.Gaming.MathUtils.Rectanglei(
                    DestinationRectangle.X + tl.Width,
                    DestinationRectangle.Y + tl.Height,
                    DestinationRectangle.Width - ml.Width - mr.Width,
                    DestinationRectangle.Height - tm.Height - bm.Height
                ),
                DrawMode.Tiled,
                Tint,
                Alpha
            );

            buffer.Draw(
                mr,
                new Pencil.Gaming.MathUtils.Rectanglei(
                    DestinationRectangle.Right - mr.Width,
                    DestinationRectangle.Y + tr.Height,
                    mr.Width,
                    DestinationRectangle.Height - tr.Height - br.Height
                ),
                DrawMode.Tiled,
                Tint,
                Alpha
            );

            // Bottom section
            //
            buffer.Draw(
                bl,
                new Pencil.Gaming.MathUtils.Rectanglei(
                    DestinationRectangle.X,
                    DestinationRectangle.Bottom - bl.Height,
                    bl.Width,
                    bl.Height
                ),
                DrawMode.Stretch,
                Tint,
                Alpha
            );

            buffer.Draw(
                bm,
                new Pencil.Gaming.MathUtils.Rectanglei(
                    DestinationRectangle.X + bl.Width,
                    DestinationRectangle.Bottom - bm.Height,
                    DestinationRectangle.Width - bl.Width - br.Width,
                    bm.Height
                ),
                DrawMode.Tiled,
                Tint,
                Alpha
            );

            buffer.Draw(
                br,
                new Pencil.Gaming.MathUtils.Rectanglei(
                    DestinationRectangle.Right - br.Width,
                    DestinationRectangle.Bottom - br.Height,
                    br.Width,
                    br.Height
                ),
                DrawMode.Stretch,
                Tint,
                Alpha
            );
            
            return buffer;
        }
    }
}