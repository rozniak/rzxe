/**
 * GLDrawInstruction.cs - OpenGL Drawing Instruction Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Util;
using Oddmatics.Rzxe.Windowing.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The OpenGL implementation of the drawing instruction interface.
    /// </summary>
    internal abstract class GLDrawInstruction : ICachedObject, IDrawInstruction
    {
        /// <inheritdoc />
        public ISpriteAtlas Atlas { get; private set; }
        
        /// <summary>
        /// Gets the GUID that tags the drawing instruction.
        /// </summary>
        public Guid Guid { get; private set; }

        /// <inheritdoc />
        public virtual Point Location
        {
            get { return _Location; }
            set
            {
                SetPropertyValue(ref _Location, ref value);
            }
        }
        private Point _Location;
        
        
        
        
        /// <inheritdoc />
        public event EventHandler Invalidated;
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLDrawInstruction"/> class.
        /// </summary>
        /// <param name="atlas">
        /// The sprite atlas to use.
        /// </param>
        public GLDrawInstruction(
            ISpriteAtlas atlas
        )
        {
            Atlas = atlas;
            Guid  = Guid.NewGuid();
        }
        
        
        /// <inheritdoc />
        public abstract object Clone();
        
        /// <summary>
        /// Composes buffer data for the drawing instruction.
        /// </summary>
        /// <returns>
        /// The composed buffer data for the drawing instruction.
        /// </returns>
        public abstract GLVboData Compose();
        

        /// <summary>
        /// Raises a notification that any cache for the draw instruction should be
        /// invalidated.
        /// </summary>
        protected void Invalidate()
        {
            Invalidated?.Invoke(this, EventArgs.Empty);
        }
        
        /// <summary>
        /// Sets the value of a property in the draw instruction.
        /// </summary>
        /// <param name="property">
        /// The reference to the property to set.
        /// </param>
        /// <param name="newValue">
        /// The reference to the new value for the property.
        /// </param>
        /// <typeparam name="T">
        /// The type of the property being set.
        /// </typeparam>
        protected void SetPropertyValue<T>(
            ref T property,
            ref T newValue
        )
        {
            if (
                property != null &&
                property.Equals(newValue)
            )
            {
                return;
            }

            property = newValue;
            Invalidate();
        }
    }
}
