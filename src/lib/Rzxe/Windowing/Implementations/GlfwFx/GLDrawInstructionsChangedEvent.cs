/**
 * GLDrawInstructionsChangedEvent.cs - OpenGL Drawing Instructions Changed Event
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// GLD raw instructions changed event handler.
    /// </summary>
    /// <param name="sender">
    /// The source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="GLDrawInstructionsChangedEventArgs"/> object that contains the
    /// event data.
    /// </param>
    internal delegate void GLDrawInstructionsChangedEventHandler(
        object                             sender,
        GLDrawInstructionsChangedEventArgs e
    );
    
    /// <summary>
    /// Provides data for the <see cref="GLDrawInstructionsList.Changed"/> event.
    /// </summary>
    internal sealed class GLDrawInstructionsChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the kind of change that occurred.
        /// </summary>
        public GLDrawInstructionChange Change { get; private set; }
        
        /// <summary>
        /// Gets the instruction that was changed.
        /// </summary>
        public GLDrawInstruction Instruction { get; private set; }
        
        /// <summary>
        /// Gets the instruction that was replaced, if a replacement occurred.
        /// </summary>
        public GLDrawInstruction OldInstruction { get; private set; }
        
        
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="GLDrawInstructionsChangedEventArgs"/>
        /// </summary>
        /// <param name="change">
        /// The change that occurred.
        /// </param>
        public GLDrawInstructionsChangedEventArgs(
            GLDrawInstructionChange change
        )
        {
            Change = change;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="GLDrawInstructionsChangedEventArgs"/>
        /// </summary>
        /// <param name="change">
        /// The change that occurred.
        /// </param>
        /// <param name="instruction">
        /// The instruction that was changed.
        /// </param>
        public GLDrawInstructionsChangedEventArgs(
            GLDrawInstructionChange change,
            GLDrawInstruction       instruction
        ) : this(change)
        {
            Instruction = instruction;
        }
        
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="GLDrawInstructionsChangedEventArgs"/>
        /// </summary>
        /// <param name="oldInstruction">
        /// The instruction that was replaced.
        /// </param>
        /// <param name="instruction">
        /// The instruction that was the replacement.
        /// </param>
        public GLDrawInstructionsChangedEventArgs(
            GLDrawInstruction       oldInstruction,
            GLDrawInstruction       instruction
        ) : this(GLDrawInstructionChange.Changed, instruction)
        {
            OldInstruction = oldInstruction;
        }
    }
}
