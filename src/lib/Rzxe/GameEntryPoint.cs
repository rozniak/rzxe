/**
 * GameEntryPoint.cs - Game Entry Point
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Game;
using Oddmatics.Rzxe.Input;
using Oddmatics.Rzxe.Windowing;
using Oddmatics.Rzxe.Windowing.Implementations.GlfwFx;
using System;
using System.Diagnostics;
using System.Threading;

namespace Oddmatics.Rzxe
{
    /// <summary>
    /// Represents the entry point for the game engine.
    /// </summary>
    public sealed class GameEntryPoint
    {
        /// <summary>
        /// Gets or sets the game engine to run.
        /// </summary>
        public IGameEngine GameEngine
        {
            get { return _GameEngine; }
            set
            {
                if (Locked)
                {
                    throw new InvalidOperationException(
                        "Game engine state has been locked."
                    );
                }
                else
                {
                    _GameEngine = value;
                }
            }
        }
        private IGameEngine _GameEngine;
        
        /// <summary>
        /// Gets a value indicating whether the game engine state has been locked.
        /// </summary>
        public bool Locked { get; private set; }
        
        
        /// <summary>
        /// The window manager.
        /// </summary>
        private IWindowManager WindowManager { get; set; }
        
        
        /// <summary>
        /// Initializes subsystems to begin running the game.
        /// </summary>
        public void Initialize()
        {
            if (Locked)
            {
                throw new InvalidOperationException(
                    "Game engine state has been locked."
                );
            }

            if (GameEngine == null)
            {
                throw new InvalidOperationException(
                    "No game engine provided."
                );
            }

            // FIXME: Replace this one day with a way of selecting the window manager
            //
            WindowManager = new GlfwWindowManager()
            {
                RenderedGameEngine = GameEngine
            };
            
            WindowManager.Initialize();
        }
        
        /// <summary>
        /// Runs the game.
        /// </summary>
        public void Run()
        {
            if (WindowManager == null || !WindowManager.Ready)
            {
                throw new InvalidOperationException("No window manager initialized.");
            }

            // Enter the main game loop
            //
            var gameTime = new Stopwatch();
            
            GameEngine.Begin();
            
            while (WindowManager.IsOpen)
            {
                TimeSpan deltaTime = gameTime.Elapsed;
                gameTime.Reset();
                gameTime.Start();

                InputEvents inputs = WindowManager.ReadInputEvents();
                GameEngine.Update(deltaTime, inputs);

                WindowManager.RenderFrame();

                Thread.Yield();
            }
        }
    }
}
