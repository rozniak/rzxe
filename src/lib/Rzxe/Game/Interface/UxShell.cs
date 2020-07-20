using Oddmatics.Rzxe.Input;
using Oddmatics.Rzxe.Logic;
using Oddmatics.Rzxe.Util.Collections;
using Oddmatics.Rzxe.Windowing.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oddmatics.Rzxe.Game.Interface
{
    public class UxShell
    {
        // TODO: Rethink how inputs are handled, use built in .NET types?
        //
        private static readonly string[] MouseButtons = new string[]
        {
            "mb.left",
            "mb.middle",
            "mb.right"
        };


        public SortedList2<UxComponent> Components { get; set; }


        private UxComponent[] ClickStartedComponent { get; set; }

        private UxComponent HoveredComponent { get; set; }


        public UxShell()
        {
            Components = new SortedList2<UxComponent>(new ZIndexComparer());

            // Init array for ClickStartedComponent, an index for each
            // mouse button - left, middle, right
            //
            ClickStartedComponent = new UxComponent[3];
        }


        public bool HandleMouseInputs(InputEvents inputs)
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

        public void RenderFrame(IGraphicsController graphics)
        {
            // Render controls back-to-front
            //
            var safeList = new List<UxComponent>(Components);
            var sb       = graphics.CreateSpriteBatch(
                               "shell-atlas"
                           );

            foreach (UxComponent component in safeList)
            {
                component.Render(sb);
            }
            
            sb.Finish();
        }


        private UxComponent MouseHitTest(PointF mousePos)
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
