﻿/**
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
        /// Creates a sub-batch that can be drawn into an executed later.
        /// </summary>
        /// <returns>
        /// The newly created sub-batch instance.
        /// </returns>
        ISubSpriteBatch CreateSubBatch();
        
        /// <summary>
        /// Draws a sprite at the specified location.
        /// </summary>
        /// <param name="sprite">
        /// The sprite to draw.
        /// </param>
        /// <param name="location">
        /// The location to draw the sprite.
        /// </param>
        /// <param name="tint">
        /// The colour to tint the sprite with, specify
        /// <see cref="Color.Transparent"/> for no tinting.
        /// </param>
        /// <param name="alpha">
        /// The opacity at which to draw the sprite.
        /// </param>
        void Draw(
            ISprite sprite,
            Point   location,
            Color   tint,
            float   alpha = 1.0f
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
        /// <param name="tint">
        /// The colour to tint the sprite with, specify
        /// <see cref="Color.Transparent"/> for no tinting.
        /// </param>
        /// <param name="alpha">
        /// The opacity at which to draw the sprite.
        /// </param>
        void Draw(
            Rectangle sourceRect,
            Point     location,
            Color     tint,
            float     alpha = 1.0f
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
        /// <param name="tint">
        /// The colour to tint the sprite with, specify
        /// <see cref="Color.Transparent"/> for no tinting.
        /// </param>
        /// <param name="alpha">
        /// The opacity at which to draw the sprite.
        /// </param>
        void Draw(
            ISprite   sprite,
            Rectangle destRect,
            DrawMode  drawMode,
            Color     tint,
            float     alpha = 1.0f
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
        /// <param name="tint">
        /// The colour to tint the sprite with, specify
        /// <see cref="Color.Transparent"/> for no tinting.
        /// </param>
        /// <param name="alpha">
        /// The opacity at which to draw the sprite.
        /// </param>
        void Draw(
            Rectangle sourceRect,
            Rectangle destRect,
            DrawMode  drawMode,
            Color     tint,
            float     alpha = 1.0f
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
        /// <param name="tint">
        /// The colour to tint the sprite with, specify
        /// <see cref="Color.Transparent"/> for no tinting.
        /// </param>
        /// <param name="alpha">
        /// The opacity at which to draw the border box.
        /// </param>
        void DrawBorderBox(
            IBorderBoxResource borderBox,
            Rectangle          destRect,
            Color              tint,
            float              alpha = 1.0f
        );
        
        /// <summary>
        /// Draws a string at the specified location.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="font">
        /// The font to use.
        /// </param>
        /// <param name="location">
        /// The location to draw the string.
        /// </param>
        /// <param name="color">
        /// The colour that the text should be.
        /// </param>
        void DrawString(
            string text,
            IFont  font,
            Point  location,
            Color  color
        );
        
        /// <summary>
        /// Draws a sub-batch.
        /// </summary>
        /// <param name="subBatch">
        /// The sub-batch.
        /// </param>
        void DrawSubBatch(
            ISubSpriteBatch subBatch
        );
        
        /// <summary>
        /// Commits the sprite batch.
        /// </summary>
        void Finish();
    }
}