/**
 * SpriteMappingModel.cs - Sprite Mapping Data Model
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics.Models
{
    /// <summary>
    /// Represents a data model for sprite mapping information.
    /// </summary>
    public class SpriteMappingModel
    {
        /// <summary>
        /// Gets or sets the bounds of the sprite on the atlas.
        /// </summary>
        public Rectangle Bounds { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the sprite.
        /// </summary>
        public string Name { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteMappingModel"/> class.
        /// </summary>
        public SpriteMappingModel()
        {
            Bounds = Rectangle.Empty;
            Name   = string.Empty;
        }
    }
}
