/**
 * DrawMode.cs - Texture Drawing Mode Enumeration
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Specifies constants defining possible drawing modes for textures.
    /// </summary>
    public enum DrawMode : byte
    {
        /// <summary>
        /// The texture should be stretched to meet the target resolution if it does
        /// not match the source resolution.
        /// </summary>
        Stretch    = 0,
        
        /// <summary>
        /// The texture should be tiled to fit the target resolution if it does not
        /// match the source resolution.
        /// </summary>
        Tiled      = 1,
        
        /// <summary>
        /// The polygon should be drawn using the tint color only.
        /// </summary>
        SolidColor = 2,
    }
}
