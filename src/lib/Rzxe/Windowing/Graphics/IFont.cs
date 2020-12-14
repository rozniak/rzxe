/**
 * IFont.cs - Font Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Represents a font.
    /// </summary>
    public interface IFont
    {
        /// <summary>
        /// Gets or sets the character spacing of the font.
        /// </summary>
        int CharacterSpacing { get; set; }
    
        /// <summary>
        /// Gets the type of font.
        /// </summary>
        FontKind FontKind { get; }
        
        /// <summary>
        /// Gets or sets the line spacing of the font.
        /// </summary>
        int LineSpacing { get; set; }
        
        /// <summary>
        /// Gets the name of the font.
        /// </summary>
        string Name { get; }
        
        
        /// <summary>
        /// Measures a string.
        /// </summary>
        /// <param name="text">
        /// The string to measure.
        /// </param>
        /// <returns>
        /// The metrics calculated for the specified string in the font.
        /// </returns>
        StringMetrics MeasureString(
            string text
        );
    }
}