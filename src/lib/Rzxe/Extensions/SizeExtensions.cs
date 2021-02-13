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
