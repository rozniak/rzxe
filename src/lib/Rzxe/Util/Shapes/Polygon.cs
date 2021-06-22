/**
 * Polygon.cs - Polygonal Shape
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Util.Shapes
{
    /// <summary>
    /// Represents a polygonal shape.
    /// </summary>
    public sealed class Polygon : Shape
    {
        /// <inheritdoc />
        public override Point[] Vertices { get; protected set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class with a
        /// set of vertices.
        /// </summary>
        /// <param name="vertices">
        /// The vertices.
        /// </param>
        public Polygon(
            params Point[] vertices
        )
        {
            // Polygons must have at least 3 vertices
            //
            if (vertices.Length < 3)
            {
                throw new ArgumentException(
                    "Polygons must have at least 3 vertices.",
                    nameof(vertices)
                );
            }
            
            Vertices = vertices;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class as a
        /// quadrilateral with a <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rect">
        /// The rectangle.
        /// </param>
        public Polygon(
            Rectangle rect
        )
        {
            Vertices =
                new Point[]
                {
                    new Point(rect.Left, rect.Top),
                    new Point(rect.Right, rect.Top),
                    new Point(rect.Right, rect.Bottom),
                    new Point(rect.Left, rect.Bottom)
                };
        }
    }
}
