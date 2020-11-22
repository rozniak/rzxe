/**
 * IBorderBoxResource.cs - Sprite Border Box Resource Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

namespace Oddmatics.Rzxe.Windowing.Graphics
{
    /// <summary>
    /// Represents a border box resource as part of a sprite atlas.
    /// </summary>
    public interface IBorderBoxResource
    {
        /// <summary>
        /// Gets the name of the border box resource.
        /// </summary>
        string Name { get; }
    }
}
