/**
 * IEngineHost.cs - rzxe Game Engine Host Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

namespace Oddmatics.Rzxe.Game.Hosting
{
    /// <summary>
    /// Represents the game engine hosting layer in rzxe.
    /// </summary>
    public interface IEngineHost
    {
        /// <summary>
        /// Gets an interface to the renderer that is currently in use.
        /// </summary>
        IHostedRenderer Renderer { get; }
    }
}