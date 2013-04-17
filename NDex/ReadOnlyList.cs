using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using NDex.Properties;

namespace NDex
{
    /// <summary>
    /// Provides methods for creating instances of ReadOnlyList.
    /// </summary>
    public static class ReadOnlyList
    {
        /// <summary>
        /// Wraps a list with a ReadOnlyLit.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to wrap.</param>
        /// <returns>A ReadOnlyList wrapping the given list.</returns>
        public static ReadOnlyList<List<T>, T> ReadOnly<T>(this List<T> list)
        {
            return new ReadOnlyList<List<T>, T>(list);
        }

        /// <summary>
        /// Wraps an array with a ReadOnlyLit.
        /// </summary>
        /// <typeparam name="T">The type of the items in the array.</typeparam>
        /// <param name="array">The array to wrap.</param>
        /// <returns>A ReadOnlyList wrapping the given array.</returns>
        public static ReadOnlyList<T[], T> ReadOnly<T>(this T[] array)
        {
            return new ReadOnlyList<T[], T>(array);
        }

        /// <summary>
        /// Wraps a collection with a ReadOnlyLit.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="collection">The collection to wrap.</param>
        /// <returns>A ReadOnlyList wrapping the given collection.</returns>
        public static ReadOnlyList<Collection<T>, T> ReadOnly<T>(this Collection<T> collection)
        {
            return new ReadOnlyList<Collection<T>, T>(collection);
        }
    }

    /// <summary>
    /// Creates a view into a list such that it cannot be modified.
    /// </summary>
    /// <typeparam name="TList">The type of the list.</typeparam>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(ReadOnlyListDebugView<,>))]
    public sealed class ReadOnlyList<TList, T> : IList<T>
        where TList : IList<T>
    {
        private readonly TList _list;

        /// <summary>
        /// Initializes a new instance of a ReadOnlyList that wraps the given list.
        /// </summary>
        /// <param name="list">The list to wrap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public ReadOnlyList(TList list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            _list = list;
        }

        /// <summary>
        /// Gets or sets the underlying list.
        /// </summary>
        public TList List
        {
            get
            {
                return _list;
            }
        }

        /// <summary>
        /// Finds the index of the given item in the list.
        /// </summary>
        /// <param name="item">The value to find the index for.</param>
        /// <returns>The index of the item in the list; otherwise -1.</returns>
        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        /// <summary>
        /// Inserts the value at the given index. This operation is not supported.
        /// </summary>
        /// <param name="index">The index to insert the item.</param>
        /// <param name="item">The item to insert.</param>
        /// <exception cref="System.NotSupportedException">The list is read-only.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Insert(int index, T item)
        {
            throw new NotSupportedException(Resources.EditReadonlyList);
        }

        /// <summary>
        /// Removes the item at the given index. This operation is not supported.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        /// <exception cref="System.NotSupportedException">The list is read-only.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveAt(int index)
        {
            throw new NotSupportedException(Resources.EditReadonlyList);
        }

        /// <summary>
        /// Gets the item at the given index.
        /// </summary>
        /// <param name="index">The index of the item to get.</param>
        /// <returns>The item at the given index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        /// <exception cref="System.NotSupportedException">The list is read-only.</exception>
        public T this[int index]
        {
            get
            {
                return _list[index];
            }
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                throw new NotSupportedException(Resources.EditReadonlyList);
            }
        }

        /// <summary>
        /// Adds the given item to the end of the list. This operation is not supported.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <exception cref="System.NotSupportedException">The list is read-only.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Add(T item)
        {
            throw new NotSupportedException(Resources.EditReadonlyList);
        }

        /// <summary>
        /// Clears the list of all items. This operation is not supported.
        /// </summary>
        /// <exception cref="System.NotSupportedException">The list is read-only.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Clear()
        {
            throw new NotSupportedException(Resources.EditReadonlyList);
        }

        /// <summary>
        /// Determines whether the given item is in the list.
        /// </summary>
        /// <param name="item">The item to search for.</param>
        /// <returns>True if the value exists in the list; otherwise false.</returns>
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
        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
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
        /// Gets whether the list is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Removes the item from the list. This operation is not supported.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        /// <exception cref="System.NotSupportedException">The list is read-only.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool Remove(T item)
        {
            throw new NotSupportedException(Resources.EditReadonlyList);
        }

        /// <summary>
        /// Gets an enumerator over the items in the list.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }

        /// <summary>
        /// Implicitly creates a new instance of a ReadOnlyList from a list.
        /// </summary>
        /// <param name="list">The list to wrap with a ReadOnlyList.</param>
        /// <returns>A new instance of a ReadOnlyList.</returns>
        public static implicit operator ReadOnlyList<TList, T>(TList list)
        {
            return new ReadOnlyList<TList, T>(list);
        }
    }
}
