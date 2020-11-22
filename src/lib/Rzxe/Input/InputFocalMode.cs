/**
 * InputFocalMode.cs - Input Focal Mode Enumeration
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

namespace Oddmatics.Rzxe.Input
{
    /// <summary>
    /// Specifies constants defining possible focal modes for game states when routing
    /// input updates.
    /// </summary>
    public enum InputFocalMode
    {
        /// <summary>
        /// Input should never be routed to the object.
        /// </summary>
        Never = 0,
        
        /// <summary>
        /// Input should be routed when the object is considered focused.
        /// </summary>
        WhenCurrentStateOnly = 20,
        
        /// <summary>
        /// Input should always be routed to the object regardless of focal state.
        /// </summary>
        Always = 100
    }
}
