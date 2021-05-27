/**
 * ISpriteDrawInstruction.cs - Sprite Drawing Instruction Interface
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
    /// Represents a sprite drawing instruction during a sprite batching process.
    /// </summary>
    public interface ISpriteDrawInstruction : IBlitDrawInstruction
    {
        /// <summary>
        /// Gets or sets the mode that defines how the sprite should be drawn.
        /// </summary>
        DrawMode DrawMode { get; set; }
        
        /// <summary>
        /// Gets or sets the source rectangle that will be sampled from the sprite
        /// atlas.
        /// </summary>
        Rectangle SourceRectangle { get; set; }
        
        /// <summary>
        /// Gets or sets the sprite to draw, if a source rectangle has been defined
        /// instead then this field is null.
        /// </summary>
        ISprite Sprite { get; set; }
    }
}
