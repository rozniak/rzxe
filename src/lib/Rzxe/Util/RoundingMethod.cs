/**
 * RoundingMethod.cs - Mathematical Rounding Method Enumeration
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
    /// Specifies constants defining the possible methods of rounding a number that
    /// is between two whole numbers.
    /// </summary>
    public enum RoundingMethod
    {
        /// <summary>
        /// Always use <see cref="Math.Ceiling(double)"/>.
        /// </summary>
        Ceiling,
        
        /// <summary>
        /// Always use <see cref="Math.Floor(double)"/>.
        /// </summary>
        Floor,
        
        /// <summary>
        /// Use typical round-to-nearest.
        /// </summary>
        ToNearest,
    }
}
