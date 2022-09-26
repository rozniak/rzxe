/**
 * ItemMembershipChangedEvent.cs - Item Membership to Collection Changed Event
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
    /// <see cref="ExCollection{T}.ItemAdded"/> and
    /// <see cref="ExCollection{T}.ItemRemoved"/> events.
    /// </summary>
    /// <param name="sender">
    /// The source of the event.
    /// </param>
    /// <param name="e">
    /// An <see cref="ItemMembershipChangedEventArgs{T}"/> object that contains event
    /// data.
    /// </param>
    public delegate void ItemMembershipChangedEventHandler<T>(
        object                            sender,
        ItemMembershipChangedEventArgs<T> e
    );


    /// <summary>
    /// Provides data for the <see cref="ExCollection{T}.ItemAdded"/> and
    /// <see cref="ExCollection{T}.ItemRemoved"/> events.
    /// </summary>
    public sealed class ItemMembershipChangedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Gets the item involved in the event.
        /// </summary>
        public T Item { get; private set; }
        
        
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ItemMembershipChangedEventArgs{T}"/> class.
        /// </summary>
        /// <param name="item">
        /// The item involved in the event.
        /// </param>
        public ItemMembershipChangedEventArgs(
            T item
        )
        {
            Item = item;
        }
    }
}
