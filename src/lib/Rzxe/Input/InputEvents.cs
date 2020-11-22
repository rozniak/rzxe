/**
 * InputEvents.cs - Game Engine Input Events
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Oddmatics.Rzxe.Input
{
    /// <summary>
    /// Represents input events read from the window manager.
    /// </summary>
    public sealed class InputEvents
    {
        /// <summary>
        /// Gets character input in the state.
        /// </summary>
        public char ConsoleInput { get; private set; }
        
        /// <summary>
        /// Gets the pressed inputs in the state.
        /// </summary>
        public IList<string> DownedInputs { get; private set; }
        
        /// <summary>
        /// Gets a value indicating whether the state is read-only.
        /// </summary>
        public bool IsReadOnly { get; private set; }
        
        /// <summary>
        /// Gets the mouse position in the state.
        /// </summary>
        public PointF MousePosition { get; private set; }
        
        /// <summary>
        /// Gets the inputs that were pressed in the state.
        /// </summary>
        public IList<string> NewPresses { get; private set; }
        
        /// <summary>
        /// Gets the inputs that were released in the state.
        /// </summary>
        public IList<string> NewReleases { get; private set; }

        
        /// <summary>
        /// The actively pressed inputs.
        /// </summary>
        private List<string> ActiveDownedInputs { get; set; }
        
        /// <summary>
        /// The previously pressed inputs.
        /// </summary>
        private IList<string> LastDownedInputs { get; set; }

        
        /// <summary>
        /// Initializes a new instance of the <see cref="InputEvents"/> class.
        /// </summary>
        public InputEvents()
        {
            ActiveDownedInputs = new List<string>();
            ConsoleInput       = char.MinValue;
            DownedInputs       = new List<string>().AsReadOnly();
            IsReadOnly         = false;
            LastDownedInputs   = null;
            MousePosition      = PointF.Empty;
            NewPresses         = new List<string>().AsReadOnly();
            NewReleases        = new List<string>().AsReadOnly();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="InputEvents"/> class with
        /// previous state data.
        /// </summary>
        /// <param name="lastDownedInputs">
        /// The previously pressed inputs.
        /// </param>
        /// <param name="lastMousePosition">
        /// The previous mouse position.
        /// </param>
        public InputEvents(
            IList<string> lastDownedInputs,
            PointF        lastMousePosition
        ) : this()
        {
            ActiveDownedInputs = new List<string>(lastDownedInputs);
            LastDownedInputs   = lastDownedInputs;
            MousePosition      = lastMousePosition;
        }

        
        /// <summary>
        /// Finalizes the class for reporting to the game engine.
        /// </summary>
        public void FinalizeForReporting()
        {
            AssertNotReadOnly();

            // Set up downed inputs
            //
            DownedInputs = new List<string>(ActiveDownedInputs).AsReadOnly();

            // Set up new presses
            //
            IEnumerable<string> newPresses =
                LastDownedInputs == null ?
                    ActiveDownedInputs :
                    ActiveDownedInputs.Except(LastDownedInputs);

            NewPresses = new List<string>(newPresses).AsReadOnly();

            // Set up new releases
            //
            IEnumerable<string> newReleases =
                LastDownedInputs == null ?
                    new List<string>() :
                    LastDownedInputs.Except(ActiveDownedInputs);

            NewReleases = new List<string>(newReleases).AsReadOnly();
            
            IsReadOnly = true;
        }
        
        /// <summary>
        /// Reports console input.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        public void ReportConsoleInput(
            char input
        )
        {
            AssertNotReadOnly();

            ConsoleInput = input;
        }
        
        /// <summary>
        /// Reports mouse movement.
        /// </summary>
        /// <param name="x">
        /// The x-coordinate of the mouse
        /// </param>
        /// <param name="y">
        /// The y-coordinate of the mouse.
        /// </param>
        public void ReportMouseMovement(
            float x,
            float y
        )
        {
            AssertNotReadOnly();

            MousePosition = new PointF(x, y);
        }
        
        /// <summary>
        /// Reports an input press.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        public void ReportPress(
            string input
        )
        {
            AssertNotReadOnly();

            if (!ActiveDownedInputs.Contains(input))
            {
                ActiveDownedInputs.Add(input);
            }
        }
        
        /// <summary>
        /// Reports an input release.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        public void ReportRelease(
            string input
        )
        {
            AssertNotReadOnly();

            if (ActiveDownedInputs.Contains(input))
            {
                ActiveDownedInputs.Remove(input);
            }
        }
        
        
        /// <summary>
        /// Asserts that the class is not read-only.
        /// </summary>
        private void AssertNotReadOnly()
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException(
                    "This input update state is currently read-only and cannot be " +
                    "modified."
                );
            }
        }
    }
}
