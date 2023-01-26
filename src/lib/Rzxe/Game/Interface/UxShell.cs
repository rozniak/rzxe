/**
 * UxShell.cs - User Interface Shell
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using System;

namespace Oddmatics.Rzxe.Game.Interface
{
    /// <summary>
    /// Represents an in-game user interface shell.
    /// </summary>
    public sealed class UxShell : UxContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UxShell"/> class.
        /// </summary>
        public UxShell()
        {
        }
        
        
        /// <inheritdoc />
        public override void Render(
            ISpriteBatch sb
        )
        {
            throw new NotSupportedException(
                "Render() is not valid for UxShell, issue RenderFrame() instead."
            );
        }

        /// <summary>
        /// Renders the shell.
        /// </summary>
        /// <param name="graphics">
        /// The graphics interface for the renderer.
        /// </param>
        public void RenderFrame(
            IGraphicsController graphics
        )
        {
            if (TargetSpriteBatch == null)
            {
                ISpriteAtlas atlas = graphics.GetSpriteAtlas("shell");
                
                TargetSpriteBatch =
                    graphics.CreateSpriteBatch(
                        atlas,
                        SpriteBatchUsageHint.Dynamic
                    );
            }

            RenderContainer(TargetSpriteBatch);
        }
    }
}
