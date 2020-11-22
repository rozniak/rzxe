/**
 * ISpriteBatch.cs - Sprite Batch Interface
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
    /// Represents a sprite batch for drawing sprites en-masse.
    /// </summary>
    public interface ISpriteBatch
    {
        /// <summary>
        /// Gets the atlas in use.
        /// </summary>
        ISpriteAtlas Atlas { get; }
        
        
        /// <summary>
        /// Draws a sprite at the specified location.
        /// </summary>
        /// <param name="sprite">
        /// The sprite to draw.
        /// </param>
        /// <param name="location">
        /// The location to draw the sprite.
        /// </param>
        void Draw(
            ISprite sprite,
            Point   location
        );
        
        /// <summary>
        /// Draws a region of the atlas at the specified location.
        /// </summary>
        /// <param name="sourceRect">
        /// The source region on the atlas.
        /// </param>
        /// <param name="location">
        /// The location to draw the region.
        /// </param>
        void Draw(
            Rectangle sourceRect,
            Point     location
        );
        
        /// <summary>
        /// Draws a sprite in the specified region, scaling if necessary using the
        /// given draw mode.
        /// </summary>
        /// <param name="sprite">
        /// The sprite to draw.
        /// </param>
        /// <param name="destRect">
        /// The region to draw into.
        /// </param>
        /// <param name="drawMode">
        /// The mode that defines how the sprite should be drawn.
        /// </param>
        void Draw(
            ISprite   sprite,
            Rectangle destRect,
            DrawMode  drawMode
        );
        
        /// <summary>
        /// Draws a region of the atlas in the specified region, scaling if necessary
        /// using the given draw mode.
        /// </summary>
        /// <param name="sourceRect">
        /// The source region on the atlas.
        /// </param>
        /// <param name="destRect">
        /// The region to draw into.
        /// </param>
        /// <param name="drawMode">
        /// The mode that defines how the region should be drawn.
        /// </param>
        void Draw(
            Rectangle sourceRect,
            Rectangle destRect,
            DrawMode  drawMode
        );
        
        /// <summary>
        /// Draws a border box at the specified location.
        /// </summary>
        /// <param name="borderBox">
        /// The border box.
        /// </param>
        /// <param name="destRect">
        /// The region to draw into.
        /// </param>
        void DrawBorderBox(
            IBorderBoxResource borderBox,
            Rectangle          destRect
        );
        
        /// <summary>
        /// Draws a string at the specified location.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="fontNameBase">
        /// The base name of the sprite font present in the atlas.
        /// </param>
        /// <param name="location">
        /// The location to draw the string.
        /// </param>
        /// <param name="scale">
        /// The scale for the sprite font.
        /// </param>
        void DrawString(
            string text,
            string fontNameBase,
            Point  location,
            int    scale = 1
        );
        
        /// <summary>
        /// Commits the sprite batch.
        /// </summary>
        void Finish();
    }
}