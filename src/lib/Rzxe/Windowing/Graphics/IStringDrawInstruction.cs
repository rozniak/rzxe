/**
 * IStringDrawInstruction - String Drawing Instruction Interface
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
    /// Represents a string drawing instruction during a sprite batching process.
    /// </summary>
    public interface IStringDrawInstruction : IDrawInstruction
    {
        /// <summary>
        /// Gets or sets the color of the string.
        /// </summary>
        Color Color { get; set; }
        
        /// <summary>
        /// Gets or sets the font the string should be drawn using.
        /// </summary>
        IFont Font { get; set; }
        
        /// <summary>
        /// Gets or sets the text of the string.
        /// </summary>
        string Text { get; set; }
    }
}
