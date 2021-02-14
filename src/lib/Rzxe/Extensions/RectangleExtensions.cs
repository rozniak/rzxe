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
        public static Rectangle AddOffset(
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
        /// Adds a position offset to the <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="RectangleF"/>.
        /// </param>
        /// <param name="delta">
        /// The <see cref="PointF"/> to add.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF AddOffset(
            this RectangleF rect,
            PointF          delta
        )
        {
            return new RectangleF(
                rect.Location.Add(delta),
                rect.Size
            );
        }
        
        /// <summary>
        /// Adds a size difference to the <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="Rectangle"/>.
        /// </param>
        /// <param name="delta">
        /// The <see cref="Size"/> to add.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle AddSize(
            this Rectangle rect,
            Size           delta
        )
        {
            return new Rectangle(
                rect.Location,
                rect.Size.Add(delta)
            );
        }
        
        /// <summary>
        /// Adds a size difference to the <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="RectangleF"/>.
        /// </param>
        /// <param name="delta">
        /// The <see cref="SizeF"/> to add.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF AddSize(
            this RectangleF rect,
            SizeF           delta
        )
        {
            return new RectangleF(
                rect.Location,
                rect.Size.Add(delta)
            );
        }
        
        /// <summary>
        /// Scales the <see cref="Rectangle"/> by a factor.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="Rectangle"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle Scale(
            this Rectangle rect,
            float          factor
        )
        {
            return rect.ToRectangleF().Scale(factor).ToRectangle();
        }
        
        /// <summary>
        /// Scales the <see cref="RectangleF"/> by a factor.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="RectangleF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF Scale(
            this RectangleF rect,
            float           factor
        )
        {
            return rect.ScaleLocation(factor)
                       .ScaleSize(factor);
        }
        
        /// <summary>
        /// Scales the <see cref="Rectangle"/> by a factor.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="Rectangle"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle Scale(
            this Rectangle rect,
            Size           factor
        )
        {
            return rect.ToRectangleF().Scale(
                factor.ToSizeF()
            ).ToRectangle();
        }
        
        /// <summary>
        /// Scales the <see cref="RectangleF"/> by a factor.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="RectangleF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF Scale(
            this RectangleF rect,
            SizeF           factor
        )
        {
            return rect.ScaleLocation(factor)
                       .ScaleSize(factor);
        }

        /// <summary>
        /// Scales the location of the <see cref="Rectangle"/> by a factor.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="Rectangle"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle ScaleLocation(
            this Rectangle rect,
            float          factor
        )
        {
            return rect.ToRectangleF().ScaleLocation(factor).ToRectangle();
        }
        
        /// <summary>
        /// Scales the location of the <see cref="RectangleF"/> by a factor.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="RectangleF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF ScaleLocation(
            this RectangleF rect,
            float           factor
        )
        {
            return new RectangleF(
                rect.Location.Product(factor),
                rect.Size
            );
        }
        
        /// <summary>
        /// Scales the location of the <see cref="Rectangle"/> by a factor.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="Rectangle"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle ScaleLocation(
            this Rectangle rect,
            Size           factor
        )
        {
            return new Rectangle(
                rect.Location.Product(factor),
                rect.Size
            );
        }
        
        /// <summary>
        /// Scales the location of the <see cref="RectangleF"/> by a factor.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="RectangleF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF ScaleLocation(
            this RectangleF rect,
            SizeF           factor
        )
        {
            return new RectangleF(
                rect.Location.Product(factor),
                rect.Size
            );
        }

        /// <summary>
        /// Scales the size of the <see cref="Rectangle"/> by a factor.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="Rectangle"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle ScaleSize(
            this Rectangle rect,
            float          factor
        )
        {
            return rect.ToRectangleF().ScaleSize(factor).ToRectangle();
        }
        
        /// <summary>
        /// Scales the size of the <see cref="RectangleF"/> by a factor.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="RectangleF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF ScaleSize(
            this RectangleF rect,
            float           factor
        )
        {
            return new RectangleF(
                rect.Location,
                rect.Size.Product(factor)
            );
        }
        
        /// <summary>
        /// Scales the size of the <see cref="Rectangle"/> by a factor.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="Rectangle"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle ScaleSize(
            this Rectangle rect,
            Size           factor
        )
        {
            return new Rectangle(
                rect.Location,
                rect.Size.Product(factor)
            );
        }
        
        /// <summary>
        /// Scales the size of the <see cref="RectangleF"/> by a factor.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="RectangleF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF ScaleSize(
            this RectangleF rect,
            SizeF           factor
        )
        {
            return new RectangleF(
                rect.Location,
                rect.Size.Product(factor)
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
        public static Rectangle SubtractOffset(
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
        /// Subtracts a position offset from the <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="RectangleF"/>.
        /// </param>
        /// <param name="delta">
        /// The <see cref="PointF"/> to subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF SubtractOffset(
            this RectangleF rect,
            PointF          delta
        )
        {
            return new RectangleF(
                rect.Location.Add(delta),
                rect.Size
            );
        }
        
        /// <summary>
        /// Subtracts a size difference from the <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="Rectangle"/>.
        /// </param>
        /// <param name="delta">
        /// The <see cref="Size"/> to subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle SubtractSize(
            this Rectangle rect,
            Size           delta
        )
        {
            return new Rectangle(
                rect.Location,
                rect.Size.Subtract(delta)
            );
        }
        
        /// <summary>
        /// Subtracts a size difference from the <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="rect">
        /// The <see cref="RectangleF"/>.
        /// </param>
        /// <param name="delta">
        /// The <see cref="SizeF"/> to subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF SubtractSize(
            this RectangleF rect,
            SizeF           delta
        )
        {
            return new RectangleF(
                rect.Location,
                rect.Size.Subtract(delta)
            );
        }
        
        /// <summary>
        /// Converts a <see cref="RectangleF"/> to a <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="r">
        /// The <see cref="RectangleF"/> to convert.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/> that was converted.
        /// </returns>
        public static Rectangle ToRectangle(
            this RectangleF r
        )
        {
            return new Rectangle(
                r.Location.ToPoint(),
                r.Size.ToSize()
            );
        }
        
        /// <summary>
        /// Converts a <see cref="Rectangle"/> to a <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="r">
        /// The <see cref="Rectangle"/> to convert.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF"/> that was converted.
        /// </returns>
        public static RectangleF ToRectangleF(
            this Rectangle r
        )
        {
            return new RectangleF(
                r.Location.ToPointF(),
                r.Size.ToSizeF()
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
