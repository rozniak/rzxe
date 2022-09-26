/**
 * DisposableBase.cs - Disposable Object Base Class
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;

namespace Oddmatics.Rzxe.Util
{
    /// <summary>
    /// Represents the base class for disposable objects.
    /// </summary>
    public abstract class DisposableBase : IDisposable
    {
        /// <summary>
        /// Gets or sets a value indicating whether the object is disposed or being
        /// disposed.
        /// </summary>
        protected virtual bool Disposing { get; set; }
        
        
        /// <inheritdoc />
        public virtual void Dispose()
        {
            AssertNotDisposed();

            Disposing = true;
        }
        
        
        /// <summary>
        /// Asserts the object is not disposed.
        /// </summary>
        protected virtual void AssertNotDisposed()
        {
            if (Disposing)
            {
                throw new ObjectDisposedException(string.Empty);
            }
        }
    }
}
