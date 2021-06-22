/**
 * GLSpriteBatchBase.cs - OpenGL Sprite Batch Base Class
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Util.Shapes;
using Oddmatics.Rzxe.Windowing.Graphics;
using Pencil.Gaming.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// Base class implementation of the sprite batch interface.
    /// </summary>
    internal abstract class GLSpriteBatchBase : ISpriteBatch
    {
        /// <inheritdoc />
        public ISpriteAtlas Atlas { get; protected set; }
        
        /// <inheritdoc />
        public IList<IDrawInstruction> Instructions
        {
            get { return _Instructions; }
        }
        protected GLDrawInstructionsList _Instructions;
        
        /// <summary>
        /// Gets the I/O usage intent for the sprite batch.
        /// </summary>
        public abstract SpriteBatchUsageHint Usage { get; }
        
        
        /// <summary>
        /// The graphics controller responsible for creating the sprite batch.
        /// </summary>
        protected GLGraphicsController OwnerController { get; set; }
        
        /// <summary>
        /// The resource cache for graphics objects.
        /// </summary>
        protected GLResourceCache ResourceCache { get; set; }


        #region GL Program IDs
        
        /// <summary>
        /// The ID for the OpenGL uniform variable holding the canvas resolution.
        /// </summary>
        protected int GLCanvasResolutionUniformId { get; set; }
        
        /// <summary>
        /// The ID for the OpenGL shader program.
        /// </summary>
        protected uint GLProgramId { get; set; }
        
        /// <summary>
        /// The ID for the OpenGL uniform variable holding the UV map resolution.
        /// </summary>
        protected int GLUvMapResolutionUniformId { get; set; }

        #endregion

        #region GL VBO Data
        
        /// <summary>
        /// The number of vertices to draw.
        /// </summary>
        protected int GLVertexCount;

        /// <summary>
        /// The ID of the VBO for populating the vsVertexPosition attribute in the
        /// vertex shader.
        /// </summary>
        protected int GLVsVertexPositionVboId;

        /// <summary>
        /// The ID of the VBO for populating the vsVertexUV attribute in the vertex
        /// shader.
        /// </summary>
        protected int GLVsVertexUVVboId;

        /// <summary>
        /// The ID of the VBO for populating the vsSourceRect attribute in the vertex
        /// shader.
        /// </summary>
        protected int GLVsSourceRectVboId;

        /// <summary>
        /// The ID of the VBO for populating the vsOrigin attribute in the vertex
        /// shader.
        /// </summary>
        protected int GLVsOriginVboId;

        /// <summary>
        /// The ID of the VBO for populating the vsDrawMode attribute in the vertex
        /// shader.
        /// </summary>
        protected int GLVsDrawModeVboId;

        /// <summary>
        /// The ID of the VBO for populating the vsTint attribute in the vertex shader.
        /// </summary>
        protected int GLVsTintVboId;

        /// <summary>
        /// The ID of the VBO for populating the vsAlphaScale attribute in the
        /// vertex shader.
        /// </summary>
        protected int GLVsAlphaScaleVboId;

        #endregion
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLSpriteBatchBase"/> class.
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
        public GLSpriteBatchBase(
            GLGraphicsController owner,
            GLSpriteAtlas        atlas,
            GLResourceCache      resourceCache
        )
        {
            _Instructions   = new GLDrawInstructionsList();
            Atlas           = atlas;
            OwnerController = owner;
            ResourceCache   = resourceCache;
            
            // Set up GL fields
            //
            GLProgramId =
                ResourceCache.GetShaderProgram("SimpleUVs"); // FIXME: Hard-coded ew!

            GLCanvasResolutionUniformId =
                GL.GetUniformLocation(
                    GLProgramId,
                    "gCanvasResolution"
                );

            GLUvMapResolutionUniformId =
                GL.GetUniformLocation(
                    GLProgramId,
                    "gUvMapResolution"
                );
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLSpriteBatchBase"/> class.
        /// </summary>
        /// <param name="instructions">
        /// A list of instructions to copy from to populate the sprite batch
        /// initially.
        /// </param>
        /// <param name="owner">
        /// The graphics controller creating the sprite batch.
        /// </param>
        /// <param name="atlas">
        /// The sprite atlas to use.
        /// </param>
        /// <param name="resourceCache">
        /// The resource cache for graphics objects.
        /// </param>
        protected GLSpriteBatchBase(
            IList<IDrawInstruction> instructions,
            GLGraphicsController    owner,
            GLSpriteAtlas           atlas,
            GLResourceCache         resourceCache
        ) : this(owner, atlas, resourceCache)
        {
            foreach (IDrawInstruction instruction in instructions)
            {
                Instructions.Add((IDrawInstruction) instruction.Clone());
            }
        }


        /// <inheritdoc />
        public abstract object Clone();
        
        /// <inheritdoc />
        public abstract void Dispose();
        
        /// <inheritdoc />
        public virtual IShapeDrawInstruction Draw(
            Shape shape,
            Point location,
            Color color
        )
        {
            var instruction = new GLShapeDrawInstruction((GLSpriteAtlas) Atlas)
                              {
                                  Color    = color,
                                  Location = location,
                                  Shape    = shape
                              };

            _Instructions.Add(instruction);

            return instruction;
        }

        /// <inheritdoc />
        public virtual ISpriteDrawInstruction Draw(
            ISprite sprite,
            Point   location,
            Color   tint,
            float   alpha = 1.0f
        )
        {
            var instruction = new GLSpriteDrawInstruction((GLSpriteAtlas) Atlas)
                              {
                                  Alpha                = alpha,
                                  DestinationRectangle = new Rectangle(
                                                             location,
                                                             sprite.Size
                                                         ),
                                  Sprite               = sprite,
                                  Tint                 = tint
                              };

            _Instructions.Add(instruction);

            return instruction;
        }
        
        /// <inheritdoc />
        public virtual ISpriteDrawInstruction Draw(
            Rectangle sourceRect,
            Point     location,
            Color     tint,
            float     alpha = 1.0f
        )
        {
            var instruction = new GLSpriteDrawInstruction((GLSpriteAtlas) Atlas)
                              {
                                  Alpha = alpha,
                                  DestinationRectangle = new Rectangle(
                                                             location,
                                                             sourceRect.Size
                                                         ),
                                  SourceRectangle      = sourceRect,
                                  Tint                 = tint
                              };

            _Instructions.Add(instruction);

            return instruction;
        }
        
        /// <inheritdoc />
        public virtual ISpriteDrawInstruction Draw(
            ISprite   sprite,
            Rectangle destRect,
            DrawMode  drawMode,
            Color     tint,
            float     alpha = 1.0f
        )
        {
            var instruction = new GLSpriteDrawInstruction((GLSpriteAtlas) Atlas)
                              {
                                  Alpha                = alpha,
                                  DestinationRectangle = destRect,
                                  DrawMode             = drawMode,
                                  Sprite               = sprite,
                                  Tint                 = tint
                              };

            _Instructions.Add(instruction);

            return instruction;
        }
        
        /// <inheritdoc />
        public virtual ISpriteDrawInstruction Draw(
            Rectangle sourceRect,
            Rectangle destRect,
            DrawMode  drawMode,
            Color     tint,
            float     alpha = 1.0f
        )
        {
            var instruction = new GLSpriteDrawInstruction((GLSpriteAtlas) Atlas)
                              {
                                  Alpha                = alpha,
                                  DestinationRectangle = destRect,
                                  DrawMode             = drawMode,
                                  SourceRectangle      = sourceRect,
                                  Tint                 = tint
                              };

            _Instructions.Add(instruction);

            return instruction;
        }
        
        /// <inheritdoc />
        public virtual IBorderBoxDrawInstruction DrawBorderBox(
            IBorderBoxResource borderBox,
            Rectangle          destRect,
            Color              tint,
            float              alpha = 1.0f
        )
        {
            var instruction = new GLBorderBoxDrawInstruction((GLSpriteAtlas) Atlas)
                              {
                                  Alpha                = alpha,
                                  BorderBox            = borderBox,
                                  DestinationRectangle = destRect,
                                  Tint                 = tint
                              };

            _Instructions.Add(instruction);

            return instruction;
        }
        
        /// <inheritdoc />
        public virtual IStringDrawInstruction DrawString(
            string text,
            IFont  font,
            Point  location,
            Color  color
        )
        {
            var instruction = new GLStringDrawInstruction((GLSpriteAtlas) Atlas)
                              {
                                  Color    = color,
                                  Font     = font,
                                  Location = location,
                                  Text     = text
                              };

            _Instructions.Add(instruction);

            return instruction;
        }
        
        /// <inheritdoc />
        public abstract void Finish();
        
        
        /// <summary>
        /// Deletes all VBOs generated for the sprite batch.
        /// </summary>
        protected virtual void DeleteBuffers()
        {
            GL.DeleteBuffer(GLVsVertexPositionVboId);
            GL.DeleteBuffer(GLVsVertexUVVboId);
            GL.DeleteBuffer(GLVsSourceRectVboId);
            GL.DeleteBuffer(GLVsOriginVboId);
            GL.DeleteBuffer(GLVsDrawModeVboId);
            GL.DeleteBuffer(GLVsTintVboId);
            GL.DeleteBuffer(GLVsAlphaScaleVboId);
        }
        
        /// <summary>
        /// Generates the VBOs using the current draw instructions state and assigned
        /// atlas.
        /// </summary>
        protected virtual void GenerateVbos(
            GLVboData buffer
        )
        {
            GLVertexCount = buffer.VertexCount;

            GeneratePopulateVbo(buffer.DrawContentsData, out GLVsVertexPositionVboId);
            GeneratePopulateVbo(buffer.UvContentsData,   out GLVsVertexUVVboId);
            GeneratePopulateVbo(buffer.SourceRectsData,  out GLVsSourceRectVboId);
            GeneratePopulateVbo(buffer.OriginsData,      out GLVsOriginVboId);
            GeneratePopulateVbo(buffer.DrawModesData,    out GLVsDrawModeVboId);
            GeneratePopulateVbo(buffer.TintsData,        out GLVsTintVboId);
            GeneratePopulateVbo(buffer.AlphaScalesData,  out GLVsAlphaScaleVboId);
        }
        
        /// <summary>
        /// Generates and populates a VBO with buffer data.
        /// </summary>
        /// <param name="bufferData">
        /// The buffer data.
        /// </param>
        /// <param name="vboIdStorage">
        /// The ID of the VBO in OpenGL.
        /// </param>
        protected void GeneratePopulateVbo(
            IList<float> bufferData,
            out int      vboIdStorage
        )
        {
            vboIdStorage = GL.GenBuffer();

            float[]         data = bufferData.ToArray();
            BufferUsageHint hint = BufferUsageHint.StreamDraw;
            
            switch (Usage)
            {
                case SpriteBatchUsageHint.Dynamic:
                    hint = BufferUsageHint.DynamicDraw;
                    break;

                case SpriteBatchUsageHint.Static:
                    hint = BufferUsageHint.StaticDraw;
                    break;

                case SpriteBatchUsageHint.Stream:
                    hint = BufferUsageHint.StreamDraw;
                    break;

                default:
                    throw new NotSupportedException("Unknown usage hint.");
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboIdStorage);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                new IntPtr(sizeof(float) * data.Length),
                data,
                hint
            );
        }
        
        /// <summary>
        /// Binds VBOs and issues the draw call to OpenGL.
        /// </summary>
        protected void IssueGLDrawCall()
        {
            // Set up shader program
            //
            GL.UseProgram(GLProgramId);

            GL.Uniform2(
                GLCanvasResolutionUniformId,
                (float) OwnerController.TargetResolution.Width,
                (float) OwnerController.TargetResolution.Height
            );
            
            GL.Uniform2(
                GLUvMapResolutionUniformId,
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
            GL.BindBuffer(BufferTarget.ArrayBuffer, GLVsVertexPositionVboId);
            GL.VertexAttribPointer(
                0,
                2,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );

            GL.EnableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, GLVsVertexUVVboId);
            GL.VertexAttribPointer(
                1,
                2,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );

            GL.EnableVertexAttribArray(2);
            GL.BindBuffer(BufferTarget.ArrayBuffer, GLVsSourceRectVboId);
            GL.VertexAttribPointer(
                2,
                4,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );

            GL.EnableVertexAttribArray(3);
            GL.BindBuffer(BufferTarget.ArrayBuffer, GLVsOriginVboId);
            GL.VertexAttribPointer(
                3,
                2,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );

            GL.EnableVertexAttribArray(4);
            GL.BindBuffer(BufferTarget.ArrayBuffer, GLVsDrawModeVboId);
            GL.VertexAttribPointer(
                4,
                1,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );

            GL.EnableVertexAttribArray(5);
            GL.BindBuffer(BufferTarget.ArrayBuffer, GLVsTintVboId);
            GL.VertexAttribPointer(
                5,
                4,
                VertexAttribPointerType.Float,
                false,
                0,
                0
            );
            
            GL.EnableVertexAttribArray(6);
            GL.BindBuffer(BufferTarget.ArrayBuffer, GLVsAlphaScaleVboId);
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
            GL.DrawArrays(BeginMode.Triangles, 0, GLVertexCount);

            // Detach VBOs
            //
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);
            GL.DisableVertexAttribArray(3);
            GL.DisableVertexAttribArray(4);
            GL.DisableVertexAttribArray(5);
            GL.DisableVertexAttribArray(6);
        }
        
        /// <summary>
        /// Creates a VBO data object from the instructions currently defined.
        /// </summary>
        /// <returns>The instructions to buffer.</returns>
        protected virtual GLVboData ReadInstructionsToBuffer()
        {
            var buffer = new GLVboData((GLSpriteAtlas) Atlas);
            
            foreach (GLDrawInstruction instruction in Instructions)
            {
                buffer.Merge(instruction.Compose());
            }

            return buffer;
        }
    }
}
