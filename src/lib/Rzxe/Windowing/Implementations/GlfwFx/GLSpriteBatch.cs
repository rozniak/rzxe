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
using System;
using System.Drawing;
using System.Linq;

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
        
        /// <summary>
        /// The main sub-batch that will be composed during the draw process.
        /// </summary>
        /// <remarks>
        /// This class makes use of "sub-batch" of its own that pretty much acts as the
        /// 'master' batch. Put simply - direct draw calls on this class will forward
        /// to this batch, any merge calls will merge into this batch.
        ///
        /// The end result is that this batch will 'compose' of everything done during
        /// the entire sprite batch as a whole. When Finish() is called, this composed
        /// batch will be analysed and a VBO created out of it.
        /// </remarks>
        private GLSubSpriteBatch ComposerSubBatch { get; set; }

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
        /// The resource cache for graphics objects.
        /// </summary>
        private GLResourceCache ResourceCache { get; set; }

        
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
            ComposerSubBatch = (GLSubSpriteBatch) CreateSubBatch();
            OwnerController  = owner;

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
        }
        
        
        /// <inheritdoc />
        public ISubSpriteBatch CreateSubBatch()
        {
            return new GLSubSpriteBatch((GLSpriteAtlas) Atlas);
        }

        /// <inheritdoc />
        public void Draw(
            ISprite sprite,
            Point   location,
            Color   tint,
            float   alpha = 1.0f
        )
        {
            ComposerSubBatch.Draw(
                sprite,
                location,
                tint,
                alpha
            );
        }
        
        /// <inheritdoc />
        public void Draw(
            System.Drawing.Rectangle sourceRect,
            Point                    location,
            Color                    tint,
            float                    alpha = 1.0f     
        )
        {
            ComposerSubBatch.Draw(
                sourceRect,
                location,
                tint,
                alpha
            );
        }
        
        /// <inheritdoc />
        public void Draw(
            ISprite                  sprite,
            System.Drawing.Rectangle destRect,
            DrawMode                 drawMode,
            Color                    tint,
            float                    alpha = 1.0f
        )
        {
            ComposerSubBatch.Draw(
                sprite,
                destRect,
                drawMode,
                tint,
                alpha
            );
        }
        
        /// <inheritdoc />
        public void Draw(
            System.Drawing.Rectangle sourceRect,
            System.Drawing.Rectangle destRect,
            DrawMode                 drawMode,
            Color                    tint,
            float                    alpha = 1.0f
        )
        {
            ComposerSubBatch.Draw(
                sourceRect,
                destRect,
                drawMode,
                tint,
                alpha
            );
        }
        
        /// <inheritdoc />
        public void DrawBorderBox(
            IBorderBoxResource       borderBox,
            System.Drawing.Rectangle destRect,
            Color                    tint,
            float                    alpha = 1.0f
        )
        {
            ComposerSubBatch.DrawBorderBox(
                borderBox,
                destRect,
                tint,
                alpha
            );
        }
        
        /// <inheritdoc />
        public void DrawString(
            string text,
            IFont  font,
            Point  location,
            Color  color
        )
        {
            ComposerSubBatch.DrawString(
                text,
                font,
                location,
                color
            );
        }
        
        /// <inheritdoc />
        public void DrawSubBatch(
            ISubSpriteBatch subBatch
        )
        {
            ComposerSubBatch.Merge((GLSubSpriteBatch) subBatch);
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
            int vboVsTintId           = GL.GenBuffer();
            int vboVsAlphaScaleId     = GL.GenBuffer();

            float[] vsVertexPositionData = ComposerSubBatch.VboDrawContents.ToArray();
            float[] vsVertexUVData       = ComposerSubBatch.VboUvContents.ToArray();
            float[] vsSourceRectData     = ComposerSubBatch.VboSourceRects.ToArray();
            float[] vsOriginData         = ComposerSubBatch.VboOrigins.ToArray();
            float[] vsDrawModeData       = ComposerSubBatch.VboDrawModes.ToArray();
            float[] vsTintData           = ComposerSubBatch.VboTints.ToArray();
            float[] vsAlphaScaleData     = ComposerSubBatch.VboAlphaScales.ToArray();

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

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsTintId);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                new IntPtr(sizeof(float) * vsTintData.Length),
                vsTintData,
                BufferUsageHint.StreamDraw
            );

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsAlphaScaleId);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                new IntPtr(sizeof(float) * vsAlphaScaleData.Length),
                vsAlphaScaleData,
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

            GL.EnableVertexAttribArray(5);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsTintId);
            GL.VertexAttribPointer(
                5,
                4,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );
            
            GL.EnableVertexAttribArray(6);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVsAlphaScaleId);
            GL.VertexAttribPointer(
                6,
                1,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );

            // Draw now!
            //
            GL.DrawArrays(BeginMode.Triangles, 0, ComposerSubBatch.VertexCount);

            // Detach and destroy VBOs
            //
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);
            GL.DisableVertexAttribArray(3);
            GL.DisableVertexAttribArray(4);
            GL.DisableVertexAttribArray(5);
            GL.DisableVertexAttribArray(6);

            GL.DeleteBuffer(vboVsVertexPositionId);
            GL.DeleteBuffer(vboVsVertexUVId);
            GL.DeleteBuffer(vboVsSourceRectId);
            GL.DeleteBuffer(vboVsOriginId);
            GL.DeleteBuffer(vboVsDrawModeId);
            GL.DeleteBuffer(vboVsTintId);
            GL.DeleteBuffer(vboVsAlphaScaleId);
        }
    }
}
