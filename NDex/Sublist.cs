using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using NDex.Properties;

namespace NDex
{
    #region CopyToResult

    /// <summary>
    /// Holds the results of a copy.
    /// </summary>
    public sealed class CopyToResult
    {
        /// <summary>
        /// Initializes a new instance of a CopyToResult.
        /// </summary>
        internal CopyToResult()
        {
        }

        /// <summary>
        /// Gets the index into the source list where the algorithm stopped.
        /// </summary>
        public int SourceOffset
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the index into the destination list where the algorithm stopped.
        /// </summary>
        public int DestinationOffset
        {
            get;
            internal set;
        }

        /// <summary>
        /// Converts the result object into the destination offset.
        /// </summary>
        /// <param name="result">The result to convert.</param>
        /// <returns>The offset into the destination where the algorithm stopped.</returns>
        public static implicit operator int(CopyToResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region CheckResult

    /// <summary>
    /// Holds the results of an algorithm that checks whether a list satisfies a condition.
    /// </summary>
    public sealed class CheckResult
    {
        /// <summary>
        /// Initializes a new instance of a CheckResult.
        /// </summary>
        internal CheckResult()
        {
        }

        /// <summary>
        /// Gets the index where the list stopped satisfying the condition.
        /// </summary>
        public int Index { get; internal set; }

        /// <summary>
        /// Gets or sets whether the full list satisfied the condition.
        /// </summary>
        public bool Success { get; internal set; }

        /// <summary>
        /// Implicitly converts the result to a boolean, representing whether the entire list satisfied the condition.
        /// </summary>
        /// <param name="result">The result to convert.</param>
        /// <returns>True if the given result indicates the entire list satisfies the condition; otherwise, false.</returns>
        public static implicit operator bool(CheckResult result)
        {
            return result.Success;
        }

        /// <summary>
        /// Implicitly converts the result to an integer, representing the index where the list stopped
        /// satisfying the condition.
        /// </summary>
        /// <param name="result">The result to convert.</param>
        /// <returns>The index where the list stopped satisfying the condition.</returns>
        public static implicit operator int(CheckResult result)
        {
            return result.Index;
        }

        /// <summary>
        /// Gets the string representation of the result.
        /// </summary>
        /// <returns>The string representation of the result.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "Success = {0}, Index = {1}", Success, Index);
        }
    }

    #endregion

    #region LowerAndUpperBoundResult

    /// <summary>
    /// Holds the results of the LowerAndUpperBound methods.
    /// </summary>
    public sealed class LowerAndUpperBoundResult
    {        /// <summary>
        /// Initializes a new instance of a LowerAndUpperBoundResult.
        /// </summary>
        internal LowerAndUpperBoundResult()
        {
        }

        /// <summary>
        /// Gets the lower bound of the value.
        /// </summary>
        public int LowerBound
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the upper bound of the value.
        /// </summary>
        public int UpperBound
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the string representation of the result.
        /// </summary>
        /// <returns>The string representation of the result.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "Lower Bound = {0}, Upper Bound = {1}", LowerBound, UpperBound);
        }
    }

    #endregion

    #region MinimumMaximumResult

    /// <summary>
    /// Holds the results of the MinimumMaximum methods.
    /// </summary>
    public sealed class MinimumMaximumResult
    {
        /// <summary>
        /// Initializes a new instance of a MinimumMaximumResult.
        /// </summary>
        internal MinimumMaximumResult()
        {
        }

