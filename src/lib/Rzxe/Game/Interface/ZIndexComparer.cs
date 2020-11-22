/**
 * ZIndexComparer.cs - Z-Index Comparer Class
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Collections.Generic;

namespace Oddmatics.Rzxe.Game.Interface
{
    /// <summary>
    /// Provides methods for comparing z-indexes of <see cref="UxComponent"/> objects.
    /// </summary>
    internal sealed class ZIndexComparer : IComparer<UxComponent>
    {
        /// <inheritdoc />
        public int Compare(
            UxComponent x,
            UxComponent y
        )
        {
            if (x.ZIndex > y.ZIndex)
            {
                return 1;
            }
            else if (x.ZIndex < y.ZIndex)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
