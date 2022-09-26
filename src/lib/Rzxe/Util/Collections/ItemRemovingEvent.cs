/**
 * ItemRemovingEvent.cs - Removing Item from Collection Event
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System;

namespace Oddmatics.Rzxe.Util.Collections
{
    /// <summary>
    /// Represents the method that will handle the
    /// <see cref="ExCollection{T}.ItemRemoving"/> event.
    /// </summary>
    /// <param name="sender">
    /// The source of the event.
    /// </param>
    /// <param name="e">
    /// An <see cref="ItemRemovingEventArgs{T}"/> object that contains event data.
    /// </param>
    public delegate void ItemRemovingEventHandler<T>(
        object                   sender,
        ItemRemovingEventArgs<T> e
    );
    
    
    /// <summary>
    /// Provides data for the <see cref="ExCollection{T}.ItemRemoving"/> event.
    /// </summary>
    public class ItemRemovingEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether to cancel the operation.
        /// </summary>
        public bool Cancel { get; set; }
        
        /// <summary>
        /// Gets the item being removed from the collection.
        /// </summary>
        public T Item { get; private set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemRemovingEventArgs{T}"/>
        /// class.
        /// </summary>
        /// <param name="item">
        /// The item being removed from the collection.
        /// </param>
        public ItemRemovingEventArgs(
            T item
        )
        {
            Item = item;
        }
    }
}
