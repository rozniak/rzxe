/**
 * SpriteAnimationFrameModel.cs - Sprite Animation Frame Model
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
    /// Represents a data model for a single frame in a sprite-based animation.
    /// </summary>
    public class SpriteAnimationFrameModel
    {
        /// <summary>
        /// Gets or sets the value indicating whether an event should be fired when the
        /// frame is entered.
        /// </summary>
        [JsonProperty(PropertyName = "emit_event")]
        public bool ShouldEmitEvent { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of sprites used in the frame.
        /// </summary>
        [JsonProperty(PropertyName = "sprites")]
        public List<SpriteAnimationSpriteDataModel> Sprites { get; set; }

        /// <summary>
        /// Gets or sets the number of ticks that the frame persists for.
        /// </summary>
        [JsonProperty(PropertyName = "ticks")]
        public byte Ticks { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteAnimationFrameModel"/>
        /// class.
        /// </summary>
        public SpriteAnimationFrameModel()
        {
            ShouldEmitEvent = false;
            Sprites         = new List<SpriteAnimationSpriteDataModel>();
            Ticks           = 0;
        }
    }
}
