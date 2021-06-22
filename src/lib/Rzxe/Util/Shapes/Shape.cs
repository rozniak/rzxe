/**
 * Shape.cs - 2D Shape Base Class
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Drawing;

namespace Oddmatics.Rzxe.Util.Shapes
{
    /// <summary>
    /// Represents the base class for 2D shapes.
    /// </summary>
    public abstract class Shape
    {
        /// <summary>
        /// Gets the verticies defined for the shape.
        /// </summary>
        public abstract Point[] Vertices { get; protected set; }
    }
}
