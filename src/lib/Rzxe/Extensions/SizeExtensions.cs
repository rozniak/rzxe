/**
 * SizeExtensions.cs - Size Extensions Methods
 *
 * This source-code is part of a clean-room recreation of Lego Junkbot by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="Size"/> struct.
    /// </summary>
    public static class SizeExtensions
    {
        /// <summary>
        /// Adds the specified offset to a <see cref="Size"/>
        /// </summary>
        /// <param name="subject">
        /// The <see cref="Size"/>.
        /// </param>
        /// <param name="delta">
        /// The offset to add.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Size"/>.
        /// </returns>
        public static Size Add(
            this Size subject,
            int       delta
        )
        {
            return new Size(
                subject.Width  + delta,
                subject.Height + delta
            );
        }
        
        /// <summary>
        /// Adds the specified offset to a <see cref="SizeF"/>
        /// </summary>
        /// <param name="subject">
        /// The <see cref="SizeF"/>.
        /// </param>
        /// <param name="delta">
        /// The offset to add.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="SizeF"/>.
        /// </returns>
        public static SizeF Add(
            this SizeF subject,
            float      delta
        )
        {
            return new SizeF(
                subject.Width  + delta,
                subject.Height + delta
            );
        }

        /// <summary>
        /// Adds one <see cref="Size"/> to another.
        /// </summary>
        /// <param name="subject">
        /// The first <see cref="Size"/>.
        /// </param>
        /// <param name="delta">
        /// The second <see cref="Size"/> (difference).
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Size"/>.
        /// </returns>
        public static Size Add(
            this Size subject,
            Size      delta
        )
        {
            return new Size(
                subject.Width  + delta.Width,
                subject.Height + delta.Height
            );
        }
        
        /// <summary>
        /// Adds one <see cref="SizeF"/> to another.
        /// </summary>
        /// <param name="subject">
        /// The first <see cref="SizeF"/>.
        /// </param>
        /// <param name="delta">
        /// The second <see cref="SizeF"/> (difference).
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="SizeF"/>.
        /// </returns>
        public static SizeF Add(
            this SizeF subject,
            SizeF      delta
        )
        {
            return new SizeF(
                subject.Width  + delta.Width,
                subject.Height + delta.Height
            );
        }
        
        /// <summary>
        /// Multiplies the <see cref="Size"/> by a factor.
        /// </summary>
        /// <param name="subject">
        /// The <see cref="Size"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Size"/>.
        /// </returns>
        public static Size Product(
            this Size subject,
            int       factor
        )
        {
            return new Size(
                subject.Width  * factor,
                subject.Height * factor
            );
        }
        
        /// <summary>
        /// Multiplies the <see cref="SizeF"/> by a factor.
        /// </summary>
        /// <param name="subject">
        /// The <see cref="SizeF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="SizeF"/>.
        /// </returns>
        public static SizeF Product(
            this SizeF subject,
            float      factor
        )
        {
            return new SizeF(
                subject.Width  * factor,
                subject.Height * factor
            );
        }

        /// <summary>
        /// Multiplies the <see cref="Size"/> by a factor.
        /// </summary>
        /// <param name="subject">
        /// The <see cref="Size"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Size"/>.
        /// </returns>
        public static Size Product(
            this Size subject,
            Size      factor
        )
        {
            return new Size(
                subject.Width  * factor.Width,
                subject.Height * factor.Height
            );
        }
        
        /// <summary>
        /// Multiplies the <see cref="SizeF"/> by a factor.
        /// </summary>
        /// <param name="subject">
        /// The <see cref="SizeF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to multiply by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="SizeF"/>.
        /// </returns>
        public static SizeF Product(
            this SizeF subject,
            SizeF      factor
        )
        {
            return new SizeF(
                subject.Width  * factor.Width,
                subject.Height * factor.Height
            );
        }
        
        /// <summary>
        /// Reduces (divides) the <see cref="Size"/> by a factor.
        /// </summary>
        /// <param name="subject">
        /// The <see cref="Size"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to reduce by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Size"/>.
        /// </returns>
        public static Size Reduce(
            this Size subject,
            int       factor
        )
        {
            return subject.ToSizeF().Reduce(factor).ToSize();
        }
        
        /// <summary>
        /// Reduces (divides) the <see cref="SizeF"/> by a factor.
        /// </summary>
        /// <param name="subject">
        /// The <see cref="SizeF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to reduce by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="SizeF"/>.
        /// </returns>
        public static SizeF Reduce(
            this SizeF subject,
            float      factor
        )
        {
            return new SizeF(
                subject.Width  / factor,
                subject.Height / factor
            );
        }

        /// <summary>
        /// Reduces (divides) the <see cref="Size"/> by a factor.
        /// </summary>
        /// <param name="subject">
        /// The <see cref="Size"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to reduce by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Size"/>.
        /// </returns>
        public static Size Reduce(
            this Size subject,
            Size      factor
        )
        {
            return subject.ToSizeF().Reduce(
                factor.ToSizeF()
            ).ToSize();
        }
        
        /// <summary>
        /// Reduces (divides) the <see cref="SizeF"/> by a factor.
        /// </summary>
        /// <param name="subject">
        /// The <see cref="SizeF"/>.
        /// </param>
        /// <param name="factor">
        /// The factor to reduce by.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="SizeF"/>.
        /// </returns>
        public static SizeF Reduce(
            this SizeF subject,
            SizeF      factor
        )
        {
            return new SizeF(
                subject.Width  / factor.Width,
                subject.Height / factor.Height
            );
        }
        
        /// <summary>
        /// Subtracts the specified offset from a <see cref="Size"/>.
        /// </summary>
        /// <param name="subject">
        /// The <see cref="Size"/>.
        /// </param>
        /// <param name="delta">
        /// The offset to subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Size"/>.
        /// </returns>
        public static Size Subtract(
            this Size subject,
            int       delta
        )
        {
            return subject.ToSizeF().Subtract(delta).ToSize();
        }
        
        /// <summary>
        /// Subtracts the specified offset from a <see cref="SizeF"/>.
        /// </summary>
        /// <param name="subject">
        /// The <see cref="SizeF"/>.
        /// </param>
        /// <param name="delta">
        /// The offset to subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="SizeF"/>.
        /// </returns>
        public static SizeF Subtract(
            this SizeF subject,
            float      delta
        )
        {
            return new SizeF(
                subject.Width  - delta,
                subject.Height - delta
            );
        }
        
        /// <summary>
        /// Subtracts one <see cref="Size"/> from another.
        /// </summary>
        /// <param name="subject">
        /// The <see cref="Size"/>.
        /// </param>
        /// <param name="delta">
        /// The offset <see cref="Size"/> subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="Size"/>.
        /// </returns>
        public static Size Subtract(
            this Size subject,
            Size      delta
        )
        {
            return subject.ToSizeF().Subtract(
                delta.ToSizeF()
            ).ToSize();
        }
        
        /// <summary>
        /// Subtracts one <see cref="SizeF"/> from another.
        /// </summary>
        /// <param name="subject">
        /// The <see cref="SizeF"/>.
        /// </param>
        /// <param name="delta">
        /// The offset <see cref="SizeF"/> subtract.
        /// </param>
        /// <returns>
        /// The result of the operation on the <see cref="SizeF"/>.
        /// </returns>
        public static SizeF Subtract(
            this SizeF subject,
            SizeF      delta
        )
        {
            return new SizeF(
                subject.Width  - delta.Width,
                subject.Height - delta.Height
            );
        }

        /// <summary>
        /// Converts a <see cref="SizeF"/> to a <see cref="Size"/>
        /// </summary>
        /// <param name="s">
        /// The <see cref="SizeF"/> to convert.
        /// </param>
        /// <returns>
        /// The <see cref="Size"/> that was converted.
        /// </returns>
        public static Size ToSize(
            this SizeF s
        )
        {
            return new Size(
                (int) Math.Floor(s.Width),
                (int) Math.Floor(s.Height)
            );
        }
        
        /// <summary>
        /// Converts a <see cref="Size"/> to a <see cref="SizeF"/>.
        /// </summary>
        /// <param name="s">
        /// THe <see cref="Size"/> to convert.
        /// </param>
        /// <returns>
        /// The <see cref="SizeF"/> that was converted.
        /// </returns>
        public static SizeF ToSizeF(
            this Size s
        )
        {
            return new SizeF(
                s.Width,
                s.Height
            );
        }
    }
}
