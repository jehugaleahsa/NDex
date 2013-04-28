using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NDex.Properties;

namespace NDex
{
    /// <summary>
    /// Provides extension methods for creating typed lists.
    /// </summary>
    public static class TypedList
    {
        /// <summary>
        /// Wraps the given array with a type-safe interface.
        /// </summary>
        /// <typeparam name="T">The type of the items in the array.</typeparam>
        /// <param name="array">The array to wrap.</param>
        /// <returns>A TypedList wrapping the array.</returns>
        public static TypedList<Array, T> Typed<T>(this Array array)
        {
            return new TypedList<Array, T>(array);
        }

        /// <summary>
        /// Wraps the given list with a type-safe interface.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to wrap.</param>
        /// <returns>A TypedList wrapping the list.</returns>
        public static TypedList<ArrayList, T> Typed<T>(this ArrayList list)
        {
            return new TypedList<ArrayList, T>(list);
        }
    }

    /// <summary>
    /// Provides a typed interface for a non-generic list.
    /// </summary>
    /// <typeparam name="TList">The type of the non-generic list to wrap.</typeparam>
    /// <typeparam name="T">The base type of the items in the non-generic list.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(TypedListDebugView<,>))]
    public sealed class TypedList<TList, T> : IList<T>
        where TList : IList
    {
        private readonly TList _list;

        /// <summary>
        /// Initializes a new instance of a TypedList.
        /// </summary>
        /// <param name="list">The list to wrap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public TypedList(TList list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            _list = list;
        }

        /// <summary>
        /// Gets the underlying list.
        /// </summary>
        public TList List
        {
            get
            {
                return _list;
            }
        }

        /// <summary>
        /// Gets or sets the item at the given index.
        /// </summary>
        /// <param name="index">The index of the item to get.</param>
        /// <returns>The item at the given index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        /// <exception cref="System.InvalidCastException">
        /// The item at the given index is incompatible with the type of the items in the list.
        /// </exception>
        public T this[int index]
        {
            get
            {
                return (T)_list[index];
            }
            set
            {
                _list[index] = value;
            }
        }

        /// <summary>
        /// Adds the given item to the end of the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            _list.Add(item);
        }

        /// <summary>
        /// Removes all of the items from the list.
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }

        /// <summary>
        /// Determines whether the given item exists in the list.
        /// </summary>
        /// <param name="item">The item to search for.</param>
        /// <returns>True if the item is in the list; otherwise, false.</returns>
        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        /// <summary>
        /// Copies the list to the give array, starting at the specified index.
        /// </summary>
        /// <param name="array">The array to copy the list to.</param>
        /// <param name="arrayIndex">The index into the array to start copying.</param>
        /// <exception cref="System.ArgumentNullException">The array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The array index is negative -or- outside the bounds of the array.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The number of items in the list is greater than the available space from arrayIndex to the end of the destination array.
        /// </exception>
        /// <exception cref="System.InvalidCastException">
        /// An item in the list is incompatible with the type of the items in the array.
        /// </exception>
        /// <remarks>
        /// If an item in the list causes an <see cref="System.InvalidCastException" /> to be thrown, 
        /// the array will contain any items that were copied.
        /// </remarks>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, Resources.IndexOutOfRange);
            }
            if (_list.Count > array.Length - arrayIndex)
            {
                throw new ArgumentException(Resources.ArrayTooSmall, "array");
            }
            for (int index = 0; index != _list.Count; ++index)
            {
                array[arrayIndex] = (T)_list[index];
                ++arrayIndex;
            }
        }

        /// <summary>
        /// Gets the number of items in the list.
        /// </summary>
        public int Count
        {
            get 
            {
                return _list.Count;
            }
        }

        /// <summary>
        /// Gets the index of the item in the list.
        /// </summary>
        /// <param name="item">The item to get the index for.</param>
        /// <returns>The index of the first occurrence of the item -or- -1 if it it is not found.</returns>
        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        /// <summary>
        /// Inserts the item at the given index.
        /// </summary>
        /// <param name="index">The index to insert the item at.</param>
        /// <param name="item">The item to insert.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        bool ICollection<T>.IsReadOnly
        {
            get 
            {
                return _list.IsReadOnly;
            }
        }

        /// <summary>
        /// Removes the first occurrence of the given item from the list.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        public bool Remove(T item)
        {
            int index = _list.IndexOf(item);
            if (index == -1)
            {
                return false;
            }
            else
            {
                _list.RemoveAt(index);
                return true;
            }
        }

        /// <summary>
        /// Removes the item at the given index.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        /// <summary>
        /// Gets an enumerator that iterates through the items in the list.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in _list)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
