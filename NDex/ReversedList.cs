using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using NDex.Properties;

namespace NDex
{
    /// <summary>
    /// Provides methods for creating instances of ReversedList.
    /// </summary>
    public static class ReversedList
    {
        /// <summary>
        /// Wraps a list with a ReversedList.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to wrap.</param>
        /// <returns>A ReversedList wrapping the given list.</returns>
        public static ReversedList<List<T>, T> Reversed<T>(this List<T> list)
        {
            return new ReversedList<List<T>, T>(list);
        }

        /// <summary>
        /// Wraps an array with a ReversedList.
        /// </summary>
        /// <typeparam name="T">The type of the items in the array.</typeparam>
        /// <param name="array">The array to wrap.</param>
        /// <returns>A ReversedList wrapping the given array.</returns>
        public static ReversedList<T[], T> Reversed<T>(this T[] array)
        {
            return new ReversedList<T[], T>(array);
        }

        /// <summary>
        /// Wraps a collection with a ReversedList.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="collection">The collection to wrap.</param>
        /// <returns>A ReversedList wrapping the given collection.</returns>
        public static ReversedList<Collection<T>, T> Reversed<T>(this Collection<T> collection)
        {
            return new ReversedList<Collection<T>, T>(collection);
        }

        /// <summary>
        /// Unwraps a ReversedList.
        /// </summary>
        /// <typeparam name="TList">The type of the underlying list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to wrap.</param>
        /// <returns>A ReversedList wrapping the given list.</returns>
        public static TList Reversed<TList, T>(this ReversedList<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return list.List;
        }

        /// <summary>
        /// Wraps a sublist in a ReversedList.
        /// </summary>
        /// <typeparam name="TList">The type of the sublist's underlying list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="sublist">The sublist to reverse.</param>
        /// <returns>A sublist, wrapping a reversed list over the original sublist.</returns>
        /// <exception cref="System.ArgumentNullException">The sublist is null.</exception>
        public static IReadOnlySublist<ReversedList<TList, T>, T> Reversed<TList, T>(this IReadOnlySublist<TList, T> sublist)
            where TList : IList<T>
        {
            if (sublist == null)
            {
                throw new ArgumentNullException("sublist");
            }
            return reversed<TList, T>(sublist.List, sublist.Offset, sublist.Count);
        }

        /// <summary>
        /// Wraps a sublist in a ReversedList.
        /// </summary>
        /// <typeparam name="TList">The type of the sublist's underlying list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="sublist">The sublist to reverse.</param>
        /// <returns>A sublist, wrapping a reversed list over the original sublist.</returns>
        /// <exception cref="System.ArgumentNullException">The sublist is null.</exception>
        public static IMutableSublist<ReversedList<TList, T>, T> Reversed<TList, T>(this IMutableSublist<TList, T> sublist)
            where TList : IList<T>
        {
            if (sublist == null)
            {
                throw new ArgumentNullException("sublist");
            }
            return reversed<TList, T>(sublist.List, sublist.Offset, sublist.Count);
        }

        /// <summary>
        /// Wraps a sublist in a ReversedList.
        /// </summary>
        /// <typeparam name="TList">The type of the sublist's underlying list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="sublist">The sublist to reverse.</param>
        /// <returns>A sublist, wrapping a reversed list over the original sublist.</returns>
        /// <exception cref="System.ArgumentNullException">The sublist is null.</exception>
        public static IExpandableSublist<ReversedList<TList, T>, T> Reversed<TList, T>(this IExpandableSublist<TList, T> sublist)
            where TList : IList<T>
        {
            if (sublist == null)
            {
                throw new ArgumentNullException("sublist");
            }
            return reversed<TList, T>(sublist.List, sublist.Offset, sublist.Count);
        }

        private static Sublist<ReversedList<TList, T>, T> reversed<TList, T>(TList list, int offset, int count)
            where TList : IList<T>
        {
            ReversedList<TList, T> reversed = new ReversedList<TList, T>(list);
            int reversedOffset = reversed.BaseIndex(offset + count - 1); // start at the end of the original sublist
            Sublist<ReversedList<TList, T>, T> result = new Sublist<ReversedList<TList, T>, T>(reversed, reversedOffset, count);
            return result;
        }

        /// <summary>
        /// Unwraps a reversed sublist.
        /// </summary>
        /// <typeparam name="TList">The type of the underlying list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="sublist">The list to unwrap.</param>
        /// <returns>The underlying list.</returns>
        public static IReadOnlySublist<TList, T> Reversed<TList, T>(this IReadOnlySublist<ReversedList<TList, T>, T> sublist)
            where TList : IList<T>
        {
            if (sublist == null)
            {
                throw new ArgumentNullException("list");
            }
            return reversed<TList, T>(sublist.List, sublist.Offset, sublist.Count);
        }

        /// <summary>
        /// Unwraps a reversed sublist.
        /// </summary>
        /// <typeparam name="TList">The type of the underlying list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="sublist">The list to unwrap.</param>
        /// <returns>The underlying list.</returns>
        public static IMutableSublist<TList, T> Reversed<TList, T>(this IMutableSublist<ReversedList<TList, T>, T> sublist)
            where TList : IList<T>
        {
            if (sublist == null)
            {
                throw new ArgumentNullException("list");
            }
            return reversed<TList, T>(sublist.List, sublist.Offset, sublist.Count);
        }

