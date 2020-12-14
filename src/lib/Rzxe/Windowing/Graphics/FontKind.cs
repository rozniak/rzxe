/**
 * FontKind.cs - Font Kind Enumeration
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Specifies constants defining possible types of font.
    /// </summary>
    public enum FontKind
    {
        /// <summary>
        /// The font is a sprite font, derived from a sprite atlas.
        /// </summary>
        SpriteFont,
        
        /// <summary>
        /// The font is a type face, derived from a font file or the operating system.
        /// </summary>
        TypeFace
    }
}
