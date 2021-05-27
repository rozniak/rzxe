/**
 * ISpriteBatch.cs - Sprite Batch Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Represents a sprite batch for drawing sprites en-masse.
    /// </summary>
    public interface ISpriteBatch : ICloneable, IDisposable
    {
        /// <summary>
        /// Gets the atlas in use.
        /// </summary>
        ISpriteAtlas Atlas { get; }
        
        /// <summary>
        /// Gets the instructions to be executed in the sprite batch.
        /// </summary>
        IList<IDrawInstruction> Instructions { get; }

        /// <summary>
        /// Gets the usage hint that describes I/O properties for the sprite batch.
        /// </summary>
        SpriteBatchUsageHint Usage { get; }
        
        
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
        /// <returns>
        /// The drawing instruction that was generated.
        /// </returns>
        ISpriteDrawInstruction Draw(
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
        /// <returns>
        /// The drawing instruction that was generated.
        /// </returns>
        ISpriteDrawInstruction Draw(
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
        /// <returns>
        /// The drawing instruction that was generated.
        /// </returns>
        ISpriteDrawInstruction Draw(
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
        /// <returns>
        /// The drawing instruction that was generated.
        /// </returns>
        ISpriteDrawInstruction Draw(
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
        /// <returns>
        /// The drawing instruction that was generated.
        /// </returns>
        IBorderBoxDrawInstruction DrawBorderBox(
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
        /// <returns>
        /// The drawing instruction that was generated.
        /// </returns>
        IStringDrawInstruction DrawString(
            string text,
            IFont  font,
            Point  location,
            Color  color
        );
        
        /// <summary>
        /// Commits the sprite batch.
        /// </summary>
        void Finish();
    }
}