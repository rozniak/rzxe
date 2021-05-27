/**
 * IBorderBoxDrawInstruction - Border Box Drawing Instruction Interface
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
    /// Represents a border box drawing instruction during a sprite batching
    /// process.
    /// </summary>
    public interface IBorderBoxDrawInstruction : IBlitDrawInstruction
    {
        /// <summary>
        /// Gets or sets the border box.
        /// </summary>
        IBorderBoxResource BorderBox { get; set; }
    }
}