        /// <summary>
        /// Gets the index of the minimum item in a list.
        /// </summary>
        public int MinimumIndex
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the index of the maximum item in a list.
        /// </summary>
        public int MaximumIndex
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the string representation of the result.
        /// </summary>
        /// <returns>The string representation of the result.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "Minimum Index = {0}, Maximum Index = {1}", MinimumIndex, MaximumIndex);
        }
    }

    #endregion

    #region SearchResult

    /// <summary>
    /// Holds the results of a search method.
    /// </summary>
    public sealed class SearchResult
    {
        /// <summary>
        /// Initializes a new instance of a SearchResult.
        /// </summary>
        internal SearchResult()
        {
        }

        /// <summary>
        /// Gets the index where the search value was found.
        /// </summary>
        public int Index { get; internal set; }

        /// <summary>
        /// Gets or sets whether the search value existed.
        /// </summary>
        public bool Exists { get; internal set; }

        /// <summary>
        /// Implicitly converts the result to a boolean, representing whether the search value was found.
        /// </summary>
        /// <param name="result">The result to convert.</param>
        /// <returns>True if the given result indicates that the value was found; otherwise, false.</returns>
        public static implicit operator bool(SearchResult result)
        {
            return result.Exists;
        }

        /// <summary>
        /// Implicitly converts the result to an integer, representing the first index of the value, if it exists,
        /// or an index past the end of the list if it is missing.
        /// </summary>
        /// <param name="result">The result to convert.</param>
        /// <returns>The index where the value was found -or- the index past the end of the list.</returns>
        public static implicit operator int(SearchResult result)
        {
            return result.Index;
        }

        /// <summary>
        /// Gets the string representation of the result.
        /// </summary>
        /// <returns>The string representation of the result.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "Exists = {0}, Index = {1}", Exists, Index);
        }
    }

    #endregion

    /// <summary>
    /// Provides methods for creating and working with instances of Sublist.
    /// </summary>
    public static partial class Sublist
    {
        private const int _sortMax = 32;

        #region ToSublist

        /// <summary>
        /// Wraps the given list such that the entirety of the list is visible.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to wrap.</param>
        /// <returns>A Sublist wrapping the given list.</returns>
        public static IExpandableSublist<List<T>, T> ToSublist<T>(this List<T> list)
        {
            return new Sublist<List<T>, T>(list);
        }

        /// <summary>
        /// Wraps the given list such that the first item starts at the given offset and contains the remaining items.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to wrap.</param>
        /// <param name="offset">The starting index into the list to create the view.</param>
        /// <returns>A Sublist wrapping the given list.</returns>
        public static IExpandableSublist<List<T>, T> ToSublist<T>(this List<T> list, int offset)
        {
            return new Sublist<List<T>, T>(list, offset);
        }

        /// <summary>
        /// Wraps the given list such that the first item starts at the given offset and contains the number of items
        /// specified by the count.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The list to wrap.</param>
        /// <param name="offset">The starting index into the list to create the view.</param>
        /// <param name="count">The number of items to include in the view.</param>
        /// <returns>A Sublist wrapping the given list.</returns>
        public static IExpandableSublist<List<T>, T> ToSublist<T>(this List<T> list, int offset, int count)
        {
            return new Sublist<List<T>, T>(list, offset, count);
        }

        /// <summary>
        /// Wraps the given array such that the entire array is visible.
        /// </summary>
        /// <typeparam name="T">The type of the items in the array.</typeparam>
        /// <param name="array">The array to wrap.</param>
        /// <returns>A Sublist wrapping the given array.</returns>
        public static IMutableSublist<T[], T> ToSublist<T>(this T[] array)
        {
            return new Sublist<T[], T>(array);
        }

        /// <summary>
        /// Wraps the given array such that the first item starts at the given offset and contains the remaining items.
        /// </summary>
        /// <typeparam name="T">The type of the items in the array.</typeparam>
        /// <param name="array">The array to wrap.</param>
        /// <param name="offset">The starting index into the array to create the view.</param>
        /// <returns>A Sublist wrapping the given array.</returns>
        public static IMutableSublist<T[], T> ToSublist<T>(this T[] array, int offset)
        {
            return new Sublist<T[], T>(array, offset);
        }

        /// <summary>
        /// Wraps the given array such that the first item starts at the given offset and contains the number of items
        /// specified by the count.
        /// </summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="array">The array to wrap.</param>
        /// <param name="offset">The starting index into the array to create the view.</param>
        /// <param name="count">The number of items to include in the view.</param>
        /// <returns>A Sublist wrapping the given array.</returns>
        public static IMutableSublist<T[], T> ToSublist<T>(this T[] array, int offset, int count)
        {
            return new Sublist<T[], T>(array, offset, count);
        }

        /// <summary>
        /// Wraps the given collection such that the entirety of the collection is visible.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="collection">The collection to wrap.</param>
        /// <returns>A Sublist wrapping the given collection.</returns>
        public static IExpandableSublist<Collection<T>, T> ToSublist<T>(this Collection<T> collection)
        {
            return new Sublist<Collection<T>, T>(collection);
        }

        /// <summary>
        /// Wraps the given collection such that the first item starts at the given offset and contains the remaining items.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="collection">The collection to wrap.</param>
        /// <param name="offset">The starting index into the collection to create the view.</param>
        /// <returns>A Sublist wrapping the given collection.</returns>
        public static IExpandableSublist<Collection<T>, T> ToSublist<T>(this Collection<T> collection, int offset)
        {
            return new Sublist<Collection<T>, T>(collection, offset);
        }

        /// <summary>
        /// Wraps the given collection such that the first item starts at the given offset and contains the number of items
        /// specified by the count.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="collection">The collection to wrap.</param>
        /// <param name="offset">The starting index into the collection to create the view.</param>
        /// <param name="count">The number of items to include in the view.</param>
        /// <returns>A Sublist wrapping the given collection.</returns>
        public static IExpandableSublist<Collection<T>, T> ToSublist<T>(this Collection<T> collection, int offset, int count)
        {
            return new Sublist<Collection<T>, T>(collection, offset, count);
        }

        /// <summary>
        /// Wraps the given typed list such that the entirety of the collection is visible.
        /// </summary>
        /// <typeparam name="TList">The type of the non-generic list wrapped by the typed list.</typeparam>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The typed list to wrap.</param>
        /// <returns>A Sublist wrapping the given list.</returns>
        public static IExpandableSublist<TypedList<TList, T>, T> ToSublist<TList, T>(this TypedList<TList, T> list)
            where TList : IList
        {
            return new Sublist<TypedList<TList, T>, T>(list);
        }

        /// <summary>
        /// Wraps the given collection such that the first item starts at the given offset and contains the remaining items.
        /// </summary>
        /// <typeparam name="TList">The type of the non-generic list wrapped by the typed list.</typeparam>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The typed list to wrap.</param>
        /// <param name="offset">The starting index into the collection to create the view.</param>
        /// <returns>A Sublist wrapping the given list.</returns>
        public static IExpandableSublist<TypedList<TList, T>, T> ToSublist<TList, T>(this TypedList<TList, T> list, int offset)
            where TList : IList
        {
            return new Sublist<TypedList<TList, T>, T>(list, offset);
        }

        /// <summary>
        /// Wraps the given typed list such that the first item starts at the given offset and contains the number of items
        /// specified by the count.
        /// </summary>
        /// <typeparam name="TList">The type of the non-generic list wrapped by the typed list.</typeparam>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The typed list to wrap.</param>
        /// <param name="offset">The starting index into the collection to create the view.</param>
        /// <param name="count">The number of items to include in the view.</param>
        /// <returns>A Sublist wrapping the given list.</returns>
        public static IExpandableSublist<TypedList<TList, T>, T> ToSublist<TList, T>(this TypedList<TList, T> list, int offset, int count)
            where TList : IList
        {
            return new Sublist<TypedList<TList, T>, T>(list, offset, count);
        }

        /// <summary>
        /// Wraps the given read-only list such that the entirety of the list is visible.
        /// </summary>
        /// <typeparam name="TList">The type of the list wrapped by the read-only list.</typeparam>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The typed list to wrap.</param>
        /// <returns>A Sublist wrapping the given list.</returns>
        public static IReadOnlySublist<ReadOnlyList<TList, T>, T> ToSublist<TList, T>(this ReadOnlyList<TList, T> list)
            where TList : IList<T>
        {
            return new Sublist<ReadOnlyList<TList, T>, T>(list);
        }

        /// <summary>
        /// Wraps the given read-only list such that the first item starts at the given offset and contains the remaining items.
        /// </summary>
        /// <typeparam name="TList">The type of the list wrapped by the read-only list.</typeparam>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The typed list to wrap.</param>
        /// <param name="offset">The starting index into the list to create the view.</param>
        /// <returns>A Sublist wrapping the given list.</returns>
        public static IReadOnlySublist<ReadOnlyList<TList, T>, T> ToSublist<TList, T>(this ReadOnlyList<TList, T> list, int offset)
            where TList : IList<T>
        {
            return new Sublist<ReadOnlyList<TList, T>, T>(list, offset);
        }

        /// <summary>
        /// Wraps the given read-only list such that the first item starts at the given offset and contains the number of items
        /// specified by the count.
        /// </summary>
        /// <typeparam name="TList">The type of the list wrapped by the read-only list.</typeparam>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The typed list to wrap.</param>
        /// <param name="offset">The starting index into the list to create the view.</param>
        /// <param name="count">The number of items to include in the view.</param>
        /// <returns>A Sublist wrapping the given list.</returns>
        public static IReadOnlySublist<ReadOnlyList<TList, T>, T> ToSublist<TList, T>(this ReadOnlyList<TList, T> list, int offset, int count)
            where TList : IList<T>
        {
            return new Sublist<ReadOnlyList<TList, T>, T>(list, offset, count);
        }

        #endregion

        #region AddTo

        /// <summary>
        /// Adds the items from a list onto a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to add.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <remarks>
        /// The destination Sublist is resized as necessary to hold the added items.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddTo<TSourceList, TDestinationList, T>(
            this IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination)
            where TDestinationList : IList<T>
            where TSourceList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            int result = add<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int add<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast)
            where TDestinationList : IList<T>
            where TSourceList : IList<T>
        {
            growAndShift<TDestinationList, T>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = copyTo<TSourceList, TDestinationList, T>(
                source, first, past,
                destination, destinationPast, destination.Count);
            return indexes.Item2;
        }

        /// <summary>
        /// Adds the items in the source collection to the end of the destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the collections.</typeparam>
        /// <param name="source">The items to add to the destination.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddTo<TDestinationList, T>(
            this IEnumerable<T> source,
            IExpandableSublist<TDestinationList, T> destination)
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            int result = add_optimized<TDestinationList, T>(source, destination.List, destination.Offset, destination.Offset + destination.Count);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int add_optimized<TDestinationList, T>(IEnumerable<T> source, TDestinationList destination, int first, int past)
            where TDestinationList : IList<T>
        {
            ICollection<T> collection = source as ICollection<T>;
            if (collection == null)
            {
                return addAndRotate<TDestinationList, T>(destination, first, past, source);
            }
            growAndShift<TDestinationList, T>(destination, past, collection.Count);
            return copyTo<TDestinationList, T>(source, destination, past, destination.Count);
        }

        private static int addAndRotate<TDestinationList, T>(TDestinationList destination, int first, int past, IEnumerable<T> collection)
            where TDestinationList : IList<T>
        {
            int pivot = destination.Count;
            foreach (T item in collection)
            {
                destination.Add(item);
            }
            rotateLeft<TDestinationList, T>(destination, past, pivot, destination.Count);
            return past + (destination.Count - pivot);
        }

        #endregion

        #region AdjustHeap

        private static void adjustHeap<TList, T>(
            TList list,
            int first,
            int hole,
            int bottom,
            T value,
            Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int top = hole;
            int index = 2 * hole + 2;
            while (index < bottom)
            {
                if (comparison(list[first + index], list[first + (index - 1)]) < 0)
                {
                    --index;
                }
                list[first + hole] = list[first + index];
                hole = index;
                index = 2 * index + 2;
            }
            if (index == bottom)
            {
                list[first + hole] = list[first + (bottom - 1)];
                hole = bottom - 1;
            }
            heapAdd<TList, T>(list, first, hole, top, value, comparison);
        }

        #endregion

        #region Aggregate

        /// <summary>
        /// Aggregates the values in the list using the given operation.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list of items to aggregate.</param>
        /// <param name="aggregator">A function that combines the aggregated value with the next item in the list.</param>
        /// <returns>The value after all items are combined.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.InvalidOperationException">The list did not contain any items.</exception>
        /// <exception cref="System.ArgumentNullException">The aggregator function is null.</exception>
        public static T Aggregate<TList, T>(
            this IReadOnlySublist<TList, T> list,
            Func<T, T, T> aggregator)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (list.Count == 0)
            {
                throw new InvalidOperationException();
            }
            if (aggregator == null)
            {
                throw new ArgumentNullException("aggregator");
            }
            return aggregate<TList, T, T>(list.List, list.Offset + 1, list.Offset + list.Count, list.List[list.Offset], aggregator);
        }

        /// <summary>
        /// Aggregates the values in the list using the given operation.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <typeparam name="TAggregate">The type of the result.</typeparam>
        /// <param name="list">The list of items to aggregate.</param>
        /// <param name="seed">The initial value to combine with the items in the list.</param>
        /// <param name="aggregator">A function that combines the aggregated value with the next item in the list.</param>
        /// <returns>The aggregated value after all items are combined.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The aggregator function is null.</exception>
        public static TAggregate Aggregate<TList, T, TAggregate>(
            this IReadOnlySublist<TList, T> list,
            TAggregate seed,
            Func<TAggregate, T, TAggregate> aggregator)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (aggregator == null)
            {
                throw new ArgumentNullException("aggregator");
            }
            return aggregate<TList, T, TAggregate>(list.List, list.Offset, list.Offset + list.Count, seed, aggregator);
        }

        private static TAggregate aggregate<TList, T, TAggregate>(
            TList list, int first, int past,
            TAggregate seed,
            Func<TAggregate, T, TAggregate> aggregator)
            where TList : IList<T>
        {
            TAggregate result = seed;
            while (first != past)
            {
                result = aggregator(result, list[first]);
                ++first;
            }
            return result;
        }

        #endregion

        #region BinarySearch

        /// <summary>
        /// Finds the index of the value in a sorted list.
        /// </summary>
        /// <typeparam name="TList">The type of the list to search.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for in the list.</param>
        /// <returns>The index of the value in the list if it is found -or- the bitwise compliment of its lower bound.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>
        /// This algorithm assumes that the list is sorted according to the default order of the items.
        /// </remarks>
        public static SearchResult BinarySearch<TList, T>(this IReadOnlySublist<TList, T> list, T value)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return binarySearch<TList, T, T>(list, value, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Finds the index of the value in a sorted list.
        /// </summary>
        /// <typeparam name="TList">The type of the list to search.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for in the list.</param>
        /// <param name="comparer">The compare to use to compare the list items to the search value.</param>
        /// <returns>The index of the value in the list if it is found -or- the bitwise compliment of its lower bound.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// This algorithm assumes that the list is sorted using a meaningful ordering and that the
        /// comparer respects that order.
        /// </remarks>
        public static SearchResult BinarySearch<TList, T>(this IReadOnlySublist<TList, T> list, T value, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return binarySearch<TList, T, T>(list, value, comparer.Compare);
        }

        /// <summary>
        /// Finds the index of the value in a sorted list.
        /// </summary>
        /// <typeparam name="TList">The type of the list to search.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <typeparam name="TSearch">The type of the value to search for.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for in the list.</param>
        /// <param name="comparison">The delegate used to compare the list items to the search value.</param>
        /// <returns>The index of the value in the list if it is found -or- the bitwise compliment of its lower bound.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// This algorithm assumes that the list is sorted using a meaningful ordering and that the
        /// comparison delegate respects that order.
        /// </remarks>
        public static SearchResult BinarySearch<TList, T, TSearch>(this IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return binarySearch<TList, T, TSearch>(list, value, comparison);
        }

        private static SearchResult binarySearch<TList, T, TSearch>(IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            int lowerBound = binarySearch<TList, T, TSearch>(
                list.List, list.Offset, list.Offset + list.Count,
                value,
                comparison);
            if (lowerBound < 0)
            {
                return new SearchResult() { Exists = false, Index = ~lowerBound - list.Offset };
            }
            else
            {
                return new SearchResult { Exists = true, Index = lowerBound - list.Offset };
            }
        }

        private static int binarySearch<TList, T, TSearch>(
            TList list, int first, int past,
            TSearch value,
            Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            int lowerBound = lowerBound<TList, T, TSearch>(list, first, past, value, comparison);
            if (lowerBound == past || comparison(list[lowerBound], value) != 0)
            {
                lowerBound = ~lowerBound;
            }
            return lowerBound;
        }

        #endregion

        #region Clear

        /// <summary>
        /// Removes all of the items in the range defined by a Sublist.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list containing the items to remove.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static IExpandableSublist<TList, T> Clear<TList, T>(this IExpandableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            removeRange_optimized<TList, T>(list.List, list.Offset, list.Offset + list.Count);
            return list.Resize(0, true);
        }

        private static void removeRange_optimized<TList, T>(TList list, int first, int past)
            where TList : IList<T>
        {
            if (typeof(ReversedList<,>) == list.GetType().GetGenericTypeDefinition())
            {
                removeReversedRange<TList, T>(list, first, past);
            }
            else
            {
                removeRange<TList, T>(list, first, past);
            }
        }

        private static void removeReversedRange<TList, T>(TList list, int first, int past)
            where TList : IList<T>
        {
            past = copyBackward<TList, TList, T>(list, 0, first, list, 0, past);
            while (past != 0)
            {
                list.RemoveAt(0);
                --past;
            }
        }

        private static void removeRange<TList, T>(TList list, int first, int past)
            where TList : IList<T>
        {
            first = copyTo<TList, TList, T>(list, past, list.Count, list, first, list.Count).Item2;
            past = list.Count;
            while (first != past)
            {
                --past;
                list.RemoveAt(past);
            }
        }

        #endregion

        #region CompareTo

        /// <summary>
        /// Compares the items in two lists returning the first non-zero result, 
        /// a number less than zero if the first list is shorter,
        /// a number greater than zero if the second list is longer
        /// or zero if the lists are equal, in that order.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <returns>
        /// An integer representing the results of comparing two list.
        /// If two items do not equal the result of comparing them is returned,
        /// otherwise, if the first list is shorter, a negative number is returned,
        /// otherwise, if the second list is longer, a positive number is returned,
        /// otherwise, if the lists are equal, zero is returned.
        /// </returns>
        public static int CompareTo<TList1, TList2, T>(this IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            return compareTo<TList1, T, TList2, T>(list1, list2, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Compares the items in two lists returning the first non-zero result, 
        /// a number less than zero if the first list is shorter,
        /// a number greater than zero if the second list is longer
        /// or zero if the lists are equal, in that order.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparer">The comparer to use to compare items in the lists.</param>
        /// <returns>
        /// An integer representing the results of comparing two list.
        /// If two items do not equal the result of comparing them is returned,
        /// otherwise, if the first list is shorter, a negative number is returned,
        /// otherwise, if the second list is longer, a positive number is returned,
        /// otherwise, if the lists are equal, zero is returned.
        /// </returns>
        public static int CompareTo<TList1, TList2, T>(
            this IReadOnlySublist<TList1, T> list1,
            IReadOnlySublist<TList2, T> list2,
            IComparer<T> comparer)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return compareTo<TList1, T, TList2, T>(list1, list2, comparer.Compare);
        }

        /// <summary>
        /// Compares the items in two lists returning the first non-zero result, 
        /// a number less than zero if the first list is shorter,
        /// a number greater than zero if the second list is longer
        /// or zero if the lists are equal, in that order.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="T1">The type of the items in the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T2">The type of the items in the second list.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the lists.</param>
        /// <returns>
        /// An integer representing the results of comparing two list.
        /// If two items do not equal the result of comparing them is returned,
        /// otherwise, if the first list is shorter, a negative number is returned,
        /// otherwise, if the second list is longer, a positive number is returned,
        /// otherwise, if the lists are equal, zero is returned.
        /// </returns>
        public static int CompareTo<TList1, T1, TList2, T2>(
            this IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return compareTo<TList1, T1, TList2, T2>(list1, list2, comparison);
        }

        private static int compareTo<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            return compareTo<TList1, T1, TList2, T2>(
                list1.List, list1.Offset, list1.Offset + list1.Count,
                list2.List, list2.Offset, list2.Offset + list2.Count,
                comparison);
        }

        private static int compareTo<TList1, T1, TList2, T2>(
            TList1 list1, int first1, int past1,
            TList2 list2, int first2, int past2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            while (first1 != past1 && first2 != past2)
            {
                int result = comparison(list1[first1], list2[first2]);
                if (result < 0)
                {
                    return -1;
                }
                else if (result > 0)
                {
                    return 1;
                }
                ++first1;
                ++first2;
            }
            return first1 == past1 ? first2 == past2 ? 0 : -1 : 1;
        }

        #endregion

        #region CopyBackward

        private static int copyBackward<TList, TDestinationList, T>(
            TList list, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast)
            where TList : IList<T>
            where TDestinationList : IList<T>
        {
            while (first != past && destinationFirst != destinationPast)
            {
                --destinationPast;
                --past;
                destination[destinationPast] = list[past];
            }
            return destinationPast;
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Copies items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="destination">The list to copy the items into.</param>
        /// <returns>The index into the destination list past the last item copied.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        public static CopyToResult CopyTo<TSourceList, TDestinationList, T>(
            this IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            Tuple<int, int> indexes = copyTo<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count);
            CopyToResult result = new CopyToResult();
            result.SourceOffset = indexes.Item1 - source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }

        private static Tuple<int, int> copyTo<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            while (first != past && destinationFirst != destinationPast)
            {
                destination[destinationFirst] = source[first];
                ++destinationFirst;
                ++first;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        /// <summary>
        /// Copies the items in the source collection to the destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the collections.</typeparam>
        /// <param name="source">The items to copy to the destination.</param>
        /// <param name="destination">The list to copy the items.</param>
        /// <returns>The index into the destination past the last item that was copied.</returns>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        public static int CopyTo<TDestinationList, T>(
            this IEnumerable<T> source,
            IMutableSublist<TDestinationList, T> destination)
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            int index = copyTo<TDestinationList, T>(source, destination.List, destination.Offset, destination.Offset + destination.Count);
            index -= destination.Offset;
            return index;
        }

        private static int copyTo<TDestinationList, T>(
            IEnumerable<T> source,
            TDestinationList destination, int first, int past)
            where TDestinationList : IList<T>
        {
            using (IEnumerator<T> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext() && first != past)
                {
                    destination[first] = enumerator.Current;
                    ++first;
                }
                return first;
            }
        }

        #endregion

        #region CopyWhile

        private static Tuple<int, int> copyWhile<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, bool> predicate)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            while (first != past && destinationFirst != destinationPast && predicate(source[first]))
            {
                destination[destinationFirst] = source[first];
                ++first;
                ++destinationFirst;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        private static Tuple<int, int> copyWhileNot<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, bool> predicate)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            while (first != past && destinationFirst != destinationPast && !predicate(source[first]))
            {
                destination[destinationFirst] = source[first];
                ++first;
                ++destinationFirst;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        #endregion

        #region CountIf

        /// <summary>
        /// Counts the items in a list that satisfy the predicate.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to count items in.</param>
        /// <param name="predicate">The condition that an item must satisfy to be counted.</param>
        /// <returns>The number of items that satisfy the predicate.</returns>
        public static int CountIf<TList, T>(this IReadOnlySublist<TList, T> list, Func<T, bool> predicate)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            return countIf<TList, T>(list.List, list.Offset, list.Offset + list.Count, predicate);
        }

        private static int countIf<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
            where TList : IList<T>
        {
            int count = 0;
            while (first != past)
            {
                if (predicate(list[first]))
                {
                    ++count;
                }
                ++first;
            }
            return count;
        }

        #endregion

        #region Distinct

        internal static int AddDistinct<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, bool> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int pivot = destination.Count;
            if (first != past)
            {
                destination.Add(source[first]);
                for (int next = first + 1; next != past; first = next, ++next)
                {
                    if (!comparison(source[first], source[next]))
                    {
                        destination.Add(source[next]);
                    }
                }
            }
            rotateLeft<TDestinationList, TSource>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        internal static Tuple<int, int> CopyDistinct<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, bool> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            if (first != past && destinationFirst != destinationPast)
            {
                destination[destinationFirst] = source[first];
                ++destinationFirst;
                for (int next = first + 1; next != past; first = next, ++next)
                {
                    if (!comparison(source[first], source[next]))
                    {
                        if (destinationFirst == destinationPast)
                        {
                            break;
                        }
                        destination[destinationFirst] = source[next];
                        ++destinationFirst;
                    }
                }
                ++first;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        internal static int Distinct<TSourceList, TSource>(TSourceList list, int first, int past, Func<TSource, TSource, bool> comparison)
            where TSourceList : IList<TSource>
        {
            first = findDuplicates<TSourceList, TSource>(list, first, past, comparison);
            if (first != past)
            {
                for (int next = first + 2; next != past; ++next)
                {
                    if (!comparison(list[first], list[next]))
                    {
                        ++first;
                        list[first] = list[next];
                    }
                }
                return first + 1;
            }
            return past;
        }

        #endregion

        #region Except

        internal static int AddExcept<TSourceList1, TSourceList2, TDestinationList, TSource>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int pivot = destination.Count;
            while (first1 != past1 && first2 != past2)
            {
                int result = comparison(source1[first1], source2[first2]);
                if (result < 0)
                {
                    destination.Add(source1[first1]);
                    ++first1;
                }
                else if (result > 0)
                {
                    ++first2;
                }
                else
                {
                    ++first1;
                    ++first2;
                }
            }
            add<TSourceList1, TDestinationList, TSource>(source1, first1, past1, destination, destination.Count);
            rotateLeft<TDestinationList, TSource>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        internal static Tuple<int, int, int> CopyExcept<TSourceList1, TSourceList2, TDestinationList, TSource>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            while (first1 != past1 && first2 != past2 && destinationFirst != destinationPast)
            {
                int result = comparison(source1[first1], source2[first2]);
                if (result < 0)
                {
                    destination[destinationFirst] = source1[first1];
                    ++first1;
                    ++destinationFirst;
                }
                else if (result > 0)
                {
                    ++first2;
                }
                else
                {
                    ++first1;
                    ++first2;
                }
            }
            Tuple<int, int> indexes = copyTo<TSourceList1, TDestinationList, TSource>(
                source1, first1, past1,
                destination, destinationFirst, destinationPast);
            first1 = indexes.Item1;
            destinationFirst = indexes.Item2;
            return new Tuple<int, int, int>(first1, first2, destinationFirst);
        }

        #endregion

        #region Find

        /// <summary>
        /// Gets the index of the given value within a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <returns>The index of the value in the list -or- an index past the last item in the list, if the value is not found.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static SearchResult Find<TList, T>(this IReadOnlySublist<TList, T> list, T value)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return find<TList, T, T>(list, value, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Gets the index of the given value within a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="comparer">The comparer to use to compare items in the list to the search value.</param>
        /// <returns>The index of the value in the list -or- an index past the last item in the list, if the value is not found.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static SearchResult Find<TList, T>(this IReadOnlySublist<TList, T> list, T value, IEqualityComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return find<TList, T, T>(list, value, comparer.Equals);
        }

        /// <summary>
        /// Gets the index of the given value within a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <typeparam name="TSearch">The type of the value being searched for.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="comparison">The comparison delegate used for comparing items in the list to the search value.</param>
        /// <returns>>The index of the value in the list -or- an index past the last item in the list, if the value is not found.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static SearchResult Find<TList, T, TSearch>(this IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, bool> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return find<TList, T, TSearch>(list, value, comparison);
        }

        private static SearchResult find<TList, T, TSearch>(IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, bool> comparison)
            where TList : IList<T>
        {
            int past = list.Offset + list.Count;
            int index = find<TList, T, TSearch>(list.List, list.Offset, past, value, comparison);
            SearchResult result = new SearchResult();
            result.Index = index - list.Offset;
            result.Exists = index != past;
            return result;
        }

        private static int find<TList, T, TSearch>(TList list, int first, int past, TSearch value, Func<T, TSearch, bool> comparison)
            where TList : IList<T>
        {
            while (first != past && !comparison(list[first], value))
            {
                ++first;
            }
            return first;
        }

        /// <summary>
        /// Finds the index in a list of the first item satisfying the predicate.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="predicate">The condition that must be satisfied.</param>
        /// <returns>
        /// The index of the first item satisfying the predicate 
        /// -or- an index past the last item in the list, if the value is not found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static SearchResult Find<TList, T>(this IReadOnlySublist<TList, T> list, Func<T, bool> predicate)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            int past = list.Offset + list.Count;
            int index = find<TList, T>(list.List, list.Offset, past, predicate);
            SearchResult result = new SearchResult();
            result.Index = index - list.Offset;
            result.Exists = index != past;
            return result;
        }

        private static int find<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
            where TList : IList<T>
        {
            while (first != past && !predicate(list[first]))
            {
                ++first;
            }
            return first;
        }

        #endregion

        #region FindAny

        /// <summary>
        /// Finds the first index in a list of a value that appears in another list.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The list to search.</param>
        /// <param name="list2">The list of values to search for.</param>
        /// <returns>
        /// The first index into the list of a value that appears in the other list 
        /// -or- an index past the last item in the list, if none of the values are found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        public static SearchResult FindAny<TList1, TList2, T>(this IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            return findAny<TList1, T, TList2, T>(list1, list2, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Finds the first index in a list of a value that appears in another list.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The list to search.</param>
        /// <param name="list2">The list of values to search for.</param>
        /// <param name="comparer">The comparer to use to comparer items between the lists.</param>
        /// <returns>
        /// The first index into the list of a value that appears in the other list 
        /// -or- an index past the last item in the list, if none of the values are found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static SearchResult FindAny<TList1, TList2, T>(
            this IReadOnlySublist<TList1, T> list1,
            IReadOnlySublist<TList2, T> list2,
            IEqualityComparer<T> comparer)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return findAny<TList1, T, TList2, T>(list1, list2, comparer.Equals);
        }

        /// <summary>
        /// Finds the first index in a list of a value that appears in another list.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="T1">The type of the items in the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T2">The type of the items in the second list.</typeparam>
        /// <param name="list1">The list to search.</param>
        /// <param name="list2">The list of values to search for.</param>
        /// <param name="comparison">The comparison delegate to use to compare items between the lists.</param>
        /// <returns>
        /// The first index into the list of a value that appears in the other list 
        /// -or- an index past the last item in the list, if none of the values are found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static SearchResult FindAny<TList1, T1, TList2, T2>(
            this IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return findAny<TList1, T1, TList2, T2>(list1, list2, comparison);
        }

        private static SearchResult findAny<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            int past = list1.Offset + list1.Count;
            int index = findAny<TList1, T1, TList2, T2>(
                list1.List, list1.Offset, past,
                list2.List, list2.Offset, list2.Offset + list2.Count,
                comparison);
            SearchResult result = new SearchResult();
            result.Index = index - list1.Offset;
            result.Exists = index != past;
            return result;
        }

        private static int findAny<TList1, T1, TList2, T2>(
            TList1 list1, int first1, int past1,
            TList2 list2, int first2, int past2,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            while (first1 != past1)
            {
                for (int next = first2; next != past2; ++next)
                {
                    if (comparison(list1[first1], list2[next]))
                    {
                        return first1;
                    }
                }
                ++first1;
            }
            return first1;
        }

        #endregion

        #region FindDuplicates

        /// <summary>
        /// Finds the index of the first occurrence of equivilent, adjacent items in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <returns>
        /// The index at the beginning of the equivilent items -or- an index past the end of the list, if no duplicates are found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>The list must be sorted according to the default order of the items.</remarks>
        public static SearchResult FindDuplicates<TList, T>(this IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return findDuplicates<TList, T>(list, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Finds the index of the first occurrence of equivilent, adjacent items in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparer">The comparer to use to determine if two items are equivalent.</param>
        /// <returns>
        /// The index at the beginning of the equivilent items -or- an index past the end of the list, if no duplicates are found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>The list must be sorted such that equivalent items are adjacent.</remarks>
        public static SearchResult FindDuplicates<TList, T>(this IReadOnlySublist<TList, T> list, IEqualityComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return findDuplicates<TList, T>(list, comparer.Equals);
        }

        /// <summary>
        /// Finds the index of the first occurrence of equivilent, adjacent items in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparison">The comparison delegate to use to determine if two items are equivalent.</param>
        /// <returns>
        /// The index at the beginning of the equivilent items -or- an index past the end of the list, if no duplicates are found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>The list must be sorted such that equivalent items are adjacent.</remarks>
        public static SearchResult FindDuplicates<TList, T>(this IReadOnlySublist<TList, T> list, Func<T, T, bool> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return findDuplicates<TList, T>(list, comparison);
        }

        private static SearchResult findDuplicates<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, bool> comparison)
            where TList : IList<T>
        {
            int past = list.Offset + list.Count;
            int index = findDuplicates<TList, T>(list.List, list.Offset, past, comparison);
            SearchResult result = new SearchResult();
            result.Index = index - list.Offset;
            result.Exists = index != past;
            return result;
        }

        private static int findDuplicates<TList, T>(TList list, int first, int past, Func<T, T, bool> comparison)
            where TList : IList<T>
        {
            if (first != past)
            {
                for (int next = first + 1; next != past; first = next, ++next)
                {
                    if (comparison(list[first], list[next]))
                    {
                        return first;
                    }
                }
            }
            return past;
        }

        #endregion

        #region FindNot

        private static int findNot<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
            where TList : IList<T>
        {
            while (first != past && predicate(list[first]))
            {
                ++first;
            }
            return first;
        }

        #endregion

        #region FindSequence

        /// <summary>
        /// Finds the index in a list of the first occurrence of the given sequence.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="sequence">The sequence to search for.</param>
        /// <returns>
        /// The index in a list of the first occurrence of the given sequence 
        /// -or- the index past the last item in the list, if the sequence wasn't found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence is null.</exception>
        public static SearchResult FindSequence<TList1, TList2, T>(this IReadOnlySublist<TList1, T> list, IReadOnlySublist<TList2, T> sequence)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            return findSequence<TList1, T, TList2, T>(list, sequence, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Finds the index in a list of the first occurrence of the given sequence.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="sequence">The sequence to search for.</param>
        /// <param name="comparer">The comparer to use to compare items in the list to those in the sequence.</param>
        /// <returns>
        /// The index in a list of the first occurrence of the given sequence
        /// -or- the index past the last item in the list, if the sequence wasn't found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static SearchResult FindSequence<TList1, TList2, T>(
            this IReadOnlySublist<TList1, T> list,
            IReadOnlySublist<TList2, T> sequence,
            IEqualityComparer<T> comparer)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return findSequence<TList1, T, TList2, T>(list, sequence, comparer.Equals);
        }

        /// <summary>
        /// Finds the index in a list of the first occurrence of the given sequence.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="T1">The type of the items in the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T2">The type of the items in the second list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="sequence">The sequence to search for.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list to those in the sequence.</param>
        /// <returns>
        /// The index in a list of the first occurrence of the given sequence
        /// -or- the index past the last item in the list, if the sequence wasn't found.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static SearchResult FindSequence<TList1, T1, TList2, T2>(
            this IReadOnlySublist<TList1, T1> list,
            IReadOnlySublist<TList2, T2> sequence,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return findSequence<TList1, T1, TList2, T2>(list, sequence, comparison);
        }

        private static SearchResult findSequence<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list,
            IReadOnlySublist<TList2, T2> sequence,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            int past = list.Offset + list.Count;
            int index = findSequence<TList1, T1, TList2, T2>(
                list.List, list.Offset, past,
                sequence.List, sequence.Offset, sequence.Offset + sequence.Count,
                comparison);
            SearchResult result = new SearchResult();
            result.Index = index - list.Offset;
            result.Exists = index != past;
            return result;
        }

        private static int findSequence<TList1, T1, TList2, T2>(
            TList1 list1, int first1, int past1,
            TList2 list2, int first2, int past2,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            int count1 = past1 - first1;
            int count2 = past2 - first2;
            while (count2 <= count1)
            {
                int middle1 = first1;
                int middle2 = first2;
                while (middle2 != past2 && comparison(list1[middle1], list2[middle2]))
                {
                    ++middle1;
                    ++middle2;
                }
                if (middle2 == past2)
                {
                    return first1;
                }
                ++first1;
                --count1;
            }
            return past1;
        }

        #endregion

        #region ForEach

        /// <summary>
        /// Performs the given action on each item in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list of items to perform the action on.</param>
        /// <param name="action">The action to perform on each item.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The action delegate is null.</exception>
        public static void ForEach<TList, T>(this IReadOnlySublist<TList, T> list, Action<T> action)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            forEach<TList, T>(list.List, list.Offset, list.Offset + list.Count, action);
        }

        private static void forEach<TList, T>(TList list, int first, int past, Action<T> action)
            where TList : IList<T>
        {
            while (first != past)
            {
                action(list[first]);
                ++first;
            }
        }

        #endregion

        #region Generate

        internal static void AddGenerate<TSourceList, TSource>(TSourceList list, int first, int past, TSource value)
            where TSourceList : IList<TSource>
        {
            while (first != past)
            {
                list.Insert(first, value);
                ++first;
            }
        }

        internal static void CopyGenerate<TSourceList, TSource>(TSourceList list, int first, int past, TSource value)
            where TSourceList : IList<TSource>
        {
            while (first != past)
            {
                list[first] = value;
                ++first;
            }
        }

        internal static void AddGenerate<TSourceList, TSource>(TSourceList list, int first, int past, Func<TSource> generator)
            where TSourceList : IList<TSource>
        {
            while (first != past)
            {
                list.Insert(first, generator());
                ++first;
            }
        }

        internal static void CopyGenerate<TSourceList, TSource>(TSourceList list, int first, int past, Func<TSource> generator)
            where TSourceList : IList<TSource>
        {
            while (first != past)
            {
                list[first] = generator();
                ++first;
            }
        }

        internal static void AddGenerate<TSourceList, TSource>(TSourceList list, int first, int past, Func<int, TSource> generator)
            where TSourceList : IList<TSource>
        {
            int adjustment = first;
            while (first != past)
            {
                list.Insert(first, generator(first - adjustment));
                ++first;
            }
        }

        internal static void CopyGenerate<TSourceList, TSource>(TSourceList list, int first, int past, Func<int, TSource> generator)
            where TSourceList : IList<TSource>
        {
            int adjustment = first;
            while (first != past)
            {
                list[first] = generator(first - adjustment);
                ++first;
            }
        }

        #endregion

        #region Grow

        private static int growAndShift<TList, T>(TList list, int middle, int growBy)
            where TList : IList<T>
        {
            int oldCount = list.Count;
            grow<TList, T>(list, oldCount + growBy, default(T));
            int index = copyBackward<TList, TList, T>(list, middle, oldCount, list, 0, list.Count);
            return index;
        }

        private static void grow<TList, T>(TList list, int size, T value)
            where TList : IList<T>
        {
            while (list.Count < size)
            {
                list.Add(value);
            }
        }

        #endregion

        #region HeapAdd

        /// <summary>
        /// Moves an item after the end of a heap to its appropriate location.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The heap to add the value to.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>The list must be a max heap.</remarks>
        public static void HeapAdd<TList, T>(this IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            heapAdd<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Moves an item after the end of a heap to its appropriate location.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The heap to add the value to.</param>
        /// <param name="comparer">The comparer to use to compare items in the heap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>The list must be a heap according to the comparer.</remarks>
        public static void HeapAdd<TList, T>(this IMutableSublist<TList, T> list, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            heapAdd<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Moves an item after the end of a heap to its appropriate location.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The heap to add the value to.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the heap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>The list must be a heap according to the comparison delegate.</remarks>
        public static void HeapAdd<TList, T>(this IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            heapAdd<TList, T>(list, comparison);
        }

        private static void heapAdd<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            heapAdd<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
        }

        private static void heapAdd<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (past - first > 1)
            {
                int last = past - 1;
                heapAdd<TList, T>(list, first, last - first, 0, list[last], comparison);
            }
        }

        private static void heapAdd<TList, T>(
            TList list,
            int first,
            int hole,
            int top,
            T value,
            Func<T, T, int> comparison)
            where TList : IList<T>
        {
            for (int index = (hole - 1) / 2;
                top < hole && comparison(list[first + index], value) < 0;
                index = (hole - 1) / 2)
            {
                list[first + hole] = list[first + index];
                hole = index;
            }
            list[first + hole] = value;
        }

        #endregion

        #region HeapRemove

        /// <summary>
        /// Moves the item at the top of the heap to the end of the list, maintaining the heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The heap to remove the top item from.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>The list must be a max heap.</remarks>
        public static void HeapRemove<TList, T>(this IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            heapRemove<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Moves the item at the top of the heap to the end of the list, maintaining the heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The heap to remove the top item from.</param>
        /// <param name="comparer">The comparer to use to compare items in the heap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The compare is null.</exception>
        /// <remarks>The list must be a heap according to the comparer.</remarks>
        public static void HeapRemove<TList, T>(this IMutableSublist<TList, T> list, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            heapRemove<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Moves the item at the top of the heap to the end of the list, maintaining the heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The heap to remove the top item from.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the heap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>The list must be a heap according to the comparison delegate.</remarks>
        public static void HeapRemove<TList, T>(this IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            heapRemove<TList, T>(list, comparison);
        }

        private static void heapRemove<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            heapRemove<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
        }

        private static void heapRemove<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int count = past - first;
            if (count > 1)
            {
                int last = past - 1;
                T value = list[last];
                list[last] = list[first];
                adjustHeap<TList, T>(list, first, 0, last - first, value, comparison);
            }
        }

        #endregion

        #region HeapSort

        /// <summary>
        /// Sorts a list representing a heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The heap to sort.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>The list must be a max heap.</remarks>
        public static void HeapSort<TList, T>(this IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            heapSort<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Sorts a list representing a heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The heap to sort.</param>
        /// <param name="comparer">The comparer to use to compare items in the heap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>The list must be a heap according to the comparer.</remarks>
        public static void HeapSort<TList, T>(this IMutableSublist<TList, T> list, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            heapSort<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Sorts a list representing a heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The heap to sort.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the heap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>The list must be a heap according to the comparison delegate.</remarks>
        public static void HeapSort<TList, T>(this IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            heapSort<TList, T>(list, comparison);
        }

        private static void heapSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            heapSort<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
        }

        private static void heapSort<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            while (past - first > 1)
            {
                heapRemove<TList, T>(list, first, past, comparison);
                --past;
            }
        }

        #endregion

        #region InsertionSort

        private static void insertionSort<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (first != past)
            {
                for (int next = first + 1; next != past; ++next)
                {
                    int hole;
                    T value = list[next];
                    if (comparison(value, list[first]) < 0)
                    {
                        hole = first;
                        copyBackward<TList, TList, T>(list, first, next, list, first + 1, next + 1);
                    }
                    else
                    {
                        hole = next;
                        for (int previous = hole - 1; comparison(value, list[previous]) < 0; hole = previous, --previous)
                        {
                            list[hole] = list[previous];
                        }
                    }
                    list[hole] = value;
                }
            }
        }

        #endregion

        #region Intersect

        internal static int AddIntersect<TSourceList1, TSourceList2, TDestinationList, TSource>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int pivot = destination.Count;
            while (first1 != past1 && first2 != past2)
            {
                int result = comparison(source1[first1], source2[first2]);
                if (result < 0)
                {
                    ++first1;
                }
                else if (result > 0)
                {
                    ++first2;
                }
                else
                {
                    destination.Add(source1[first1]);
                    ++first1;
                    ++first2;
                }
            }
            rotateLeft<TDestinationList, TSource>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        internal static Tuple<int, int, int> CopyIntersect<TSourceList1, TSourceList2, TDestinationList, TSource>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            while (first1 != past1 && first2 != past2 && destinationFirst != destinationPast)
            {
                int result = comparison(source1[first1], source2[first2]);
                if (result < 0)
                {
                    ++first1;
                }
                else if (result > 0)
                {
                    ++first2;
                }
                else
                {
                    destination[destinationFirst] = source1[first1];
                    ++first1;
                    ++first2;
                    ++destinationFirst;
                }
            }
            return new Tuple<int, int, int>(first1, first2, destinationFirst);
        }

        #endregion

        #region IsEqualTo

        /// <summary>
        /// Determines whether two lists have all the same items in the same order.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <returns>True if the lists contain the same items in the same order; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        public static bool IsEqualTo<TList1, TList2, T>(this IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            return isEqualTo<TList1, T, TList2, T>(list1, list2, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Determines whether two lists have equivilent items in the same order.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparer">The comparer to use to determine if two items are equivilent.</param>
        /// <returns>True if the lists have equivilent items in the same order; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static bool IsEqualTo<TList1, TList2, T>(
            this IReadOnlySublist<TList1, T> list1,
            IReadOnlySublist<TList2, T> list2,
            IEqualityComparer<T> comparer)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return isEqualTo<TList1, T, TList2, T>(list1, list2, comparer.Equals);
        }

        /// <summary>
        /// Determines whether two lists have equivilent items in the same order.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="T1">The type of the items in the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T2">The type of the items in the second list.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparison">The comparison delegate used to determine if two items are equivilent.</param>
        /// <returns>True if the lists have equivilent items in the same order; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static bool IsEqualTo<TList1, T1, TList2, T2>(
            this IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return isEqualTo<TList1, T1, TList2, T2>(list1, list2, comparison);
        }

        private static bool isEqualTo<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            return isEqualTo_optimized(
                list1.List, list1.Offset, list1.Offset + list1.Count,
                list2.List, list2.Offset, list2.Offset + list2.Count,
                comparison);
        }

        private static bool isEqualTo_optimized<TList1, T1, TList2, T2>(
            TList1 list1, int first1, int past1,
            TList2 list2, int first2, int past2,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            int count1 = past1 - first1;
            int count2 = past2 - first2;
            if (count1 != count2)
            {
                return false;
            }
            if (ReferenceEquals(list1, list2) && first1 == first2)
            {
                return true;
            }
            Tuple<int, int> indexes = mismatch<TList1, T1, TList2, T2>(list1, first1, past1, list2, first2, past2, comparison);
            return indexes.Item1 == past1 && indexes.Item2 == past2;
        }

        #endregion

        #region IsHeap

        /// <summary>
        /// Finds the index in which the list stops being a valid heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <returns>
        /// The index in which the list stops being a valid heap -or- an index past the last item in the list, if the entire list is a valid heap.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static CheckResult IsHeap<TList, T>(this IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return isHeap<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Finds the index in which the list stops being a valid heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>
        /// The index in which the list stops being a valid heap -or- an index past the last item in the list, if the entire list is a valid heap.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static CheckResult IsHeap<TList, T>(this IReadOnlySublist<TList, T> list, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return isHeap<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Finds the index in which the list stops being a valid heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>
        /// The index in which the list stops being a valid heap -or- an index past the last item in the list, if the entire list is a valid heap.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static CheckResult IsHeap<TList, T>(this IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return isHeap<TList, T>(list, comparison);
        }

        private static CheckResult isHeap<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int past = list.Offset + list.Count;
            int index = isHeapUntil<TList, T>(list.List, list.Offset, past, comparison);
            CheckResult result = new CheckResult();
            result.Index = index - list.Offset;
            result.Success = index == past;
            return result;
        }

        private static int isHeapUntil<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int count = past - first;
            if (count > 1)
            {
                for (int offset = 1; offset < count; ++offset)
                {
                    int parentIndex = first + (offset - 1) / 2;
                    int childIndex = first + offset;
                    if (comparison(list[parentIndex], list[childIndex]) < 0)
                    {
                        return childIndex;
                    }
                }
            }
            return past;
        }

        #endregion

        #region IsOverlapping

        /// <summary>
        /// Gets whether the two lists share any items.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <returns>True if any of the items exist in both lists; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <remarks>The lists must be sorted sets.</remarks>
        public static bool IsOverlapping<TList1, TList2, T>(this IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            return isOverlapping<TList1, T, TList2, T>(list1, list2, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Gets whether the two lists share any items.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparer">Compares an item from the first list to an item in the second list.</param>
        /// <returns>True if any of the items exist in both lists; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>The lists must be sorted sets.</remarks>
        public static bool IsOverlapping<TList1, TList2, T>(
            this IReadOnlySublist<TList1, T> list1,
            IReadOnlySublist<TList2, T> list2,
            IComparer<T> comparer)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return isOverlapping<TList1, T, TList2, T>(list1, list2, comparer.Compare);
        }

        /// <summary>
        /// Gets whether the two lists share any items.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="T1">The type of the items in the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T2">The type of the items in the second list.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparison">Compares an item from the first list to an item in the second list.</param>
        /// <returns>True if any of the items exist in both lists; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>The lists must be sorted sets.</remarks>
        public static bool IsOverlapping<TList1, T1, TList2, T2>(
            this IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return isOverlapping<TList1, T1, TList2, T2>(list1, list2, comparison);
        }

        private static bool isOverlapping<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            return isOverlapping<TList1, T1, TList2, T2>(
                list1.List, list1.Offset, list1.Offset + list1.Count,
                list2.List, list2.Offset, list2.Offset + list2.Count,
                comparison);
        }

        private static bool isOverlapping<TList1, T1, TList2, T2>(
            TList1 list1, int first1, int past1,
            TList2 list2, int first2, int past2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            while (first1 != past1 && first2 != past2)
            {
                int result = comparison(list1[first1], list2[first2]);
                if (result < 0)
                {
                    ++first1;
                }
                else if (result > 0)
                {
                    ++first2;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region IsPartitioned

        /// <summary>
        /// Finds the index in which the list stops being partitioned.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="predicate">The condition that the list is partitioned by.</param>
        /// <returns>
        /// The index in which the list stops being partitioned -or- an index past the last item in the list, if the entire list is partitioned.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static CheckResult IsPartitioned<TList, T>(this IReadOnlySublist<TList, T> list, Func<T, bool> predicate)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            int past = list.Offset + list.Count;
            int index = isPartitionedUntil<TList, T>(list.List, list.Offset, past, predicate);
            CheckResult result = new CheckResult();
            result.Index = index - list.Offset;
            result.Success = index == past;
            return result;
        }

        private static int isPartitionedUntil<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
            where TList : IList<T>
        {
            first = findNot<TList, T>(list, first, past, predicate);
            first = find<TList, T>(list, first, past, predicate);
            return first;
        }

        #endregion

        #region IsSet

        /// <summary>
        /// Finds the index in which the list stops being a valid set.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <returns>
        /// The index in which the list stops being a valid set -or- an index past the last item in the list, if the entire list is a set.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static CheckResult IsSet<TList, T>(this IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return isSet<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Finds the index in which the list stops being a valid set.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>
        /// The index in which the list stops being a valid set -or- an index past the last item in the list, if the entire list is a set.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static CheckResult IsSet<TList, T>(this IReadOnlySublist<TList, T> list, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return isSet<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Finds the index in which the list stops being a valid set.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>
        /// The index in which the list stops being a valid set -or- an index past the last item in the list, if the entire list is a set.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static CheckResult IsSet<TList, T>(this IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return isSet<TList, T>(list, comparison);
        }

        private static CheckResult isSet<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int past = list.Offset + list.Count;
            int index = isSetUntil<TList, T>(list.List, list.Offset, past, comparison);
            CheckResult result = new CheckResult();
            result.Index = index - list.Offset;
            result.Success = index == past;
            return result;
        }

        private static int isSetUntil<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (past - first > 1)
            {
                for (int next = first + 1; next != past; first = next, ++next)
                {
                    int result = comparison(list[first], list[next]);
                    if (result >= 0)
                    {
                        return next;
                    }
                }
            }
            return past;
        }

        #endregion

        #region IsSorted

        /// <summary>
        /// Finds the index in which the list stops being sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <returns>The index in which the list stops being sorted -or- an index past the end of the list, if the list is sorted.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static CheckResult IsSorted<TList, T>(this IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return isSorted<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Finds the index in which the list stops being sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>The index in which the list stops being sorted -or- an index past the end of the list, if the list is sorted.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static CheckResult IsSorted<TList, T>(this IReadOnlySublist<TList, T> list, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return isSorted<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Finds the index in which the list stops being sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparison">The comparison delegate to use to comparer items in the list.</param>
        /// <returns>The index in which the list stops being sorted -or- an index past the end of the list, if the list is sorted.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static CheckResult IsSorted<TList, T>(this IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return isSorted<TList, T>(list, comparison);
        }

        private static CheckResult isSorted<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int past = list.Offset + list.Count;
            int index = isSortedUntil<TList, T>(list.List, list.Offset, past, comparison);
            CheckResult result = new CheckResult();
            result.Index = index - list.Offset;
            result.Success = index == past;
            return result;
        }

        private static int isSortedUntil<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (first != past)
            {
                for (int next = first + 1; next != past; first = next, ++next)
                {
                    if (comparison(list[first], list[next]) > 0)
                    {
                        return next;
                    }
                }
            }
            return past;
        }

        #endregion

        #region IsSubset

        /// <summary>
        /// Determines whether all of the items in the first list appear in the second list.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The list to check for whether it is a subset.</param>
        /// <param name="list2">The list to look for the items in.</param>
        /// <returns>True if all of the items in the first list appear in the second list; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <remarks>
        /// This algorithm assumes that both lists are sorted and have unique values.
        /// </remarks>
        public static CheckResult IsSubset<TList1, TList2, T>(this IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            return isSubset<TList1, T, TList2, T>(list1, list2, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Determines whether all of the items in the first list appear in the second list.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The list to check for whether it is a subset.</param>
        /// <param name="list2">The list to look for the items in.</param>
        /// <param name="comparer">The comparer to use to compare the items.</param>
        /// <returns>True if all of the items in the first list appear in the second list; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// This algorithm assumes that both lists are sorted and have unique values.
        /// </remarks>
        public static CheckResult IsSubset<TList1, TList2, T>(
            this IReadOnlySublist<TList1, T> list1,
            IReadOnlySublist<TList2, T> list2,
            IComparer<T> comparer)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return isSubset<TList1, T, TList2, T>(list1, list2, comparer.Compare);
        }

        /// <summary>
        /// Determines whether all of the items in the first list appear in the second list.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T1">The type of the items in the first list.</typeparam>
        /// <typeparam name="T2">The type of the items in the second list.</typeparam>
        /// <param name="list1">The list to check for whether it is a subset.</param>
        /// <param name="list2">The list to look for the items in.</param>
        /// <param name="comparison">The comparison to use to compare items.</param>
        /// <returns>True if all of the items in the first list appear in the second list; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        /// <remarks>
        /// This algorithm assumes that both lists are sorted and have unique values.
        /// </remarks>
        public static CheckResult IsSubset<TList1, T1, TList2, T2>(
            this IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return isSubset<TList1, T1, TList2, T2>(list1, list2, comparison);
        }

        private static CheckResult isSubset<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            int past = list1.Offset + list1.Count;
            int index = isSubsetUntil<TList1, T1, TList2, T2>(
                list1.List, list1.Offset, past,
                list2.List, list2.Offset, list2.Offset + list2.Count,
                comparison);
            CheckResult result = new CheckResult();
            result.Index = index - list1.Offset;
            result.Success = index == past;
            return result;
        }

        private static int isSubsetUntil<TList1, T1, TList2, T2>(
            TList1 list1, int first1, int past1,
            TList2 list2, int first2, int past2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            while (first1 != past1 && first2 != past2)
            {
                int result = comparison(list1[first1], list2[first2]);
                if (result < 0)
                {
                    return first1;
                }
                else if (result > 0)
                {
                    ++first2;
                }
                else
                {
                    ++first1;
                    ++first2;
                }
            }
            return first1;
        }

        #endregion

        #region ItemAt

        internal static int AddItemAt<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int middle, int past, 
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int count = past - first;
            growAndShift<TDestinationList, TSource>(destination, destinationPast, count);
            CopyItemAt<TSourceList, TDestinationList, TSource>(
                source, first, middle, past,
                destination, destinationPast, destinationPast + count,
                comparison);
            return destinationPast + count;
        }

        internal static Tuple<int, int> CopyItemAt<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int middle, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int destinationCount = destinationPast - destinationFirst;
            int middleCount = middle - first;
            Tuple<int, int> indexes = copyTo<TSourceList, TDestinationList, TSource>(source, first, past, destination, destinationFirst, destinationPast);
            if (indexes.Item2 > destinationFirst + middleCount)
            {
                ItemAt<TDestinationList, TSource>(destination, destinationFirst, destinationFirst + middleCount, indexes.Item2, comparison);
            }
            return indexes;
        }

        internal static void ItemAt<TList, T>(TList list, int first, int middle, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            while (past - first > _sortMax)
            {
                int pivot = partition<TList, T>(list, first, past, comparison);
                if (pivot <= middle)
                {
                    first = pivot;
                }
                else
                {
                    past = pivot;
                }
            }
            insertionSort<TList, T>(list, first, past, comparison);
        }

        #endregion

        #region LowerAndUpperBound

        /// <summary>
        /// Gets the range of the items in a list that are equal to the given value.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <returns>
        /// The indexes representing the lower and upper bounds.
        /// </returns>
        /// <remarks>
        /// This algorithm assumes that the list is sorted according to the default order of the items.
        /// </remarks>
        public static LowerAndUpperBoundResult LowerAndUpperBound<TList, T>(this IReadOnlySublist<TList, T> list, T value)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return lowerAndUpperBound<TList, T, T>(list, value, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Gets the range of the items in a list that are equivilent to the given value.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">Th value to search for.</param>
        /// <param name="comparer">The comparison delegate to use to compare items from the list and the search value.</param>
        /// <returns>
        /// The indexes representing the lower and upper bounds.
        /// </returns>
        /// <remarks>
        /// This algorithm assumes that the list is sorted using a meaningful ordering and that the
        /// comparison delegate respects that order.
        /// </remarks>
        public static LowerAndUpperBoundResult LowerAndUpperBound<TList, T>(this IReadOnlySublist<TList, T> list, T value, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return lowerAndUpperBound<TList, T, T>(list, value, comparer.Compare);
        }

        /// <summary>
        /// Gets the range of the items in a list that are equivilent to the given value.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <typeparam name="TSearch">The type of the value to search for.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">Th value to search for.</param>
        /// <param name="comparison">The comparison delegate to use to compare items from the list and the search value.</param>
        /// <returns>
        /// The indexes representing the lower and upper bounds.
        /// </returns>
        /// <remarks>
        /// This algorithm assumes that the list is sorted using a meaningful ordering and that the
        /// comparison delegate respects that order.
        /// </remarks>
        public static LowerAndUpperBoundResult LowerAndUpperBound<TList, T, TSearch>(
            this IReadOnlySublist<TList, T> list,
            TSearch value,
            Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return lowerAndUpperBound<TList, T, TSearch>(list, value, comparison);
        }

        private static LowerAndUpperBoundResult lowerAndUpperBound<TList, T, TSearch>(
            IReadOnlySublist<TList, T> list,
            TSearch value,
            Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            Tuple<int, int> indexes = lowerAndUpperBound<TList, T, TSearch>(
                list.List, list.Offset, list.Offset + list.Count,
                value,
                comparison);
            LowerAndUpperBoundResult result = new LowerAndUpperBoundResult();
            result.LowerBound = indexes.Item1 - list.Offset;
            result.UpperBound = indexes.Item2 - list.Offset;
            return result;
        }

        private static Tuple<int, int> lowerAndUpperBound<TList, T, TSearch>(
            TList list, int first, int past,
            TSearch value,
            Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            int count = past - first;
            while (count != 0)
            {
                int half = count / 2;
                int middle = first + half;
                int result = comparison(list[middle], value);
                if (result < 0)
                {
                    first = middle + 1;
                    count -= half + 1;
                }
                else if (result > 0)
                {
                    count = half;
                }
                else
                {
                    int start = lowerBound<TList, T, TSearch>(list, first, middle, value, comparison);
                    int end = upperBound<TList, T, TSearch>(list, middle + 1, first + count, value, comparison);
                    return new Tuple<int, int>(start, end);
                }
            }
            return new Tuple<int, int>(first, first);
        }

        #endregion

        #region LowerBound

        /// <summary>
        /// Finds the first index in a sorted list where the given value would belong.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <returns>The first index in a sorted list where the given value would belong.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>This algorithm assumes that the list is sorted according to the default order of the items.</remarks>
        public static int LowerBound<TList, T>(this IReadOnlySublist<TList, T> list, T value)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return lowerBound<TList, T, T>(list, value, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Finds the first index in a sorted list where the given value would belong.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="comparer">The comparer to use to compare items in the list to the search value.</param>
        /// <returns>The first index in a sorted list where the given value would belong.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// This algorithm assumes that the list is sorted using a meaningful ordering and that the
        /// comparer respects that order.
        /// </remarks>
        public static int LowerBound<TList, T>(this IReadOnlySublist<TList, T> list, T value, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return lowerBound<TList, T, T>(list, value, comparer.Compare);
        }

        /// <summary>
        /// Finds the first index in a sorted list where the given value would belong.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <typeparam name="TSearch">The type of the search value.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list to the search value.</param>
        /// <returns>The first index in a sorted list where the given value would belong.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// This algorithm assumes that the list is sorted using a meaningful ordering and that the
        /// comparison delegate respects that order.
        /// </remarks>
        public static int LowerBound<TList, T, TSearch>(this IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return lowerBound<TList, T, TSearch>(list, value, comparison);
        }

        private static int lowerBound<TList, T, TSearch>(IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            int result = lowerBound<TList, T, TSearch>(list.List, list.Offset, list.Offset + list.Count, value, comparison);
            result -= list.Offset;
            return result;
        }

        private static int lowerBound<TList, T, TSearch>(
            TList list, int first, int past,
            TSearch value,
            Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            int count = past - first;
            while (count > 0)
            {
                int half = count / 2;
                int middle = first + half;
                int result = comparison(list[middle], value);
                if (result < 0)
                {
                    first = middle + 1;
                    count -= half + 1;
                }
                else
                {
                    count = half;
                }
            }
            return first;
        }

        #endregion

        #region MakeHeap

        internal static void MakeHeap<TSourceList, TSource>(TSourceList list, int first, int past, Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
        {
            int bottom = past - first;
            for (int hole = bottom / 2; hole > 0; )
            {
                --hole;
                adjustHeap<TSourceList, TSource>(list, first, hole, bottom, list[first + hole], comparison);
            }
        }

        internal static int AddMakeHeap<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int count = past - first;
            growAndShift<TDestinationList, TSource>(destination, destinationPast, count);
            return CopyMakeHeap<TSourceList, TDestinationList, TSource>(source, first, past, destination, destinationPast, destinationPast + count, comparison).Item2;
        }

        internal static Tuple<int, int> CopyMakeHeap<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int numberOfItems = destinationPast - destinationFirst;
            Tuple<int, int> indexes = copyTo<TSourceList, TDestinationList, TSource>(source, first, past, destination, destinationFirst, destinationPast);
            MakeHeap<TDestinationList, TSource>(destination, destinationFirst, indexes.Item2, comparison);
            return indexes;
        }

        #endregion

        #region MakeSet

        internal static int AddMakeSet<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past, 
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            IComparer<TSource> comparer = ComparisonWrapper<TSource>.GetComparer(comparison);
            SortedSet<TSource> set = new SortedSet<TSource>(comparer);
            while (first != past)
            {
                set.Add(source[first]);
                ++first;
            }
            growAndShift<TDestinationList, TSource>(destination, destinationPast, set.Count);
            int newPast = destinationPast + set.Count;
            copyTo<TDestinationList, TSource>(set, destination, destinationPast, newPast);
            return newPast;
        }

        internal static Tuple<int, int> CopyMakeSet<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            IComparer<TSource> comparer = ComparisonWrapper<TSource>.GetComparer(comparison);
            SortedSet<TSource> set = new SortedSet<TSource>(comparer);
            int count = destinationPast - destinationFirst;
            while (set.Count != count && first != past)
            {
                set.Add(source[first]);
                ++first;
            }
            int index = copyTo<TDestinationList, TSource>(set, destination, destinationFirst, destinationPast);
            return new Tuple<int, int>(first, index);
        }

        internal static int MakeSet<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (past - first > 1)
            {
                Sort<TList, T>(list, first, past, past - first, comparison);
                int next = first + 1;
                while (next != past && comparison(list[first], list[next]) != 0)
                {
                    first = next;
                    ++next;
                }
                if (next != past)
                {
                    for (++next; next != past; ++next)
                    {
                        if (comparison(list[first], list[next]) != 0)
                        {
                            ++first;
                            list[first] = list[next];
                        }
                    }
                    return first + 1;
                }
            }
            return past;
        }

        #endregion

        #region Maximum

        /// <summary>
        /// Finds the index of the largest item in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <returns>The index of the largest item in the list -or- the index past the end of the list if it is empty.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static int Maximum<TList, T>(this IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return maximum<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Finds the index of the largest item in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>The index of the largest item in the list -or- the index past the end of the list if it is empty.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static int Maximum<TList, T>(this IReadOnlySublist<TList, T> list, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return maximum<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Finds the index of the largest item in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list"></param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>The index of the largest item in the list -or- the index past the end of the list if it is empty.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static int Maximum<TList, T>(this IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return maximum<TList, T>(list, comparison);
        }

        private static int maximum<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int result = maximum<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
            result -= list.Offset;
            return result;
        }

        private static int maximum<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (past - first == 0)
            {
                return past;
            }
            int maxIndex = first;
            for (++first; first != past; ++first)
            {
                if (comparison(list[maxIndex], list[first]) < 0)
                {
                    maxIndex = first;
                }
            }
            return maxIndex;
        }

        #endregion

        #region Merge

        internal static int AddMerge<TSourceList1, TSourceList2, TDestinationList, TSource>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            growAndShift<TDestinationList, TSource>(destination, destinationPast, (past1 - first1) + (past2 - first2));
            Tuple<int, int, int> indexes = CopyMerged<TSourceList1, TSourceList2, TDestinationList, TSource>(
                source1, first1, past1,
                source2, first2, past2,
                destination, destinationPast, destination.Count,
                comparison);
            return indexes.Item3;
        }

        internal static Tuple<int, int, int> CopyMerged<TSourceList1, TSourceList2, TDestinationList, TSource>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            while (first1 != past1 && first2 != past2 && destinationFirst != destinationPast)
            {
                int result = comparison(source2[first2], source1[first1]);
                if (result < 0)
                {
                    destination[destinationFirst] = source2[first2];
                    ++first2;
                }
                else
                {
                    destination[destinationFirst] = source1[first1];
                    ++first1;
                }
                ++destinationFirst;
            }
            Tuple<int, int> indexes1 = copyTo<TSourceList1, TDestinationList, TSource>(
                source1, first1, past1,
                destination, destinationFirst, destinationPast);
            first1 = indexes1.Item1;
            destinationFirst = indexes1.Item2;
            Tuple<int, int> indexes2 = copyTo<TSourceList2, TDestinationList, TSource>(
                source2, first2, past2,
                destination, destinationFirst, destinationPast);
            first2 = indexes2.Item1;
            destinationFirst = indexes2.Item2;
            return new Tuple<int, int, int>(first1, first2, destinationFirst);
        }

        private static void mergeBuffered<TList, TBuffer, T>(
            TList list, int first, int middle, int past,
            TBuffer buffer, int bufferFirst, int bufferPast,
            Func<T, T, int> comparison)
            where TList : IList<T>
            where TBuffer : IList<T>
        {
            if (past - first == 2)
            {
                if (comparison(list[first], list[middle]) > 0)
                {
                    T temp = list[first];
                    list[first] = list[middle];
                    list[middle] = temp;
                }
            }
            else
            {
                int bufferCount = bufferPast - bufferFirst;
                int count1 = middle - first;
                int count2 = past - middle;
                if (count1 <= count2 && count1 <= bufferCount)
                {
                    int bufferMiddle = copyTo<TList, TBuffer, T>(list, first, middle, buffer, bufferFirst, bufferPast).Item2;
                    CopyMerged<TBuffer, TList, TList, T>(
                        buffer, bufferFirst, bufferMiddle,
                        list, middle, past,
                        list, first, past,
                        comparison);
                }
                else if (count2 <= bufferCount)
                {
                    int bufferMiddle = copyTo<TList, TBuffer, T>(list, middle, past, buffer, bufferFirst, bufferPast).Item2;
                    copyMergedBackward<TList, TBuffer, TList, T>(
                        list, first, middle,
                        buffer, bufferFirst, bufferMiddle,
                        list, first, past,
                        comparison);
                }
                else
                {
                    int middle1;
                    int middle2;
                    if (count2 < count1)
                    {
                        middle1 = first + (middle - first) / 2;
                        T value = list[middle1];
                        middle2 = lowerBound<TList, T, T>(list, middle, past, value, comparison);
                    }
                    else
                    {
                        middle2 = middle + (past - middle) / 2;
                        T value = list[middle2];
                        middle1 = upperBound<TList, T, T>(list, first, middle, value, comparison);
                    }
                    int middleN = rotateBuffered<TList, TBuffer, T>(
                        list, middle1, middle, middle2,
                        buffer, bufferFirst, bufferPast);
                    mergeBuffered<TList, TBuffer, T>(
                        list, first, middle1, middleN,
                        buffer, bufferFirst, bufferPast,
                        comparison);
                    mergeBuffered<TList, TBuffer, T>(
                        list, middleN, middle2, past,
                        buffer, bufferFirst, bufferPast,
                        comparison);
                }
            }
        }

        private static int copyMergedBackward<TList1, TList2, TDestinationList, T>(
            TList1 list1, int first1, int past1,
            TList2 list2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, T, int> comparison)
            where TList1 : IList<T>
            where TList2 : IList<T>
            where TDestinationList : IList<T>
        {
            while (first1 != past1 && first2 != past2 && destinationFirst != destinationPast)
            {
                --destinationPast;
                int result = comparison(list1[past1 - 1], list2[past2 - 1]);
                if (result > 0)
                {
                    --past1;
                    destination[destinationPast] = list1[past1];
                }
                else
                {
                    --past2;
                    destination[destinationPast] = list2[past2];
                }
            }
            destinationPast = copyBackward<TList1, TDestinationList, T>(list1, first1, past1, destination, destinationFirst, destinationPast);
            destinationPast = copyBackward<TList2, TDestinationList, T>(list2, first2, past2, destination, destinationFirst, destinationPast);
            return destinationPast;
        }

        #endregion

        #region Minimum

        /// <summary>
        /// Finds the index of the smallest item in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <returns>The index of the smaller item in the list -or- the index past the end of the list, if it is empty.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static int Minimum<TList, T>(this IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return minimum<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Finds the index of the smallest item in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>The index of the smaller item in the list -or- the index past the end of the list, if it is empty.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static int Minimum<TList, T>(this IReadOnlySublist<TList, T> list, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return minimum<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Finds the index of the smallest item in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>The index of the smaller item in the list -or- the index past the end of the list, if it is empty.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static int Minimum<TList, T>(this IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return minimum<TList, T>(list, comparison);
        }

        private static int minimum<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int result = minimum<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
            result -= list.Offset;
            return result;
        }

        private static int minimum<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (past - first == 0)
            {
                return past;
            }
            int minIndex = first;
            for (++first; first != past; ++first)
            {
                if (comparison(list[minIndex], list[first]) > 0)
                {
                    minIndex = first;
                }
            }
            return minIndex;
        }

        #endregion

        #region MinimumMaximum

        /// <summary>
        /// Finds the indexes of the smallest and largest items in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <returns>
        /// The indexes of the smallest and largest items in the list, in that order -or- indexes past the end of the list if it is empty.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static MinimumMaximumResult MinimumMaximum<TList, T>(this IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return minimumMaximum<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Finds the indexes of the smallest and largest items in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>
        /// The indexes of the smallest and largest items in the list, in that order -or- indexes past the end of the list if it is empty.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static MinimumMaximumResult MinimumMaximum<TList, T>(this IReadOnlySublist<TList, T> list, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return minimumMaximum<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Finds the indexes of the smallest and largest items in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>
        /// The indexes of the smallest and largest items in the list, in that order -or- indexes past the end of the list if it is empty.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static MinimumMaximumResult MinimumMaximum<TList, T>(this IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return minimumMaximum<TList, T>(list, comparison);
        }

        private static MinimumMaximumResult minimumMaximum<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            Tuple<int, int> indexes = minimumMaximum<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
            MinimumMaximumResult result = new MinimumMaximumResult()
            {
                MinimumIndex = indexes.Item1 - list.Offset,
                MaximumIndex = indexes.Item2 - list.Offset
            };
            return result;
        }

        private static Tuple<int, int> minimumMaximum<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (past - first == 0)
            {
                return new Tuple<int, int>(past, past);
            }
            int minIndex = first;
            int maxIndex = first;
            for (++first; first != past; ++first)
            {
                if (comparison(list[minIndex], list[first]) > 0)
                {
                    minIndex = first;
                }
                else if (comparison(list[maxIndex], list[first]) < 0)
                {
                    maxIndex = first;
                }
            }
            return new Tuple<int, int>(minIndex, maxIndex);
        }

        #endregion

        #region Mismatch

        /// <summary>
        /// Finds the offset into the given lists where they differ.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <returns>
        /// The index into the given lists where they differ 
        /// -or- the index past the end of a list, if one is shorter
        /// -or- the index past the end of both lists, if they are equal.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        public static int Mismatch<TList1, TList2, T>(this IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            return mismatch<TList1, T, TList2, T>(list1, list2, EqualityComparer<T>.Default.Equals);
        }


        /// <summary>
        /// Finds the offset into the given lists where they differ.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparer">The comparer to use to compare items in the lists.</param>
        /// <returns>
        /// The index into the given lists where they differ 
        /// -or- the index past the end of a list, if one is shorter
        /// -or- the index past the end of both lists, if they are equal.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static int Mismatch<TList1, TList2, T>(
            this IReadOnlySublist<TList1, T> list1,
            IReadOnlySublist<TList2, T> list2,
            IEqualityComparer<T> comparer)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return mismatch<TList1, T, TList2, T>(list1, list2, comparer.Equals);
        }

        /// <summary>
        /// Finds the offset into the given lists where they differ.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="T1">The type of the items in the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T2">The type of the items in the second list.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the lists.</param>
        /// <returns>
        /// The index into the given lists where they differ 
        /// -or- the index past the end of a list, if one is shorter
        /// -or- the index past the end of both lists, if they are equal.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static int Mismatch<TList1, T1, TList2, T2>(
            this IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return mismatch<TList1, T1, TList2, T2>(list1, list2, comparison);
        }

        private static int mismatch<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            Tuple<int, int> result = mismatch<TList1, T1, TList2, T2>(
                list1.List, list1.Offset, list1.Offset + list1.Count,
                list2.List, list2.Offset, list2.Offset + list2.Count,
                comparison);
            return result.Item1 - list1.Offset;
        }

        private static Tuple<int, int> mismatch<TList1, T1, TList2, T2>(
            TList1 list1, int first1, int past1,
            TList2 list2, int first2, int past2,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            while (first1 != past1 && first2 != past2)
            {
                if (!comparison(list1[first1], list2[first2]))
                {
                    break;
                }
                ++first1;
                ++first2;
            }
            return new Tuple<int, int>(first1, first2);
        }

        #endregion

        #region NextPermutation

        /// <summary>
        /// Arranges the items in a list to the next lexicographic permutation and returns true. 
        /// If there isn't another permutation, it arranges the sequence to the first permutation and returns false.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to find the next permutation for.</param>
        /// <returns>
        /// True if the items were arranged to the next permutation; otherwise, false if the items were rearranged to the first permutation.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>
        /// This algorithm assumes that, in order to enumerate every permutation, 
        /// the list is initially sorted according to the default ordering of the items.
        /// </remarks>
        public static bool NextPermutation<TList, T>(this IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return nextPermutation<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Arranges the items in a list to the next lexicographic permutation and returns true. 
        /// If there isn't another permutation, it arranges the sequence to the first permutation and returns false.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to find the next permutation for.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>
        /// True if the items were arranged to the next permutation; otherwise, false if the items were rearranged to the first permutation.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// This algorithm assumes that, in order to enumerate every permutation, 
        /// the list is initially sorted according to the comparer.
        /// </remarks>
        public static bool NextPermutation<TList, T>(this IMutableSublist<TList, T> list, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return nextPermutation<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Arranges the items in a list to the next lexicographic permutation and returns true. 
        /// If there isn't another permutation, it arranges the sequence to the first permutation and returns false.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to find the next permutation for.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>
        /// True if the items were arranged to the next permutation; otherwise, false if the items were rearranged to the first permutation.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// This algorithm assumes that, in order to enumerate every permutation, 
        /// the list is initially sorted according to the comparer.
        /// </remarks>
        public static bool NextPermutation<TList, T>(this IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return nextPermutation<TList, T>(list, comparison);
        }

        private static bool nextPermutation<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            return nextPermutation<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
        }

        private static bool nextPermutation<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int previous = past;
            --previous;
            if (first == past || first == previous)
            {
                return false;
            }
            while (true)
            {
                int previous1 = previous--;
                if (comparison(list[previous], list[previous1]) < 0)
                {
                    int middle = past;
                    --middle;
                    while (comparison(list[previous], list[middle]) >= 0)
                    {
                        --middle;
                    }
                    T temp = list[previous];
                    list[previous] = list[middle];
                    list[middle] = temp;
                    Reverse<TList, T>(list, previous1, past);
                    return true;
                }

                if (previous == first)
                {
                    Reverse<TList, T>(list, first, past);
                    return false;
                }
            }
        }

        #endregion

        #region PartialSort

        internal static int AddPartialSort<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int middle, int past,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int count = middle - first;
            growAndShift<TDestinationList, TSource>(destination, destinationPast, count);
            return CopyPartialSort<TSourceList, TDestinationList, TSource>(
                source, first, past,
                destination, destinationPast, destinationPast + count,
                comparison);
        }

        internal static int CopyPartialSort<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int destinationMiddle = copyMakeHeapForSort<TSourceList, TDestinationList, TSource>(source, first, past, destination, destinationFirst, destinationPast, comparison);
            heapSort<TDestinationList, TSource>(destination, destinationFirst, destinationMiddle, comparison);
            return destinationMiddle;
        }

        private static int copyMakeHeapForSort<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int numberOfItems = destinationPast - destinationFirst;
            Tuple<int, int> indexes = copyTo<TSourceList, TDestinationList, TSource>(source, first, past, destination, destinationFirst, destinationPast);
            first = indexes.Item1;
            int destinationMiddle = indexes.Item2;
            MakeHeap<TDestinationList, TSource>(destination, destinationFirst, destinationMiddle, comparison);

            while (first != past)
            {
                if (comparison(source[first], destination[destinationFirst]) < 0)
                {
                    adjustHeap<TDestinationList, TSource>(destination, destinationFirst, 0, numberOfItems, source[first], comparison);
                }
                ++first;
            }
            return destinationMiddle;
        }

        internal static void PartialSort<TSourceList, TSource>(TSourceList list, int first, int middle, int past, Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
        {
            if (past - first > 1)
            {
                MakeHeap<TSourceList, TSource>(list, first, middle, comparison);
                int numberOfItems = middle - first;
                for (int next = middle; next != past; ++next)
                {
                    if (comparison(list[next], list[first]) < 0)
                    {
                        TSource value = list[next];
                        list[next] = list[first];
                        adjustHeap<TSourceList, TSource>(list, first, 0, numberOfItems, value, comparison);
                    }
                }
                heapSort<TSourceList, TSource>(list, first, middle, comparison);
            }
        }

        #endregion

        #region Partition

        internal static Tuple<int, int> AddPartition<TSourceList, TDestinationList1, TDestinationList2, TSource>(
            TSourceList source, int first, int past,
            TDestinationList1 destination1, int destinationPast1,
            TDestinationList2 destination2, int destinationPast2,
            Func<TSource, bool> predicate)
            where TSourceList : IList<TSource>
            where TDestinationList1 : IList<TSource>
            where TDestinationList2 : IList<TSource>
        {
            int pivot1 = destination1.Count;
            int pivot2 = destination2.Count;
            while (first != past)
            {
                if (predicate(source[first]))
                {
                    destination1.Add(source[first]);
                }
                else
                {
                    destination2.Add(source[first]);
                }
                ++first;
            }
            rotateLeft<TDestinationList1, TSource>(destination1, destinationPast1, pivot1, destination1.Count);
            rotateLeft<TDestinationList2, TSource>(destination2, destinationPast2, pivot2, destination2.Count);
            destinationPast1 += destination1.Count - pivot1;
            destinationPast2 += destination2.Count - pivot2;
            return new Tuple<int, int>(destinationPast1, destinationPast2);
        }

        internal static Tuple<int, int, int> CopyPartition<TSourceList, TDestinationList1, TDestinationList2, TSource>(
            TSourceList source, int first, int past,
            TDestinationList1 destination1, int destinationFirst1, int destinationPast1,
            TDestinationList2 destination2, int destinationFirst2, int destinationPast2,
            Func<TSource, bool> predicate)
            where TSourceList : IList<TSource>
            where TDestinationList1 : IList<TSource>
            where TDestinationList2 : IList<TSource>
        {
            while (first != past && destinationFirst1 != destinationPast1 && destinationFirst2 != destinationPast2)
            {
                if (predicate(source[first]))
                {
                    destination1[destinationFirst1] = source[first];
                    ++destinationFirst1;
                }
                else
                {
                    destination2[destinationFirst2] = source[first];
                    ++destinationFirst2;
                }
                ++first;
            }
            Tuple<int, int> indexes = copyWhile<TSourceList, TDestinationList1, TSource>(
                source, first, past, 
                destination1, destinationFirst1, destinationPast1, 
                predicate);
            first = indexes.Item1;
            destinationFirst1 = indexes.Item2;
            indexes = copyWhileNot<TSourceList, TDestinationList2, TSource>(
                source, first, past,
                destination2, destinationFirst2, destinationPast2,
                predicate);
            first = indexes.Item1;
            destinationFirst2 = indexes.Item2;
            return new Tuple<int, int, int>(first, destinationFirst1, destinationFirst2);
        }

        internal static int Partition<TSourceList, TSource>(TSourceList list, int first, int past, Func<TSource, bool> predicate)
            where TSourceList : IList<TSource>
        {
            while (true)
            {
                while (first != past && predicate(list[first]))
                {
                    ++first;
                }
                if (first == past)
                {
                    break;
                }
                --past;
                while (first != past && !predicate(list[past]))
                {
                    --past;
                }
                if (first == past)
                {
                    break;
                }
                TSource temp = list[first];
                list[first] = list[past];
                list[past] = temp;
                ++first;
            }
            return first;
        }

        #endregion

        #region PreviousPermutation

        /// <summary>
        /// Arranges the items in a list to the previous lexicographic permutation and returns true. 
        /// If there isn't another permutation, it arranges the sequence to the last permutation and returns false.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to find the previous permutation for.</param>
        /// <returns>
        /// True if the items were arranged to the previous permutation; otherwise, false if the items were rearranged to the last permutation.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>
        /// This algorithm assumes that, in order to enumerate every permutation, 
        /// the list is initially sorted according to the default ordering of the items.
        /// </remarks>
        public static bool PreviousPermutation<TList, T>(this IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return previousPermutation<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Arranges the items in a list to the previous lexicographic permutation and returns true. 
        /// If there isn't another permutation, it arranges the sequence to the last permutation and returns false.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to find the previous permutation for.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>
        /// True if the items were arranged to the previous permutation; otherwise, false if the items were rearranged to the last permutation.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// This algorithm assumes that, in order to enumerate every permutation, 
        /// the list is initially sorted in reverse according to the comparer.
        /// </remarks>
        public static bool PreviousPermutation<TList, T>(this IMutableSublist<TList, T> list, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return previousPermutation<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Arranges the items in a list to the previous lexicographic permutation and returns true. 
        /// If there isn't another permutation, it arranges the sequence to the last permutation and returns false.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to find the previous permutation for.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>
        /// True if the items were arranged to the previous permutation; otherwise, false if the items were rearranged to the last permutation.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// This algorithm assumes that, in order to enumerate every permutation, 
        /// the list is initially sorted in reverse according to the comparer.
        /// </remarks>
        public static bool PreviousPermutation<TList, T>(this IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return previousPermutation<TList, T>(list, comparison);
        }

        private static bool previousPermutation<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            return previousPermutation<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
        }

        private static bool previousPermutation<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int previous = past;
            --previous;
            if (first == past || first == previous)
            {
                return false;
            }
            while (true)
            {
                int previous1 = previous--;
                if (comparison(list[previous1], list[previous]) < 0)
                {
                    int middle = past;
                    --middle;
                    while (comparison(list[middle], list[previous]) >= 0)
                    {
                        --middle;
                    }
                    T temp = list[previous];
                    list[previous] = list[middle];
                    list[middle] = temp;
                    Reverse<TList, T>(list, previous1, past);
                    return true;
                }
                if (previous == first)
                {
                    Reverse<TList, T>(list, first, past);
                    return false;
                }
            }
        }

        #endregion

        #region Sort

        internal static void Sort<TList, T>(TList list, int first, int past, int ideal, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            while (past - first > _sortMax && ideal > 0)
            {
                int middle = partition<TList, T>(list, first, past, comparison);
                ideal /= 2;
                ideal += ideal / 2;
                if (middle - first < past - middle)
                {
                    Sort<TList, T>(list, first, middle, ideal, comparison);
                    first = middle;
                }
                else
                {
                    Sort<TList, T>(list, middle, past, ideal, comparison);
                    past = middle;
                }
            }
            if (past - first > _sortMax)
            {
                MakeHeap<TList, T>(list, first, past, comparison);
                heapSort<TList, T>(list, first, past, comparison);
            }
            else if (past - first > 1)
            {
                insertionSort<TList, T>(list, first, past, comparison);
            }
        }

        private static int partition<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (past != first)
            {
                int middle = first + (past - first) / 2;
                findPivot(list, first, middle, past - 1, comparison);
                T pivot = list[middle];
                while (first < past)
                {
                    while (first != past && comparison(list[first], pivot) < 0)
                    {
                        ++first;
                    }
                    do
                    {
                        --past;
                    }
                    while (first < past && comparison(pivot, list[past]) <= 0);
                    if (first < past)
                    {
                        T temp = list[first];
                        list[first] = list[past];
                        list[past] = temp;
                        ++first;
                    }
                }
            }
            return first;
        }

        private static void findPivot<TList, T>(TList list, int first, int middle, int last, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (last - first <= 40)
            {
                sort3<TList, T>(list, first, middle, last, comparison);
            }
            else
            {
                int step = (last - first + 1) / 8;
                sort3<TList, T>(list, first, first + step, first + 2 * step, comparison);
                sort3<TList, T>(list, middle - step, middle, middle + step, comparison);
                sort3<TList, T>(list, last - 2 * step, last - step, last, comparison);
                sort3<TList, T>(list, first + step, middle, last - step, comparison);
            }
        }

        private static void sort3<TList, T>(TList list, int first, int second, int third, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (comparison(list[second], list[first]) < 0)
            {
                T temp = list[first];
                if (comparison(list[third], list[second]) < 0)
                {
                    list[first] = list[third];
                    list[third] = temp;
                }
                else if (comparison(list[third], temp) < 0)
                {
                    list[first] = list[second];
                    list[second] = list[third];
                    list[third] = temp;
                }
                else
                {
                    list[first] = list[second];
                    list[second] = temp;
                }
            }
            else if (comparison(list[third], list[second]) < 0)
            {
                T temp = list[third];
                list[third] = list[second];
                if (comparison(temp, list[first]) < 0)
                {
                    list[second] = list[first];
                    list[first] = temp;
                }
                else
                {
                    list[second] = temp;
                }
            }
        }

        internal static int AddSort<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int count = past - first;
            growAndShift<TDestinationList, TSource>(destination, destinationPast, count);
            return CopySort<TSourceList, TDestinationList, TSource>(
                source, first, past, 
                destination, destinationPast, destinationPast + count, 
                comparison).Item2;
        }

        internal static Tuple<int, int> CopySort<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            Tuple<int, int> indexes = copyTo<TSourceList, TDestinationList, TSource>(
                source, first, past,
                destination, destinationFirst, destinationPast);
            Sort<TDestinationList, TSource>(destination, destinationFirst, indexes.Item2, indexes.Item2 - destinationFirst, comparison);
            return indexes;
        }

        #endregion

        #region StableSort

        internal static void StableSort<TSourceList, TBufferList, TSource>(
            TSourceList source, int first, int past,
            TBufferList buffer, int bufferFirst, int bufferPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TBufferList : IList<TSource>
        {
            if (buffer.Count < 3)
            {
                insertionSort<TSourceList, TSource>(source, first, past, comparison);
            }
            else
            {
                mergeSort<TSourceList, TBufferList, TSource>(
                    source, first, past,
                    buffer, bufferFirst, bufferPast,
                    comparison);
            }
        }

        private static void mergeSort<TList, TBuffer, T>(
            TList list, int first, int past,
            TBuffer buffer, int bufferFirst, int bufferPast,
            Func<T, T, int> comparison)
            where TList : IList<T>
            where TBuffer : IList<T>
        {
            int count = past - first;
            if (count <= _sortMax)
            {
                insertionSort<TList, T>(list, first, past, comparison);
            }
            else
            {
                int half = count / 2;
                int middle = first + half;
                if (half <= bufferPast - bufferFirst)
                {
                    mergeSortBuffered<TList, TBuffer, T>(list, first, middle, buffer, bufferFirst, bufferPast, comparison);
                    mergeSortBuffered<TList, TBuffer, T>(list, middle, past, buffer, bufferFirst, bufferPast, comparison);
                }
                else
                {
                    mergeSort<TList, TBuffer, T>(list, first, middle, buffer, bufferFirst, bufferPast, comparison);
                    mergeSort<TList, TBuffer, T>(list, middle, past, buffer, bufferFirst, bufferPast, comparison);
                }
                mergeBuffered<TList, TBuffer, T>(list, first, middle, past, buffer, bufferFirst, bufferPast, comparison);
            }
        }

        private static void mergeSortBuffered<TList, TBuffer, T>(
            TList list, int first, int past,
            TBuffer buffer, int bufferFirst, int bufferPast,
            Func<T, T, int> comparison)
            where TList : IList<T>
            where TBuffer : IList<T>
        {
            // first sort each chunk
            int first1 = first;
            for (int past1 = first1 + _sortMax; past1 < past; past1 += _sortMax)
            {
                insertionSort<TList, T>(list, first1, past1, comparison);
                first1 = past1;
                past1 += _sortMax;
            }
            insertionSort<TList, T>(list, first1, past, comparison); // sort any trailing items

            int count = past - first;
            // now we need to merge the chunks
            for (int chunkSize = _sortMax; chunkSize < count; chunkSize *= 2)
            {
                first1 = first;
                int middle = first + chunkSize;
                int bufferMiddle;
                for (int past1 = middle + chunkSize; past1 < past; first1 = middle, middle = past1, past1 += chunkSize)
                {
                    bufferMiddle = copyTo<TList, TBuffer, T>(list, first1, middle, buffer, bufferFirst, bufferPast).Item2;
                    CopyMerged<TBuffer, TList, TList, T>(buffer, bufferFirst, bufferMiddle, list, middle, past1, list, first1, past1, comparison);
                }
                bufferMiddle = copyTo<TList, TBuffer, T>(list, first1, middle, buffer, bufferFirst, bufferPast).Item2;
                CopyMerged<TBuffer, TList, TList, T>(buffer, bufferFirst, bufferMiddle, list, middle, past, list, first1, past, comparison);
            }
        }

        internal static int AddStableSort<TSourceList, TBufferList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TBufferList buffer, int bufferFirst, int bufferPast,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TBufferList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int count = past - first;
            growAndShift<TDestinationList, TSource>(destination, destinationPast, count);
            CopyStableSort<TSourceList, TBufferList, TDestinationList, TSource>(
                source, first, past,
                buffer, bufferFirst, bufferPast,
                destination, destinationPast, destinationPast + count,
                comparison);
            return destinationPast + count;
        }

        internal static Tuple<int, int> CopyStableSort<TSourceList, TBufferList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TBufferList buffer, int bufferFirst, int bufferPast,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TBufferList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            Tuple<int, int> indexes = copyTo<TSourceList, TDestinationList, TSource>(source, first, past, destination, destinationFirst, destinationPast);
            StableSort<TDestinationList, TBufferList, TSource>(
                destination, destinationFirst, indexes.Item2,
                buffer, bufferFirst, bufferPast,
                comparison);
            return indexes;
        }

        #endregion

        #region RandomSamples

        internal static int AddRandomSamples<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            int numberOfSamples,
            Func<int> generator)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            growAndShift<TDestinationList, TSource>(destination, destinationPast, numberOfSamples);
            return CopyRandomSamples<TSourceList, TDestinationList, TSource>(
                source, first, past,
                destination, destinationPast, destinationPast + numberOfSamples,
                generator);
        }

        internal static int CopyRandomSamples<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<int> generator)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            first = copyTo<TSourceList, TDestinationList, TSource>(source, first, past, destination, destinationFirst, destinationPast).Item1;
            int numberOfSamples = destinationPast - destinationFirst;
            int total = numberOfSamples;
            while (first != past)
            {
                ++total;
                int likelihood = generator() % total;
                if (likelihood < 0)
                {
                    likelihood += total;
                }
                if (likelihood < numberOfSamples)
                {
                    destination[destinationFirst + likelihood] = source[first];
                }
                ++first;
            }
            return destinationPast;
        }

        #endregion

        #region RandomShuffle

        internal static void RandomShuffle<TList, T>(TList list, int first, int past, Func<int> generator)
            where TList : IList<T>
        {
            int count = past - first;
            for (int next = first; next != past; ++next)
            {
                int position = generator() % count;
                if (position < 0)
                {
                    position += count;
                }
                position += first;
                T temp = list[next];
                list[next] = list[position];
                list[position] = temp;
            }
        }

        internal static int AddRandomShuffle<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<int> generator)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int count = past - first;
            growAndShift<TDestinationList, TSource>(destination, destinationPast, count);
            CopyRandomShuffle<TSourceList, TDestinationList, TSource>(
                source, first, past,
                destination, destinationPast, destinationPast + count,
                generator);
            return destinationPast + count;
        }

        internal static Tuple<int, int> CopyRandomShuffle<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<int> generator)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int total = 0;
            while (first != past && destinationFirst + total != destinationPast)
            {
                ++total;
                int index = generator() % total;
                if (index < 0)
                {
                    index += total;
                }
                index += destinationFirst;
                destination[destinationFirst + total - 1] = destination[index];
                destination[index] = source[first];
                ++first;
            }
            return new Tuple<int, int>(first, destinationFirst + total);
        }

        #endregion

        #region Replace

        internal static int AddReplace<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<TSource, bool> predicate,
            TSource replacement)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            growAndShift<TDestinationList, TSource>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = CopyReplace<TSourceList, TDestinationList, TSource>(
                source, first, past,
                destination, destinationPast, destination.Count,
                predicate,
                replacement);
            return indexes.Item2;
        }

        internal static Tuple<int, int> CopyReplace<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, bool> predicate,
            TSource replacement)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            while (first != past && destinationFirst != destinationPast)
            {
                if (predicate(source[first]))
                {
                    destination[destinationFirst] = replacement;
                }
                else
                {
                    destination[destinationFirst] = source[first];
                }
                ++first;
                ++destinationFirst;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        internal static int AddReplace<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<TSource, bool> predicate,
            Func<TSource, TSource> generator)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            growAndShift<TDestinationList, TSource>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = CopyReplace<TSourceList, TDestinationList, TSource>(
                source, first, past,
                destination, destinationPast, destination.Count,
                predicate,
                generator);
            return indexes.Item2;
        }

        internal static Tuple<int, int> CopyReplace<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, bool> predicate,
            Func<TSource, TSource> generator)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            while (first != past && destinationFirst != destinationPast)
            {
                if (predicate(source[first]))
                {
                    destination[destinationFirst] = generator(source[first]);
                }
                else
                {
                    destination[destinationFirst] = source[first];
                }
                ++first;
                ++destinationFirst;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        internal static int AddReplace<TSourceList, TSequenceList, TReplacementList, TDestinationList, TSource, TSequence>(
            TSourceList source, int first, int past,
            TSequenceList sequence, int sequenceFirst, int sequencePast,
            TReplacementList replacement, int replacementFirst, int replacementPast,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSequence, bool> comparison)
            where TSourceList : IList<TSource>
            where TSequenceList : IList<TSequence>
            where TReplacementList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int sequenceCount = sequencePast - sequenceFirst;
            int index = findSequence<TSourceList, TSource, TSequenceList, TSequence>(source, first, past, sequence, sequenceFirst, sequencePast, comparison);
            destinationPast = add<TSourceList, TDestinationList, TSource>(source, first, index, destination, destinationPast);

            while (index != past)
            {
                destinationPast = add<TReplacementList, TDestinationList, TSource>(replacement, replacementFirst, replacementPast, destination, destinationPast);
                index += sequenceCount;
                int next = findSequence<TSourceList, TSource, TSequenceList, TSequence>(source, index, past, sequence, sequenceFirst, sequencePast, comparison);
                destinationPast = add<TSourceList, TDestinationList, TSource>(source, index, next, destination, destinationPast);
                index = next;
            }
            return destinationPast;
        }

        internal static Tuple<int, int> CopyReplace<TSourceList, TSequenceList, TReplacementList, TDestinationList, TSource, TSequence>(
            TSourceList source, int first, int past,
            TSequenceList sequence, int sequenceFirst, int sequencePast,
            TReplacementList replacement, int replacementFirst, int replacementPast,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSequence, bool> comparison)
            where TSourceList : IList<TSource>
            where TSequenceList : IList<TSequence>
            where TReplacementList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int sequenceCount = sequencePast - sequenceFirst;
            int replacementCount = replacementPast - replacementFirst;

            int index = findSequence<TSourceList, TSource, TSequenceList, TSequence>(source, first, past, sequence, sequenceFirst, sequencePast, comparison);
            Tuple<int, int> indexes = copyTo<TSourceList, TDestinationList, TSource>(source, first, index, destination, destinationFirst, destinationPast);
            first = indexes.Item1;
            destinationFirst = indexes.Item2;

            while (index != past && destinationFirst + replacementCount <= destinationPast)
            {
                indexes = copyTo<TReplacementList, TDestinationList, TSource>(replacement, replacementFirst, replacementPast, destination, destinationFirst, destinationPast);
                destinationFirst = indexes.Item2;
                index += sequenceCount;

                int next = findSequence<TSourceList, TSource, TSequenceList, TSequence>(source, index, past, sequence, sequenceFirst, sequencePast, comparison);
                indexes = copyTo<TSourceList, TDestinationList, TSource>(source, index, next, destination, destinationFirst, destinationPast);
                first = indexes.Item1;
                destinationFirst = indexes.Item2;
                index = next;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        internal static void Replace<TSourceList, TSource>(TSourceList list, int first, int past, Func<TSource, bool> predicate, TSource replacement)
            where TSourceList : IList<TSource>
        {
            while (first != past)
            {
                if (predicate(list[first]))
                {
                    list[first] = replacement;
                }
                ++first;
            }
        }

        internal static void Replace<TSourceList, TSource>(TSourceList list, int first, int past, Func<TSource, bool> predicate, Func<TSource, TSource> generator)
            where TSourceList : IList<TSource>
        {
            while (first != past)
            {
                if (predicate(list[first]))
                {
                    list[first] = generator(list[first]);
                }
                ++first;
            }
        }

        internal static int Replace<TSourceList, TSequenceList, TReplacementList, TSource, TSequence>(
            TSourceList list, int first, int past,
            TSequenceList sequence, int sequenceFirst, int sequencePast,
            TReplacementList replacement, int replacementFirst, int replacementPast,
            Func<TSource, TSequence, bool> comparison)
            where TSourceList : IList<TSource>
            where TSequenceList : IList<TSequence>
            where TReplacementList : IList<TSource>
        {
            int temp = past;
            int sequenceCount = sequencePast - sequenceFirst;
            int replacementCount = replacementPast - replacementFirst;
            first = findSequence<TSourceList, TSource, TSequenceList, TSequence>(list, first, past, sequence, sequenceFirst, sequencePast, comparison);

            while (first != past)
            {
                if (sequenceCount < replacementCount)
                {
                    int difference = replacementCount - sequenceCount;
                    growAndShift<TSourceList, TSource>(list, first, difference);
                    past += difference;
                }
                else if (sequenceCount > replacementCount)
                {
                    int index = first + sequenceCount;
                    int difference = sequenceCount - replacementCount;
                    copyTo<TSourceList, TSourceList, TSource>(list, index, past, list, index - difference, past);
                    past -= difference;
                }
                first = copyTo<TReplacementList, TSourceList, TSource>(replacement, replacementFirst, replacementPast, list, first, past).Item2;
                first = findSequence<TSourceList, TSource, TSequenceList, TSequence>(list, first, past, sequence, sequenceFirst, sequencePast, comparison);
            }
            if (past < temp)
            {
                removeRange<TSourceList, TSource>(list, past, temp);
            }
            return past;
        }

        #endregion

        #region Reverse

        internal static int AddReversed<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            growAndShift<TDestinationList, TSource>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = CopyReversed<TSourceList, TDestinationList, TSource>(
                source, first, past,
                destination, destinationPast, destination.Count);
            return indexes.Item2;
        }

        internal static Tuple<int, int> CopyReversed<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int count1 = past - first;
            int count2 = destinationPast - destinationFirst;
            if (count2 < count1)
            {
                past -= count1 - count2;
            }
            int position = past;
            while (first != position)
            {
                --position;
                destination[destinationFirst] = source[position];
                ++destinationFirst;
            }
            return new Tuple<int, int>(past, destinationFirst);
        }

        internal static void Reverse<TList, T>(TList list, int first, int past)
            where TList : IList<T>
        {
            int half = first + (past - first) / 2;
            while (first != half)
            {
                --past;
                T temp = list[first];
                list[first] = list[past];
                list[past] = temp;
                ++first;
            }
        }

        #endregion

        #region RotateLeft

        internal static int AddRotatedLeftUnreduced<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            int shift)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int middle = getReducedOffset<TSourceList, TSource>(source, first, past, shift);
            middle += first;
            return addRotatedLeft<TSourceList, TDestinationList, TSource>(source, first, middle, past, destination, destinationPast);
        }

        private static int addRotatedLeft<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int middle, int past,
            TDestinationList destination, int destinationPast)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            growAndShift<TDestinationList, TSource>(destination, destinationPast, past - first);
            destinationPast = copyTo<TSourceList, TDestinationList, TSource>(source, middle, past, destination, destinationPast, destination.Count).Item2;
            destinationPast = copyTo<TSourceList, TDestinationList, TSource>(source, first, middle, destination, destinationPast, destination.Count).Item2;
            return destinationPast;
        }

        internal static Tuple<int, int> CopyRotatedLeftUnreduced<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            int shift)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int middle = getReducedOffset<TSourceList, TSource>(source, first, past, shift);
            middle += first;
            return copyRotatedLeft<TSourceList, TDestinationList, TSource>(
                source, first, middle, past,
                destination, destinationFirst, destinationPast);
        }

        private static Tuple<int, int> copyRotatedLeft<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int middle, int past,
            TDestinationList destination, int destinationFirst, int destinationPast)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            Tuple<int, int> indexes1 = copyTo<TSourceList, TDestinationList, TSource>(
                source, middle, past,
                destination, destinationFirst, destinationPast);
            int position = indexes1.Item1;
            destinationFirst = indexes1.Item2;
            if (position == past)
            {
                Tuple<int, int> indexes2 = copyTo<TSourceList, TDestinationList, TSource>(
                    source, first, middle,
                    destination, destinationFirst, destinationPast);
                position = indexes2.Item1;
                destinationFirst = indexes2.Item2;
            }
            return new Tuple<int, int>(position, destinationFirst);
        }

        internal static void RotateLeftUnreduced<TSourceList, TSource>(TSourceList list, int first, int past, int shift)
            where TSourceList : IList<TSource>
        {
            int middle = getReducedOffset<TSourceList, TSource>(list, first, past, shift);
            middle += first;
            rotateLeft<TSourceList, TSource>(list, first, middle, past);
        }

        private static int getReducedOffset<TList, T>(TList list, int first, int past, int shift)
            where TList : IList<T>
        {
            int count = past - first;
            shift %= count;
            if (shift < 0)
            {
                shift += count;
            }
            return shift;
        }

        private static void rotateLeft<TList, T>(TList list, int first, int middle, int past)
            where TList : IList<T>
        {
            int shift = middle - first;
            int count = past - first;
            for (int factor = shift; factor != 0; )
            {
                int temp = count % factor;
                count = factor;
                factor = temp;
            }
            if (count < past - first)
            {
                while (count > 0)
                {
                    int hole = first + count;
                    T value = list[hole];
                    int temp = hole + shift;
                    int next = temp == past ? first : temp;
                    int current = hole;
                    while (next != hole)
                    {
                        list[current] = list[next];
                        current = next;
                        int difference = past - next;
                        if (shift < difference)
                        {
                            next += shift;
                        }
                        else
                        {
                            next = first + (shift - difference);
                        }
                    }
                    list[current] = value;
                    --count;
                }
            }
        }

        private static int rotateBuffered<TList, TBuffer, T>(
            TList list, int first, int middle, int past,
            TBuffer buffer, int bufferFirst, int bufferPast)
            where TList : IList<T>
            where TBuffer : IList<T>
        {
            int count1 = middle - first;
            int count2 = past - middle;
            int bufferCount = bufferPast - bufferFirst;
            if (count1 <= count2 && count1 <= bufferCount)
            {
                int bufferMiddle = copyTo<TList, TBuffer, T>(list, first, middle, buffer, bufferFirst, bufferPast).Item2;
                copyTo<TList, TList, T>(list, middle, past, list, first, past);
                return copyBackward<TBuffer, TList, T>(buffer, bufferFirst, bufferMiddle, list, first, past);
            }
            else if (count2 <= bufferCount)
            {
                int bufferMiddle = copyTo<TList, TBuffer, T>(list, middle, past, buffer, bufferFirst, bufferPast).Item2;
                copyBackward<TList, TList, T>(list, first, middle, list, first, past);
                return copyTo<TBuffer, TList, T>(buffer, bufferFirst, bufferMiddle, list, first, past).Item2;
            }
            else
            {
                rotateLeft<TList, T>(list, first, middle, past);
                return first + count2;
            }
        }

        #endregion

        #region Select

        internal static int AddSelect<TSourceList, TSource, TDestinationList, TDestination>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<TSource, TDestination> selector)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TDestination>
        {
            growAndShift<TDestinationList, TDestination>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = CopySelect<TSourceList, TSource, TDestinationList, TDestination>(
                source, first, past,
                destination, destinationPast, destination.Count,
                selector);
            return indexes.Item2;
        }

        internal static Tuple<int, int> CopySelect<TSourceList, TSource, TDestinationList, TDestination>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TDestination> selector)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TDestination>
        {
            while (first != past && destinationFirst != destinationPast)
            {
                destination[destinationFirst] = selector(source[first]);
                ++first;
                ++destinationFirst;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        #endregion

        #region SwapWith

        /// <summary>
        /// Swaps the items between two lists.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <returns>The indexes into the list after the last items were swapped.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <remarks>The algorithm will stop when all of the items from the shorter list are swapped.</remarks>
        public static int SwapWith<TList1, TList2, T>(this IMutableSublist<TList1, T> list1, IMutableSublist<TList2, T> list2)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("list1");
            }
            if (list2 == null)
            {
                throw new ArgumentNullException("list2");
            }
            Tuple<int, int> result = swapRanges<TList1, TList2, T>(
                list1.List, list1.Offset, list1.Offset + list1.Count,
                list2.List, list2.Offset, list2.Offset + list2.Count);
            int offset = result.Item1 - list1.Offset;
            return offset;
        }

        private static Tuple<int, int> swapRanges<TList1, TList2, T>(
            TList1 list1, int first1, int past1,
            TList2 list2, int first2, int past2)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            while (first1 != past1 && first2 != past2)
            {
                T temp = list1[first1];
                list1[first1] = list2[first2];
                list2[first2] = temp;
                ++first1;
                ++first2;
            }
            return new Tuple<int, int>(first1, first2);
        }

        #endregion

        #region SymmetricExcept

        internal static int AddSymmetricExcept<TSourceList1, TSourceList2, TDestinationList, TSource>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int pivot = destination.Count;
            while (first1 != past1 && first2 != past2)
            {
                int result = comparison(source1[first1], source2[first2]);
                if (result < 0)
                {
                    destination.Add(source1[first1]);
                    ++first1;
                }
                else if (result > 0)
                {
                    destination.Add(source2[first2]);
                    ++first2;
                }
                else
                {
                    ++first1;
                    ++first2;
                }
            }
            add<TSourceList1, TDestinationList, TSource>(source1, first1, past1, destination, destination.Count);
            add<TSourceList2, TDestinationList, TSource>(source2, first2, past2, destination, destination.Count);
            rotateLeft<TDestinationList, TSource>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        internal static Tuple<int, int, int> CopySymmetricExcept<TSourceList1, TSourceList2, TDestinationList, TSource>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            while (first1 != past1 && first2 != past2 && destinationFirst != destinationPast)
            {
                int result = comparison(source1[first1], source2[first2]);
                if (result < 0)
                {
                    destination[destinationFirst] = source1[first1];
                    ++first1;
                    ++destinationFirst;
                }
                else if (result > 0)
                {
                    destination[destinationFirst] = source2[first2];
                    ++first2;
                    ++destinationFirst;
                }
                else
                {
                    ++first1;
                    ++first2;
                }
            }
            Tuple<int, int> indexes1 = copyTo<TSourceList1, TDestinationList, TSource>(
                source1, first1, past1,
                destination, destinationFirst, destinationPast);
            first1 = indexes1.Item1;
            destinationFirst = indexes1.Item2;
            Tuple<int, int> indexes2 = copyTo<TSourceList2, TDestinationList, TSource>(
                source2, first2, past2,
                destination, destinationFirst, destinationPast);
            first2 = indexes2.Item1;
            destinationFirst = indexes2.Item2;
            return new Tuple<int, int, int>(first1, first2, destinationFirst);
        }

        #endregion

        #region Union

        internal static int AddUnion<TSourceList1, TSourceList2, TDestinationList, TSource>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int pivot = destination.Count;
            while (first1 != past1 && first2 != past2)
            {
                int result = comparison(source1[first1], source2[first2]);
                if (result < 0)
                {
                    destination.Add(source1[first1]);
                    ++first1;
                }
                else if (result > 0)
                {
                    destination.Add(source2[first2]);
                    ++first2;
                }
                else
                {
                    destination.Add(source1[first1]);
                    ++first1;
                    ++first2;
                }
            }
            add<TSourceList1, TDestinationList, TSource>(source1, first1, past1, destination, destination.Count);
            add<TSourceList2, TDestinationList, TSource>(source2, first2, past2, destination, destination.Count);
            rotateLeft<TDestinationList, TSource>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        internal static Tuple<int, int, int> CopyUnion<TSourceList1, TSourceList2, TDestinationList, TSource>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            while (first1 != past1 && first2 != past2 && destinationFirst != destinationPast)
            {
                int result = comparison(source1[first1], source2[first2]);
                if (result < 0)
                {
                    destination[destinationFirst] = source1[first1];
                    ++first1;
                    ++destinationFirst;
                }
                else if (result > 0)
                {
                    destination[destinationFirst] = source2[first2];
                    ++first2;
                    ++destinationFirst;
                }
                else
                {
                    destination[destinationFirst] = source1[first1];
                    ++first1;
                    ++first2;
                    ++destinationFirst;
                }
            }
            Tuple<int, int> indexes1 = copyTo<TSourceList1, TDestinationList, TSource>(
                source1, first1, past1,
                destination, destinationFirst, destinationPast);
            first1 = indexes1.Item1;
            destinationFirst = indexes1.Item2;
            Tuple<int, int> indexes2 = copyTo<TSourceList2, TDestinationList, TSource>(
                source2, first2, past2,
                destination, destinationFirst, destinationPast);
            first2 = indexes2.Item1;
            destinationFirst = indexes2.Item2;
            return new Tuple<int, int, int>(first1, first2, destinationFirst);
        }

        #endregion

        #region UpperBound

        /// <summary>
        /// Finds the last index in a sorted list where the given value would belong.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <returns>The last index in a sorted list where the given value would belong.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>This algorithm assumes that the list is sorted according to the default order of the items.</remarks>
        public static int UpperBound<TList, T>(this IReadOnlySublist<TList, T> list, T value)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return upperBound<TList, T, T>(list, value, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Finds the last index in a sorted list where the given value would belong.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>The last index in a sorted list where the given value would belong.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>This algorithm assumes that the list is sorted according to the given comparer.</remarks>
        public static int UpperBound<TList, T>(this IReadOnlySublist<TList, T> list, T value, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return upperBound<TList, T, T>(list, value, comparer.Compare);
        }

        /// <summary>
        /// Finds the last index in a sorted list where the given value would belong.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <typeparam name="TSearch">The type of the value to search for.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>The last index in a sorted list where the given value would belong.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>This algorithm assumes that the list is sorted according to the given comparison delegate.</remarks>
        public static int UpperBound<TList, T, TSearch>(this IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return upperBound<TList, T, TSearch>(list, value, comparison);
        }

        private static int upperBound<TList, T, TSearch>(IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            int result = upperBound<TList, T, TSearch>(list.List, list.Offset, list.Offset + list.Count, value, comparison);
            result -= list.Offset;
            return result;
        }

        private static int upperBound<TList, T, TSearch>(
            TList list, int first, int past,
            TSearch value,
            Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            int count = past - first;
            while (count > 0)
            {
                int half = count / 2;
                int middle = first + half;
                int result = comparison(list[middle], value);
                if (result > 0)
                {
                    count = half;
                }
                else
                {
                    first = middle + 1;
                    count -= half + 1;
                }
            }
            return first;
        }

        #endregion

        #region Where

        internal static int AddWhere<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<TSource, bool> predicate)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            int pivot = destination.Count;
            while (first != past)
            {
                if (predicate(source[first]))
                {
                    destination.Add(source[first]);
                }
                ++first;
            }
            rotateLeft<TDestinationList, TSource>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        internal static Tuple<int, int> CopyWhere<TSourceList, TDestinationList, TSource>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, bool> predicate)
            where TSourceList : IList<TSource>
            where TDestinationList : IList<TSource>
        {
            while (first != past)
            {
                if (predicate(source[first]))
                {
                    if (destinationFirst == destinationPast)
                    {
                        break;
                    }
                    destination[destinationFirst] = source[first];
                    ++destinationFirst;
                }
                ++first;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        internal static int Where<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
            where TList : IList<T>
        {
            for (int position = first; position != past; ++position)
            {
                if (!predicate(list[position]))
                {
                    list[first] = list[position];
                    ++first;
                }
            }
            return first;
        }

        #endregion

        #region Zip

        internal static int AddZip<TSourceList1, TSource1, TSourceList2, TSource2, TDestinationList, TDestination>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<TSource1, TSource2, TDestination> combiner)
            where TSourceList1 : IList<TSource1>
            where TSourceList2 : IList<TSource2>
            where TDestinationList : IList<TDestination>
        {
            int count = Math.Min(past1 - first1, past2 - first2);
            growAndShift<TDestinationList, TDestination>(destination, destinationPast, count);
            Tuple<int, int, int> indexes = CopyZip<TSourceList1, TSource1, TSourceList2, TSource2, TDestinationList, TDestination>(
                source1, first1, past1,
                source2, first2, past2,
                destination, destinationPast, destination.Count,
                combiner);
            return indexes.Item3;
        }

        internal static Tuple<int, int, int> CopyZip<TSourceList1, TSource1, TSourceList2, TSource2, TDestinationList, TDestination>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource1, TSource2, TDestination> combiner)
            where TSourceList1 : IList<TSource1>
            where TSourceList2 : IList<TSource2>
            where TDestinationList : IList<TDestination>
        {
            while (first1 != past1 && first2 != past2 && destinationFirst != destinationPast)
            {
                destination[destinationFirst] = combiner(source1[first1], source2[first2]);
                ++first1;
                ++first2;
                ++destinationFirst;
            }
            return new Tuple<int, int, int>(first1, first2, destinationFirst);
        }

        #endregion
    }

    /// <summary>
    /// Creates a view into a list starting at an offset and containing a designated number of items.
    /// </summary>
    /// <typeparam name="TList">The type of the list to wrap.</typeparam>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(SublistDebugView<,>))]
    public sealed class Sublist<TList, T> : IExpandableSublist<TList, T>
        where TList : IList<T>
    {
        private readonly TList _list;
        private readonly int _offset;
        private readonly int _count;

        /// <summary>
        /// Initializes a new instance of a Sublist representing a splice containing the entire list.
        /// </summary>
        /// <param name="list">The list to wrap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public Sublist(TList list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            _list = list;
            _offset = 0;
            _count = list.Count;
        }

        /// <summary>
        /// Initializes a new instance of a Sublist representing a splice starting at the given offset and containing
        /// the remaining items.
        /// </summary>
        /// <param name="list">The list to wrap.</param>
        /// <param name="offset">The index into the list to treat as the start of the splice.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The offset is negative -or- outside the bounds of the list.</exception>
        public Sublist(TList list, int offset)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (offset < 0 || offset > list.Count)
            {
                throw new ArgumentOutOfRangeException("offset", offset, Resources.IndexOutOfRange);
            }
            _list = list;
            _offset = offset;
            _count = list.Count - offset;
        }

        /// <summary>
        /// Initializes a new instance of a Sublist representing a splice starting at the given offset and containing
        /// count items.
        /// </summary>
        /// <param name="list">The list to wrap.</param>
        /// <param name="offset">The index into the list to treat as the start of the splice.</param>
        /// <param name="count">The number of items to include in the splice.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The offset is negative -or- outside the bounds of the list.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The count is negative.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The count is greater than the remaining items in the list.</exception>
        public Sublist(TList list, int offset, int count)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (offset < 0 || offset > list.Count)
            {
                throw new ArgumentOutOfRangeException("offset", offset, Resources.IndexOutOfRange);
            }
            if (count < 0 || count > list.Count - offset)
            {
                throw new ArgumentOutOfRangeException("count", count, Resources.CountOutOfRange);
            }
            _list = list;
            _offset = offset;
            _count = count;
        }

        /// <summary>
        /// Creates a new Sublist that acts as a splice into the Sublist, starting at the given offset.
        /// </summary>
        /// <param name="offset">The offset into the Sublist to start the new splice.</param>
        /// <returns>A new Sublist starting at the given offset into the Sublist, consisting of the remaining items.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The offset is negative -or- outside the bounds of the Sublist.</exception>
        public Sublist<TList, T> Nest(int offset)
        {
            if (offset < 0 || offset > _count)
            {
                throw new ArgumentOutOfRangeException("offset", offset, Resources.IndexOutOfRange);
            }
            return new Sublist<TList, T>(_list, offset + _offset, _count - offset);
        }

        IExpandableSublist<TList, T> IExpandableSublist<TList, T>.Nest(int offset)
        {
            return Nest(offset);
        }

        IMutableSublist<TList, T> IMutableSublist<TList, T>.Nest(int offset)
        {
            return Nest(offset);
        }

        IReadOnlySublist<TList, T> IReadOnlySublist<TList, T>.Nest(int offset)
        {
            return Nest(offset);
        }

        /// <summary>
        /// Creates a new Sublist that acts as a splice into the Sublist, starting at the given offset, spanning
        /// the number of items specified by the count.
        /// </summary>
        /// <param name="offset">The offset into the Sublist to start the new splice.</param>
        /// <param name="count">The number of items to include in the splice.</param>
        /// <returns>
        /// A new Sublist starting at the given offset into the Sublist, spanning the number of items specified by the count.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The offset is negative -or- outside the bounds of the Sublist.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The count is negative -or-  beyond the bounds of the Sublist.
        /// </exception>
        public Sublist<TList, T> Nest(int offset, int count)
        {
            if (offset < 0 || offset > _count)
            {
                throw new ArgumentOutOfRangeException("offset", offset, Resources.IndexOutOfRange);
            }
            if (count < 0 || offset + count > _count)
            {
                throw new ArgumentOutOfRangeException("count", count, Resources.CountOutOfRange);
            }
            return new Sublist<TList, T>(_list, offset + _offset, count);
        }

        IExpandableSublist<TList, T> IExpandableSublist<TList, T>.Nest(int offset, int count)
        {
            return Nest(offset, count);
        }

        IMutableSublist<TList, T> IMutableSublist<TList, T>.Nest(int offset, int count)
        {
            return Nest(offset, count);
        }

        IReadOnlySublist<TList, T> IReadOnlySublist<TList, T>.Nest(int offset, int count)
        {
            return Nest(offset, count);
        }

        /// <summary>
        /// Attempts to shift the sublist to the right by the given shift.
        /// If the shift is negative, the sublist is shifted to the left.
        /// The sublist will be automatically resized if it is too big.
        /// </summary>
        /// <param name="shift">The amount to shift the sublist to the right.</param>
        /// <param name="isChecked">If checked, an exception will be thrown if the sublist would extend beyond the list.</param>
        /// <returns>True if the sublist remained the size; otherwise, false if the sublist shrank.</returns>
        public Sublist<TList, T> Shift(int shift, bool isChecked)
        {
            int newOffset = _offset + shift;
            if (newOffset < 0 || newOffset > _list.Count)
            {
                throw new ArgumentOutOfRangeException("shift", shift, Resources.IndexOutOfRange);
            }
            int newCount = _count;
            if (newOffset + newCount > _list.Count)
            {
                if (isChecked)
                {
                    throw new ArgumentOutOfRangeException("shift", shift, Resources.IndexOutOfRange);
                }
                newCount = _list.Count - newOffset;
            }
            return new Sublist<TList, T>(_list, newOffset, newCount);
        }

        IExpandableSublist<TList, T> IExpandableSublist<TList, T>.Shift(int shift, bool isChecked)
        {
            return Shift(shift, isChecked);
        }

        IMutableSublist<TList, T> IMutableSublist<TList, T>.Shift(int shift, bool isChecked)
        {
            return Shift(shift, isChecked);
        }

        IReadOnlySublist<TList, T> IReadOnlySublist<TList, T>.Shift(int shift, bool isChecked)
        {
            return Shift(shift, isChecked);
        }

        /// <summary>
        /// Attempts to resize the sublist so that its count equals the given limit.
        /// If the limit is too large, the count gets as large as it can.
        /// </summary>
        /// <param name="size">The desired length of the sublist.</param>
        /// <param name="isChecked">If checked, an exception will be thrown if the sublist would be too large.</param>
        /// <returns>True if the sublist fit in the list; otherwise, false.</returns>
        public Sublist<TList, T> Resize(int size, bool isChecked)
        {
            int newCount = size;
            if (newCount < 0)
            {
                throw new ArgumentOutOfRangeException("size", size, Resources.CountOutOfRange);
            }
            if (newCount > _count && _offset + newCount > _list.Count)
            {
                if (isChecked)
                {
                    throw new ArgumentOutOfRangeException("size", size, Resources.CountOutOfRange);
                }
                newCount = _list.Count - _offset;
            }
            return new Sublist<TList, T>(_list, _offset, newCount);
        }

        IExpandableSublist<TList, T> IExpandableSublist<TList, T>.Resize(int size, bool isChecked)
        {
            return Resize(size, isChecked);
        }

        IMutableSublist<TList, T> IMutableSublist<TList, T>.Resize(int size, bool isChecked)
        {
            return Resize(size, isChecked);
        }

        IReadOnlySublist<TList, T> IReadOnlySublist<TList, T>.Resize(int size, bool isChecked)
        {
            return Resize(size, isChecked);
        }

        /// <summary>
        /// Gets the underlying list.
        /// </summary>
        public TList List 
        { 
            get { return _list; } 
        }

        /// <summary>
        /// Gets or sets the offset into the underlying list.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The value is negative -or- outside the bounds of the underlying list.
        /// </exception>
        public int Offset
        {
            get { return _offset; }
        }

        /// <summary>
        /// Gets or sets the number of items to include in the Sublist.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The value is negative -or- outside the bounds of the underlying list.
        /// </exception>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// Gets or sets the item at the given index.
        /// </summary>
        /// <param name="index">The index into the Sublist to get the item.</param>
        /// <returns>The item at the given index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- beyond the bounds of the list.</exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
                }
                return _list[_offset + index];
            }
            set
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
                }
                _list[_offset + index] = value;
            }
        }

        /// <summary>
        /// Gets an enumerator that iterates through the list.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int index = 0; index < _count; ++index)
            {
                yield return _list[index + _offset];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
