/**
 * GLDrawInstructionsList.cs - OpenGL Drawing Instructions List
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using Oddmatics.Rzxe.Windowing.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// Represents a collection of drawing instructions for the OpenGL renderer.
    /// </summary>
    internal sealed class GLDrawInstructionsList : IList<IDrawInstruction>
    {
        /// <inheritdoc />
        public int Count { get { return BackingList.Count; } }
        
        /// <inheritdoc />
        public bool IsReadOnly { get; private set; }
        
        
        /// <summary>
        /// The backing <see cref="List{T}"/> that the collection wraps.
        /// </summary>
        private List<GLDrawInstruction> BackingList { get; set; }
        
        
        /// <summary>
        /// Occurs when the list of instructions has changed, or when an instruction
        /// within the list has changed.
        /// </summary>
        public event GLDrawInstructionsChangedEventHandler Changed;
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GLDrawInstructionsList"/>
        /// class.
        /// </summary>
        public GLDrawInstructionsList()
        {
            BackingList = new List<GLDrawInstruction>();
            IsReadOnly  = false;
        }
        
        
        /// <inheritdoc />
        public IDrawInstruction this[int index]
        {
            get
            {
                return BackingList[index];
            }

            set
            {
                AssertNotReadOnly();
                AssertNotDuplicate(value);

                var oldInstruction = BackingList[index];
                var instruction    = (GLDrawInstruction) value;
                
                BackingList[index] = instruction;
                oldInstruction.Invalidated    -= Instruction_Invalidated;
                oldInstruction.InvalidatedBig -= Instruction_InvalidatedBig;
                instruction.Invalidated       += Instruction_Invalidated;
                instruction.InvalidatedBig    += Instruction_InvalidatedBig;
                
                Changed?.Invoke(
                    this,
                    new GLDrawInstructionsChangedEventArgs(
                        oldInstruction,
                        instruction
                    )
                );
            }
        }

        
        /// <inheritdoc />
        public void Add(
            IDrawInstruction item
        )
        {
            AssertNotReadOnly();
            AssertNotDuplicate(item);

            var instruction = (GLDrawInstruction) item;

            BackingList.Add(instruction);
            instruction.Invalidated    += Instruction_Invalidated;
            instruction.InvalidatedBig += Instruction_InvalidatedBig;

            Changed?.Invoke(
                this,
                new GLDrawInstructionsChangedEventArgs(
                    GLDrawInstructionChange.Added,
                    instruction
                )
            );
        }


        /// <inheritdoc />
        public void Clear()
        {
            AssertNotReadOnly();
            
            foreach (GLDrawInstruction instruction in BackingList)
            {
                instruction.Invalidated    -= Instruction_Invalidated;
                instruction.InvalidatedBig -= Instruction_InvalidatedBig;
            }

            BackingList.Clear();

            Changed?.Invoke(
                this,
                new GLDrawInstructionsChangedEventArgs(
                    GLDrawInstructionChange.Cleared
                )
            );
        }
        
        /// <inheritdoc />
        public bool Contains(
            IDrawInstruction item
        )
        {
            return BackingList.Contains(
                (GLDrawInstruction) item
            );
        }
        
        /// <inheritdoc />
        public void CopyTo(
            IDrawInstruction[] array,
            int                arrayIndex
        )
        {
            Array.Copy(
                BackingList.Cast<IDrawInstruction>().ToArray(),
                0,
                array,
                arrayIndex,
                BackingList.Count
            );
        }
        
        /// <inheritdoc />
        public IEnumerator<IDrawInstruction> GetEnumerator()
        {
            return BackingList.Cast<IDrawInstruction>().GetEnumerator();
        }
        
        /// <inheritdoc />
        public int IndexOf(
            IDrawInstruction item
        )
        {
            return BackingList.IndexOf((GLDrawInstruction) item);
        }
        
        /// <inheritdoc />
        public void Insert(
            int              index,
            IDrawInstruction item
        )
        {
            AssertNotReadOnly();
            AssertNotDuplicate(item);

            var instruction = (GLDrawInstruction) item;

            BackingList.Insert(index, instruction);
            instruction.Invalidated    += Instruction_Invalidated;
            instruction.InvalidatedBig += Instruction_InvalidatedBig;

            Changed?.Invoke(
                this,
                new GLDrawInstructionsChangedEventArgs(
                    GLDrawInstructionChange.Added,
                    instruction
                )
            );
        }
        
        /// <summary>
        /// Locks the list from further editing.
        /// </summary>
        public void Lock()
        {
            IsReadOnly = true;
        }

        /// <inheritdoc />
        public bool Remove(
            IDrawInstruction item
        )
        {
            AssertNotReadOnly();

            var  instruction = (GLDrawInstruction) item;
            bool ret         = BackingList.Remove(instruction);
            
            if (ret)
            {
                instruction.Invalidated    -= Instruction_Invalidated;
                instruction.InvalidatedBig -= Instruction_InvalidatedBig;
                
                Changed?.Invoke(
                    this,
                    new GLDrawInstructionsChangedEventArgs(
                        GLDrawInstructionChange.Removed,
                        instruction
                    )
                );
            }

            return ret;
        }
        
        /// <inheritdoc />
        public void RemoveAt(
            int index
        )
        {
            AssertNotReadOnly();

            var instruction = BackingList[index];

            BackingList.RemoveAt(index);
            instruction.Invalidated    -= Instruction_Invalidated;
            instruction.InvalidatedBig -= Instruction_InvalidatedBig;

            Changed?.Invoke(
                this,
                new GLDrawInstructionsChangedEventArgs(
                    GLDrawInstructionChange.Removed,
                    instruction
                )
            );
        }
        
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        
        /// <summary>
        /// Asserts that the item does not already exist in the list.
        /// </summary>
        /// <param name="instruction">
        /// The instruction.
        /// </param>
        private void AssertNotDuplicate(
            IDrawInstruction instruction
        )
        {
            if (Contains(instruction))
            {
                throw new ArgumentException(
                    "The instruction already exists in the list."
                );
            }
        }

        /// <summary>
        /// Asserts that the list is not read-only.
        /// </summary>
        private void AssertNotReadOnly()
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException(
                    "The draw instruction collection is read-only."
                );
            }
        }
        
        
        /// <summary>
        /// (Event) Handles an instruction in the list being invalidated.
        /// </summary>
        private void Instruction_Invalidated(
            object    sender,
            EventArgs e
        )
        {
            Changed?.Invoke(
                this,
                new GLDrawInstructionsChangedEventArgs(
                    GLDrawInstructionChange.Changed,
                    (GLDrawInstruction) sender
                )
            );
        }
        
        /// <summary>
        /// (Event) Handles an instruction in the list being invalidated.
        /// </summary>
        private void Instruction_InvalidatedBig(
            object    sender,
            EventArgs e
        )
        {
            Changed?.Invoke(
                this,
                new GLDrawInstructionsChangedEventArgs(
                    GLDrawInstructionChange.BigChange,
                    (GLDrawInstruction) sender
                )
            );
        }
    }
}
