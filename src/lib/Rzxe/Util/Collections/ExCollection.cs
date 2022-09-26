/**
 * ExCollection.cs - Extensible Collection
 *
 * This source-code is part of rzxe - an experimental game engine by Oddmatics:
 * <<https://www.oddmatics.uk>>
 *
 * Author(s): Rory Fewell <roryf@oddmatics.uk>
 */

using System.Collections;
using System.Collections.Generic;

namespace Oddmatics.Rzxe.Util.Collections
{
    /// <summary>
    /// Represents the method that will pre-process an <see cref="IEnumerable{T}"/>
    /// to populate an <see cref="ExCollection{T}"/>.
    /// </summary>
    /// <param name="data">
    /// The data to process.
    /// </param>
    /// <returns>
    /// The processed <see cref="IEnumerable{T}"/> that will be used to populate the
    /// <see cref="ExCollection{T}"/>.
    /// </returns>
    public delegate IEnumerable<T> ExCollectionPreprocess<T>(
        IEnumerable<T> data
    );
    
    
    /// <summary>
    /// Represents a collection that provides events for tracking changes and hooking
    /// operations.
    /// </summary>
    public sealed class ExCollection<T> : IList<T>
    {
        /// <inheritdoc />
        public int Count { get { return BackingList.Count; } }
        
        /// <inheritdoc />
        public bool IsReadOnly { get { return false; } }
        
        
        /// <summary>
        /// The backing collection.
        /// </summary>
        private List<T> BackingList { get; set; }


        /// <summary>
        /// Occurs when an item is added to the collection.
        /// </summary>
        public event ItemMembershipChangedEventHandler<T> ItemAdded;
        
        /// <summary>
        /// Occurs when an item is being added to the collection.
        /// </summary>
        public event ItemAddingEventHandler<T> ItemAdding;
        
        /// <summary>
        /// Occurs when an item is removed from the collection.
        /// </summary>
        public event ItemMembershipChangedEventHandler<T> ItemRemoved;
        
        /// <summary>
        /// Occurs when an item is being removed fromthe collection.
        /// </summary>
        public event ItemRemovingEventHandler<T> ItemRemoving;
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ExCollection{T}"/> class.
        /// </summary>
        public ExCollection()
        {
            BackingList = new List<T>();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ExCollection{T}"/> class with
        /// a source collection of items.
        /// </summary>
        /// <param name="collection">
        /// The collection to source items from.
        /// </param>
        /// <param name="preprocess">
        /// (Optional) The method to call for preprocessing <paramref name="collection"/>
        /// before it is used to populate items.
        /// </param>
        public ExCollection(
            IEnumerable<T>            collection,
            ExCollectionPreprocess<T> preprocess = null
        )
        {
            IEnumerable<T> source = collection;
            
            if (preprocess != null)
            {
                source = preprocess(collection);
            }

            BackingList = new List<T>(source);
        }
        
        
        /// <inheritdoc />
        public T this[
            int index
        ]
        {
            get { return BackingList[index]; }
            set { BackingList[index] = value; }
        }

        /// <inheritdoc />
        public void Add(
            T item
        )
        {
            HandleAdd(item);
        }
        
        /// <inheritdoc />
        public void Clear()
        {
            BackingList.Clear();
        }
        
        /// <inheritdoc />
        public bool Contains(
            T item
        )
        {
            return BackingList.Contains(item);
        }
        
        /// <inheritdoc />
        public void CopyTo(
            T[] array,
            int arrayIndex
        )
        {
            BackingList.CopyTo(array, arrayIndex);
        }
        
        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return BackingList.GetEnumerator();
        }
        
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return BackingList.GetEnumerator();
        }
        
        /// <inheritdoc />
        public int IndexOf(
            T item
        )
        {
            return BackingList.IndexOf(item);
        }
        
        /// <inheritdoc />
        public void Insert(
            int index,
            T   item
        )
        {
            HandleAdd(item, index);
        }
        
        /// <inheritdoc />
        public bool Remove(
            T item
        )
        {
            int index = BackingList.IndexOf(item);
            
            if (index < 0)
            {
                return false;
            }

            return HandleRemove(index);
        }
        
        /// <inheritdoc />
        public void RemoveAt(
            int index
        )
        {
            HandleRemove(index);
        }
        
        
        /// <summary>
        /// Internal procedure for adding an item to the collection.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="desiredIndex">
        /// (Optional) The index to insert the item at, if required.
        /// </param>
        private void HandleAdd(
            T   item,
            int desiredIndex = -1
        )
        {
            var opEvent = new ItemAddingEventArgs<T>(item);

            ItemAdding?.Invoke(this, opEvent);
            
            if (opEvent.Cancel)
            {
                return;
            }

            if (opEvent.InsertAtIndex >= 0)
            {
                BackingList.Insert(opEvent.InsertAtIndex, item);
            }
            else
            {
                if (desiredIndex >= 0)
                {
                    BackingList.Insert(desiredIndex, item);
                }
                else
                {
                    BackingList.Add(item);
                }
            }

            ItemAdded?.Invoke(this, new ItemMembershipChangedEventArgs<T>(item));
        }
        
        /// <summary>
        /// Internal procedure for removing an item from the collection.
        /// </summary>
        /// <param name="index">
        /// The index of the item to remove.
        /// </param>
        /// <returns>
        /// True if the item was removed.
        /// </returns>
        private bool HandleRemove(
            int index
        )
        {
            T   item    = BackingList[index];
            var opEvent = new ItemRemovingEventArgs<T>(item);

            ItemRemoving?.Invoke(this, opEvent);
            
            if (opEvent.Cancel)
            {
                return false;
            }

            BackingList.RemoveAt(index);

            ItemRemoved?.Invoke(this, new ItemMembershipChangedEventArgs<T>(item));

            return true;
        }
    }
}
