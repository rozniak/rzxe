/**
 * GLStaticSpriteBatch.cs - OpenGL Static Buffer Sprite Batch Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using System;
using System.Collections.Generic;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL static buffer implementation of the sprite batch interface.
    /// </summary>
    internal sealed class GLStaticSpriteBatch : GLSpriteBatchBase
    {
        /// <inheritdoc />
        public override SpriteBatchUsageHint Usage
        {
            get { return SpriteBatchUsageHint.Static; }
        }
        
        
        /// <summary>
        /// The value that indicates whether a VBO has been created.
        /// </summary>
        private bool GLGenerated { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLStaticSpriteBatch"/> class.
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
        public GLStaticSpriteBatch(
            GLGraphicsController owner,
            GLSpriteAtlas        atlas,
            GLResourceCache      resourceCache
        ) : base(owner, atlas, resourceCache) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLStaticSpriteBatch"/> class.
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
        private GLStaticSpriteBatch(
            IList<IDrawInstruction> instructions,
            GLGraphicsController    owner,
            GLSpriteAtlas           atlas,
            GLResourceCache         resourceCache
        ) : base(instructions, owner, atlas, resourceCache) { }


        /// <inheritdoc />
        public override object Clone()
        {
            return new GLStaticSpriteBatch(
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
            if (!GLGenerated)
            {
                var buffer = ReadInstructionsToBuffer();

                GenerateVbos(buffer);

                _Instructions.Lock();
            
                GLGenerated = true;
            }

            IssueGLDrawCall();
        }
    }
}
