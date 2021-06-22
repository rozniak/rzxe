/**
 * MouseInputEvent.cs - Mouse Input Event
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Input;
using System;
using System.Drawing;

namespace Oddmatics.Rzxe.Game.Interface
{
    /// <summary>
    /// Represents the method that will handle mouse events in the shell.
    /// </summary>
    /// <param name="sender">
    /// The source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="MouseInputEventArgs"/> that contains the event data.
    /// </param>
    public delegate void MouseInputEventHandler(
        object              sender,
        MouseInputEventArgs e
    );
    
    /// <summary>
    /// Mouse input event arguments.
    /// </summary>
    public class MouseInputEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the mouse button that was involved in the event.
        /// </summary>
        public ControlInput Button { get; private set; }
        
        /// <summary>
        /// Gets the location of the mouse during the event.
        /// </summary>
        public Point Location { get; private set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseInputEventArgs"/> class.
        /// </summary>
        /// <param name="button">
        /// The mouse button that was involved in the event.
        /// </param>
        /// <param name="location">
        /// The location of the mouse during the event.
        /// </param>
        public MouseInputEventArgs(
            ControlInput button,
            Point        location
        )
        {
            Button   = button;
            Location = location;
        }
    }
}
