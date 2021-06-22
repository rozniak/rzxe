/**
 * IShapeDrawInstruction.cs - Shape Drawing Instruction Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Util.Shapes;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Represents a shape drawing instruction during a sprite batching process.
    /// </summary>
    public interface IShapeDrawInstruction : IDrawInstruction
    {
        /// <summary>
        /// Gets or sets the color of the shape.
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// Gets or sets the shape to draw.
        /// </summary>
        Shape Shape { get; set; }
    }
}
