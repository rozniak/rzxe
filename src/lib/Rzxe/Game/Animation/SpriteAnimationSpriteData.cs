/**
 * SpriteAnimationSpriteData.cs - Sprite Animation Sprite Data
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Game.Animation.Models;
using System.Drawing;

namespace Oddmatics.Rzxe.Game.Animation
{
    /// <summary>
    /// Represents a sprite used in a single frame of a sprite-based animation.
    /// </summary>
    public class SpriteAnimationSpriteData
    {
        /// <summary>
        /// Gets the offset from the origin of the sprite.
        /// </summary>
        public Point Offset
        {
            get { return SpriteData.Offset; }
        }
        
        /// <summary>
        /// Gets the name of the sprite used in the frame.
        /// </summary>
        public string SpriteName
        {
            get { return SpriteData.SpriteName; }
        }


        /// <summary>
        /// The underlying sprite data model.
        /// </summary>
        private SpriteAnimationSpriteDataModel SpriteData { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteAnimationSpriteData"/>
        /// class.
        /// </summary>
        public SpriteAnimationSpriteData(
            SpriteAnimationSpriteDataModel spriteModel
        )
        {
            SpriteData = spriteModel;
        }
    }
}
