/**
 * GLStreamSpriteBatch.cs - OpenGL Stream Buffer Sprite Batch Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using System.Collections.Generic;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL stream buffer implementation of the sprite batch interface.
    /// </summary>
    internal sealed class GLStreamSpriteBatch : GLSpriteBatchBase
    {
        /// <inheritdoc />
        public override SpriteBatchUsageHint Usage
        {
            get { return SpriteBatchUsageHint.Stream; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GLStreamSpriteBatch"/> class.
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
        public GLStreamSpriteBatch(
            GLGraphicsController owner,
            GLSpriteAtlas        atlas,
            GLResourceCache      resourceCache
        ) : base(owner, atlas, resourceCache) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLStreamSpriteBatch"/> class.
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
        private GLStreamSpriteBatch(
            IList<IDrawInstruction> instructions,
            GLGraphicsController     owner,
            GLSpriteAtlas            atlas,
            GLResourceCache          resourceCache
        ) : base(instructions, owner, atlas, resourceCache) { }


        /// <inheritdoc />
        public override object Clone()
        {
            return new GLStreamSpriteBatch(
                Instructions,
                OwnerController,
                (GLSpriteAtlas) Atlas,
                ResourceCache
            );
        }
        
        /// <inheritdoc />
        public override void Dispose()
        {
            // Nothing to do!
        }
        
        /// <inheritdoc />
        public override void Finish()
        {
            GLVboData buffer = ReadInstructionsToBuffer();
            
            GenerateVbos(buffer);
            IssueGLDrawCall();
            DeleteBuffers();
        }
    }
}
