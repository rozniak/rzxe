﻿/**
 * UxComponent.cs - User Interface Component
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Extensions;
using Oddmatics.Rzxe.Input;
using Oddmatics.Rzxe.Windowing.Graphics;
using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Game.Interface
{
    /// <summary>
    /// Represents an in-game user interface component.
    /// </summary>
    public abstract class UxComponent : IDisposable
    {
        /// <summary>
        /// Gets the bounds of the component.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(Location, Size);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the component is enabled.
        /// </summary>
        public virtual bool Enabled { get; set; }
        
        /// <summary>
        /// Gets or sets the location of the component.
        /// </summary>
        public virtual Point Location { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        public virtual string Name { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether the component is selectable.
        /// </summary>
        public virtual bool Selectable { get; }
        
        /// <summary>
        /// Gets or sets the size of the component.
        /// </summary>
        public virtual Size Size { get; set; }
        
        /// <summary>
        /// Gets or sets the z-index of the component.
        /// </summary>
        public int ZIndex { get; set; }
        
        
        /// <summary>
        /// The value that indicates whether the component has been disposed or is
        /// currently being disposed.
        /// </summary>
        protected bool Disposing { get; set; }


        /// <summary>
        /// Handles a mouse click on the component..
        /// </summary>
        /// <param name="mouseButton">
        /// The mouse button that was clicked.
        /// </param>
        /// <param name="mouseLocation">
        /// The location of the mouse during the click.
        /// </param>
        public virtual void OnClick(
            ControlInput mouseButton,
            Point        mouseLocation
        ) { }
        
        /// <summary>
        /// Handles a mouse button being pressed on the component.
        /// </summary>
        /// <param name="mouseButton">
        /// The mouse button that was pressed.
        /// </param>
        /// <param name="mouseLocation">
        /// The location of the mouse during the press.
        /// </param>
        public virtual void OnMouseDown(
            ControlInput mouseButton,
            Point        mouseLocation
        ) { }
        
        /// <summary>
        /// Handles the mouse entering the bounds of the component.
        /// </summary>
        public virtual void OnMouseEnter() { }
        
        /// <summary>
        /// Handles the mouse leaving the bounds of the component.
        /// </summary>
        public virtual void OnMouseLeave() { }
        
        /// <summary>
        /// Handles the mouse moving within the bounds of the component.
        /// </summary>
        /// <param name="mouseLocation">
        /// The location of the mouse.
        /// </param>
        public virtual void OnMouseMove(
            Point mouseLocation
        ) { }

        /// <summary>
        /// Handles a mouse button being released from the component.
        /// </summary>
        /// <param name="mouseButton">
        /// The mouse button that was pressed.
        /// </param>
        /// <param name="mouseLocation">
        /// The location of the mouse during the release.
        /// </param>
        public virtual void OnMouseUp(
            ControlInput mouseButton,
            Point        mouseLocation
        ) { }
        
        
        /// <inheritdoc />
        public abstract void Dispose();
        
        /// <summary>
        /// Computes the location of the specified viewport point into client
        /// coordinates.
        /// </summary>
        /// <param name="p">
        /// The viewport coordinate <see cref="Point"/> to convert.
        /// </param>
        /// <returns>
        /// A <see cref="Point"/> that represents the converted <see cref="Point"/>
        /// <paramref name="p"/>, in client coordinates.
        /// </returns>
        public Point PointToClient(
            Point p
        )
        {
            return p.Subtract(Location);
        }
        
        /// <summary>
        /// Renders the component.
        /// </summary>
        /// <param name="sb">
        /// The sprite batch.
        /// </param>
        public virtual void Render(
            ISpriteBatch sb
        ) { }


        /// <summary>
        /// Asserts that the component is not disposed.
        /// </summary>
        protected void AssertNotDisposed()
        {
            if (Disposing)
            {
                throw new ObjectDisposedException(Name);
            }
        }
    }
}
