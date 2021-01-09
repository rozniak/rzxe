/**
 * SpriteAnimationSpriteDataModel.cs - Sprite Animation Sprite Data Model
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Newtonsoft.Json;
using System.Drawing;

namespace Oddmatics.Rzxe.Game.Animation.Models
{
    /// <summary>
    /// Represents a data model for metadata for a sprite used in a single frame of a
    /// sprite-based animation.
    /// </summary>
    public class SpriteAnimationSpriteDataModel
    {
        /// <summary>
        /// Gets or sets the offset from the origin of the sprite.
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public Point Offset { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the sprite used in the frame.
        /// </summary>
        [JsonProperty(PropertyName = "sprite")]
        public string SpriteName { get; set; }


        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SpriteAnimationSpriteDataModel"/> class.
        /// </summary>
        public SpriteAnimationSpriteDataModel()
        {
            Offset     = Point.Empty;
            SpriteName = string.Empty;
        }
    }
}
