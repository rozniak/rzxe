/**
 * GLStringDrawInstruction.cs - OpenGL String Drawing Instruction Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL implementation of the string drawing instruction interface.
    /// </summary>
    internal sealed class GLStringDrawInstruction : GLDrawInstruction,
                                                    IStringDrawInstruction
    {
        /// <inheritdoc />
        public Color Color
        {
            get { return _Color; }
            set
            {
                SetPropertyValue(ref _Color, ref value);
            }
        }
        private Color _Color;
        
        /// <inheritdoc />
        public IFont Font
        {
            get { return _Font; }
            set
            {
                SetPropertyValue(ref _Font, ref value);
            }
        }
        private IFont _Font;
        
        /// <inheritdoc />
        public string Text
        {
            get { return _Text; }
            set
            {
                SetPropertyValue(ref _Text, ref value);
            }
        }
        private string _Text;


        /// <summary>
        /// Initializes a new instance of the <see cref="GLStringDrawInstruction"/>
        /// class.
        /// </summary>
        /// <param name="atlas">
        /// The sprite atlas to use.
        /// </param>
        public GLStringDrawInstruction(
            GLSpriteAtlas atlas
        ) : base(atlas)
        {
            Color = Color.Black;
        }
        
        
        /// <inheritdoc />
        public override object Clone()
        {
            return new GLStringDrawInstruction((GLSpriteAtlas)Atlas)
            {
                Color    = Color,
                Font     = Font,
                Location = Location,
                Text     = Text
            };
        }
        
        /// <inheritdoc />
        public override GLVboData Compose()
        {
            switch (Font.FontKind)
            {
                case FontKind.SpriteFont:
                    return GetBufferForSpriteFont();

                case FontKind.TypeFace:
                    throw new NotImplementedException(); // TODO: Code this

                default:
                    throw new NotSupportedException(
                        "Unsupported font specified."
                    );
            }
        }
        
        
        /// <summary>
        /// Generates a buffer for drawing the string as a sprite font.
        /// </summary>
        /// <returns>
        /// The generated buffer.
        /// </returns>
        private GLVboData GetBufferForSpriteFont()
        {
            var buffer = new GLVboData((GLSpriteAtlas) Atlas);

            // Acquire colour component
            //
            float alpha;
            Color tint;

            SplitFontTintAndAlpha(
                Color,
                out tint,
                out alpha
            );

            // The actual text rendering
            //
            var      glFont   = (GLSpriteFont)Font;
            string[] lines    = Text.Split('\n');
            char     prevChar = '\0';
            int      xCurrent = Location.X;
            int      yOffset  = Location.Y;
            
            foreach (string line in lines)
            {
                SingleLineStringMetrics lineMetrics = glFont.MeasureSingleLine(line);
                int                     yBased      = yOffset + lineMetrics.YBaseline;

                foreach (char c in line)
                {
                    GLSprite sprite;
                    char     thisChar = glFont.TryGetCharacterSprite(c, out sprite);
                    
                    // Retrieve kerning metrics for the character
                    //
                    int[]  kerning = { 0, 0 };
                    string kernStr = $"{prevChar}{thisChar}";
                    
                    if (
                        prevChar != '\0' &&
                        glFont.Kerning.ContainsKey(kernStr)
                    )
                    {
                        kerning = glFont.Kerning[kernStr];
                    }
                    
                    xCurrent += kerning[0] * glFont.Scale;
                    
                    // Draw at current spot
                    //
                    buffer.Draw(
                        new Pencil.Gaming.MathUtils.Rectanglei(
                            sprite.Bounds.X,
                            sprite.Bounds.Y,
                            sprite.Bounds.Width,
                            sprite.Bounds.Height
                        ),
                        new Pencil.Gaming.MathUtils.Rectanglei(
                            xCurrent,
                            yBased - kerning[1] - (sprite.Size.Height * glFont.Scale),
                            sprite.Size.Width  * glFont.Scale,
                            sprite.Size.Height * glFont.Scale
                        ),
                        DrawMode.Stretch,
                        tint,
                        alpha
                    );

                    // Advance x-offset
                    //
                    xCurrent +=
                        (sprite.Size.Width + glFont.CharacterSpacing) * glFont.Scale;
                }

                // We're done with the line - advance offsets
                //
                xCurrent  = 0;
                yOffset  += lineMetrics.Size.Height +
                            (glFont.LineSpacing * glFont.Scale);
            }

            return buffer;
        }
        
        /// <summary>
        /// Splits the tint and alpha components out from a colour for rendering text.
        /// </summary>
        /// <param name="color">
        /// The intended font colour.
        /// </param>
        /// <param name="tint">
        /// (Output) The tint component.
        /// </param>
        /// <param name="alpha">
        /// (Output) The alpha component.
        /// </param>
        private void SplitFontTintAndAlpha(
            Color     color,
            out Color tint,
            out float alpha
        )
        {
            tint  = Color.FromArgb(255, color.R, color.G, color.B);
            alpha = color.A / 255f;
        }
    }
}
