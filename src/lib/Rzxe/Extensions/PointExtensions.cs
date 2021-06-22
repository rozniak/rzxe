/**
 * PointExtensions.cs - Point Extension Methods
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Util;
using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="Point"/> struct.
    /// </summary>
    public static class PointExtensions
    {
        /// <summary>
        /// Adds the specified offset to a <see cref="Point"/>.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="Point"/>.
        /// </param>
        /// <param name="delta">
        /// The offset to add.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Point"/>.
        /// </returns>
        public static Point Add(
            this Point origin,
            int        delta
        )
        {
            return new Point(
                origin.X + delta,
                origin.Y + delta
            );
        }
        
        /// <summary>
        /// Adds the specified offset to a <see cref="PointF"/>.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="PointF"/>.
        /// </param>
        /// <param name="delta">
        /// The offset to add.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="PointF"/>.
        /// </returns>
        public static PointF Add(
            this PointF origin,
            float       delta
        )
        {
            return new PointF(
                origin.X + delta,
                origin.Y + delta
            );
        }

        /// <summary>
        /// Adds one <see cref="Point"/> to another.
        /// </summary>
        /// <param name="origin">
        /// The first <see cref="Point"/>.
        /// </param>
        /// <param name="delta">
        /// The second <see cref="Point"/> (difference).
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Point"/>.
        /// </returns>
        public static Point Add(
            this Point origin,
            Point      delta
        )
        {
            return new Point(
                origin.X + delta.X,
                origin.Y + delta.Y
            );
        }
        
        /// <summary>
        /// Adds one <see cref="PointF"/> to another.
        /// </summary>
        /// <param name="origin">
        /// The first <see cref="PointF"/>.
        /// </param>
        /// <param name="delta">
        /// The second <see cref="PointF"/> (difference).
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="PointF"/>.
        /// </returns>
        public static PointF Add(
            this PointF origin,
            PointF      delta
        )
        {
            return new PointF(
                origin.X + delta.X,
                origin.Y + delta.Y
            );
        }
        
        /// <summary>
        /// Adds the specified offset to a <see cref="Point"/>.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="Point"/>.
        /// </param>
        /// <param name="delta">
        /// The offset to add.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Point"/>.
        /// </returns>
        public static Point Add(
            this Point origin,
            Size       delta
        )
        {
            return new Point(
                origin.X + delta.Width,
                origin.Y + delta.Height
            );
        }
        
        /// <summary>
        /// Adds the specified offset to a <see cref="PointF"/>.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="PointF"/>.
        /// </param>
        /// <param name="delta">
        /// The offset to add.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="PointF"/>.
        /// </returns>
        public static PointF Add(
            this PointF origin,
            SizeF       delta
        )
        {
            return new PointF(
                origin.X + delta.Width,
                origin.Y + delta.Height
            );
        }
        
        /// <summary>
        /// Computes the location of the specified point in its source region into the
        /// target region coordinate space.
        /// </summary>
        /// <param name="p">
        /// The source region <see cref="Point"/> to convert.
        /// </param>
        /// <param name="source">
        /// The source region size.
        /// </param>
        /// <param name="target">
        /// The target region size.
        /// </param>
        /// <returns>
        /// A <see cref="Point"/> that represents the converted <see cref="Point"/>,
        /// <paramref name="p"/>, in the target region coordinate space.
        /// </returns>
        public static Point PointTo(
            this Point p,
            Size  source,
            Size  target
        )
        {
            return PointTo(
                p.ToPointF(),
                source.ToSizeF(),
                target.ToSizeF()
            ).ToPoint();
        }
        
        /// <summary>
        /// Computes the location of the specified point in its source region into the
        /// target region coordinate space.
        /// </summary>
        /// <param name="p">
        /// The source region <see cref="PointF"/> to convert.
        /// </param>
        /// <param name="source">
        /// The source region size.
        /// </param>
        /// <param name="target">
        /// The target region size.
        /// </param>
        /// <returns>
        /// A <see cref="PointF"/> that represents the converted <see cref="PointF"/>,
        /// <paramref name="p"/>, in the target region coordinate space.
        /// </returns>
        public static PointF PointTo(
            this PointF p,
            SizeF       source,
            SizeF       target
        )
        {
            SizeF ratio = target.Reduce(source);

            return p.Product(ratio);
        }
        
        /// <summary>
        /// Multiplies the <see cref="Point"/> by a factor.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="Point"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Point"/>.
        /// </returns>
        public static Point Product(
            this Point origin,
            int        factor
        )
        {
            return new Point(
                origin.X * factor,
                origin.Y * factor
            );
        }
        
        /// <summary>
        /// Multiplies the <see cref="PointF"/> by a factor.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="PointF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="PointF"/>.
        /// </returns>
        public static PointF Product(
            this PointF origin,
            float       factor
        )
        {
            return new PointF(
                origin.X * factor,
                origin.Y * factor
            );
        }

        /// <summary>
        /// Multiplies the <see cref="Point"/> by a factor.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="Point"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Point"/>.
        /// </returns>
        public static Point Product(
            this Point origin,
            Size       factor
        )
        {
            return new Point(
                origin.X * factor.Width,
                origin.Y * factor.Height
            );
        }
        
        /// <summary>
        /// Multiplies the <see cref="PointF"/> by a factor.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="PointF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="PointF"/>.
        /// </returns>
        public static PointF Product(
            this PointF origin,
            SizeF       factor
        )
        {
            return new PointF(
                origin.X * factor.Width,
                origin.Y * factor.Height
            );
        }
        
        /// <summary>
        /// Reduces (divides) the <see cref="Point"/> by a factor.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="Point"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to reduce by.
        /// </param>
        /// <param name="rounding">
        /// Specification for how to round <paramref name="origin"/> if its values are
        /// midway between two other numbers.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Point"/>.
        /// </returns>
        public static Point Reduce(
            this Point     origin,
            int            factor,
            RoundingMethod rounding = RoundingMethod.ToNearest
        )
        {
            return origin.ToPointF().Reduce(factor).ToPoint(rounding);
        }
        
        /// <summary>
        /// Reduces (divides) the <see cref="PointF"/> by a factor.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="PointF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to reduce by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="PointF"/>.
        /// </returns>
        public static PointF Reduce(
            this PointF origin,
            float       factor
        )
        {
            return new PointF(
                origin.X / factor,
                origin.Y / factor
            );
        }

        /// <summary>
        /// Reduces (divides) the <see cref="Point"/> by a factor.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="Point"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to reduce by.
        /// </param>
        /// <param name="rounding">
        /// Specification for how to round <paramref name="origin"/> if its values are
        /// midway between two other numbers.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Point"/>.
        /// </returns>
        public static Point Reduce(
            this Point     origin,
            Size           factor,
            RoundingMethod rounding = RoundingMethod.ToNearest
        )
        {
            return origin.ToPointF().Reduce(
                factor.ToSizeF()
            ).ToPoint(rounding);
        }
        
        /// <summary>
        /// Reduces (divides) the <see cref="PointF"/> by a factor.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="PointF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to reduce by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="PointF"/>.
        /// </returns>
        public static PointF Reduce(
            this PointF origin,
            SizeF       factor
        )
        {
            return new PointF(
                origin.X / factor.Width,
                origin.Y / factor.Height
            );
        }

        /// <summary>
        /// Subtracts the specified offset from a <see cref="Point"/>.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="Point"/>.
        /// </param>
        /// <param name="delta">
        /// The offset to subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Point"/>.
        /// </returns>
        public static Point Subtract(
            this Point origin,
            int        delta
        )
        {
            return new Point(
                origin.X - delta,
                origin.Y - delta
            );
        }
        
        /// <summary>
        /// Subtracts the specified offset from a <see cref="PointF"/>.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="PointF"/>.
        /// </param>
        /// <param name="delta">
        /// The offset to subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="PointF"/>.
        /// </returns>
        public static PointF Subtract(
            this PointF origin,
            float       delta
        )
        {
            return new PointF(
                origin.X - delta,
                origin.Y - delta
            );
        }
        
        /// <summary>
        /// Subtract one <see cref="Point"/> from another.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="Point"/>.
        /// </param>
        /// <param name="delta">
        /// The offset <see cref="Point"/> subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Point"/>.
        /// </returns>
        public static Point Subtract(
            this Point origin,
            Point      delta
        )
        {
            return new Point(
                origin.X - delta.X,
                origin.Y - delta.Y
            );
        }
        
        /// <summary>
        /// Subtract one <see cref="PointF"/> from another.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="PointF"/>.
        /// </param>
        /// <param name="delta">
        /// The offset <see cref="PointF"/> subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="PointF"/>.
        /// </returns>
        public static PointF Subtract(
            this PointF origin,
            PointF      delta
        )
        {
            return new PointF(
                origin.X - delta.X,
                origin.Y - delta.Y
            );
        }
        
        /// <summary>
        /// Subtracts the specified offset from a <see cref="Point"/>.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="Point"/>.
        /// </param>
        /// <param name="delta">
        /// The offset to subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Point"/>.
        /// </returns>
        public static Point Subtract(
            this Point origin,
            Size       delta
        )
        {
            return new Point(
                origin.X - delta.Width,
                origin.Y - delta.Height
            );
        }
        
        /// <summary>
        /// Subtracts the specified offset from a <see cref="PointF"/>.
        /// </summary>
        /// <param name="origin">
        /// The <see cref="PointF"/>.
        /// </param>
        /// <param name="delta">
        /// The offset to subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="PointF"/>.
        /// </returns>
        public static PointF Subtract(
            this PointF origin,
            SizeF       delta
        )
        {
            return new PointF(
                origin.X - delta.Width,
                origin.Y - delta.Height
            );
        }

        /// <summary>
        /// Converts a <see cref="PointF"/> to a <see cref="Point"/>.
        /// </summary>
        /// <param name="p">
        /// The <see cref="PointF"/> to convert.
        /// </param>
        /// <param name="rounding">
        /// Specification for how to round <paramref name="p"/> if its values are
        /// midway between two other numbers.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/> that was converted.
        /// </returns>
        public static Point ToPoint(
            this PointF    p,
            RoundingMethod rounding = RoundingMethod.ToNearest
        )
        {
            double x = 0;
            double y = 0;
            
            switch (rounding)
            {
                case RoundingMethod.Ceiling:
                    x = Math.Ceiling(p.X);
                    y = Math.Ceiling(p.Y);
                    break;

                case RoundingMethod.Floor:
                    x = Math.Floor(p.X);
                    y = Math.Floor(p.Y);
                    break;

                case RoundingMethod.ToNearest:
                    x = Math.Round(p.X, 0, MidpointRounding.ToEven);
                    y = Math.Round(p.Y, 0, MidpointRounding.ToEven);
                    break;
            }
            
            return new Point((int) x, (int) y);
        }
        
        /// <summary>
        /// Converts a <see cref="Point"/> to a <see cref="PointF"/>.
        /// </summary>
        /// <param name="p">
        /// The <see cref="Point"/> to convert.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/> that was converted.
        /// </returns>
        public static PointF ToPointF(
            this Point p
        )
        {
            return new PointF(
                p.X,
                p.Y
            );
        }
    }
}
