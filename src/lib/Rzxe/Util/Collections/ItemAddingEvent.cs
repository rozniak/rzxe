/**
 * ItemAddingEvent.cs - Adding Item to Collection Event
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
    /// <see cref="ExCollection{T}.ItemAdding"/> event.
    /// </summary>
    /// <param name="sender">
    /// The source of the event.
    /// </param>
    /// <param name="e">
    /// An <see cref="ItemAddingEventArgs{T}"/> object that contains event data.
    /// </param>
    public delegate void ItemAddingEventHandler<T>(
        object                 sender,
        ItemAddingEventArgs<T> e
    );
    
    
    /// <summary>
    /// Provides data for the <see cref="ExCollection{T}.ItemAdding"/> event.
    /// </summary>
    public sealed class ItemAddingEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether to cancel the operation.
        /// </summary>
        public bool Cancel { get; set; }
        
        /// <summary>
        /// Gets or sets the index at which to insert the item, -1 if the item should be
        /// added as per the default operation.
        /// </summary>
        public int InsertAtIndex { get; set; }
        
        /// <summary>
        /// Gets the item being added to the collection.
        /// </summary>
        public T Item { get; private set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemAddingEventArgs{T}"/>
        /// class.
        /// </summary>
        /// <param name="item">
        /// The item being added to the collection.
        /// </param>
        public ItemAddingEventArgs(
            T item
        )
        {
            InsertAtIndex = -1;
            Item          = item;
        }
    }
}
