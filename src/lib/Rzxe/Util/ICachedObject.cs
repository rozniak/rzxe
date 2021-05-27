/**
 * ICachedObject.cs - Cached Object Interface
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
    /// Represents an object that is typically cached and can invalidate itself.
    /// </summary>
    public interface ICachedObject
    {
        /// <summary>
        /// Occurs when the object has changed and any cache should be invalidated.re
        /// </summary>
        event EventHandler Invalidated;
    }
}