        /// <summary>
        /// Unwraps a reversed sublist.
        /// </summary>
        /// <typeparam name="TList">The type of the underlying list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="sublist">The list to unwrap.</param>
        /// <returns>The underlying list.</returns>
        public static IExpandableSublist<TList, T> Reversed<TList, T>(this IExpandableSublist<ReversedList<TList, T>, T> sublist)
            where TList : IList<T>
        {
            if (sublist == null)
            {
                throw new ArgumentNullException("list");
            }
            return reversed<TList, T>(sublist.List, sublist.Offset, sublist.Count);
        }

        private static Sublist<TList, T> reversed<TList, T>(ReversedList<TList, T> reversed, int offset, int count)
            where TList : IList<T>
        {
            int originalOffset = reversed.BaseIndex(offset + count - 1);
            Sublist<TList, T> result = new Sublist<TList, T>(reversed.List, originalOffset, count);
            return result;
        }
    }

    /// <summary>
    /// Creates a view into a list such that it appears that the items are reversed.
    /// </summary>
    /// <typeparam name="TList">The type of the list.</typeparam>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(ReversedListDebugView<,>))]
    public sealed class ReversedList<TList, T> : IList<T>
        where TList : IList<T>
    {
        private readonly TList _list;
        private int _currentVersion;

        /// <summary>
        /// Initializes a new instance of a ReversedList that wraps the given list.
        /// </summary>
        /// <param name="list">The list to wrap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public ReversedList(TList list)
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
        /// Gets the index to use to retrieve the value in the underlying list that the given index refers to.
        /// </summary>
        /// <param name="index">The index to translate.</param>
        /// <returns>The index to use to retrieve the value in the underlying list that the given index refers to.</returns>
        /// <remarks>If the given index is out of range, the result will also be out of range in the underlying list.</remarks>
        public int BaseIndex(int index)
        {
            return _list.Count - index - 1;
        }

        /// <summary>
        /// Finds the index of the given item in the list.
        /// </summary>
        /// <param name="item">The value to find the index for.</param>
        /// <returns>The index of the item in the list; otherwise -1.</returns>
        public int IndexOf(T item)
        {
            int index = lastIndexOf(_list, item);
            index = _list.Count - index - 1;
            return index;
        }

        private int lastIndexOf(TList list, T value)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            int position = _list.Count;
            while (position != 0)
            {
                --position;
                if (comparer.Equals(list[position], value))
                {
                    return position;
                }
            }
            return _list.Count;
        }

        /// <summary>
        /// Inserts the value at the given index.
        /// </summary>
        /// <param name="index">The index to insert the item.</param>
        /// <param name="item">The item to insert.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The index is negative -or- beyond the bound of the list.
        /// </exception>
        /// <exception cref="System.NotSupportedException">The list is read-only.</exception>
        public void Insert(int index, T item)
        {
            _list.Insert(_list.Count - index, item);
            ++_currentVersion;
        }

        /// <summary>
        /// Removes the item at the given index.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        /// <exception cref="System.NotSupportedException">The list is read-only.</exception>
        public void RemoveAt(int index)
        {
            _list.RemoveAt(_list.Count - index - 1);
            ++_currentVersion;
        }

        /// <summary>
        /// Gets or sets the item at the given index.
        /// </summary>
        /// <param name="index">The index of the item to get or set.</param>
        /// <returns>The item at the given index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        public T this[int index]
        {
            get
            {
                return _list[_list.Count - index - 1];
            }
            set
            {
                _list[_list.Count - index - 1] = value;
            }
        }

        /// <summary>
        /// Adds the given item to the end of the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <exception cref="System.NotSupportedException">The list is read-only.</exception>
        public void Add(T item)
        {
            _list.Insert(0, item);
            ++_currentVersion;
        }

        /// <summary>
        /// Clears the list of all items.
        /// </summary>
        /// <exception cref="System.NotSupportedException">The list is read-only.</exception>
        public void Clear()
        {
            _list.Clear();
            ++_currentVersion;
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
        /// <exception cref="System.ArgumentOutOfRangeException">The array index is negative.</exception>
        /// <exception cref="System.ArgumentException">
        /// The number of items in the list is greater than the available space from arrayIndex to the end of the destination array.
        /// </exception>
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
            Sublist.CopyReversed<TList, T[], T>(_list, 0, _list.Count, array, arrayIndex, array.Length);
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
        bool ICollection<T>.IsReadOnly
        {
            get
            {
                return _list.IsReadOnly;
            }
        }

        /// <summary>
        /// Removes the item from the list.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        /// <exception cref="System.NotSupportedException">The list is read-only.</exception>
        public bool Remove(T item)
        {
            int index = lastIndexOf(_list, item);
            if (index == _list.Count)
            {
                return false;
            }
            else
            {
                _list.RemoveAt(index);
                ++_currentVersion;
                return true;
            }
        }

        /// <summary>
        /// Gets an enumerator over the items in the list.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            int startingVersion = _currentVersion;
            for (int index = _list.Count; index != 0; )
            {
                --index;
                yield return _list[index];
                if (startingVersion != _currentVersion)
                {
                    throw new InvalidOperationException(Resources.ListChanged);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Implicitly creates a new instance of a ReversedList from a list.
        /// </summary>
        /// <param name="list">The list to wrap with a ReversedList.</param>
        /// <returns>A new instance of a ReversedList.</returns>
        public static implicit operator ReversedList<TList, T>(TList list)
        {
            return new ReversedList<TList, T>(list);
        }
    }
}
