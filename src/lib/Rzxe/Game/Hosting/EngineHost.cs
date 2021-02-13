/**
 * IEngineHost.cs - rzxe Game Engine Host
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Input;
using Oddmatics.Rzxe.Windowing;
using Oddmatics.Rzxe.Windowing.Implementations.GlfwFx;
using System;
using System.Diagnostics;
using System.Threading;

namespace Oddmatics.Rzxe.Game.Hosting
{
    /// <summary>
    /// Provides the functionality for hosting a game engine in rzxe.
    /// </summary>
    public class EngineHost : IEngineHost
    {
        /// <inheritdoc />
        public IHostedRenderer Renderer
        {
            get { return WindowManager.HostInterface; }
        }


        /// <summary>
        /// The game engine that is being, or will be, hosted.
        /// </summary>
        private IGameEngine HostedGame { get; set; }
        
        /// <summary>
        /// The value that indicates whether the engine host has been initialized.
        /// </summary>
        private bool Initialized { get; set; }

        /// <summary>
        /// The window manager.
        /// </summary>
        private IWindowManager WindowManager { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="EngineHost"/> class.
        /// </summary>
        /// <param name="game">
        /// The game engine that will be hosted.
        /// </param>
        public EngineHost(
            IGameEngine game
        )
        {
            HostedGame = game;
        }
        
        
        /// <summary>
        /// Initializes subsystems to begin running the game.
        /// </summary>
        public void Initialize()
        {
            if (Initialized)
            {
                throw new InvalidOperationException(
                    "The engine host has already been initialized."
                );
            }

            // FIXME: Replace this one day with a way of selecting the window manager
            //
            WindowManager = new GlfwWindowManager();

            WindowManager.Initialize(HostedGame);

            Initialized = true;
        }
        
        /// <summary>
        /// Runs the game.
        /// </summary>
        public void Run()
        {
            if (!Initialized)
            {
                throw new InvalidOperationException(
                    "The engine host has not yet been initialized."
                );
            }

            // Enter the main game loop
            //
            var gameTime = new Stopwatch();

            HostedGame.Begin(this);
            
            while (WindowManager.IsOpen)
            {
                TimeSpan deltaTime = gameTime.Elapsed;
                gameTime.Reset();
                gameTime.Start();

                InputEvents inputs = WindowManager.ReadInputEvents();
                HostedGame.Update(deltaTime, inputs);

                WindowManager.RenderFrame();

                Thread.Yield();
            }
        }
    }
}
