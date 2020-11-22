/**
 * Collision.cs - Utility Methods for Collision Detection
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Drawing;

namespace Oddmatics.Rzxe.Logic
{
    /// <summary>
    /// Provides methods for working with collision detection.
    /// </summary>
    public static class Collision
    {
        /// <summary>
        /// Determines whether a point is inside a rectangle.
        /// </summary>
        /// <param name="point">
        /// The point to investigate.
        /// </param>
        /// <param name="rect">
        /// The rectangle that may contain the point (with inclusive bounds).
        /// </param>
        /// <returns>
        /// True if the point is inside the rectangle.
        /// </returns>
        public static bool PointInRect(
            PointF     point,
            RectangleF rect
        )
        {
            return point.X >= rect.Left && point.X <= rect.Right &&
                   point.Y >= rect.Top  && point.Y <= rect.Bottom;
        }
    }
}
