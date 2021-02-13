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
        /// Gets the directory path for the game content root.
        /// </summary>
        string GameContentRoot { get; }
        
        /// <summary>
        /// Gets the title of the game.
        /// </summary>
        string GameTitle { get; }
        
        /// <summary>
        /// Gets the initial value that indicates that the viewport should be scaled
        /// to the size of the window's client area.
        /// </summary>
        bool InitialViewportScalingOption { get; }
        
        /// <summary>
        /// Gets the initial size of the viewport.
        /// </summary>
        Size InitialViewportSize { get; }
    }
}
