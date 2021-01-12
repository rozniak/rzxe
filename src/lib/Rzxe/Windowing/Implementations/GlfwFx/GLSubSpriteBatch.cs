/**
 * GLSubSpriteBatch.cs - OpenGL Sprite Batch (Portion) Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using Pencil.Gaming.MathUtils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// GLS ub sprite batch.
    /// </summary>
    internal sealed class GLSubSpriteBatch : ISubSpriteBatch
    {
        /// <inheritdoc />
        public ISpriteAtlas Atlas { get; private set; }
        
        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// alpha component data for scaling alpha on sampled textures.
        /// </summary>
        public IList<float> VboAlphaScales
        {
            get { return _VboAlphaScales.AsReadOnly(); }
        }
        private List<float> _VboAlphaScales;

        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// target vertex data.
        /// </summary>
        public IList<float> VboDrawContents
        {
            get { return _VboDrawContents.AsReadOnly(); }
        }
        private List<float> _VboDrawContents;

        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// draw mode data.
        /// </summary>
        public IList<float> VboDrawModes
        {
            get { return _VboDrawModes.AsReadOnly(); }
        }
        private List<float> _VboDrawModes;

        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// origin vertex data.
        /// </summary>
        /// <remarks>
        /// This is used by the shader program in order to do tiling of sprites when
        /// drawing to a region larger than the sprite itself. The origin vertex is
        /// repeated in the VBO so that it can be modulo'd to calculate the UV
        /// co-ordinate to sample from.
        /// </remarks>
        public IList<float> VboOrigins
        {
            get { return _VboOrigins.AsReadOnly(); }
        }
        private List<float> _VboOrigins;

        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// UV source rectangles.
        /// </summary>
        /// <remarks>
        /// This is also used by the shader program for sampling calculations, alongside
        /// <see cref="VboOrigins"/>.
        /// </remarks>
        public IList<float> VboSourceRects
        {
            get { return _VboSourceRects.AsReadOnly(); }
        }
        private List<float> _VboSourceRects;
        
        /// <summary>
        /// Gets the collection of floating-point values that will populate a VBO with
        /// UV vertex data.
        /// </summary>
        public IList<float> VboUvContents
        {
            get { return _VboUvContents.AsReadOnly(); }
        }
        private List<float> _VboUvContents { get; set; }
        
        /// <summary>
        /// Gets the number of vertices to draw.
        /// </summary>
        public int VertexCount { get; private set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLSubSpriteBatch"/> class.
        /// </summary>
        public GLSubSpriteBatch(
            GLSpriteAtlas atlas
        )
        {
            Atlas = atlas;
            
            VertexCount      = 0;
            _VboAlphaScales  = new List<float>();
            _VboDrawContents = new List<float>();
            _VboDrawModes    = new List<float>();
            _VboOrigins      = new List<float>();
            _VboSourceRects  = new List<float>();
            _VboUvContents   = new List<float>();
        }
        
        
        /// <inheritdoc />
        public void Draw(
            ISprite sprite,
            Point   location,
            float   alpha = 1.0f
        )
        {
            Draw(
                ((GLSprite) sprite).Bounds,
                location
            );
        }
        
        /// <inheritdoc />
        public void Draw(
            System.Drawing.Rectangle sourceRect,
            Point                    location,
            float                    alpha = 1.0f
        )
        {
            Draw(
                new Rectanglei(
                    sourceRect.X,
                    sourceRect.Y,
                    sourceRect.Width,
                    sourceRect.Height
                ),
                location,
                alpha
            );
        }
        
        /// <inheritdoc />
        public void Draw(
            ISprite                  sprite,
            System.Drawing.Rectangle destRect,
            DrawMode                 drawMode,
            float                    alpha = 1.0f
        )
        {
            Draw(
                ((GLSprite) sprite).Bounds,
                destRect,
                drawMode,
                alpha
            );
        }
        
        /// <inheritdoc />
        public void Draw(
            System.Drawing.Rectangle sourceRect,
            System.Drawing.Rectangle destRect,
            DrawMode                 drawMode,
            float                    alpha = 1.0f
        )
        {
            Draw(
                new Rectanglei(
                    sourceRect.X,
                    sourceRect.Y,
                    sourceRect.Width,
                    sourceRect.Height
                ),
                new Rectanglei(
                    destRect.X,
                    destRect.Y,
                    destRect.Width,
                    destRect.Height
                ),
                drawMode,
                alpha
            );
        }
        
        /// <inheritdoc />
        public void DrawBorderBox(
            IBorderBoxResource       borderBox,
            System.Drawing.Rectangle destRect,
            float                    alpha = 1.0f
        )
        {
            //
            // Build the border-box rect, it will be made up of 9 segments:
            //
            // tl - tm - tr
            // |    |    |
            // ml - mm - mr
            // |    |    |
            // bl - bm - br
            //
            var glBorderBox = (GLBorderBoxResource) borderBox;
            
            Rectanglei tl = glBorderBox.GetRect(BorderBoxSegment.TopLeft);
            Rectanglei tm = glBorderBox.GetRect(BorderBoxSegment.TopMiddle);
            Rectanglei tr = glBorderBox.GetRect(BorderBoxSegment.TopRight);
            Rectanglei ml = glBorderBox.GetRect(BorderBoxSegment.MiddleLeft);
            Rectanglei mm = glBorderBox.GetRect(BorderBoxSegment.MiddleMiddle);
            Rectanglei mr = glBorderBox.GetRect(BorderBoxSegment.MiddleRight);
            Rectanglei bl = glBorderBox.GetRect(BorderBoxSegment.BottomLeft);
            Rectanglei bm = glBorderBox.GetRect(BorderBoxSegment.BottomMiddle);
            Rectanglei br = glBorderBox.GetRect(BorderBoxSegment.BottomRight);
            
            // Top section
            //
            Draw(
                tl,
                destRect.Location,
                alpha
            );
            
            Draw(
                tm,
                new Rectanglei(
                    destRect.X + tl.Width,
                    destRect.Y,
                    destRect.Width - tl.Width - tr.Width,
                    tm.Height
                ),
                DrawMode.Tiled,
                alpha
            );
            
            Draw(
                tr,
                new Point(
                    destRect.Right - tr.Width,
                    destRect.Y
                ),
                alpha
            );
            
            // Middle section
            //
            Draw(
                ml,
                new Rectanglei(
                    destRect.X,
                    destRect.Y + tl.Height,
                    ml.Width,
                    destRect.Height - tl.Height - bl.Height
                ),
                DrawMode.Tiled,
                alpha
            );
            
            Draw(
                mm,
                new Rectanglei(
                    destRect.X + tl.Width,
                    destRect.Y + tl.Height,
                    destRect.Width - ml.Width - mr.Width,
                    destRect.Height - tm.Height - bm.Height
                ),
                DrawMode.Tiled,
                alpha
            );
            
            Draw(
                mr,
                new Rectanglei(
                    destRect.X + destRect.Width - mr.Width,
                    destRect.Y + tr.Height,
                    mr.Width,
                    destRect.Height - tr.Height - br.Height
                ),
                DrawMode.Tiled,
                alpha
            );
            
            // Bottom section
            //
            Draw(
                bl,
                new Point(
                    destRect.X,
                    destRect.Bottom - bl.Height
                ),
                alpha
            );
            
            Draw(
                bm,
                new Rectanglei(
                    destRect.X + bl.Width,
                    destRect.Bottom - bm.Height,
                    destRect.Width - bl.Width - br.Width,
                    bm.Height
                ),
                DrawMode.Tiled,
                alpha
            );
            
            Draw(
                br,
                new Point(
                    destRect.Right - br.Width,
                    destRect.Bottom - br.Height
                ),
                alpha
            );
        }
        
        /// <inheritdoc />
        public void DrawString(
            string text,
            IFont  font,
            Point  location
        )
        {
            switch (font.FontKind)
            {
                case FontKind.SpriteFont:
                    DrawStringWithSpriteFont(
                        text,
                        (GLSpriteFont) font,
                        location
                    );
                    
                    break;

                default:
                    throw new NotSupportedException(
                        "Unsupported font specified."
                    );
            }
        }
        
        /// <summary>
        /// Merges another sub-batch onto the end of the current batch.
        /// </summary>
        /// <param name="otherBatch">
        /// The sub-batch.
        /// </param>
        public void Merge(
            GLSubSpriteBatch otherBatch
        )
        {
            _VboDrawContents.AddRange(otherBatch.VboDrawContents);
            _VboDrawModes.AddRange(otherBatch.VboDrawModes);
            _VboOrigins.AddRange(otherBatch.VboOrigins);
            _VboSourceRects.AddRange(otherBatch.VboSourceRects);
            _VboUvContents.AddRange(otherBatch.VboUvContents);
            _VboAlphaScales.AddRange(otherBatch.VboAlphaScales);

            VertexCount += otherBatch.VertexCount;
        }
        
        
        /// <summary>
        /// Clones a floating-point value the specified number of times for insertion
        /// into a VBO.
        /// </summary>
        /// <param name="source">
        /// The value.
        /// </param>
        /// <param name="count">
        /// The number of times to clone the value.
        /// </param>
        /// <returns>
        /// The cloned value as an <see cref="IList{float}"/> collection.
        /// </returns>
        private IList<float> CloneVbo(
            float source,
            int   count
        )
        {
            var target = new List<float>();
            
            for (int i = 0; i < count; i++)
            {
                target.Add(source);
            }
            
            return target;
        }
        
        /// <summary>
        /// Clones a collection of floating-point values the specified number of times
        /// for insertion into a VBO.
        /// </summary>
        /// <param name="source">
        /// The values.
        /// </param>
        /// <param name="count">
        /// The number of times to clone the values.
        /// </param>
        /// <returns>
        /// The cloned value as an <see cref="IList{float}"/> collection.
        /// </returns>
        private IList<float> CloneVbo(
            IList<float> source,
            int          count
        )
        {
            var target = new List<float>();
            
            for (int i = 0; i < count; i++)
            {
                target.AddRange(source);
            }
            
            return target;
        }
        
        /// <summary>
        /// Draws the region of the atlas at the specified location.
        /// </summary>
        /// <param name="sourceRect">
        /// The source region on the atlas.
        /// </param>
        /// <param name="location">
        /// The location to draw the region.
        /// </param>
        /// <param name="alpha">
        /// The opacity at which to draw the border box.
        /// </param>
        private void Draw(
            Rectanglei sourceRect,
            Point      location,
            float      alpha
        )
        {
            Draw(
                sourceRect,
                new Rectanglei(
                    new Vector2i(
                        location.X,
                        location.Y
                    ),
                    new Vector2i(
                        sourceRect.Width,
                        sourceRect.Height
                    )
                ),
                DrawMode.Stretch,
                alpha
            );
        }
        
        /// <summary>
        /// Draws the region of the atlas into a rectangular region using the
        /// specified draw mode.
        /// </summary>
        /// <param name="sourceRect">
        /// The source region on the atlas.
        /// </param>
        /// <param name="destRect">
        /// The region to draw into.
        /// </param>
        /// <param name="drawMode">
        /// The mode that defines how the sprite should be drawn.
        /// </param>
        /// <param name="alpha">
        /// The opacity at which to draw the sprite.
        /// </param>
        private void Draw(
            Rectanglei sourceRect,
            Rectanglei destRect,
            DrawMode   drawMode,
            float      alpha
        )
        {
            _VboDrawContents.AddRange(GLUtility.MakeVboData(destRect));
            _VboUvContents.AddRange(GLUtility.MakeVboData(sourceRect));
            
            _VboSourceRects.AddRange(
                CloneVbo(MakeSourceRectData(sourceRect), 6)
            );
            
            _VboOrigins.AddRange(
                CloneVbo(MakeOriginData(destRect.Position), 6)
            );
            
            _VboDrawModes.AddRange(
                CloneVbo((float) drawMode, 6)
            );

            _VboAlphaScales.AddRange(
                CloneVbo(alpha, 6)
            );

            VertexCount += 6;
        }

        /// <summary>
        /// Draws a string at the specified location using a sprite font.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="font">
        /// The sprite font to use.
        /// </param>
        /// <param name="location">
        /// The location to draw the string.
        /// </param>
        private void DrawStringWithSpriteFont(
            string       text,
            GLSpriteFont font,
            Point        location
        )
        {
            string[]      lines    = text.Split('\n');
            char          prevChar = '\0';
            int           xCurrent = location.X;
            int           yOffset  = location.Y;
            
            foreach (string line in lines)
            {
                SingleLineStringMetrics lineMetrics = font.MeasureSingleLine(line);
                int                     yBased      = yOffset + lineMetrics.YBaseline;

                foreach (char c in line)
                {
                    GLSprite sprite;
                    char     thisChar = font.TryGetCharacterSprite(c, out sprite);
                    
                    // Retrieve kerning metrics for the character
                    //
                    int[]  kerning = { 0, 0 };
                    string kernStr = $"{prevChar}{thisChar}";
                    
                    if (
                        prevChar != '\0' &&
                        font.Kerning.ContainsKey(kernStr)
                    )
                    {
                        kerning = font.Kerning[kernStr];
                    }
                    
                    xCurrent += kerning[0] * font.Scale;
                    
                    // Draw at current spot
                    //
                    Draw(
                        sprite,
                        new System.Drawing.Rectangle(
                            new Point(
                                xCurrent,
                                yBased - kerning[1] - (sprite.Size.Height * font.Scale)
                            ),
                            new Size(
                                sprite.Size.Width  * font.Scale,
                                sprite.Size.Height * font.Scale
                            )
                        ),
                        DrawMode.Stretch
                    );

                    // Advance x-offset
                    //
                    xCurrent +=
                        (sprite.Size.Width + font.CharacterSpacing) * font.Scale;
                }

                // We're done with the line - advance offsets
                //
                xCurrent  = 0;
                yOffset  += lineMetrics.Size.Height + (font.LineSpacing * font.Scale);
            }
        }
        
        /// <summary>
        /// Creates VBO data for the origin VBO from a given origin point.
        /// </summary>
        /// <param name="origin">
        /// The origin point.
        /// </param>
        /// <returns>
        /// A collection of floating-point values based on the origin point ready for
        /// insertion into a VBO.
        /// </returns>
        private IList<float> MakeOriginData(
            Vector2i origin        
        )
        {
            var data = new List<float>();
            
            data.Add(origin.X);
            data.Add(origin.Y);
            
            return data;
        }
        
        /// <summary>
        /// Creates VBO data for the source rectangle VBO from a given UV rectangle.
        /// </summary>
        /// <param name="rect">
        /// The source UV rectangle.
        /// </param>
        /// <returns>
        /// A collection of floating-point values based on the source UV rectangle
        /// ready for insertion into a VBO.
        /// </returns>
        private IList<float> MakeSourceRectData(
            Rectanglei rect
        )
        {
            var data = new List<float>();
            
            data.Add(rect.Left);
            data.Add(rect.Top);
            data.Add(rect.Width);
            data.Add(rect.Height);
            
            return data;
        }
    }
}
