/**
 * EdgeMetrics.cs - Box Model Edge Metrics
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Represents a set of edge metrics for box models.
    /// </summary>
    public struct EdgeMetrics
    {
        /// <summary>
        /// Represents an <see cref="EdgeMetrics"/> with all metrics set to zero.
        /// </summary>
        public static readonly EdgeMetrics Zero = new EdgeMetrics(0);
        
        
        /// <summary>
        /// The size of the bottom side.
        /// </summary>
        public int Bottom { get; private set; }
        
        /// <summary>
        /// The size of the left side.
        /// </summary>
        public int Left { get; private set; }
        
        /// <summary>
        /// The size of the right side.
        /// </summary>
        public int Right { get; private set; }
        
        /// <summary>
        /// The size of the top side.
        /// </summary>
        public int Top { get; private set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeMetrics"/> struct with all
        /// sides of equal length
        /// </summary>
        /// <param name="allSides">
        /// The size for all sides.
        /// </param>
        public EdgeMetrics(
            int allSides
        )
        {
            Bottom = allSides;
            Left   = allSides;
            Right  = allSides;
            Top    = allSides;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeMetrics"/> struct with
        /// specified sizes for the vertical and horizontal sides.
        /// </summary>
        /// <param name="verticals">
        /// The size for the verticals (top and bottom).
        /// </param>
        /// <param name="horizontals">
        /// The size for the horizontals (left and right).
        /// </param>
        public EdgeMetrics(
            int verticals,
            int horizontals
        )
        {
            Bottom = verticals;
            Left   = horizontals;
            Right  = horizontals;
            Top    = verticals;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Oddmatics.Rzxe.Windowing.Graphics.EdgeMetrics"/> struct.
        /// </summary>
        /// <param name="top">
        /// The size of the top side.
        /// </param>
        /// <param name="right">
        /// The size of the right side.
        /// </param>
        /// <param name="bottom">
        /// The size of the bottom side.
        /// </param>
        /// <param name="left">
        /// The size of the left side.
        /// </param>
        public EdgeMetrics(
            int top,
            int right,
            int bottom,
            int left
        )
        {
            Bottom = bottom;
            Left   = left;
            Right  = right;
            Top    = top;
        }
    }
}
