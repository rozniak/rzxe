/**
 * StringMetrics.cs - Measured String Font Metrics
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Represents font measurements/metrics for a string that has been measured out.
    /// </summary>
    public class StringMetrics
    {
        /// <summary>
        /// Gets the measured metrics for each line in the string.
        /// </summary>
        public IEnumerable<SingleLineStringMetrics> LineMetrics { get; }
        
        /// <summary>
        /// Gets the size of the string.
        /// </summary>
        public Size Size;
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="StringMetrics"/> class.
        /// </summary>
        /// <param name="lineMetrics">
        /// The measured metrics for each line in the string.
        /// </param>
        /// <param name="lineSpacing">
        /// The line spacing from the font.
        /// </param>
        public StringMetrics(
            IEnumerable<SingleLineStringMetrics> lineMetrics,
            int                                  lineSpacing
        )
        {
            LineMetrics = lineMetrics;
            
            // Calculate the total size of the string
            //
            int largestWidth = 0;
            int totalHeight  = 0;
            
            foreach (SingleLineStringMetrics metrics in lineMetrics)
            {
                if (metrics.Size.Width > largestWidth)
                {
                    largestWidth = metrics.Size.Width;
                }
                
                totalHeight += metrics.Size.Height;
            }
            
            totalHeight += lineSpacing * (LineMetrics.Count() - 1);
        }
    }
}
