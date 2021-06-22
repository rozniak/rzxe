/**
 * AtlasModel.cs - Sprite Atlas Data Model
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
    /// Represents a data model for sprite atlas information.
    /// </summary>
    public sealed class AtlasModel
    {
        /// <summary>
        /// Gets or sets the collection of border box definitions.
        /// </summary>
        public List<BorderBoxModel> BorderBoxes { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of sprite font definitions.
        /// </summary>
        public List<FontModel> Fonts { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of sprite mapping definitions.
        /// </summary>
        public List<SpriteMappingModel> SpriteMappings { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AtlasModel"/> class.
        /// </summary>
        public AtlasModel()
        {
            BorderBoxes    = new List<BorderBoxModel>();
            Fonts          = new List<FontModel>();
            SpriteMappings = new List<SpriteMappingModel>();
        }
    }
}
