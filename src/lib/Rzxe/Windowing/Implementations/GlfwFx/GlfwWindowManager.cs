/**
 * GlfwWindowManager.cs - GLFW Window Manager Implementation
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;
using Oddmatics.Rzxe.Game;
using Oddmatics.Rzxe.Input;
using Pencil.Gaming;
using Pencil.Gaming.Graphics;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The GLFW (OpenGL) implementation of the window manager.
    /// </summary>
    internal sealed class GlfwWindowManager : IWindowManager
    {
        /// <inheritdoc />
        public bool IsOpen { get; private set; }
        
        /// <inheritdoc />
        public bool Ready { get; private set; }

        /// <inheritdoc />
        public IGameEngine RenderedGameEngine
        {
            get { return _RenderedGameEngine; }
            set
            {
                if (Locked)
                {
                    throw new InvalidOperationException(
                        "Window manager state has been locked."
                    );
                }
                else
                {
                    _RenderedGameEngine = value;
                }
            }
        }
        private IGameEngine _RenderedGameEngine;
        
        
        /// <summary>
        /// The currrent input state.
        /// </summary>
        private InputEvents CurrentInputState { get; set; }
        
        /// <summary>
        /// The value that indicates whether the class is disposing or has been
        /// disposed.
        private bool Disposing { get; set; }
        
        /// <summary>
        /// The value that indicates whether the class properties are locked.
        /// </summary>
        private bool Locked { get; set; }


        #region GLFW Bits and Bobs
        
        /// <summary>
        /// The OpenGL ID for the active VAO.
        /// </summary>
        private int GlVaoId { get; set; }
        
        /// <summary>
        /// The resource cache for graphics objects.
        /// </summary>
        private GLResourceCache ResourceCache { get; set; }
        
        /// <summary>
        /// The window pointer in GLFW for the main game window.
        /// </summary>
        private GlfwWindowPtr WindowPtr { get; set; }

        #endregion
        
        
        /// <inheritdoc />
        public void Dispose()
        {
            if (Disposing)
            {
                throw new ObjectDisposedException(
                    "The window manager has already been disposed."
                );
            }

            Glfw.Terminate();
            IsOpen = false;
        }
        
        /// <inheritdoc />
        public void Initialize()
        {
            string title = RenderedGameEngine.Parameters.GameTitle;
            
            CurrentInputState = new InputEvents();
            ResourceCache     = new GLResourceCache(RenderedGameEngine.Parameters);

            // Set up GLFW parameters and create the window
            //
            Glfw.Init();

            Glfw.SetErrorCallback(OnError);

            Glfw.WindowHint(WindowHint.ContextVersionMajor, 3);
            Glfw.WindowHint(WindowHint.ContextVersionMinor, 2);
            Glfw.WindowHint(WindowHint.OpenGLForwardCompat, 1);
            Glfw.WindowHint(WindowHint.OpenGLProfile, (int) OpenGLProfile.Core);

            WindowPtr =
                Glfw.CreateWindow(
                    RenderedGameEngine.Parameters.DefaultClientWindowSize.Width,
                    RenderedGameEngine.Parameters.DefaultClientWindowSize.Height,
                    $"{title} (OpenGL 3.2)",
                    GlfwMonitorPtr.Null,
                    GlfwWindowPtr.Null
                );

            IsOpen = true;

            Glfw.MakeContextCurrent(WindowPtr);

            // Set GL pixel storage parameter
            //
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            // Set up VAO
            //
            GlVaoId = GL.GenVertexArray();
            GL.BindVertexArray(GlVaoId);

            // Set up enabled features
            //
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(
                BlendingFactorSrc.SrcAlpha,
                BlendingFactorDest.OneMinusSrcAlpha
            );

            // Set up viewport defaults
            //
            GL.Viewport(
                0,
                0,
                RenderedGameEngine.Parameters.DefaultClientWindowSize.Width,
                RenderedGameEngine.Parameters.DefaultClientWindowSize.Height
            );

            // Set up input callbacks
            //
            Glfw.SetCharCallback(WindowPtr, OnChar);
            Glfw.SetCursorPosCallback(WindowPtr, OnCursorPos);
            Glfw.SetKeyCallback(WindowPtr, OnKey);
            Glfw.SetMouseButtonCallback(WindowPtr, OnMouseButton);
            Glfw.SetWindowSizeCallback(WindowPtr, OnWindowSize);

            Locked = true;
            Ready  = true;
        }
        
        /// <inheritdoc />
        public InputEvents ReadInputEvents()
        {
            var lastUpdate = CurrentInputState;

            lastUpdate.FinalizeForReporting();

            CurrentInputState = new InputEvents(lastUpdate);

            return lastUpdate;
        }
        
        /// <inheritdoc />
        public void RenderFrame()
        {
            if (Glfw.WindowShouldClose(WindowPtr))
            {
                Dispose();
                return;
            }
            
            RenderedGameEngine.RenderFrame(
                new GLGraphicsController(
                    ResourceCache,
                    RenderedGameEngine.Parameters.DefaultClientWindowSize
                )
            );

            Glfw.SwapBuffers(WindowPtr);
            Glfw.PollEvents();
        }


        #region GLFW Callbacks
        
        /// <summary>
        /// (Callback) Character input received a from GLFW window.
        /// </summary>
        /// <param name="wnd">
        /// The window that received the input.
        /// </param>
        /// <param name="ch">
        /// The input.
        /// </param>
        private void OnChar(
            GlfwWindowPtr wnd,
            char          ch
        )
        {
            CurrentInputState.ReportConsoleInput(ch);
        }
        
        /// <summary>
        /// (Callback) Mouse cursor position update received from a GLFW window.
        /// </summary>
        /// <param name="wnd">
        /// The window that received the input.
        /// </param>
        /// <param name="x">
        /// The new x-coordinate of the mouse cursor.
        /// </param>
        /// <param name="y">
        /// The new y-coordinate of the mouse cursor.
        /// </param>
        private void OnCursorPos(
            GlfwWindowPtr wnd,
            double        x,
            double        y
        )
        {
            CurrentInputState.ReportMouseMovement(
                (float) x,
                (float) y
            );
        }
        
        /// <summary>
        /// (Callback) Error reported from GLFW.
        /// </summary>
        /// <param name="code">
        /// The error code.
        /// </param>
        /// <param name="desc">
        /// The error description.
        /// </param>
        private void OnError(
            GlfwError code,
            string    desc
        )
        {
            Console.WriteLine(desc);
        }
        
        /// <summary>
        /// (Callback) Key press update received from a GLFW window.
        /// </summary>
        /// <param name="wnd">
        /// The window that received the input.
        /// </param>
        /// <param name="key">
        /// The key involved.
        /// </param>
        /// <param name="scanCode">
        /// The scan code of the key involved.
        /// </param>
        /// <param name="action">
        /// The key event that occurred.
        /// </param>
        /// <param name="mods">
        /// The modifier keys active during the event.
        /// </param>
        private void OnKey(
            GlfwWindowPtr wnd,
            Key           key,
            int           scanCode,
            KeyAction     action,
            KeyModifiers  mods
        )
        {
            ControlInput input = GlfwInputMapping.GetMapping(key);

            switch (action)
            {
                case KeyAction.Press:
                    CurrentInputState.ReportPress(input);
                    break;

                case KeyAction.Release:
                    CurrentInputState.ReportRelease(input);
                    break;
            }
        }
        
        /// <summary>
        /// (Callback) Mouse button update received from a GLFW window.
        /// </summary>
        /// <param name="wnd">
        /// The window that received the input.
        /// </param>
        /// <param name="btn">
        /// The mouse button involved.
        /// </param>
        /// <param name="action">
        /// The mouse button event that occurred.
        /// </param>
        private void OnMouseButton(
            GlfwWindowPtr wnd,
            MouseButton   btn,
            KeyAction     action
        )
        {
            ControlInput input = GlfwInputMapping.GetMapping(btn);

            switch (action)
            {
                case KeyAction.Press:
                    CurrentInputState.ReportPress(input);
                    break;

                case KeyAction.Release:
                    CurrentInputState.ReportRelease(input);
                    break;
            }
        }
        
        /// <summary>
        /// (Callback) A resize event occurred in a GLFW window.
        /// </summary>
        /// <param name="wnd">
        /// The window that fired the event.
        /// </param>
        /// <param name="width">
        /// The new width of the window.
        /// </param>
        /// <param name="height">
        /// The new height of the window.
        /// </param>
        private void OnWindowSize(
            GlfwWindowPtr wnd,
            int           width,
            int           height
        )
        {
            GL.Viewport(0, 0, width, height);
        }

        #endregion
    }
}
