/**
 * RectangleExtensions.cs - Rectangle Extension Methods
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Drawing;

namespace Oddmatics.Rzxe.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="Rectangle"/> struct.
    /// </summary>
    public static class RectangleExtensions
    {
        /// <summary>
        /// Adds a position offset to the <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="Rectangle"/>.
        /// </param>
        /// <param name="delta">
        /// The <see cref="Point"/> to add.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle Add(
            this Rectangle rect,
            Point          delta
        )
        {
            return new Rectangle(
                rect.Location.Add(delta),
                rect.Size
            );
        }
        
        /// <summary>
        /// Clips one <see cref="Rectangle"/> inside another.
        /// </summary>
        /// <param name="srcRect">
        /// The source <see cref="Rectangle"/>.
        /// </param>
        /// <param name="boundRect">
        /// The <see cref="Rectangle"/> that will clip the source.
        /// </param>
        /// <returns>
        /// The clipped <see cref="Rectangle"/> within the boundaries.
        /// </returns>
        public static Rectangle ClipInside(
            this Rectangle srcRect,
            Rectangle      boundRect
        )
        {
            int clipBottom = ClipValue(
                                 srcRect.Bottom,
                                 boundRect.Bottom,
                                 false
                             );
            int clipLeft   = ClipValue(
                                 srcRect.Left,
                                 boundRect.Left,
                                 true
                             );
            int clipRight  = ClipValue(
                                 srcRect.Right,
                                 boundRect.Right,
                                 false
                             );
            int clipTop    = ClipValue(
                                 srcRect.Top,
                                 boundRect.Top,
                                 true
                             );

            return new Rectangle(
                clipLeft,
                clipTop,
                clipRight - clipLeft,
                clipBottom - clipTop
            );
        }

        /// <summary>
        /// Subtracts a position offset from the <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="Rectangle"/>.
        /// </param>
        /// <param name="delta">
        /// The <see cref="Point"/> to subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle Subtract(
            this Rectangle rect,
            Point          delta
        )
        {
            return new Rectangle(
                rect.Location.Subtract(delta),
                rect.Size
            );
        }
        
        
        /// <summary>
        /// Clips the value above or below a limit.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="limit">
        /// The limit.
        /// </param>
        /// <param name="above">
        /// True to clip above the value.
        /// </param>
        /// <returns>
        /// The clipped value.
        /// </returns>
        private static int ClipValue(
            int     value,
            int     limit,
            bool    above
        )
        {
            if (above && value < limit)
            {
                return limit;
            }
            
            if (!above && value > limit)
            {
                return limit;
            }
            
            return value;
        }
    }
}
