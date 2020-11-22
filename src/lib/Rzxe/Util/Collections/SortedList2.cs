/**
 * SortedList2.cs - Sorted List Collection
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
    /// Represents a collection that will sort items as they are inserted.
    /// </summary>
    public class SortedList2<T> : ICollection<T>
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count { get { return BackingList.Count; } }
        
        /// <summary>
        /// Gets a value indicating whether the collection is read only.
        /// </summary>
        public bool IsReadOnly { get { return false; } }
        
        
        /// <summary>
        /// The backing collection.
        /// </summary>
        private List<T> BackingList { get; set; }
        
        /// <summary>
        /// The comparer for sorting incoming items.
        /// </summary>
        private IComparer<T> Comparer { get; set; }
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SortedList2{T}"/> class with
        /// a comparer.
        /// </summary>
        /// <param name="comparer">
        /// The comparer used to sort incoming items.
        /// </param>
        public SortedList2(
            IComparer<T> comparer
        )
        {
            BackingList = new List<T>();
            Comparer    = comparer;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SortedList2{T}"/> class with
        /// a source collection of items and a comparer.
        /// </summary>
        /// <param name="collection">
        /// The collection to source items from.
        /// </param>
        /// <param name="comparer">
        /// The comparer used to sort incoming items.
        /// </param>
        public SortedList2(
            IEnumerable<T> collection,
            IComparer<T>   comparer
        )
        {
            BackingList = new List<T>(collection);
            Comparer    = comparer;

            BackingList.Sort(Comparer);
        }
        
        
        /// <inheritdoc />
        public void Add(
            T item
        )
        {
            // TODO: Improve this (currently O(n))
            //
            int total = Count;

            for (int i = 0; i < total; i++)
            {
                if (Comparer.Compare(item, BackingList[i]) < 0)
                {
                    BackingList.Insert(i, item);
                }
            }

            BackingList.Add(item);
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
        public bool Remove(
            T item
        )
        {
            return BackingList.Remove(item);
        }
        
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return BackingList.GetEnumerator();
        }
    }
}
