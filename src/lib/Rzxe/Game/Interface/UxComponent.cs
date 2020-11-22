/**
 * UxComponent.cs - User Interface Component
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using System.Drawing;

namespace Oddmatics.Rzxe.Game.Interface
{
    /// <summary>
    /// Represents an in-game user interface component.
    /// </summary>
    public abstract class UxComponent
    {
        /// <summary>
        /// Gets or sets the bounds of the component.
        /// </summary>
        public Rectangle Bounds { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the component is enabled.
        /// </summary>
        public bool Enabled { get; set; }
        
        /// <summary>
        /// Gets or sets the location of the component.
        /// </summary>
        public Point Location { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether the component is selectable.
        /// </summary>
        public bool Selectable { get; }
        
        /// <summary>
        /// Gets or sets the size of the component.
        /// </summary>
        public Size Size { get; set; }
        
        /// <summary>
        /// Gets or sets the z-index of the component.
        /// </summary>
        public int ZIndex { get; set; }
        
        
        /// <summary>
        /// Handles a mouse click on the component..
        /// </summary>
        public virtual void OnClick() { }
        
        /// <summary>
        /// Handles a mouse button being pressed on the component.
        /// </summary>
        public virtual void OnMouseDown() { }
        
        /// <summary>
        /// Handles the mouse entering the bounds of the component.
        /// </summary>
        public virtual void OnMouseEnter() { }
        
        /// <summary>
        /// Handles the mouse leaving the bounds of the component.
        /// </summary>
        public virtual void OnMouseLeave() { }
        
        /// <summary>
        /// Handles a mouse button being released from the component.
        /// </summary>
        public virtual void OnMouseUp() { }
        
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
