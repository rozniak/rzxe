/**
 * SpriteAnimationModel.cs - Sprite Animation Model
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Oddmatics.Rzxe.Game.Animation.Models
{
    /// <summary>
    /// Represents a data model for a sprite-based animation.
    /// </summary>
    internal sealed class SpriteAnimationModel
    {
        /// <summary>
        /// Gets or sets the collection of frames in the animation.
        /// </summary>
        [JsonProperty(PropertyName = "frames")]
        public List<SpriteAnimationFrameModel> Frames { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the animation.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the length of a single tick (in milliseconds) when stepping
        /// through the animation.
        /// </summary>
        [JsonProperty(PropertyName = "tick-size")]
        public double TickSize { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteAnimationModel"/> class.
        /// </summary>
        public SpriteAnimationModel()
        {
            Frames   = new List<SpriteAnimationFrameModel>();
            Name     = string.Empty;
            TickSize = 0;
        }
    }
}
