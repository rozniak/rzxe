/**
 * IDrawInstruction.cs - Drawing Instruction Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Represents a single drawing instruction or 'step' during a sprite batching
    /// process.
    /// </summary>
    public interface IDrawInstruction : ICloneable
    {
        /// <summary>
        /// Gets the atlas used as the texture source.
        /// </summary>
        ISpriteAtlas Atlas { get; }

        /// <summary>
        /// Gets or sets the location to draw.
        /// </summary>
        Point Location { get; set; }
    }
}
