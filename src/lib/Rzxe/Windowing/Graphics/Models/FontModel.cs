/**
 * FontModel.cs - Font Data Model
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Collections.Generic;

namespace Oddmatics.Rzxe.Windowing.Graphics.Models
{
    /// <summary>
    /// Represents a data model for sprite font information.
    /// </summary>
    public class FontModel
    {
        /// <summary>
        /// Gets or sets the character spacing of the font.
        /// </summary>
        public int CharacterSpacing { get; set; }
    
        /// <summary>
        /// Gets or sets kerning metrics for character combinations in the font.
        /// </summary>
        /// <remarks>
        /// The kerning provides offsets for shifting the character on the x and y
        /// axes when following certain other characters.
        /// </remarks>
        public Dictionary<string, int[]> Kerning { get; set; }
        
        /// <summary>
        /// Gets or sets the line spacing of the font.
        /// </summary>
        public int LineSpacing { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the font.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the base name when searching for sprites by name to use for
        /// characters in the font.
        /// </summary>
        public string SpriteNameBase { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FontModel"/> class.
        /// </summary>
        public FontModel()
        {
            CharacterSpacing = 0;
            Kerning          = new Dictionary<string, int[]>();
            LineSpacing      = 0;
            Name             = string.Empty;
            SpriteNameBase   = string.Empty;
        }
    }
}
