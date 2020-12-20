/**
 * GLSpriteFont.cs - OpenGL Sprite Font Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using Oddmatics.Rzxe.Windowing.Graphics.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL implementation of the font interface for sprite fonts.
    /// </summary>
    internal sealed class GLSpriteFont : IFont
    {
        /// <inheritdoc />
        public int CharacterSpacing { get; set; }
    
        /// <summary>
        /// Gets kerning metrics for character combinations in the font.
        /// </summary>
        public IReadOnlyDictionary<string, int[]> Kerning { get; private set; }
    
        /// <inheritdoc />
        public FontKind FontKind { get { return FontKind.SpriteFont; } }
        
        /// <inheritdoc />
        public int LineSpacing { get; set; }
        
        /// <inheritdoc />
        public string Name { get; private set; }
        
        /// <summary>
        /// Gets the scale of the font.
        /// </summary>
        public int Scale { get; private set; }
        
        /// <summary>
        /// Gets the base name when searching for sprites by name to use for
        /// characters in the font.
        /// </summary>
        public string SpriteNameBase { get; private set; }
        
        
        /// <summary>
        /// The atlas that contains the font.
        /// </summary>
        private GLSpriteAtlas Atlas { get; set; }
        
        /// <summary>
        /// The sprites for characters defined in the font.
        /// </summary>
        private IReadOnlyDictionary<char, GLSprite> CharacterSprites { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLSpriteFont"/> class.
        /// </summary>
        /// <param name="atlas">
        /// The sprite atlas containig the font.
        /// </param>
        /// <param name="model">
        /// The data model of the font.
        /// </param>
        /// <param name="scale">
        /// (Optional) The scale of the font.
        /// </param>
        public GLSpriteFont(
            GLSpriteAtlas atlas,
            FontModel     model,
            int           scale = 1
        )
        {
            Atlas            = atlas;
            CharacterSpacing = model.CharacterSpacing;
            Kerning          = new ReadOnlyDictionary<string, int[]>(model.Kerning);
            LineSpacing      = model.LineSpacing;
            Name             = model.Name;
            Scale            = scale;
            SpriteNameBase   = model.SpriteNameBase;
            
            if (scale < 1)
            {
                throw new ArgumentOutOfRangeException(
                    $"{scale} must be greater or equal to 1."
                );
            }
            
            // Gather sprites for the font
            //
            int baseEnd  = SpriteNameBase.Length;
            var charDict = new Dictionary<char, GLSprite>();
            var found    = Atlas.Sprites.Where(
                               (pair) => pair.Key.Contains($"{SpriteNameBase}")
                           );
            
            foreach (var pair in found)
            {
                charDict.Add(
                    pair.Key[baseEnd], // Strip the char off the end like 'base_font_z'
                    (GLSprite) pair.Value
                );
            }
            
            CharacterSprites = new ReadOnlyDictionary<char, GLSprite>(charDict);
        }
        
        
        /// <inheritdoc />
        public SingleLineStringMetrics MeasureSingleLine(
            string text
        )
        {
            return MeasureLineOfString(text, 0);
        }

        /// <inheritdoc />
        public StringMetrics MeasureString(
            string text
        )
        {
            int      currentYOrigin = 0;
            string[] lines          = text.Split('\n');
            var      metrics        = new List<SingleLineStringMetrics>();
            
            foreach (string line in lines)
            {
                SingleLineStringMetrics nextMetrics =
                    MeasureLineOfString(
                        line,
                        currentYOrigin
                    );
            
                metrics.Add(nextMetrics);
                
                currentYOrigin += nextMetrics.Size.Height + LineSpacing;
            }
            
            return new StringMetrics(metrics, LineSpacing);
        }
        
        /// <summary>
        /// Gets the sprite for a particular character in the font.
        /// </summary>
        /// <param name="c">
        /// The character.
        /// </param>
        /// <param name="result">
        /// When this method returns, contains the sprite that was retrieved based on
        /// the specified character - if the character does not exist in the font then
        /// the default sprite will be referenced instead.
        /// </param>
        /// <returns>
        /// The character whose sprite was retrieved - this may be the default character
        /// if the specified sprite has no associated sprite in the font.
        /// </returns>
        public char TryGetCharacterSprite(
            char         c,
            out GLSprite result
        )
        {
            char theChar = c;
        
            // Check we have the character in the font... if we don't then we need
            // to replace it with a ?
            //
            if (!CharacterSprites.ContainsKey(c))
            {
                if (!CharacterSprites.ContainsKey('?'))
                {
                    throw new InvalidOperationException(
                        "No way of handling a missing character in the font."
                    );
                }
                    
                theChar = '?';
            }

            result = CharacterSprites[theChar];
            
            return theChar;
        }
        
        
        /// <summary>
        /// Measures a single line of text.
        /// </summary>
        /// <param name="line">
        /// The line of text.
        /// </param>
        /// <param name="currentYOrigin">
        /// The y-coordinate for the origin of the text, based on any lines that came
        /// before the one being measured.
        /// </param>
        /// <returns>
        /// The metrics calculated for the specified line of text in the font.
        /// </returns>
        private SingleLineStringMetrics MeasureLineOfString(
            string line,
            int    currentYOrigin
        )
        {
            char prevChar        = '\0';
            int  maxNegYBaseline = 0;
            int  maxPosYBaseline = 0;
            int  maxWidth        = 0;
            
            foreach (char c in line)
            {
                GLSprite sprite;
                char     thisChar = TryGetCharacterSprite(c, out sprite);
                
                // Retrieve kerning metrics (if present)
                //
                int[]  kerning = { 0, 0 };
                string kernStr = $"{prevChar}{thisChar}";
                
                if (
                    prevChar != '\0' &&
                    Kerning.ContainsKey(kernStr)
                )
                {
                    kerning = Kerning[kernStr];
                }
                
                // Calculate the metrics now
                //
                int netAdvance      = (
                                          sprite.Size.Width +
                                          CharacterSpacing  +
                                          kerning[0]
                                      ) * Scale;
                int netNegYBaseline = kerning[1] * Scale;
                int netPosYBaseline = (sprite.Size.Height - kerning[1]) * Scale;
                
                if (netAdvance > 0)
                {
                    maxWidth += netAdvance;
                }
                
                if (netNegYBaseline > maxNegYBaseline)
                {
                    maxNegYBaseline = netNegYBaseline;
                }
                
                if (netPosYBaseline > maxPosYBaseline)
                {
                    maxPosYBaseline = netPosYBaseline;
                }

                prevChar = thisChar;
            }

            // The above loop will have an extra space - we subtract that now
            //
            maxWidth -= CharacterSpacing;
            
            return new SingleLineStringMetrics(
                new Point(0, currentYOrigin),
                new Size(maxWidth, maxPosYBaseline + maxNegYBaseline),
                maxPosYBaseline
            );
        }
    }
}
