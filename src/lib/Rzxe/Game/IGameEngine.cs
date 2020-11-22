/**
 * IGameEngine.cs - Game Engine Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Input;
using Oddmatics.Rzxe.Windowing.Graphics;
using System;

namespace Oddmatics.Rzxe.Game
{
    /// <summary>
    /// Represents the game engine.
    /// </summary>
    public interface IGameEngine
    {
        /// <summary>
        /// Gets or sets the current game state.
        /// </summary>
        GameState CurrentGameState { get; set; }
        
        /// <summary>
        /// Gets the game engine parameters.
        /// </summary>
        IGameEngineParameters Parameters { get; }
        
        
        /// <summary>
        /// Starts the game engine.
        /// </summary>
        void Begin();
        
        /// <summary>
        /// Renders the next frame.
        /// </summary>
        /// <param name="graphics">
        /// The graphics interface for the renderer.
        /// </param>
        void RenderFrame(
            IGraphicsController graphics
        );
        
        /// <summary>
        /// Updates the game engine with the latest inputs from the window manager.
        /// </summary>
        /// <param name="deltaTime">
        /// The time difference since the last update.
        /// </param>
        /// <param name="inputs">
        /// The latest state of inputs.
        /// </param>
        void Update(
            TimeSpan deltaTime,
            InputEvents inputs
        );
    }
}