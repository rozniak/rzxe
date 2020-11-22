/**
 * ISprite.cs - Sprite Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Represents a sprite.
    /// </summary>
    public interface ISprite
    {
        /// <summary>
        /// Gets the name of the sprite.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the size of the sprite.
        /// </summary>
        Size Size { get; }
    }
}
