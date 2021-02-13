/**
 * IHostedRenderer.cs - rzxe Game Engine Host Renderer Interface
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Drawing;

namespace Oddmatics.Rzxe.Game.Hosting
{
    /// <summary>
    /// Represents a renderer in use by the rzxe game engine host.
    /// </summary>
    public interface IHostedRenderer
    {
        /// <summary>
        /// Gets or sets the size of the client area.
        /// </summary>
        Size ClientSize { get; set; }
        
        /// <summary>
        /// Gets the name of the renderer.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets or sets the value that indicates that the viewport should be scaled
        /// to the size of the window's client area.
        /// </summary>
        bool ScaleViewport { get; set; }

        /// <summary>
        /// Gets or sets the size of the viewport - if <see cref="ScaleViewport"/> is
        /// not set then this is always the size of the window's client area.
        /// </summary>
        Size ViewportSize { get; set; }
        
        /// <summary>
        /// Gets or sets the window title.
        /// </summary>
        string WindowTitle { get; set; }
        
        
        /// <summary>
        /// Computes the location of the specified viewport point into client
        /// coordinates.
        /// </summary>
        /// <param name="p">
        /// The viewport <see cref="Point"/> to convert.
        /// </param>
        /// <returns>
        /// A <see cref="Point"/> that represents the converted <see cref="Point"/>,
        /// <paramref name="p"/>, in client coordinates.
        /// </returns>
        Point PointToClient(
            Point p
        );
        
        /// <summary>
        /// Computes the location of the specified viewport point into client
        /// coordinates.
        /// </summary>
        /// <param name="p">
        /// The viewport <see cref="PointF"/> to convert.
        /// </param>
        /// <returns>
        /// A <see cref="PointF"/> that represents the converted <see cref="PointF"/>,
        /// <paramref name="p"/>, in client coordinates.
        /// </returns>
        PointF PointToClient(
            PointF p
        );
        
        /// <summary>
        /// Computes the location of the specified client point into viewport
        /// coordinates.
        /// </summary>
        /// <param name="p">
        /// The client <see cref="Point"/> to convert.
        /// </param>
        /// <returns>
        /// A <see cref="Point"/> that represents the converted <see cref="Point"/>,
        /// <paramref name="p"/>, in viewport coordinates.
        /// </returns>
        Point PointToViewport(
            Point p
        );
        
        /// <summary>
        /// Computes the location of the specified client point into viewport
        /// coordinates.
        /// </summary>
        /// <param name="p">
        /// The client <see cref="PointF"/> to convert.
        /// </param>
        /// <returns>
        /// A <see cref="PointF"/> that represents the converted <see cref="PointF"/>,
        /// <paramref name="p"/>, in viewport coordinates.
        /// </returns>
        PointF PointToViewport(
            PointF p
        );
    }
}
