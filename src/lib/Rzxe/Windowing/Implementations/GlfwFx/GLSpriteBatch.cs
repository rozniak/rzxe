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
        public ISpriteAtlas Atlas { get; private set; }
    
    
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

        #endregion

        
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
            ISprite sprite,
            Point   location
        )
        {
            Draw(
                ((GLSprite) sprite).Bounds,
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
            Point      location,
            DrawMode   drawMode = DrawMode.Stretch
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
                drawMode
            );
        }

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
        
        private IList<float> MakeOriginData(
            Vector2i origin        
        )
        {
            var data = new List<float>();
            
            data.Add(origin.X);
            data.Add(origin.Y);
            
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
