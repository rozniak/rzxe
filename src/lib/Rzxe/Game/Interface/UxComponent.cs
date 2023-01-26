/**
 * UxComponent.cs - User Interface Component
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Extensions;
using Oddmatics.Rzxe.Input;
using Oddmatics.Rzxe.Util;
using Oddmatics.Rzxe.Windowing.Graphics;
using System.Drawing;

namespace Oddmatics.Rzxe.Game.Interface
{
    /// <summary>
    /// Represents an in-game user interface component.
    /// </summary>
    public abstract class UxComponent : DisposableBase
    {
        /// <summary>
        /// Gets the bounds of the component.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(ActualLocation, Size);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the component is enabled.
        /// </summary>
        public virtual bool Enabled { get; set; }
        
        /// <summary>
        /// Gets or sets the location of the component.
        /// </summary>
        public virtual Point Location
        {
            get { return _Location; }
            set
            {
                _Location = value;
                Invalidate();
            }
        }
        protected Point _Location;

        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        public virtual string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the owner of the component.
        /// </summary>
        public virtual UxContainer Owner
        {
            get { return _Owner; }
            set
            {
                _Owner = value;
                Invalidate();
            }
        }
        protected UxContainer _Owner;

        /// <summary>
        /// Gets a value indicating whether the component is selectable.
        /// </summary>
        public virtual bool Selectable { get; }
        
        /// <summary>
        /// Gets or sets the size of the component.
        /// </summary>
        public virtual Size Size
        {
            get { return _Size; }
            set
            {
                _Size = value;
                Invalidate();
            }
        }
        protected Size _Size;

        /// <summary>
        /// Gets or sets the z-index of the component.
        /// </summary>
        public int ZIndex { get; set; }
        
        
        /// <summary>
        /// Gets the location of the component, taking into account if the component
        /// resides within a container.
        /// </summary>
        protected Point ActualLocation
        {
            get
            {
                if (Owner == null)
                {
                    return Location;
                }
                
                return Owner.Location.Add(Location);
            }
        }
        
        /// <summary>
        /// The value that indicates whether the state of the component is dirty and the
        /// draw instructions must be updated on the next render call.
        /// </summary>
        protected bool Dirty { get; set; }
        
        
        /// <summary>
        /// Invalidates the rendered state of the control, drawing should be updated on
        /// the next render call.
        /// </summary>
        public void Invalidate()
        {
            Dirty = true;
        }

        /// <summary>
        /// Handles a mouse click on the component.
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
    }
}
