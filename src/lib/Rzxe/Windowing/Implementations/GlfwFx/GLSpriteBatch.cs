using Oddmatics.Rzxe.Windowing.Graphics;
using Pencil.Gaming.Graphics;
using Pencil.Gaming.MathUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    internal sealed class GLSpriteBatch : ISpriteBatch
    {
        private GLGraphicsController OwnerController { get; set; }

        #region GL Stuff

        private int GlCanvasResolutionUniformId { get; set; }

        private uint GlProgramId { get; set; }

        private int GlUvMapResolutionUniformId { get; set; }

        private List<float> VboDrawContents { get; set; }
        
        private List<float> VboDrawModes { get; set; }
        
        private List<float> VboOrigins { get; set; }

        private List<float> VboSourceRects { get; set; }

        private List<float> VboUvContents { get; set; }

        private int VertexCount { get; set; }

        #endregion

        #region Resource Stuff

        private GLResourceCache ResourceCache { get; set; }

        private GLSpriteAtlas SpriteAtlas { get; set; }

        #endregion

        
        public GLSpriteBatch(
            GLGraphicsController owner,
            string atlasName,
            GLResourceCache resourceCache
            )
        {
            OwnerController = owner;

            // Set up resource bits
            //
            ResourceCache = resourceCache;
            SpriteAtlas = ResourceCache.GetAtlas(atlasName);

            // Set up GL fields
            //
            GlProgramId = ResourceCache.GetShaderProgram("SimpleUVs"); // FIXME: Hard-coded ew!

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
        
        
        public void Draw(
            string spriteName,
            Point  location
        )
        {
            Draw(
                SpriteAtlas.GetSpriteUV(spriteName),
                location
            );
        }
        
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

        public void Draw(
            string                   spriteName,
            System.Drawing.Rectangle rect,
            DrawMode                 drawMode
        )
        {
            Draw(
                SpriteAtlas.GetSpriteUV(spriteName),
                rect,
                drawMode
            );
        }
        
        public void Draw(
            System.Drawing.Rectangle sourceRect,
            System.Drawing.Rectangle rect,
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
                rect,
                drawMode
            );
        }

        public void DrawBorderBox(
            string                   spriteName,
            System.Drawing.Rectangle rect
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
            Rectanglei tl = SpriteAtlas.GetSpriteUV(spriteName + "_tl");
            Rectanglei tm = SpriteAtlas.GetSpriteUV(spriteName + "_tm");
            Rectanglei tr = SpriteAtlas.GetSpriteUV(spriteName + "_tr");
            Rectanglei ml = SpriteAtlas.GetSpriteUV(spriteName + "_ml");
            Rectanglei mm = SpriteAtlas.GetSpriteUV(spriteName + "_mm");
            Rectanglei mr = SpriteAtlas.GetSpriteUV(spriteName + "_mr");
            Rectanglei bl = SpriteAtlas.GetSpriteUV(spriteName + "_bl");
            Rectanglei bm = SpriteAtlas.GetSpriteUV(spriteName + "_bm");
            Rectanglei br = SpriteAtlas.GetSpriteUV(spriteName + "_br");
            
            // Top section
            //
            Draw(
                tl,
                rect.Location
            );
            
            Draw(
                tm,
                new System.Drawing.Rectangle(
                    new Point(
                        rect.X + tl.Width,
                        rect.Y
                    ),
                    new Size(
                        rect.Width - tl.Width - tr.Width,
                        tm.Height
                    )
                ),
                DrawMode.Tiled
            );
            
            Draw(
                tr,
                new Point(
                    rect.Right - tr.Width,
                    rect.Y
                )
            );
            
            // Middle section
            //
            Draw(
                ml,
                new System.Drawing.Rectangle(
                    new Point(
                        rect.X,
                        rect.Y + tl.Height
                    ),
                    new Size(
                        ml.Width,
                        rect.Height - tl.Height - bl.Height
                    )
                ),
                DrawMode.Tiled
            );
            
            Draw(
                mm,
                new System.Drawing.Rectangle(
                    new Point(
                        rect.X + tl.Width,
                        rect.Y + tl.Height
                    ),
                    new Size(
                        rect.Width - ml.Width - mr.Width,
                        rect.Height - tm.Height - bm.Height
                    )
                ),
                DrawMode.Tiled
            );
            
            Draw(
                mr,
                new System.Drawing.Rectangle(
                    new Point(
                        rect.X + rect.Width - mr.Width,
                        rect.Y + tr.Height
                    ),
                    new Size(
                        mr.Width,
                        rect.Height - tr.Height - br.Height
                    )
                ),
                DrawMode.Tiled
            );
            
            // Bottom section
            //
            Draw(
                bl,
                new Point(
                    rect.X,
                    rect.Bottom - bl.Height
                )
            );
            
            Draw(
                bm,
                new System.Drawing.Rectangle(
                    new Point(
                        rect.X + bl.Width,
                        rect.Bottom - bm.Height
                    ),
                    new Size(
                        rect.Width - bl.Width - br.Width,
                        bm.Height
                    )
                ),
                DrawMode.Tiled
            );
            
            Draw(
                br,
                new Point(
                    rect.Right - br.Width,
                    rect.Bottom - br.Height
                )
            );
        }
        
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
                string     spriteName = string.Format(
                                            "{0}_{1}",
                                            fontBaseName,
                                            c
                                        );
            
                Rectanglei uv         = SpriteAtlas.GetSpriteUV(
                                            spriteName
                                        );
                
                Draw(
                    spriteName,
                    new System.Drawing.Rectangle(
                        new Point(
                            x,
                            location.Y - uv.Height * scale
                        ),
                        new Size(
                            uv.Width * scale,
                            uv.Height * scale
                        )
                    ),
                    DrawMode.Stretch
                );
                
                x += uv.Width * scale + scale;
            }
        }

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
                SpriteAtlas.Size
            );

            // Bind the atlas
            //
            GL.BindTexture(
                TextureTarget.Texture2D,
                SpriteAtlas.GlTextureId
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
        
        private void Draw(
            Rectanglei sourceRect,
            Point      location        
        )
        {
            Draw(
                sourceRect,
                new System.Drawing.Rectangle(
                    location,
                    new Size(
                        sourceRect.Width,
                        sourceRect.Height
                    )
                ),
                DrawMode.Stretch
            );
        }

        private void Draw(
            Rectanglei                sourceRect,
            System.Drawing.Rectangle  rect,
            DrawMode                  drawMode
        )
        {
            VboDrawContents.AddRange(GLUtility.MakeVboData(rect));
            VboUvContents.AddRange(GLUtility.MakeVboData(sourceRect));
            
            VboSourceRects.AddRange(
                CloneVbo(MakeSourceRectData(sourceRect), 6)
            );
            
            VboOrigins.AddRange(
                CloneVbo(MakeOriginData(rect.Location), 6)
            );
            
            VboDrawModes.AddRange(
                CloneVbo((float) drawMode, 6)
            );

            VertexCount += 6;
        }
        
        private IList<float> MakeOriginData(
            Point origin        
        )
        {
            var data = new List<float>();
            
            data.Add((float) origin.X);
            data.Add((float) origin.Y);
            
            return data;
        }

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
