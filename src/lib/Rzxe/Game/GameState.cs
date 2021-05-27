/**
 * GameState.cs - Game State
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
    /// Represents a state for the game.
    /// </summary>
    public abstract class GameState : IDisposable, IEquatable<GameState>
    {
        /// <summary>
        /// Gets the focal mode for the state.
        /// </summary>
        public abstract InputFocalMode FocalMode { get; }
        
        /// <summary>
        /// Gets the nice name of the state.
        /// </summary>
        public abstract string Name { get; }
        
        
        /// <inheritdoc />
        public virtual void Dispose() { }

        /// <inheritdoc />
        public bool Equals(
            GameState other
        )
        {
            return Name == other.Name;
        }
        
        /// <summary>
        /// Renders the game state.
        /// </summary>
        /// <param name="graphics">
        /// The graphics interface for the renderer.
        /// </param>
        public abstract void RenderFrame(
            IGraphicsController graphics
        );
        
        /// <summary>
        /// Updates the game state with the latest inputs from the game engine.
        /// </summary>
        /// <param name="deltaTime">
        /// The time difference since the last update.
        /// </param>
        /// <param name="inputs">
        /// The latest state of inputs.
        /// </param>
        public abstract void Update(
            TimeSpan    deltaTime,
            InputEvents inputs
        );
    }
}
