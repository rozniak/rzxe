/**
 * GLDynamicSpriteBatch.cs - OpenGL Dynamic Buffer Sprite Batch Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Pencil.Gaming.Graphics;
using Oddmatics.Rzxe.Windowing.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL dynamic buffer implementation of the sprite batch interface.
    /// </summary>
    internal sealed class GLDynamicSpriteBatch : GLSpriteBatchBase
    {
        /// <inheritdoc />
        public override SpriteBatchUsageHint Usage
        {
            get { return SpriteBatchUsageHint.Dynamic; }
        }
        
        
        /// <summary>
        /// The mapping of draw instructions and their corresponding indexes in the
        /// vertex buffers.
        /// </summary>
        /// <remarks>
        /// This is used for updating buffers in-place when items are only edited
        /// within the instructions collection.
        /// 
        /// Essentially this is the vertex index of the first index of the
        /// instruction, the actual index in each buffer will vary based on the size
        /// of the type in that buffer (eg. value * 2 for vec2).
        /// </remarks>
        private Dictionary<GLDrawInstruction, int> BufferMapping { get; set; }

        /// <summary>
        /// The value that indicates whether the draw instruction collection has been
        /// modified since the last render.
        /// </summary>
        private bool Dirty { get; set; }
        
        /// <summary>
        /// The collection of instructions that have been changed.
        /// </summary>
        private List<GLDrawInstruction> DirtyInstructions { get; set; }

        /// <summary>
        /// The value that indicates that the buffers must be completely regenerated
        /// because they changed size.
        /// </summary>
        private bool FullGenerationRequired { get; set; }

        /// <summary>
        /// The value that indicates whether a VBO has been created.
        /// </summary>
        private bool GLGenerated { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLDynamicSpriteBatch"/> class.
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
        public GLDynamicSpriteBatch(
            GLGraphicsController owner,
            GLSpriteAtlas        atlas,
            GLResourceCache      resourceCache
        ) : base(owner, atlas, resourceCache)
        {
            CtorInit();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLDynamicSpriteBatch"/> class.
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
        private GLDynamicSpriteBatch(
            IList<IDrawInstruction> instructions,
            GLGraphicsController    owner,
            GLSpriteAtlas           atlas,
            GLResourceCache         resourceCache
        ) : base(instructions, owner, atlas, resourceCache)
        {
            CtorInit();
        }
        
        /// <summary>
        /// Additional initialization from the constructor.
        /// </summary>
        private void CtorInit()
        {
            _Instructions.Changed += Instructions_Changed;
            BufferMapping          = new Dictionary<GLDrawInstruction, int>();
            Dirty                  = true;
            DirtyInstructions      = new List<GLDrawInstruction>();
            FullGenerationRequired = true;
        }


        /// <inheritdoc />
        public override object Clone()
        {
            return new GLDynamicSpriteBatch(
                Instructions,
                OwnerController,
                (GLSpriteAtlas) Atlas,
                ResourceCache
            );
        }
        
        /// <inheritdoc />
        public override void Dispose()
        {
            if (!GLGenerated)
            {
                return;
            }

            DeleteBuffers();
        }
        
        /// <inheritdoc />
        public override void Finish()
        {
            if (Dirty)
            {
                UpdateBuffers();
            }

            IssueGLDrawCall();
        }
        
        
        /// <inheritdoc />
        protected override void DeleteBuffers()
        {
            base.DeleteBuffers();
            
            BufferMapping.Clear();
        }
        
        /// <inheritdoc />
        protected override GLVboData ReadInstructionsToBuffer()
        {
            var buffer = new GLVboData((GLSpriteAtlas) Atlas);
            
            foreach (GLDrawInstruction instruction in Instructions)
            {
                BufferMapping.Add(instruction, buffer.VertexCount);
                buffer.Merge(instruction.Compose());
            }

            return buffer;
        }


        /// <summary>
        /// Populates updated data for a portion of a VBO.
        /// </summary>
        /// <param name="vboId">
        /// The ID of the VBO.
        /// </param>
        /// <param name="vertexIndex">
        /// The vertex index to begin updating at.
        /// </param>
        /// <param name="typeSize">
        /// The size of the data type held by the VBO.
        /// </param>
        /// <param name="bufferData">
        /// The buffer data.
        /// </param>
        private void PopulateVboSubData(
            int          vboId,
            int          vertexIndex,
            int          typeSize,
            IList<float> bufferData
        )
        {
            float[] data = bufferData.ToArray();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
            GL.BufferSubData(
                BufferTarget.ArrayBuffer,
                new IntPtr(sizeof(float) * vertexIndex * typeSize),
                new IntPtr(sizeof(float) * data.Length),
                data
            );
        }

        /// <summary>
        /// Regenerates the VBOs for drawing the updated sprite batch.
        /// </summary>
        private void RegenerateVbos()
        {
            DeleteBuffers();

            GLVboData buffer = ReadInstructionsToBuffer();

            GenerateVbos(buffer);
            
            FullGenerationRequired = false;
            GLGenerated            = true;
        }
        
        /// <summary>
        /// Splices in-place changes into the VBOs for drawing the updated sprite
        /// batch.
        /// </summary>
        private void SpliceVbos()
        {
            foreach (GLDrawInstruction instruction in DirtyInstructions)
            {
                GLVboData bufferData  = instruction.Compose();
                int       vertexIndex = BufferMapping[instruction];
                
                PopulateVboSubData(
                    GLVsVertexPositionVboId,
                    vertexIndex,
                    2,
                    bufferData.DrawContentsData
                );
                PopulateVboSubData(
                    GLVsVertexUVVboId,
                    vertexIndex,
                    2,
                    bufferData.UvContentsData
                );
                PopulateVboSubData(
                    GLVsSourceRectVboId,
                    vertexIndex,
                    4,
                    bufferData.SourceRectsData
                );
                PopulateVboSubData(
                    GLVsOriginVboId,
                    vertexIndex,
                    2,
                    bufferData.OriginsData
                );
                PopulateVboSubData(
                    GLVsDrawModeVboId,
                    vertexIndex,
                    1,
                    bufferData.DrawModesData
                );
                PopulateVboSubData(
                    GLVsTintVboId,
                    vertexIndex,
                    4,
                    bufferData.TintsData
                );
                PopulateVboSubData(
                    GLVsAlphaScaleVboId,
                    vertexIndex,
                    1,
                    bufferData.AlphaScalesData
                );
            }
        }
        
        /// <summary>
        /// Updates VBOs for drawing the sprite batch.
        /// </summary>
        private void UpdateBuffers()
        {
            if (FullGenerationRequired)
            {
                RegenerateVbos();
            }
            else
            {
                SpliceVbos();
            }

            Dirty = false;
            DirtyInstructions.Clear();
        }


        /// <summary>
        /// (Event) Handles changes in the draw instruction list.
        /// </summary>
        private void Instructions_Changed(
            object                             sender,
            GLDrawInstructionsChangedEventArgs e
        )
        {
            Dirty = true;
            
            if (e.Change == GLDrawInstructionChange.Changed)
            {
                DirtyInstructions.Add(e.Instruction);
            }
            else
            {
                FullGenerationRequired = true;
            }
        }
    }
}
