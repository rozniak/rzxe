/**
 * SpriteBatchUsageHint - Sprite Batch Usage Hint Enumeration
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Specifies constants defining the I/O usage for a sprite batch.
    /// </summary>
    public enum SpriteBatchUsageHint
    {
        /// <summary>
        /// The sprite batch should persist, and will allow in-place updating after
        /// an initial draw.
        /// </summary>
        Dynamic,
        
        /// <summary>
        /// The sprite batch should persist, it will be locked from further editing
        /// after initial draw.
        /// </summary>
        Static,
        
        /// <summary>
        /// The sprite batch should be disposed after drawing.
        /// </summary>
        Stream
    }
}
