/**
 * IBlitDrawInstruction - Blit Drawing Instruction Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Represents a general 'blitting' draw instruction during a sprite batching
    /// process.
    /// </summary>
    public interface IBlitDrawInstruction : IDrawInstruction
    {
        /// <summary>
        /// Gets or sets the alpha component for drawing.
        /// </summary>
        float Alpha { get; set; }
        
        /// <summary>
        /// Gets or sets the destination rectangle for drawing.
        /// </summary>
        Rectangle DestinationRectangle { get; set; }
        
        /// <summary>
        /// Gets or sets the size of the drawing.
        /// </summary>
        Size Size { get; set; }
        
        /// <summary>
        /// Gets or sets the tint for drawing.
        /// </summary>
        Color Tint { get; set; }
    }
}
