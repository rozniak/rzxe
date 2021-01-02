/**
 * TimeSpanExtension.cs - TimeSpan Extension Methods
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;

namespace Oddmatics.Rzxe.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="TimeSpan"/> class.
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Divides the <see cref="TimeSpan"/> by another <see cref="TimeSpan"/> and
        /// returns the quotient.
        /// </summary>
        /// <param name="t">
        /// The <see cref="TimeSpan"/>.
        /// </param>
        /// <param name="divisor">Divisor.</param>
        /// <returns>
        /// The quotient of the division.
        /// </returns>
        public static int Div(
            this TimeSpan t,
            TimeSpan      divisor
        )
        {
            TimeSpan mod;

            return ModDiv(t, divisor, out mod);
        }
        
        /// <summary>
        /// Performs a modulo operation on the <see cref="TimeSpan"/> by another
        /// <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="t">
        /// The <see cref="TimeSpan"/>.
        /// </param>
        /// <param name="divisor">
        /// The amount to divide by.
        /// </param>
        /// <returns>
        /// The remainder of the division.
        /// </returns>
        public static TimeSpan Mod(
            this TimeSpan t,
            TimeSpan      divisor
        )
        {
            TimeSpan mod;

            ModDiv(t, divisor, out mod);

            return mod;
        }

        /// <summary>
        /// Divides the <see cref="TimeSpan"/> by another <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="t">
        /// The <see cref="TimeSpan"/>.
        /// </param>
        /// <param name="divisor">
        /// The amount to divide by.
        /// </param>
        /// <param name="remainder">
        /// When the method returns, contains the remainder from the division.
        /// </param>
        /// <returns>
        /// The quotient of the division.
        /// </returns>
        public static int ModDiv(
            this TimeSpan t,
            TimeSpan      divisor,
            out TimeSpan  remainder
        )
        {
            int      quotient = 0;
            TimeSpan rem      = t;
            
            if (divisor == TimeSpan.Zero)
            {
                throw new DivideByZeroException(
                    "Attempted to divide by zero time span."
                );
            }
            
            // Perform the simple divide
            //
            while (rem > TimeSpan.Zero)
            {
                quotient++;
                rem = rem.Subtract(divisor);
            }

            // Rollback one step
            //
            quotient--;
            rem = rem.Add(divisor);
            
            // Return now
            //
            remainder = rem;
            return quotient;
        }
        
        /// <summary>
        /// Multiplies the <see cref="TimeSpan"/> by the specified factor.
        /// </summary>
        /// <param name="t">
        /// The <see cref="TimeSpan"/>.
        /// </param>
        /// <param name="factor">
        /// The multiplication factor.
        /// </param>
        /// <returns>
        /// The multiplied <see cref="TimeSpan"/>.
        /// </returns>
        public static TimeSpan Multiply(
            this TimeSpan t,
            int           factor
        )
        {
            TimeSpan res = t;
        
            for (int i = 0; i < factor; i++)
            {
                res = res.Add(t);
            }

            return res;
        }
    }
}
