/**
 * GlfwHostedRenderer.cs - rzxe Game Engine Host Renderer Interface (GLFW)
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Extensions;
using Oddmatics.Rzxe.Game.Hosting;
using System.Drawing;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// The GLFW (OpenGL) implementation for the hosted renderer interface.
    /// </summary>
    internal class GlfwHostedRenderer : IHostedRenderer
    {
        /// <inheritdoc />
        public Size ClientSize
        {
            get { return WindowManager.ClientSize; }
            set { WindowManager.ClientSize = value; }
        }

        /// <inheritdoc />
        public string Name
        {
            get { return WindowManager.Name; }
        }

        /// <inheritdoc />
        public bool ScaleViewport
        {
            get { return WindowManager.ScaleViewport; }
            set { WindowManager.ScaleViewport = value; }
        }

        /// <inheritdoc />
        public Size ViewportSize
        {
            get { return WindowManager.ViewportSize; }
            set { WindowManager.ViewportSize = value; }
        }

        /// <inheritdoc />
        public string WindowTitle
        {
            get { return WindowManager.WindowTitle; }
            set { WindowManager.WindowTitle = value; }
        }


        /// <summary>
        /// The window manager instance that is being interfaced to.
        /// </summary>
        private GlfwWindowManager WindowManager { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GlfwWindowManager"/> class.
        /// </summary>
        /// <param name="wm">
        /// The window manager to interface to.
        /// </param>
        public GlfwHostedRenderer(
            GlfwWindowManager wm
        )
        {
            WindowManager = wm;
        }
        
        
        /// <inheritdoc />
        public Point PointToClient(
            Point p
        )
        {
            return p.PointTo(ViewportSize, ClientSize);
        }
        
        /// <inheritdoc />
        public PointF PointToClient(
            PointF p
        )
        {
            return p.PointTo(
                ViewportSize.ToSizeF(),
                ClientSize.ToSizeF()
            );
        }
        
        /// <inheritdoc />
        public Point PointToViewport(
            Point p
        )
        {
            return p.PointTo(ClientSize, ViewportSize);
        }
        
        /// <inheritdoc />
        public PointF PointToViewport(
            PointF p
        )
        {
            return p.PointTo(
                ClientSize.ToSizeF(),
                ViewportSize.ToSizeF()
            );
        }
    }
}
