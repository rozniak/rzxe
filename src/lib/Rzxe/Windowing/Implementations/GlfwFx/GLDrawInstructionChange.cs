/**
 * GLDrawInstructionsChange.cs - OpenGL Drawing Instructions Change Enumeration
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

namespace Oddmatics.Rzxe.Windowing.Implementations.GlfwFx
{
    /// <summary>
    /// Specifies constants defining the type of change that has taken place on a
    /// draw instruction list.
    /// </summary>
    internal enum GLDrawInstructionChange
    {
        /// <summary>
        /// An instruction has been added to the list.
        /// </summary>
        Added,
        
        /// <summary>
        /// An instruction has been changed in the list.
        /// </summary>
        Changed,
        
        /// <summary>
        /// All instructions have been cleared in the list.
        /// </summary>
        Cleared,
        
        /// <summary>
        /// An instruction has been removed from the list.
        /// </summary>
        Removed
    }
}
