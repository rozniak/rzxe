/**
 * ActorAnimationFrame.cs - Actor Animation Frame
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Newtonsoft.Json;
using System.Drawing;

namespace Oddmatics.Rzxe.Game.Actors.Animation
{
    /// <summary>
    /// Represents a single frame from an actor's animation.
    /// </summary>
    public class ActorAnimationFrame
    {
        /// <summary>
        /// Gets the offset from the origin of the sprite.
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public Point Offset { get; private set; }
        
        /// <summary>
        /// Gets a value indicating whether an event should be fired when the frame is
        /// entered.
        /// </summary>
        [JsonProperty(PropertyName = "emit_event")]
        public bool ShouldEmitEvent { get; private set; }
        
        /// <summary>
        /// Gets the name of the sprite used in the frame.
        /// </summary>
        [JsonProperty(PropertyName = "sprite")]
        public string SpriteName { get; private set; }
        
        /// <summary>
        /// Gets the number of ticks that the frame persists for.
        /// </summary>
        [JsonProperty(PropertyName = "ticks")]
        public byte Ticks { get; private set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ActorAnimationFrame"/> class.
        /// </summary>
        /// <param name="shouldEmitEvent">
        /// True to specify that an event should be emitted upon entering the frame.
        /// </param>
        /// <param name="offset">
        /// THe offset from the origin of the sprite.
        /// </param>
        /// <param name="spriteName">
        /// The name of the sprite used in the frame.
        /// </param>
        /// <param name="ticks">
        /// The number of ticks that the frame persists for.
        /// </param>
        public ActorAnimationFrame(
            bool   shouldEmitEvent,
            Point  offset,
            string spriteName,
            byte   ticks
        )
        {
            Offset          = offset;
            ShouldEmitEvent = shouldEmitEvent;
            SpriteName      = spriteName;
            Ticks           = ticks;
        }
    }
}
