/**
 * IGameEngineParameter.cs - Game Engine Parameters Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Drawing;

namespace Oddmatics.Rzxe.Game
{
    /// <summary>
    /// Represents parameters defined by the game engine.
    /// </summary>
    public interface IGameEngineParameters
    {
        /// <summary>
        /// Gets the default size of the client window.
        /// </summary>
        Size DefaultClientWindowSize { get; }
        
        /// <summary>
        /// Gets the directory path for the game content root.
        /// </summary>
        string GameContentRoot { get; }
        
        /// <summary>
        /// Gets the title of the game.
        /// </summary>
        string GameTitle { get; }
    }
}
