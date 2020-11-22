/**
 * IGraphicsController.cs - Graphics Controller Interface
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
    /// Represents a graphics controller used to interact with the underlying renderer.
    /// </summary>
    public interface IGraphicsController
    {
        /// <summary>
        /// Gets the target resolution of the renderer.
        /// </summary>
        Size TargetResolution { get; }
        
        
        /// <summary>
        /// Clears the viewport with the specified color.
        /// </summary>
        /// <param name="color">
        /// The color.
        /// </param>
        void ClearViewport(
            Color color
        );
        
        /// <summary>
        /// Creates a sprite batch.
        /// </summary>
        /// <param name="atlas">
        /// The sprite atlas to use.
        /// </param>
        /// <returns>
        /// The new sprite batch instance.
        /// </returns>
        ISpriteBatch CreateSpriteBatch(
            ISpriteAtlas atlas
        );
        
        /// <summary>
        /// Gets a sprite atlas from the graphics resources.
        /// </summary>
        /// <param name="name">
        /// The name of the atlas.
        /// </param>
        /// <returns>
        /// The sprite atlas.
        /// </returns>
        ISpriteAtlas GetSpriteAtlas(
            string name
        );
    }
}
