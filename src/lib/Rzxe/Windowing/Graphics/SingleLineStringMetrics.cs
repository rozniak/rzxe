/**
 * SingleLineStringMetrics.cs - Measured String (Single Line) Font Metrics
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Represents font measurements/metrics for a single line string that has been
    /// measured out.
    /// </summary>
    public class SingleLineStringMetrics
    {
        /// <summary>
        /// Gets the relative origin for the string as part of a multi-line string; if
        /// the string is only a single line then this is zero.
        /// </summary>
        public Point RelativeOrigin { get; private set; }
        
        /// <summary>
        /// Gets the size of the string.
        /// </summary>
        public Size Size { get; private set; }
        
        /// <summary>
        /// Gets the y-offset for the baseline.
        /// </summary>
        public int YBaseline { get; private set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleLineStringMetrics"/>
        /// class.
        /// </summary>
        /// <param name="relativeOrigin">
        /// The relative origin for the string.
        /// </param>
        /// <param name="size">
        /// The size of the string.
        /// </param>
        /// <param name="yBaseline">
        /// The y-offset for the baseline.
        /// </param>
        public SingleLineStringMetrics(
            Point relativeOrigin,
            Size  size,
            int   yBaseline
        )
        {
            RelativeOrigin = relativeOrigin;
            Size           = size;
            YBaseline      = yBaseline;
        }
    }
}
