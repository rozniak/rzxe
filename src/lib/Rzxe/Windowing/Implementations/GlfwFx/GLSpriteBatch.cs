/**
 * GLSpriteBatch.cs - OpenGL Sprite Batch Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL implementation of the sprite batch interface.
    /// </summary>
    internal sealed class GLSpriteBatch : ISpriteBatch
    {
        /// <inheritdoc />
        public ISpriteAtlas Atlas { get; private set; }
        
        
        /// <summary>
        /// The graphics controller responsible for creating the sprite batch.
        /// </summary>
        private GLGraphicsController OwnerController { get; set; }

        #region GL Stuff
        
        /// <summary>
        /// The ID for the OpenGL uniform variable holding the canvas resolution.
        /// </summary>
        private int GlCanvasResolutionUniformId { get; set; }
        
        /// <summary>
        /// The ID for the OpenGL shader program.
        /// </summary>
        private uint GlProgramId { get; set; }
        
        /// <summary>
        /// The ID for the OpenGL uniform variable holding the UV map resolution.
        /// </summary>
        private int GlUvMapResolutionUniformId { get; set; }
        
        /// <summary>
        /// The collection of floating-point values that will populate a VBO with
        /// target vertex data.
        /// </summary>
        private List<float> VboDrawContents { get; set; }
        
        /// <summary>
        /// The collection of floating-point values that will populate a VBO with draw
        /// mode data.
        /// </summary>
        private List<float> VboDrawModes { get; set; }
        
        /// <summary>
        /// The collection of floating-point values that will populate a VBO with
        /// origin vertex data.
        /// </summary>
        /// <remarks>
        /// This is used by the shader program in order to do tiling of sprites when
        /// drawing to a region larger than the sprite itself. The origin vertex is
        /// repeated in the VBO so that it can be modulo'd to calculate the UV
        /// co-ordinate to sample from.
        /// </remarks>
        private List<float> VboOrigins { get; set; }
        
        /// <summary>
        /// The collection of floating-point values that will populate a VBO with UV
        /// source rectangles.
        /// </summary>
        /// <remarks>
        /// This is also used by the shader program for sampling calculations, alongside
        /// <see cref="VboOrigins"/>.
        /// </remarks>
        private List<float> VboSourceRects { get; set; }
        
        /// <summary>
        /// The collection of floating-point values that will populate a VBO with UV
        /// vertex data.
        /// </summary>
        private List<float> VboUvContents { get; set; }
        
        /// <summary>
        /// The number of vertices to draw.
        /// </summary>
        private int VertexCount { get; set; }

        #endregion

        #region Resource Stuff
        
        /// <summary>
        /// The resource cache for graphics objects.
        /// </summary>
        private GLResourceCache ResourceCache { get; set; }

        #endregion

        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLSpriteBatch"/> class.
        /// </summary>
        /// <param name="owner">
        /// The graphics controller creating the sprite batch.
        /// </param>
        /// <param name="atlas">
        /// The sprite atlas to use.
        /// </param>
        /// <param name="resourceCache">
        /// The resource cache for graphics objects.
        /// </param>
        public GLSpriteBatch(
            GLGraphicsController owner,
            GLSpriteAtlas        atlas,
            GLResourceCache      resourceCache
        )
        {
            OwnerController = owner;

            // Set up resource bits
            //
            Atlas         = atlas;
            ResourceCache = resourceCache;

            // Set up GL fields
            //
            GlProgramId =
                ResourceCache.GetShaderProgram("SimpleUVs"); // FIXME: Hard-coded ew!

            GlCanvasResolutionUniformId =
                GL.GetUniformLocation(
                    GlProgramId,
                    "gCanvasResolution"
                );

            GlUvMapResolutionUniformId =
                GL.GetUniformLocation(
                    GlProgramId,
                    "gUvMapResolution"
                );

            VertexCount     = 0;
            VboDrawContents = new List<float>();
            VboDrawModes    = new List<float>();
            VboOrigins      = new List<float>();
            VboSourceRects  = new List<float>();
            VboUvContents   = new List<float>();
        }
        
        
        /// <inheritdoc />
        public void Draw(
            ISprite sprite,
            Point   location
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
            Point                    location        
        )
        {
            Draw(
                new Rectanglei(
                    sourceRect.X,
                    sourceRect.Y,
                    sourceRect.Width,
                    sourceRect.Height
                ),
                location
            );
        }
        
        /// <inheritdoc />
        public void Draw(
            ISprite                  sprite,
            System.Drawing.Rectangle destRect,
            DrawMode                 drawMode
        )
        {
            Draw(
                ((GLSprite) sprite).Bounds,
                destRect,
                drawMode
            );
        }
        
        /// <inheritdoc />
        public void Draw(
            System.Drawing.Rectangle sourceRect,
            System.Drawing.Rectangle destRect,
            DrawMode                 drawMode
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
                drawMode
            );
        }
        
        /// <inheritdoc />
        public void DrawBorderBox(
            IBorderBoxResource       borderBox,
            System.Drawing.Rectangle destRect
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
                destRect.Location
            );
            
            Draw(
                tm,
                new Rectanglei(
                    destRect.X + tl.Width,
                    destRect.Y,
                    destRect.Width - tl.Width - tr.Width,
                    tm.Height
                ),
                DrawMode.Tiled
            );
            
            Draw(
                tr,
                new Point(
                    destRect.Right - tr.Width,
                    destRect.Y
                )
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
                DrawMode.Tiled
            );
            
            Draw(
                mm,
                new Rectanglei(
                    destRect.X + tl.Width,
                    destRect.Y + tl.Height,
                    destRect.Width - ml.Width - mr.Width,
                    destRect.Height - tm.Height - bm.Height
                ),
                DrawMode.Tiled
            );
            
            Draw(
                mr,
                new Rectanglei(
                    destRect.X + destRect.Width - mr.Width,
                    destRect.Y + tr.Height,
                    mr.Width,
                    destRect.Height - tr.Height - br.Height
                ),
                DrawMode.Tiled
            );
            
            // Bottom section
            //
            Draw(
                bl,
                new Point(
                    destRect.X,
                    destRect.Bottom - bl.Height
                )
            );
            
            Draw(
                bm,
                new Rectanglei(
                    destRect.X + bl.Width,
                    destRect.Bottom - bm.Height,
                    destRect.Width - bl.Width - br.Width,
                    bm.Height
                ),
                DrawMode.Tiled
            );
            
            Draw(
                br,
                new Point(
                    destRect.Right - br.Width,
                    destRect.Bottom - br.Height
                )
            );
        }
        
        /// <inheritdoc />
        public void DrawString(
            string text,
            string fontBaseName,
            Point  location,
            int    scale = 1
        )
        {
            int x = location.X;
            
            foreach (char c in text)
            {
                string spriteName = $"{fontBaseName}_{c}";
                var    sprite     = (GLSprite) Atlas.Sprites[spriteName];
                
                Draw(
                    sprite,
                    new System.Drawing.Rectangle(
                        new Point(
                            x,
                            location.Y - sprite.Bounds.Height * scale
                        ),
                        new Size(
                            sprite.Bounds.Width * scale,
                            sprite.Bounds.Height * scale
                        )
                    ),
                    DrawMode.Stretch
                );
                
                x += sprite.Bounds.Width * scale + scale;
            }
        }
        
        /// <inheritdoc />
        public void Finish()
        {
            // Create VBOs for the batch
            //
            int vboVsVertexPositionId = GL.GenBuffer();
            int vboVsVertexUVId       = GL.GenBuffer();
            int vboVsSourceRectId     = GL.GenBuffer();
            int vboVsOriginId         = GL.GenBuffer();
            int vboVsDrawModeId       = GL.GenBuffer();

            float[] vsVertexPositionData = VboDrawContents.ToArray();
            float[] vsVertexUVData       = VboUvContents.ToArray();
            float[] vsSourceRectData     = VboSourceRects.ToArray();
            float[] vsOriginData         = VboOrigins.ToArray();
            float[] vsDrawModeData       = VboDrawModes.ToArray();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsVertexPositionId);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                new IntPtr(sizeof(float) * vsVertexPositionData.Length),
                vsVertexPositionData,
                BufferUsageHint.StreamDraw
            );

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsVertexUVId);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                new IntPtr(sizeof(float) * vsVertexUVData.Length),
                vsVertexUVData,
                BufferUsageHint.StreamDraw
            );

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsSourceRectId);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                new IntPtr(sizeof(float) * vsSourceRectData.Length),
                vsSourceRectData,
                BufferUsageHint.StreamDraw
            );

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsOriginId);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                new IntPtr(sizeof(float) * vsOriginData.Length),
                vsOriginData,
                BufferUsageHint.StreamDraw
            );

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsDrawModeId);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                new IntPtr(sizeof(float) * vsDrawModeData.Length),
                vsDrawModeData,
                BufferUsageHint.StreamDraw
            );

            // Set up shader program
            //
            GL.UseProgram(GlProgramId);

            GL.Uniform2(
                GlCanvasResolutionUniformId,
                (float) OwnerController.TargetResolution.Width,
                (float) OwnerController.TargetResolution.Height
            );
            
            GL.Uniform2(
                GlUvMapResolutionUniformId,
                ((GLSpriteAtlas) Atlas).GlAtlasSize
            );

            // Bind the atlas
            //
            GL.BindTexture(
                TextureTarget.Texture2D,
                ((GLSpriteAtlas) Atlas).GlTextureId
            );

            // Assign attribs
            //
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsVertexPositionId);
            GL.VertexAttribPointer(
                0,
                2,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );

            GL.EnableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsVertexUVId);
            GL.VertexAttribPointer(
                1,
                2,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );

            GL.EnableVertexAttribArray(2);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsSourceRectId);
            GL.VertexAttribPointer(
                2,
                4,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );

            GL.EnableVertexAttribArray(3);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsOriginId);
            GL.VertexAttribPointer(
                3,
                2,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );

            GL.EnableVertexAttribArray(4);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsDrawModeId);
            GL.VertexAttribPointer(
                4,
                1,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );

            // Draw now!
            //
            GL.DrawArrays(BeginMode.Triangles, 0, VertexCount);

            // Detach and destroy VBOs
            //
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);
            GL.DisableVertexAttribArray(3);
            GL.DisableVertexAttribArray(4);

            GL.DeleteBuffer(vboVsVertexPositionId);
            GL.DeleteBuffer(vboVsVertexUVId);
            GL.DeleteBuffer(vboVsSourceRectId);
            GL.DeleteBuffer(vboVsOriginId);
            GL.DeleteBuffer(vboVsDrawModeId);
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
        private void Draw(
            Rectanglei sourceRect,
            Point      location
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
                DrawMode.Stretch
            );
        }
        
        /// <summary>
        /// Draw the specified sourceRect, destRect and drawMode.
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
        private void Draw(
            Rectanglei sourceRect,
            Rectanglei destRect,
            DrawMode   drawMode
        )
        {
            VboDrawContents.AddRange(GLUtility.MakeVboData(destRect));
            VboUvContents.AddRange(GLUtility.MakeVboData(sourceRect));
            
            VboSourceRects.AddRange(
                CloneVbo(MakeSourceRectData(sourceRect), 6)
            );
            
            VboOrigins.AddRange(
                CloneVbo(MakeOriginData(destRect.Position), 6)
            );
            
            VboDrawModes.AddRange(
                CloneVbo((float) drawMode, 6)
            );

            VertexCount += 6;
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
