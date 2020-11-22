/**
 * BorderBoxModel.cs - Border Box Data Model
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

namespace Oddmatics.Rzxe.Windowing.Graphics.Models
{
    /// <summary>
    /// Represents a data model for border box information.
    /// </summary>
    public class BorderBoxModel
    {
        /// <summary>
        /// Gets or sets the name of the border box.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the slices for constructing the border box.
        /// </summary>
        /// <remarks>
        /// This borrows from the idea of 'slices' in CSS for the 'border-image'
        /// property for dividing a box into 9 cells via offsets for slicing a source
        /// image from the top, right, bottom, and left.
        /// </remarks>
        public int[] Slices { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the base sprite that is sliced to construct the
        /// border box.
        /// </summary>
        public string SpriteName { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BorderBoxModel"/> class.
        /// </summary>
        public BorderBoxModel()
        {
            Name       = string.Empty;
            Slices     = new int[] { };
            SpriteName = string.Empty;
        }
    }
}
