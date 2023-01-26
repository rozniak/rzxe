/**
 * SpriteBatchProxy.cs - Sprite Batch Proxy Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Util.Collections;
using Oddmatics.Rzxe.Util.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Represents a proxy sprite batch for self-contained drawing.
    /// </summary>
    /// <remarks>
    /// The intent of this class is to provide a layer of separation when different
    /// classes are responsible for drawing within the same draw call.
    ///
    /// Rather than rely on the classes to share the sprite batch correctly, this class
    /// exists to enforce containment of each class' drawing. This also means said classes
    /// can call Dispose() on the instance of the proxy, instead of having to keep
    /// track of their ownership.
    ///
    /// The main use of this class in rzxe is UxContainer, which creates a proxy for each
    /// component to draw into. The shell can then render itself in a single draw call.
    /// </remarks>
    public sealed class SpriteBatchProxy : ISpriteBatch
    {
        /// <inheritdoc />
        public ISpriteAtlas Atlas { get { return TargetSpriteBatch.Atlas; } }
        
        /// <summary>
        /// Gets or sets a value indicating whether the proxy owns the instructions it
        /// draws.
        /// </summary>
        /// <remarks>
        /// This informs the class whether it is responsible for disposing the
        /// instructions.
        ///
        /// By default, this is true. But if the proxy is itself a target of other
        /// proxies, then it should not be responsible for the disposal of instructions.
        ///
        /// The most downstream proxy should be the one to dispose its instructions.
        /// </remarks>
        public bool IsInstructionsOwner { get; set; }
        
        /// <inheritdoc />
        public IList<IDrawInstruction> Instructions
        {
            get { return _Instructions; }
        }
        private ExCollection<IDrawInstruction> _Instructions { get; set; }
        
        /// <inheritdoc />
        public SpriteBatchUsageHint Usage { get { return TargetSpriteBatch.Usage; } }
        
        
        /// <summary>
        /// The value that indicates whether the sprite batch proxy has been disposed or
        /// is currently being disposed.
        /// </summary>
        private bool Disposing { get; set; }
        
        /// <summary>
        /// The value that indicates whether drawing is taking place.
        /// </summary>
        /// <remarks>
        /// This is used for tracking whether draw instructions being added to the proxy
        /// collection should be forwarded to the proxied sprite batch. If this class is
        /// the one adding to the collection, then do not forward.
        /// </remarks>
        private bool Drawing { get; set; }

        /// <summary>
        /// The sprite batch to proxy draw requests to.
        /// </summary>
        private ISpriteBatch TargetSpriteBatch { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteBatchProxy"/> class.
        /// </summary>
        /// <param name="target">
        /// The target sprite batch for draw requests to be passed to.
        /// </param>
        public SpriteBatchProxy(
            ISpriteBatch target
        )
        {
            _Instructions       = new ExCollection<IDrawInstruction>();
            IsInstructionsOwner = true;
            TargetSpriteBatch   = target;
            
            _Instructions.ItemAdded += Instructions_ItemAdded;
            _Instructions.ItemRemoved += Instructions_ItemRemoved;
        }
        
        
        /// <inheritdoc />
        public object Clone()
        {
            throw new InvalidOperationException(
                "Cloning a proxy is not supported as it may result in desyncs."
            );
        }
        
        /// <inheritdoc />
        public void Dispose()
        {
            if (Disposing)
            {
                throw new ObjectDisposedException(nameof(SpriteBatchProxy));
            }

            Disposing = true;
            
            if (!IsInstructionsOwner)
            {
                return;
            }
            
            foreach (IDrawInstruction instruction in _Instructions)
            {
                TargetSpriteBatch.Instructions.Remove(instruction);
            }
        }
        
        /// <inheritdoc />
        public IShapeDrawInstruction Draw(
            Shape shape,
            Point location,
            Color color
        )
        {
            IShapeDrawInstruction ret =
                TargetSpriteBatch.Draw(shape, location, color);

            InternalAddInstruction(ret);

            return ret;
        }
        
        /// <inheritdoc />
        public ISpriteDrawInstruction Draw(
            ISprite sprite,
            Point   location,
            Color   tint,
            float   alpha = 1
        )
        {
            ISpriteDrawInstruction ret =
                TargetSpriteBatch.Draw(sprite, location, tint, alpha);

            InternalAddInstruction(ret);

            return ret;
        }
        
        /// <inheritdoc />
        public ISpriteDrawInstruction Draw(
            Rectangle sourceRect,
            Point     location,
            Color     tint,
            float     alpha = 1
        )
        {
            ISpriteDrawInstruction ret =
                TargetSpriteBatch.Draw(sourceRect, location, tint, alpha);

            InternalAddInstruction(ret);

            return ret;
        }
        
        /// <inheritdoc />
        public ISpriteDrawInstruction Draw(
            ISprite   sprite,
            Rectangle destRect,
            DrawMode  drawMode,
            Color     tint,
            float     alpha = 1
        )
        {
            ISpriteDrawInstruction ret =
                TargetSpriteBatch.Draw(sprite, destRect, drawMode, tint, alpha);

            InternalAddInstruction(ret);

            return ret;
        }
        
        /// <inheritdoc />
        public ISpriteDrawInstruction Draw(
            Rectangle sourceRect,
            Rectangle destRect,
            DrawMode  drawMode,
            Color     tint,
            float     alpha = 1
        )
        {
            ISpriteDrawInstruction ret =
                TargetSpriteBatch.Draw(sourceRect, destRect, drawMode, tint, alpha);

            InternalAddInstruction(ret);

            return ret;
        }
        
        /// <inheritdoc />
        public IBorderBoxDrawInstruction DrawBorderBox(
            IBorderBoxResource borderBox,
            Rectangle          destRect,
            Color              tint,
            float              alpha = 1
        )
        {
            IBorderBoxDrawInstruction ret =
                TargetSpriteBatch.DrawBorderBox(borderBox, destRect, tint, alpha);

            InternalAddInstruction(ret);

            return ret;
        }
        
        /// <inheritdoc />
        public IStringDrawInstruction DrawString(
            string text,
            IFont  font,
            Point  location,
            Color  color
        )
        {
            IStringDrawInstruction ret =
                TargetSpriteBatch.DrawString(text, font, location, color);

            InternalAddInstruction(ret);

            return ret;
        }
        
        /// <inheritdoc />
        public void Finish()
        {
            // Don't think we need to do anything here
            //
            return;
        }
        
        
        /// <summary>
        /// Adds an instruction drawn via the proxy, to the local collection.
        /// </summary>
        /// <param name="instruction">
        /// The instruction that was drawn.
        /// </param>
        private void InternalAddInstruction(
            IDrawInstruction instruction
        )
        {
            Drawing = true;
            _Instructions.Add(instruction);
            Drawing = false;
        }


        /// <summary>
        /// (Event) Occurs when a drawing instruction is added to the collection.
        /// </summary>
        private void Instructions_ItemAdded(
            object                                           sender,
            ItemMembershipChangedEventArgs<IDrawInstruction> e
        )
        {
            if (!Drawing)
            {
                TargetSpriteBatch.Instructions.Add(e.Item);
            }
        }
        
        /// <summary>
        /// (Event) Occurs when a drawing instruction is removed from the collection.
        /// </summary>
        private void Instructions_ItemRemoved(
            object                                           sender,
            ItemMembershipChangedEventArgs<IDrawInstruction> e
        )
        {
            TargetSpriteBatch.Instructions.Remove(e.Item);
        }
    }
}
