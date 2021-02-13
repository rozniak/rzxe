/**
 * IWindowManager.cs - Window Manager Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Game;
using Oddmatics.Rzxe.Game.Hosting;
using Oddmatics.Rzxe.Input;
using System;

namespace Oddmatics.Rzxe.Windowing
{
    /// <summary>
    /// Represents a window manager.
    /// </summary>
    public interface IWindowManager : IDisposable
    {
        /// <summary>
        /// Gets the host interface that is used by the game for interacting with the
        /// window manager.
        /// </summary>
        IHostedRenderer HostInterface { get; }

        /// <summary>
        /// Gets a value indicating whether the window is open.
        /// </summary>
        bool IsOpen { get; }
        
        /// <summary>
        /// Gets the name of the window manager.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether the window manager is ready.
        /// </summary>
        bool Ready { get; }
        
        /// <summary>
        /// Gets or sets the game engine that will be rendered.
        /// </summary>
        IGameEngine RenderedGameEngine { get; }
        
        
        /// <summary>
        /// Initializes the window manager.
        /// </summary>
        /// <param name="game">
        /// The game that will be rendered by the window manager.
        /// </param>
        void Initialize(
            IGameEngine game
        );
        
        /// <summary>
        /// Reads in the latest input events from the window manager.
        /// </summary>
        /// <returns>
        /// The latest input events from the window manager.
        /// </returns>
        InputEvents ReadInputEvents();
        
        /// <summary>
        /// Renders the next frame to the window.
        /// </summary>
        void RenderFrame();
    }
}
