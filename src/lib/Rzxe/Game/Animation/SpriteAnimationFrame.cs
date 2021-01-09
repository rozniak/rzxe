/**
 * SpriteAnimationFrame.cs - Sprite Animation Frame
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Game.Animation.Models;
using System.Collections.Generic;

namespace Oddmatics.Rzxe.Game.Animation
{
    /// <summary>
    /// Represents a frame in a sprite-based animation.
    /// </summary>
    public class SpriteAnimationFrame
    {
        /// <summary>
        /// Gets the value indicating whether an event should be fired when the frame
        /// is entered.
        /// </summary>
        public bool ShouldEmitEvent
        {
            get { return AnimationFrame.ShouldEmitEvent; }
        }
        
        /// <summary>
        /// Gets the collection of sprites used in the frame.
        /// </summary>
        public IList<SpriteAnimationSpriteData> Sprites { get; private set; }

        /// <summary>
        /// Gets the number of ticks that the frame persists for.
        /// </summary>
        public byte Ticks
        {
            get { return AnimationFrame.Ticks; }
        }


        /// <summary>
        /// The underlying animation frame model.
        /// </summary>
        private SpriteAnimationFrameModel AnimationFrame { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteAnimationFrame"/>
        /// class.
        /// </summary>
        /// <param name="frameModel">
        /// The data model that contains information about the frame.
        /// </param>
        public SpriteAnimationFrame(
           SpriteAnimationFrameModel frameModel
        )
        {
            AnimationFrame = frameModel;

            var spriteData = new List<SpriteAnimationSpriteData>();
            
            foreach (SpriteAnimationSpriteDataModel model in AnimationFrame.Sprites)
            {
                spriteData.Add(new SpriteAnimationSpriteData(model));
            }

            Sprites = spriteData.AsReadOnly();
        }
    }
}
