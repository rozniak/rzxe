/**
 * AnimationModel.cs - Animation Model
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Oddmatics.Rzxe.Game.Actors.Animation.Models
{
    /// <summary>
    /// Represents a data model for animation information.
    /// </summary>
    internal sealed class AnimationModel
    {
        /// <summary>
        /// Gets or sets the collection of frames in the animation.
        /// </summary>
        [JsonProperty(PropertyName = "frames")]
        public List<ActorAnimationFrame> Frames { get; set; }
        
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
        /// Initializes a new instance of the <see cref="AnimationModel"/> class.
        /// </summary>
        public AnimationModel()
        {
            Frames   = new List<ActorAnimationFrame>();
            Name     = string.Empty;
            TickSize = 0;
        }
    }
}
