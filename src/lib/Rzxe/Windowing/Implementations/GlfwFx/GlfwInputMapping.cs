/**
 * GlfwInputMapping.cs - GLFW Input Mapping Helper
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Pencil.Gaming;
using Oddmatics.Rzxe.Input;
using System;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// Helper class for mapping GLFW inputs to <see cref="ControlInput"/> enum
    /// values.
    /// </summary>
    internal static class GlfwInputMapping
    {
        /// <summary>
        /// Gets the mapping for a GLFW joystick input to a <see cref="ControlInput"/>
        /// enum value.
        /// </summary>
        /// <param name="joystickButton">
        /// </param>
        /// <returns>
        /// The <see cref="ControlInput"/> value that corresponds to the joystick
        /// button.
        /// </returns>
        public static ControlInput GetMapping(
            Joystick joystickButton
        )
        {
            // Joystick values are offset by 512
            //
            int offset = (int) joystickButton | 0x0200;

            return (ControlInput) offset;
        }
        
        /// <summary>
        /// Gets the mapping for a GLFW key input to a <see cref="ControlInput"/> enum
        /// value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="ControlInput"/> value that corresponds to the key.
        /// </returns>
        public static ControlInput GetMapping(
            Key key
        )
        {
            // Keys are one-to-one at the moment
            //
            return (ControlInput) key;
        }
        
        /// <summary>
        /// Gets the mapping for a GLFW mouse button to a <see cref="ControlInput"/>
        /// enum value.
        /// </summary>
        /// <param name="mouseButton">
        /// The mouse button.
        /// </param>
        /// <returns>
        /// The <see cref="ControlInput"/> value that corresponds to the mouse
        /// button.
        /// </returns>
        public static ControlInput GetMapping(
            MouseButton mouseButton
        )
        {
            // Mouse buttons are one-to-one at the moment
            //
            return (ControlInput) mouseButton;
        }
    }
}
