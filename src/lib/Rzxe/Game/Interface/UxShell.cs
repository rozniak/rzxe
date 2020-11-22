﻿/**
 * UxShell.cs - User Interface Shell
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Input;
using Oddmatics.Rzxe.Logic;
using Oddmatics.Rzxe.Util.Collections;
using Oddmatics.Rzxe.Windowing.Graphics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Oddmatics.Rzxe.Game.Interface
{
    /// <summary>
    /// Represents an in-game user interface shell.
    /// </summary>
    public class UxShell
    {
        // TODO: Rethink how inputs are handled, use built in .NET types?
        //
        /// <summary>
        /// The mouse button controls.
        /// </summary>
        private static readonly string[] MouseButtons = new string[]
        {
            "mb.left",
            "mb.middle",
            "mb.right"
        };
        
        
        /// <summary>
        /// Gets or sets the components in the shell.
        /// </summary>
        public SortedList2<UxComponent> Components { get; set; }
        
        
        /// <summary>
        /// The map of mouse buttons to components that they were originally clicked
        /// on.
        /// </summary>
        private UxComponent[] ClickStartedComponent { get; set; }
        
        /// <summary>
        /// The component that the mouse is currently hovering over.
        /// </summary>
        private UxComponent HoveredComponent { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="UxShell"/> class.
        /// </summary>
        public UxShell()
        {
            Components = new SortedList2<UxComponent>(new ZIndexComparer());

            // Init array for ClickStartedComponent, an index for each
            // mouse button - left, middle, right
            //
            ClickStartedComponent = new UxComponent[3];
        }
        
        
        /// <summary>
        /// Handles a mouse input update for the shell.
        /// </summary>
        /// <param name="inputs">
        /// The new input state.
        /// </param>
        /// <returns>
        /// True if the mouse is hit tested on a component.
        /// </returns>
        public bool HandleMouseInputs(
            InputEvents inputs
        )
        {
            UxComponent component = MouseHitTest(inputs.MousePosition);

            if (component != null)
            {
                // Check - has the mouse entered a component?
                //
                if (HoveredComponent != component)
                {
                    if (HoveredComponent != null)
                    {
                        HoveredComponent.OnMouseLeave();
                    }

                    component.OnMouseEnter();
                }

                //
                // Check mouse button changes
                //
                for (byte i = 0; i < MouseButtons.Length; i++)
                {
                    var button = MouseButtons[i];

                    // Check - has button pressed state changed?
                    //
                    if (inputs.NewPresses.Contains(button))
                    {
                        ClickStartedComponent[i] = component;

                        component.OnMouseDown();
                    }
                    else if (inputs.NewReleases.Contains(button))
                    {
                        component.OnMouseUp();

                        // If the originally clicked component is the one
                        // currently hovered, then trigger a click event
                        //
                        if (ClickStartedComponent[i] == component)
                        {
                            component.OnClick();
                        }
                    }
                }

                HoveredComponent = component;

                return true;
            }
            else
            {
                // Check - has the mouse left a component?
                //
                if (HoveredComponent != null)
                {
                    HoveredComponent.OnMouseLeave();
                }

                HoveredComponent = null;
            }

            return false;
        }
        
        /// <summary>
        /// Renders the shell.
        /// </summary>
        /// <param name="graphics">
        /// The graphics interface for the renderer.
        /// </param>
        public void RenderFrame(
            IGraphicsController graphics
        )
        {
            // Render controls back-to-front
            //
            ISpriteAtlas atlas    = graphics.GetSpriteAtlas("shell");
            var          safeList = new List<UxComponent>(Components);
            ISpriteBatch sb       = graphics.CreateSpriteBatch(atlas);

            foreach (UxComponent component in safeList)
            {
                component.Render(sb);
            }
            
            sb.Finish();
        }
        
        
        /// <summary>
        /// Performs the hit test for the mouse on components inside the shell.
        /// </summary>
        /// <param name="mousePos">
        /// The position of the mouse.
        /// </param>
        /// <returns>
        /// The top-most component that is hit tested by the mouse, null otherwise.
        /// </returns>
        private UxComponent MouseHitTest(
            PointF mousePos
        )
        {
            IEnumerable<UxComponent> components = Components.Reverse();

            foreach (UxComponent component in components)
            {
                if (Collision.PointInRect(mousePos, component.Bounds))
                {
                    return component;
                }
            }

            return null;
        }
    }
}
