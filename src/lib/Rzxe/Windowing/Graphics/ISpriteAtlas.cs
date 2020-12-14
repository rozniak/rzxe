/**
 * ISpriteAtlas.cs - Sprite Atlas Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Collections.Generic;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Represents a sprite atlas.
    /// </summary>
    public interface ISpriteAtlas
    {
        /// <summary>
        /// Gets the border boxes defined for the atlas.
        /// </summary>
        IReadOnlyDictionary<string, IBorderBoxResource> BorderBoxes { get; }
        
        /// <summary>
        /// Gets the name of the atlas.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the size of the atlas.
        /// </summary>
        Size Size { get; }
        
        /// <summary>
        /// Gets the sprites defined for the atlas.
        /// </summary>
        IReadOnlyDictionary<string, ISprite> Sprites { get; }
        
        
        /// <summary>
        /// Gets a sprite font from the atlas.
        /// </summary>
        /// <param name="name">
        /// The name of the font.
        /// </param>
        /// <returns>
        /// The sprite font from the atlas, at the requested <paramref name="scale"/>.
        /// </returns>
        IFont GetSpriteFont(
            string name,
            int    scale = 1
        );
    }
}
