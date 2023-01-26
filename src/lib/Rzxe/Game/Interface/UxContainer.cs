/**
 * UxContainer.cs - User Interface Component Container
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Extensions;
using Oddmatics.Rzxe.Input;
using Oddmatics.Rzxe.Logic;
using Oddmatics.Rzxe.Util.Collections;
using Oddmatics.Rzxe.Windowing.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Oddmatics.Rzxe.Game.Interface
{
    /// <summary>
    /// Represents an in-game user interface component container.
    /// </summary>
    public class UxContainer : UxComponent
    {
        /// <summary>
        /// The mouse buttons to process.
        /// </summary>
        protected static readonly ControlInput[] MouseButtons = new ControlInput[]
        {
            ControlInput.MouseButtonLeft,
            ControlInput.MouseButtonMiddle,
            ControlInput.MouseButtonRight
        };


        /// <summary>
        /// Gets or sets the components in the container.
        /// </summary>
        public ExCollection<UxComponent> Components { get; set; }
        
        
        /// <summary>
        /// The map of mouse buttons to components that they were originally clicked on.
        /// </summary>
        protected UxComponent[] ClickStartedComponent { get; set; }
        
        /// <summary>
        /// The component that the mouse is currently hovering over.
        /// </summary>
        protected UxComponent HoveredComponent { get; set; }
        
        /// <summary>
        /// The sprite batch proxies provided to each component.
        /// </summary>
        protected Dictionary<UxComponent, SpriteBatchProxy> SpriteBatchProxies
        {
            get;
            set;
        }
        
        /// <summary>
        /// The container that has stolen input handling.
        /// </summary>
        protected UxContainer StolenInputContainer { get; set; }
        
        /// <summary>
        /// The target sprite batch.
        /// </summary>
        protected ISpriteBatch TargetSpriteBatch { get; set; }


        /// <summary>
        /// The comparer used to sort components within the container.
        /// </summary>
        private IComparer<UxComponent> ComponentComparer { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="UxContainer"/> class.
        /// </summary>
        protected UxContainer()
        {
            Components = new ExCollection<UxComponent>();
            ComponentComparer = new ZIndexComparer();
            
            Components.ItemAdded += Components_ItemAdded;
            Components.ItemAdding += Components_ItemAdding;
            Components.ItemRemoved += Components_ItemRemoved;
            Components.ItemRemoving += Components_ItemRemoving;

            SpriteBatchProxies = new Dictionary<UxComponent, SpriteBatchProxy>();

            // Init array for ClickStartedComponent, an index for each mouse button -
            // left, middle, right
            //
            ClickStartedComponent = new UxComponent[3];
        }


        /// <inheritdoc />
        public override void Dispose()
        {
            base.Dispose();

            Disposing = true;
            
            foreach (UxComponent component in Components)
            {
                component.Dispose();
            }

            TargetSpriteBatch?.Dispose();
        }
        
        /// <summary>
        /// Releases input routing after having previously taken over control.
        /// </summary>
        /// <param name="container">
        /// The container releasing input routing.
        /// </param>
        public void ReleaseInputRouting(
            UxContainer container
        )
        {
            if (StolenInputContainer == null || StolenInputContainer != container)
            {
                throw new InvalidOperationException(
                    "Container attempted to release input routing that it didn't have."
                );
            }

            StolenInputContainer = null;
        }

        /// <inheritdoc />
        public override void Render(
            ISpriteBatch sb
        )
        {
            if (TargetSpriteBatch == null)
            {
                TargetSpriteBatch = sb;
            }
            
            RenderContainer(sb);
        }
        
        /// <summary>
        /// Steals input routing so that it is handled by the specified container.
        /// </summary>
        /// <param name="container">
        /// The container that should become the input handler.
        /// </param>
        public void StealInputRouting(
            UxContainer container
        )
        {
            if (StolenInputContainer != null)
            {
                throw new InvalidOperationException(
                    "Attempt to steal input routing when it's already been stolen."
                );
            }
            
            if (!Components.Contains(container))
            {
                throw new ArgumentException(
                    "Attempt to steal input routing by a container that is not a child."
                );
            }

            StolenInputContainer = container;
        }
        
        /// <summary>
        /// Updates the state of the container and its components with the latest inputs
        /// from the game engine.
        /// </summary>
        /// <param name="deltaTime">
        /// The time difference since the last update.
        /// </param>
        /// <param name="inputs">
        /// The latest state of inputs.
        /// </param>
        public virtual void Update(
            TimeSpan    deltaTime,
            InputEvents inputs
        )
        {
            //
            // TODO: Loads of stuff:
            //         - No 'focused' control
            //         - No keyboard support (for above)
            //         - No gamepad support (ditto)
            //         - Probably other things as well
            //
            
            if (inputs == null)
            {
                return;
            }

            // Hand off input handling if a child container has stolen it
            //
            if (StolenInputContainer != null)
            {
                StolenInputContainer.Update(deltaTime, inputs);
                return;
            }
            
            // Our input handling begins here
            //
            HandleMouseInputs(inputs);
        }


        /// <summary>
        /// Renders the contents of the container
        /// </summary>
        /// <param name="sb">
        /// The sprite batch.
        /// </param>
        protected void RenderContainer(
            ISpriteBatch sb
        )
        {
            // Render controls back-to-front
            //
            foreach (UxComponent component in Components)
            {
                // Check if the control has a sprite batch proxy
                //
                if (!SpriteBatchProxies.ContainsKey(component))
                {
                    SpriteBatchProxies.Add(
                        component,
                        new SpriteBatchProxy(TargetSpriteBatch)
                    );
                }
                
                // Render the component!
                //
                component.Render(SpriteBatchProxies[component]);
            }

            sb.Finish();
        }
        
        
        /// <summary>
        /// Handles mouse input updates.
        /// </summary>
        /// <param name="inputs">
        /// The new input state.
        /// </param>
        /// <returns>
        /// True if the mouse is hit tested on a component.
        /// </returns>
        private bool HandleMouseInputs(
            InputEvents inputs
        )
        {
            UxComponent component = MouseHitTest(inputs.MousePosition);

            if (component != null)
            {
                bool enteredComponent = false;
                
                // Check - has the mouse entered a component?
                //
                if (HoveredComponent != component)
                {
                    if (HoveredComponent != null)
                    {
                        HoveredComponent.OnMouseLeave();
                    }

                    component.OnMouseEnter();
                    component.OnMouseMove(inputs.MousePosition.ToPoint());

                    enteredComponent = true;
                }

                // Check - has mouse moved within a component?
                //   (don't bother if we've already sent a mouse move during entry!)
                //
                if (
                    !enteredComponent &&
                    inputs.MouseMovement != PointF.Empty
                )
                {
                    component.OnMouseMove(inputs.MousePosition.ToPoint());
                }

                //
                // Check mouse button changes
                //
                for (byte i = 0; i < MouseButtons.Length; i++)
                {
                    ControlInput button = MouseButtons[i];

                    // Check - has button pressed state changed?
                    //
                    if (inputs.NewPresses.Contains(button))
                    {
                        ClickStartedComponent[i] = component;

                        component.OnMouseDown(
                            button,
                            inputs.MousePosition.ToPoint()
                        );
                    }
                    else if (inputs.NewReleases.Contains(button))
                    {
                        component.OnMouseUp(
                            button,
                            inputs.MousePosition.ToPoint()
                        );

                        // If the originally clicked component is the one
                        // currently hovered, then trigger a click event
                        //
                        if (ClickStartedComponent[i] == component)
                        {
                            component.OnClick(
                                button,
                                inputs.MousePosition.ToPoint()
                            );
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
        /// Performs the hit test for the mouse on components inside the container.
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
        
        
        /// <summary>
        /// (Event) Handles when a component has been added to the container.
        /// </summary>
        private void Components_ItemAdded(
            object sender,
            ItemMembershipChangedEventArgs<UxComponent> e
        )
        {
            e.Item.Owner = this;
        }


        /// <summary>
        /// (Event) Handles when a component is being added to the container.
        /// </summary>
        private void Components_ItemAdding(
            object                           sender,
            ItemAddingEventArgs<UxComponent> e
        )
        {
            // TODO: Improve this (currently O(n))
            //
            int total = Components.Count;
            
            for (int i = 0; i < total; i++)
            {
                if (ComponentComparer.Compare(e.Item, Components[i]) < 0)
                {
                    e.InsertAtIndex = i;
                    return;
                }
            }

            e.InsertAtIndex = total;
        }
        
        /// <summary>
        /// (Event) Handles when a component has been removed from the container.
        /// </summary>
        private void Components_ItemRemoved(
            object                                      sender,
            ItemMembershipChangedEventArgs<UxComponent> e
        )
        {
            if (SpriteBatchProxies.ContainsKey(e.Item))
            {
                SpriteBatchProxies.Remove(e.Item);
            }
        }
        
        /// <summary>
        /// (Event) Handles when a component is being removed from the container.
        /// </summary>
        private void Components_ItemRemoving(
            object                             sender,
            ItemRemovingEventArgs<UxComponent> e
        )
        {
            e.Item.Owner = null;
            
            if (StolenInputContainer != null)
            {
                var container = e.Item as UxContainer;
                
                if (container != null && StolenInputContainer == container)
                {
                    throw new InvalidOperationException(
                        "Attempt to remove container that has stolen input routing."
                    );
                }
            }
        }
    }
}
