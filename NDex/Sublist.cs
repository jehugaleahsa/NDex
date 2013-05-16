using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using NDex.Properties;

namespace NDex
{
    #region AddPartitionedResult

    /// <summary>
    /// Holds the results of the AddPartitioned method.
    /// </summary>
    public sealed class AddPartitionedResult<TDestinationList1, TDestinationList2, T>
    {
        /// <summary>
        /// Initializes a new instance of an AddPartitionedResult.
        /// </summary>
        internal AddPartitionedResult()
        {
        }

        /// <summary>
        /// Gets the subset wrapping the first destination list.
        /// </summary>
        public IExpandableSublist<TDestinationList1, T> Destination1
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the subset wrapping the second destination list.
        /// </summary>
        public IExpandableSublist<TDestinationList2, T> Destination2
        {
            get;
            internal set;
        }
    }

    #endregion

    #region BinarySearchResult

    /// <summary>
    /// Holds the results of the BinarySearch methods.
    /// </summary>
    public sealed class BinarySearchResult
    {
        /// <summary>
        /// Initializes a new instance of a BinarySearchResult.
        /// </summary>
        internal BinarySearchResult()
        {
        }

        /// <summary>
        /// Gets the first index where the search value was found, if it exists, or belongs, if it does not exist.
        /// </summary>
        public int Index
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets whether the value was found in the list.
        /// </summary>
        public bool Exists
        {
            get;
            internal set;
        }

        /// <summary>
        /// Implicitly converts the result to a boolean, representing whether the search value was found
        /// by the BinarySearch algorithm.
        /// </summary>
        /// <param name="result">The BinarySearchResult to convert.</param>
        /// <returns>True if the given BinarySearchResult indicates that the value was found; otherwise, false.</returns>
        public static implicit operator bool(BinarySearchResult result)
        {
            return result.Exists;
        }

        /// <summary>
        /// Implicitly converts the result to an integer, representing the first index of the value, if it exists, 
        /// or where it would belong, if it does not exist.
        /// </summary>
        /// <param name="result">The BinarySearchResult to convert.</param>
        /// <returns>True if the given BinarySearchResult indicates that the value was found; otherwise, false.</returns>
        public static implicit operator int(BinarySearchResult result)
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

    #region CopyResult

    /// <summary>
    /// Holds the results of a copy method with one source list.
    /// </summary>
    public sealed class CopyResult
    {
        /// <summary>
        /// Initializes a new instance of a CopyResult.
        /// </summary>
        internal CopyResult()
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
        public static implicit operator int(CopyResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region CopyTwoSourcesResult

    /// <summary>
    /// Holds the results of a copy operation with two source lists.
    /// </summary>
    public sealed class CopyTwoSourcesResult
    {
        /// <summary>
        /// Initializes a new instance of a CopyTwoSourcesResult.
        /// </summary>
        internal CopyTwoSourcesResult()
        {
        }

        /// <summary>
        /// Gets the index into the first source list where the algorithm stopped.
        /// </summary>
        public int SourceOffset1
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the index into the second source list where the algorithm stopped.
        /// </summary>
        public int SourceOffset2
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
        public static implicit operator int(CopyTwoSourcesResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region CopyPartitionedResult

    /// <summary>
    /// Holds the indexes of a copy method with two destination lists.
    /// </summary>
    public sealed class CopyPartitionedResult
    {
        /// <summary>
        /// Initializes a new instance of a CopyPartitionedResult.
        /// </summary>
        internal CopyPartitionedResult()
        {
        }

        /// <summary>
        /// Gets the index into the source list where the items stopped being copied.
        /// </summary>
        public int SourceOffset
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the index into the first destination list.
        /// </summary>
        public int DestinationOffset1
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the index into the second destination list.
        /// </summary>
        public int DestinationOffset2
        {
            get;
            internal set;
        }
    }

    #endregion

    #region FindResult

    /// <summary>
    /// Holds the results of a find method.
    /// </summary>
    public sealed class FindResult
    {
        /// <summary>
        /// Initializes a new instance of a FindResult.
        /// </summary>
        internal FindResult()
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
        /// <param name="result">The FindResult to convert.</param>
        /// <returns>True if the given result indicates that the value was found; otherwise, false.</returns>
        public static implicit operator bool(FindResult result)
        {
            return result.Exists;
        }

        /// <summary>
        /// Implicitly converts the result to an integer, representing the first index of the value, if it exists,
        /// or an index past the end of the list if it is missing.
        /// </summary>
        /// <param name="result">The FindResult to convert.</param>
        /// <returns>True if the given result indicates that the value was found; otherwise, false.</returns>
        public static implicit operator int(FindResult result)
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

    /// <summary>
    /// Provides methods for creating instances of Sublist.
    /// </summary>
    public static class Sublist
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

        #region Add

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
        public static IExpandableSublist<TDestinationList, T> Add<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
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
            Tuple<int, int> indexes = copy<TSourceList, TDestinationList, T>(
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
        public static IExpandableSublist<TDestinationList, T> Add<TDestinationList, T>(
            IEnumerable<T> source, 
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
            return copy<TDestinationList, T>(source, destination, past, destination.Count);
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

        #region AddCombined

        /// <summary>
        /// Combines the items from two lists and adds the results to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="T1">The type of the items in the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="T2">The type of the items in the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="TDest">The type of the items in the destination list.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The destination list.</param>
        /// <param name="combiner">A delegate used to combine the items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The combiner delegate is null.</exception>
        /// <remarks>
        /// The destination Sublist is resized as necessary to hold the added items.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, TDest> AddCombined<TSourceList1, T1, TSourceList2, T2, TDestinationList, TDest>(
            IReadOnlySublist<TSourceList1, T1> source1,
            IReadOnlySublist<TSourceList2, T2> source2,
            IExpandableSublist<TDestinationList, TDest> destination,
            Func<T1, T2, TDest> combiner)
            where TSourceList1 : IList<T1>
            where TSourceList2 : IList<T2>
            where TDestinationList : IList<TDest>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (combiner == null)
            {
                throw new ArgumentNullException("combiner");
            }
            int result = addCombined<TSourceList1, T1, TSourceList2, T2, TDestinationList, TDest>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset + destination.Count,
                combiner);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addCombined<TSourceList1, T1, TSourceList2, T2, TDestinationList, TDest>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<T1, T2, TDest> combiner)
            where TSourceList1 : IList<T1>
            where TSourceList2 : IList<T2>
            where TDestinationList : IList<TDest>
        {
            int count = Math.Min(past1 - first1, past2 - first2);
            growAndShift<TDestinationList, TDest>(destination, destinationPast, count);
            Tuple<int, int, int> indexes = copyCombined<TSourceList1, T1, TSourceList2, T2, TDestinationList, TDest>(
                source1, first1, past1,
                source2, first2, past2,
                destination, destinationPast, destination.Count,
                combiner);
            return indexes.Item3;
        }

        #endregion

        #region AddConverted

        /// <summary>
        /// Converts items from a list and adds the results to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="TDest">The type of the items in the destination list.</typeparam>
        /// <param name="source">The list of items to convert.</param>
        /// <param name="destination">The list to add the converted items to.</param>
        /// <param name="converter">The conversion delegate to convert instances of T to TDest.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        public static IExpandableSublist<TDestinationList, TDest> AddConverted<TSourceList, T, TDestinationList, TDest>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, TDest> destination,
            Func<T, TDest> converter)
            where TSourceList : IList<T>
            where TDestinationList : IList<TDest>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (converter == null)
            {
                throw new ArgumentNullException("converter");
            }
            return addConverted<TSourceList, T, TDestinationList, TDest>(source, destination, converter);
        }

        private static IExpandableSublist<TDestinationList, TDest> addConverted<TSourceList, T, TDestinationList, TDest>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, TDest> destination,
            Func<T, TDest> converter)
            where TSourceList : IList<T>
            where TDestinationList : IList<TDest>
        {
            int result = addConverted<TSourceList, T, TDestinationList, TDest>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                converter);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addConverted<TSourceList, T, TDestinationList, TDest>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<T, TDest> converter)
            where TSourceList : IList<T>
            where TDestinationList : IList<TDest>
        {
            growAndShift<TDestinationList, TDest>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = copyConverted<TSourceList, T, TDestinationList, TDest>(
                source, first, past,
                destination, destinationPast, destination.Count,
                converter);
            return indexes.Item2;
        }

        #endregion

        #region AddDifference

        /// <summary>
        /// Adds the items from the first list that are not in a second list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The destination list.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted the same and must not contain duplicates.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return addDifference(source1, source2, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Adds the items from the first list that are not in a second list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The destination list.</param>
        /// <param name="comparer">The comparer to use for comparing items in the lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer and must not contain duplicates.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return addDifference(source1, source2, destination, comparer.Compare);
        }

        /// <summary>
        /// Adds the items from the first list that are not in a second list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The destination list.</param>
        /// <param name="comparison">The comparison delegate used for comparing items in the lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate and must not contain duplicates.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addDifference(source1, source2, destination, comparison);
        }

        private static IExpandableSublist<TDestinationList, T> addDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addDifference<TSourceList1, TSourceList2, TDestinationList, T>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
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
            add<TSourceList1, TDestinationList, T>(source1, first1, past1, destination, destination.Count);
            rotateLeft<TDestinationList, T>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        #endregion

        #region AddGenerated

        /// <summary>
        /// Copies the given value into the list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to fill.</param>
        /// <param name="numberOfItems">The number of items to add.</param>
        /// <param name="value">The value to fill the list with.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>If T is a reference type, each item in the list will refer to the same instance.</remarks>
        public static IExpandableSublist<TList, T> AddGenerated<TList, T>(IExpandableSublist<TList, T> list, int numberOfItems, T value)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (numberOfItems < 0)
            {
                string message = String.Format(CultureInfo.CurrentCulture, Resources.TooSmall, 0);
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, message);
            }
            int past = list.Offset + list.Count;
            addGenerated<TList, T>(list.List, past, past + numberOfItems, value);
            return list.Resize(list.Count + numberOfItems, true);
        }

        private static void addGenerated<TList, T>(TList list, int first, int past, T value)
            where TList : IList<T>
        {
            while (first != past)
            {
                list.Insert(first, value);
                ++first;
            }
        }

        /// <summary>
        /// Copies the result of each call to the generator into the list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to fill.</param>
        /// <param name="numberOfItems">The number of items to add.</param>
        /// <param name="generator">The delegate to use to fill the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator delegate is null.</exception>
        /// <remarks>The generator will be called to set each item in the list.</remarks>
        public static IExpandableSublist<TList, T> AddGenerated<TList, T>(IExpandableSublist<TList, T> list, int numberOfItems, Func<T> generator)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (numberOfItems < 0)
            {
                string message = String.Format(CultureInfo.CurrentCulture, Resources.TooSmall, 0);
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, message);
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            int past = list.Offset + list.Count;
            addGenerated<TList, T>(list.List, past, past + numberOfItems, generator);
            return list.Resize(list.Count + numberOfItems, true);
        }

        private static void addGenerated<TList, T>(TList list, int first, int past, Func<T> generator)
            where TList : IList<T>
        {
            while (first != past)
            {
                list.Insert(first, generator());
                ++first;
            }
        }

        /// <summary>
        /// Copies the result of each call to the generator into the list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to fill.</param>
        /// <param name="numberOfItems">The number of items to add.</param>
        /// <param name="generator">The delegate to use to fill the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator delegate is null.</exception>
        /// <remarks>
        /// The generator will be called to set each item in the list. 
        /// The relative index of the item is passed with each call to the generator.
        /// </remarks>
        public static IExpandableSublist<TList, T> AddGenerated<TList, T>(IExpandableSublist<TList, T> list, int numberOfItems, Func<int, T> generator)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (numberOfItems < 0)
            {
                string message = String.Format(CultureInfo.CurrentCulture, Resources.TooSmall, 0);
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, message);
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            int past = list.Offset + list.Count;
            addGenerated<TList, T>(list.List, past, past + numberOfItems, generator);
            return list.Resize(list.Count + numberOfItems, true);
        }

        private static void addGenerated<TList, T>(TList list, int first, int past, Func<int, T> generator)
            where TList : IList<T>
        {
            int adjustment = first;
            while (first != past)
            {
                list.Insert(first, generator(first - adjustment));
                ++first;
            }
        }

        #endregion

        #region AddIf

        /// <summary>
        /// Adds the items from a list that satisfy the predicate to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to conditionally add.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="predicate">The condition an item must satisfy to be added to the destination.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddIf<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, bool> predicate)
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
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            int result = addIf<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                predicate);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addIf<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<T, bool> predicate)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
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
            rotateLeft<TDestinationList, T>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        #endregion

        #region AddIntersection

        /// <summary>
        /// Adds the items that appear in both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to add the intersecting items to.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>
        /// The items in the lists must be sorted according to the default ordering of the items. Neither of
        /// the lists can contain duplicate items.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddIntersection<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return addIntersection<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Adds the items that appear in both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to add the intersecting items to.</param>
        /// <param name="comparer">The comparer to use to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer and must not contain duplicates.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddIntersection<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return addIntersection<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparer.Compare);
        }

        /// <summary>
        /// Adds the items that appear in both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to add the intersecting items to.</param>
        /// <param name="comparison">The delegate used to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate and must not contain duplicates.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddIntersection<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addIntersection<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparison);
        }

        private static IExpandableSublist<TDestinationList, T> addIntersection<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addIntersection<TSourceList1, TSourceList2, TDestinationList, T>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addIntersection<TSourceList1, TSourceList2, TDestinationList, T>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
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
            rotateLeft<TDestinationList, T>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        #endregion

        #region AddMerged

        /// <summary>
        /// Merges the items from two lists such that they remain in sorted order, adding the items
        /// to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list to merge.</param>
        /// <param name="source2">The second list to merge.</param>
        /// <param name="destination">The list to add the merged items to.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>
        /// The items in the lists must be sorted according to the default ordering of the items.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddMerged<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return addMerged<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Merges the items from two lists such that they remain in sorted order, adding the items
        /// to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list to merge.</param>
        /// <param name="source2">The second list to merge.</param>
        /// <param name="destination">The list to add the merged items to.</param>
        /// <param name="comparer">The comparer to use to compare items from the lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddMerged<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return addMerged<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparer.Compare);
        }

        /// <summary>
        /// Merges the items from two lists such that they remain in sorted order, adding the items
        /// to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list to merge.</param>
        /// <param name="source2">The second list to merge.</param>
        /// <param name="destination">The list to add the merged items to.</param>
        /// <param name="comparison">The delegate to use to compare items from the lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddMerged<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addMerged<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparison);
        }

        private static IExpandableSublist<TDestinationList, T> addMerged<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addMerged<TSourceList1, TSourceList2, TDestinationList, T>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addMerged<TSourceList1, TSourceList2, TDestinationList, T>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            growAndShift<TDestinationList, T>(destination, destinationPast, (past1 - first1) + (past2 - first2));
            Tuple<int, int, int> indexes = copyMerged<TSourceList1, TSourceList2, TDestinationList, T>(
                source1, first1, past1,
                source2, first2, past2,
                destination, destinationPast, destination.Count,
                comparison);
            return indexes.Item3;
        }

        #endregion

        #region AddPartiallySorted

        /// <summary>
        /// Adds the given number of items from a list to a destination list as if the source list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to be added.</param>
        /// <param name="numberOfItems">The number of items to add to the destination.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The specified number of items is larger than the source list.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>
        /// The items in the list will be sorted according to the default ordering of the items.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddPartiallySorted<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            int numberOfItems,
            IExpandableSublist<TDestinationList, T> destination)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfItems < 0 || numberOfItems > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, Resources.IndexOutOfRange);
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return addPartiallySorted<TSourceList, TDestinationList, T>(source, numberOfItems, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Adds the given number of items from a list to a destination list as if the source list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to be added.</param>
        /// <param name="numberOfItems">The number of items to add to the destination.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="comparer">The comparer to use to compare items in the source list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The specified number of items is larger than the source list.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddPartiallySorted<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            int numberOfItems,
            IExpandableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfItems < 0 || numberOfItems > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, Resources.IndexOutOfRange);
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return addPartiallySorted<TSourceList, TDestinationList, T>(source, numberOfItems, destination, comparer.Compare);
        }

        /// <summary>
        /// Adds the given number of items from a list to a destination list as if the source list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to be added.</param>
        /// <param name="numberOfItems">The number of items to add to the destination.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="comparison">The delegate used to compare items in the source list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The specified number of items is larger than the source list.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddPartiallySorted<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            int numberOfItems,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfItems < 0 || numberOfItems > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, Resources.IndexOutOfRange);
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addPartiallySorted<TSourceList, TDestinationList, T>(source, numberOfItems, destination, comparison);
        }

        private static IExpandableSublist<TDestinationList, T> addPartiallySorted<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            int numberOfItems,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addPartiallySorted<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + numberOfItems, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addPartiallySorted<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int middle, int past,
            TDestinationList destination, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int count = middle - first;
            growAndShift<TDestinationList, T>(destination, destinationPast, count);
            return copyPartiallySorted<TSourceList, TDestinationList, T>(
                source, first, past,
                destination, destinationPast, destinationPast + count,
                comparison);
        }

        #endregion

        #region AddPartitioned

        /// <summary>
        /// Adds the items from a list satisfying the predicate to the first destination list and the remaining
        /// to the second destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList1">The type of the first destination list.</typeparam>
        /// <typeparam name="TDestinationList2">The type of the second destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to partition.</param>
        /// <param name="destination1">The list to add the items that satisfy the predicate.</param>
        /// <param name="destination2">The list to add the items that don't satisfy the predicate.</param>
        /// <param name="predicate">The condition an item must satisfy to be added to the first destination list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The first destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static AddPartitionedResult<TDestinationList1, TDestinationList2, T> AddPartitioned<TSourceList, TDestinationList1, TDestinationList2, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList1, T> destination1,
            IExpandableSublist<TDestinationList2, T> destination2,
            Func<T, bool> predicate)
            where TSourceList : IList<T>
            where TDestinationList1 : IList<T>
            where TDestinationList2 : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination1 == null)
            {
                throw new ArgumentNullException("destination1");
            }
            if (destination2 == null)
            {
                throw new ArgumentNullException("destination2");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            Tuple<int, int> indexes = addPartitioned<TSourceList, TDestinationList1, TDestinationList2, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination1.List, destination1.Offset + destination1.Count,
                destination2.List, destination2.Offset + destination2.Count,
                predicate);
            AddPartitionedResult<TDestinationList1, TDestinationList2, T> result = new AddPartitionedResult<TDestinationList1, TDestinationList2, T>();
            result.Destination1 = destination1.Resize(indexes.Item1 - destination1.Offset, true);
            result.Destination2 = destination2.Resize(indexes.Item2 - destination2.Offset, true);
            return result;
        }

        private static Tuple<int, int> addPartitioned<TSourceList, TDestinationList1, TDestinationList2, T>(
            TSourceList source, int first, int past,
            TDestinationList1 destination1, int destinationPast1,
            TDestinationList2 destination2, int destinationPast2,
            Func<T, bool> predicate)
            where TSourceList : IList<T>
            where TDestinationList1 : IList<T>
            where TDestinationList2 : IList<T>
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
            rotateLeft<TDestinationList1, T>(destination1, destinationPast1, pivot1, destination1.Count);
            rotateLeft<TDestinationList2, T>(destination2, destinationPast2, pivot2, destination2.Count);
            destinationPast1 += destination1.Count - pivot1;
            destinationPast2 += destination2.Count - pivot2;
            return new Tuple<int, int>(destinationPast1, destinationPast2);
        }

        #endregion

        #region AddRandomSamples

        /// <summary>
        /// Randomly adds the requested number of items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items to randomly choose values from.</param>
        /// <param name="numberOfSamples">The number of items to add to the destination.</param>
        /// <param name="destination">The list to add items to.</param>
        /// <param name="random">The random number generator to use.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The number of samples is negative -or- larger than the size of the list.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">The random number generator is null.</exception>
        /// <remarks>The order that the items appear in the destination is not guaranteed.</remarks>
        public static IExpandableSublist<TDestinationList, T> AddRandomSamples<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            int numberOfSamples,
            IExpandableSublist<TDestinationList, T> destination,
            Random random)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfSamples < 0 || numberOfSamples > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfSamples", numberOfSamples, Resources.IndexOutOfRange);
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (random == null)
            {
                throw new ArgumentNullException("random");
            }
            return addRandomSamples<TSourceList, TDestinationList, T>(source, numberOfSamples, destination, random.Next);
        }

        /// <summary>
        /// Randomly adds the requested number of items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items to randomly choose values from.</param>
        /// <param name="numberOfSamples">The number of items to add to the destination.</param>
        /// <param name="destination">The list to add items to.</param>
        /// <param name="generator">The random number generator to use.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The number of samples is negative -or- larger than the size of the list.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">The random number generator is null.</exception>
        /// <remarks>The order that the items appear in the destination is not guaranteed.</remarks>
        public static IExpandableSublist<TDestinationList, T> AddRandomSamples<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            int numberOfSamples,
            IExpandableSublist<TDestinationList, T> destination,
            Func<int> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfSamples < 0 || numberOfSamples > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfSamples", numberOfSamples, Resources.IndexOutOfRange);
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            return addRandomSamples<TSourceList, TDestinationList, T>(source, numberOfSamples, destination, generator);
        }

        private static IExpandableSublist<TDestinationList, T> addRandomSamples<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            int numberOfSamples,
            IExpandableSublist<TDestinationList, T> destination,
            Func<int> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addRandomSamples<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                numberOfSamples,
                generator);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addRandomSamples<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            int numberOfSamples,
            Func<int> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            growAndShift<TDestinationList, T>(destination, destinationPast, numberOfSamples);
            return copyRandomSamples<TSourceList, TDestinationList, T>(
                source, first, past,
                destination, destinationPast, destinationPast + numberOfSamples,
                generator);
        }

        #endregion

        #region AddReplaced

        /// <summary>
        /// Adds the items from a list to a destination list, replacing any items satisfying the predicate with the given value.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to add.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="predicate">The condition an item must satisfy to be replaced.</param>
        /// <param name="replacement">The value to replace items satisfying the predicate with.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddReplaced<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, bool> predicate,
            T replacement)
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
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            int result = addReplaced<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                predicate,
                replacement);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addReplaced<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<T, bool> predicate,
            T replacement)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            growAndShift<TDestinationList, T>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = copyReplaced<TSourceList, TDestinationList, T>(
                source, first, past,
                destination, destinationPast, destination.Count,
                predicate,
                replacement);
            return indexes.Item2;
        }

        /// <summary>
        /// Adds the items from a list to a destination list, replacing any items satisfying the predicate with the result of the generator.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to add.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="predicate">The condition an item must satisfy to be replaced.</param>
        /// <param name="generator">The delegate to use to generate the replacements.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddReplaced<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, bool> predicate,
            Func<T, T> generator)
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
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            int result = addReplaced<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                predicate,
                generator);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addReplaced<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<T, bool> predicate,
            Func<T, T> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {

            growAndShift<TDestinationList, T>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = copyReplaced<TSourceList, TDestinationList, T>(
                source, first, past,
                destination, destinationPast, destination.Count,
                predicate,
                generator);
            return indexes.Item2;
        }

        /// <summary>
        /// Adds the items in the source to the destination, replacing all occurrences of the sequence with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequence">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacement">The type of the replacement list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The source of the items to add to the destination.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The replacement items.</param>
        /// <param name="destination">The list to add the values to.</param>
        /// <returns>A new sublist wrapping the destination and the new items.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequence, T> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IExpandableSublist<TDestinationList, T> destination)
            where TSourceList : IList<T>
            where TSequence : IList<T>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return addReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T, T>(
                source, 
                sequence, 
                replacement, 
                destination, 
                EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Adds the items in the source to the destination, replacing all occurrences of the sequence with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequence">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacement">The type of the replacement list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The source of the items to add to the destination.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The replacement items.</param>
        /// <param name="destination">The list to add the values to.</param>
        /// <param name="comparer">The comparer to use to find the sequences.</param>
        /// <returns>A new sublist wrapping the destination and the new items.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequence, T> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IExpandableSublist<TDestinationList, T> destination,
            IEqualityComparer<T> comparer)
            where TSourceList : IList<T>
            where TSequence : IList<T>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T, T>(source, sequence, replacement, destination, comparer.Equals);
        }

        /// <summary>
        /// Adds the items in the source to the destination, replacing all occurrences of the sequence with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequenceList">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacement">The type of the replacement list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <typeparam name="TSequence">The type of the items in the sequence.</typeparam>
        /// <param name="source">The source of the items to add to the destination.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The replacement items.</param>
        /// <param name="destination">The list to add the values to.</param>
        /// <param name="comparison">The function to use to find the sequences.</param>
        /// <returns>A new sublist wrapping the destination and the new items.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, TSequence, bool> comparison)
            where TSourceList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(source, sequence, replacement, destination, comparison);
        }

        private static IExpandableSublist<TDestinationList, T> addReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, TSequence, bool> comparison)
            where TSourceList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
                source.List, source.Offset, source.Offset + source.Count,
                sequence.List, sequence.Offset, sequence.Offset + sequence.Count,
                replacement.List, replacement.Offset, replacement.Offset + replacement.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
            TSourceList source, int first, int past,
            TSequenceList sequence, int sequenceFirst, int sequencePast,
            TReplacement replacement, int replacementFirst, int replacementPast,
            TDestinationList destination, int destinationPast,
            Func<T, TSequence, bool> comparison)
            where TSourceList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            int sequenceCount = sequencePast - sequenceFirst;
            int index = indexOfSequence<TSourceList, T, TSequenceList, TSequence>(source, first, past, sequence, sequenceFirst, sequencePast, comparison);
            destinationPast = add<TSourceList, TDestinationList, T>(source, first, index, destination, destinationPast);

            while (index != past)
            {
                destinationPast = add<TReplacement, TDestinationList, T>(replacement, replacementFirst, replacementPast, destination, destinationPast);
                index += sequenceCount;
                int next = indexOfSequence<TSourceList, T, TSequenceList, TSequence>(source, index, past, sequence, sequenceFirst, sequencePast, comparison);
                destinationPast = add<TSourceList, TDestinationList, T>(source, index, next, destination, destinationPast);
                index = next;
            }
            return destinationPast;
        }

        #endregion

        #region AddReversed

        /// <summary>
        /// Adds the items in a list in reverse to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list whose items are to be added in reverse.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddReversed<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination)
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
            int result = addReversed<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addReversed<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            growAndShift<TDestinationList, T>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = CopyReversed<TSourceList, TDestinationList, T>(
                source, first, past,
                destination, destinationPast, destination.Count);
            return indexes.Item2;
        }

        #endregion

        #region AddRotatedLeft

        /// <summary>
        /// Adds the items from a list to a destination, rotated to the left by the given shift amount.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="shift">The amount to rotate the items to the left.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <remarks>
        /// If the shift is negative, the algoritm simulates rotating the items to the right. If the shift is larger than the number of items, 
        /// the algorithm will simulate a complete rotation as many times as necessary.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddRotatedLeft<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination, int shift)
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
            int result = addRotatedLeftUnreduced<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                shift);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addRotatedLeftUnreduced<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            int shift)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int middle = getReducedOffset<TSourceList, T>(source, first, past, shift);
            middle += first;
            return addRotatedLeft<TSourceList, TDestinationList, T>(source, first, middle, past, destination, destinationPast);
        }

        private static int addRotatedLeft<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int middle, int past,
            TDestinationList destination, int destinationPast)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            growAndShift<TDestinationList, T>(destination, destinationPast, past - first);
            destinationPast = copy<TSourceList, TDestinationList, T>(source, middle, past, destination, destinationPast, destination.Count).Item2;
            destinationPast = copy<TSourceList, TDestinationList, T>(source, first, middle, destination, destinationPast, destination.Count).Item2;
            return destinationPast;
        }

        #endregion

        #region AddSymmetricDifference

        /// <summary>
        /// Adds the items that do not appear in both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the third list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>
        /// The items in the lists must be sorted according to the default ordering of the items. Neither of
        /// the lists can contain duplicate items.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddSymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return addSymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Adds the items that do not appear in both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="comparer">The comparer to use to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer and must not contain duplicates.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddSymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return addSymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparer.Compare);
        }

        /// <summary>
        /// Adds the items that do not appear in both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="comparison">The delegate used to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate and must not contain duplicates.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddSymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addSymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparison);
        }

        private static IExpandableSublist<TDestinationList, T> addSymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addSymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addSymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
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
            add<TSourceList1, TDestinationList, T>(source1, first1, past1, destination, destination.Count);
            add<TSourceList2, TDestinationList, T>(source2, first2, past2, destination, destination.Count);
            rotateLeft<TDestinationList, T>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        #endregion

        #region AddUnion

        /// <summary>
        /// Adds the items that are unique across both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the third list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>
        /// The items in the lists must be sorted according to the default ordering of the items. Neither of
        /// the lists can contain duplicate items.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return addUnion<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Adds the items that are unique across both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="comparer">The comparer to use to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer and must not contain duplicates.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return addUnion<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparer.Compare);
        }

        /// <summary>
        /// Adds the items that are unique across both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="comparison">The delegate used to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate and must not contain duplicates.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addUnion<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparison);
        }

        private static IExpandableSublist<TDestinationList, T> addUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addUnion<TSourceList1, TSourceList2, TDestinationList, T>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
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
            add<TSourceList1, TDestinationList, T>(source1, first1, past1, destination, destination.Count);
            add<TSourceList2, TDestinationList, T>(source2, first2, past2, destination, destination.Count);
            rotateLeft<TDestinationList, T>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        #endregion

        #region AddUnique

        /// <summary>
        /// Adds the unique items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to add.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>The items in the list must be sorted according to the default ordering of the items.</remarks>
        public static IExpandableSublist<TDestinationList, T> AddUnique<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination)
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
            return addUnique<TSourceList, TDestinationList, T>(source, destination, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Adds the unique items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to add.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>The list must be sorted.</remarks>
        public static IExpandableSublist<TDestinationList, T> AddUnique<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination,
            IEqualityComparer<T> comparer)
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
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return addUnique<TSourceList, TDestinationList, T>(source, destination, comparer.Equals);
        }

        /// <summary>
        /// Adds the unique items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to add.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="comparison">The comparison delegate to use to compare items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>The list must be sorted.</remarks>
        public static IExpandableSublist<TDestinationList, T> AddUnique<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, bool> comparison)
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
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addUnique<TSourceList, TDestinationList, T>(source, destination, comparison);
        }

        private static IExpandableSublist<TDestinationList, T> addUnique<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, bool> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addUnique<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addUnique<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<T, T, bool> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
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
            rotateLeft<TDestinationList, T>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
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
            IReadOnlySublist<TList, T> list,
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
            IReadOnlySublist<TList, T> list,
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

        #region AreDisjoint

        /// <summary>
        /// Determines if two lists share no items.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of items in both lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <returns>True if no items are shared between the lists.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <remarks>
        /// This algorithm assumes that both lists are sorted according to the default order of the items.
        /// Both lists must contain distinct values.
        /// </remarks>
        public static bool AreDisjoint<TList1, TList2, T>(IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
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
            return areDisjoint<TList1, T, TList2, T>(list1, list2, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Determines if two sorted lists share no equivilent items.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparer">Compares an item from the first list to an item in the second list.</param>
        /// <returns>True if no items are equivilents between the lists.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// This algorithm assumes that the lists are sorted using a meaningful ordering that applies to both lists and that the
        /// comparer respects that order. Both lists must contain distinct values.
        /// </remarks>
        public static bool AreDisjoint<TList1, TList2, T>(
            IReadOnlySublist<TList1, T> list1,
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
            return areDisjoint<TList1, T, TList2, T>(list1, list2, comparer.Compare);
        }

        /// <summary>
        /// Determines if two sorted lists share no equivilent items.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="T1">The type of the items in the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T2">The type of the items in the second list.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparison">Compares an item from the first list to an item in the second list.</param>
        /// <returns>True if no items are equivilents between the lists.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// This algorithm assumes that the lists are sorted using a meaningful ordering that applies to both lists and that the
        /// comparison delegate respects that order. Both lists must contain distinct values.
        /// </remarks>
        public static bool AreDisjoint<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
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
            return areDisjoint<TList1, T1, TList2, T2>(list1, list2, comparison);
        }

        private static bool areDisjoint<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            return areDisjoint<TList1, T1, TList2, T2>(
                list1.List, list1.Offset, list1.Offset + list1.Count,
                list2.List, list2.Offset, list2.Offset + list2.Count,
                comparison);
        }

        private static bool areDisjoint<TList1, T1, TList2, T2>(
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
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region AreEqual

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
        public static bool AreEqual<TList1, TList2, T>(IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
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
            return areEqual<TList1, T, TList2, T>(list1, list2, EqualityComparer<T>.Default.Equals);
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
        public static bool AreEqual<TList1, TList2, T>(
            IReadOnlySublist<TList1, T> list1,
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
            return areEqual<TList1, T, TList2, T>(list1, list2, comparer.Equals);
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
        public static bool AreEqual<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
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
            return areEqual<TList1, T1, TList2, T2>(list1, list2, comparison);
        }

        private static bool areEqual<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            return areEqual_optimized(
                list1.List, list1.Offset, list1.Offset + list1.Count,
                list2.List, list2.Offset, list2.Offset + list2.Count,
                comparison);
        }

        private static bool areEqual_optimized<TList1, T1, TList2, T2>(
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
        public static BinarySearchResult BinarySearch<TList, T>(IReadOnlySublist<TList, T> list, T value)
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
        public static BinarySearchResult BinarySearch<TList, T>(IReadOnlySublist<TList, T> list, T value, IComparer<T> comparer)
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
        public static BinarySearchResult BinarySearch<TList, T, TSearch>(IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, int> comparison)
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

        private static BinarySearchResult binarySearch<TList, T, TSearch>(IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, int> comparison)
            where TList : IList<T>
        {
            int lowerBound = binarySearch<TList, T, TSearch>(
                list.List, list.Offset, list.Offset + list.Count,
                value,
                comparison);
            if (lowerBound < 0)
            {
                return new BinarySearchResult() { Exists = false, Index = ~lowerBound - list.Offset };
            }
            else
            {
                return new BinarySearchResult { Exists = true, Index = lowerBound - list.Offset };
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

        #region BubbleSort

        /// <summary>
        /// Sorts a list using the bubble sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list to sort.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static void BubbleSort<TList, T>(IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            bubbleSort<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Sorts a list using the bubble sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list to sort.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static void BubbleSort<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
            bubbleSort<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Sorts a list using the bubble sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list to sort.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparison">The comparison delegate to use to compare items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static void BubbleSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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
            bubbleSort<TList, T>(list, comparison);
        }

        private static void bubbleSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            bubbleSort<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
        }

        private static void bubbleSort<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            bool swapped = true;
            for (int back = past; first + 1 < back && swapped; --back)
            {
                swapped = false;
                for (int current = first, next = first + 1; next != back; current = next, ++next)
                {
                    if (comparison(list[current], list[next]) > 0)
                    {
                        T temp = list[current];
                        list[current] = list[next];
                        list[next] = temp;
                        swapped = true;
                    }
                }
            }
        }

        #endregion

        #region Compare

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
        public static int Compare<TList1, TList2, T>(IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
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
            return compare<TList1, T, TList2, T>(list1, list2, Comparer<T>.Default.Compare);
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
        public static int Compare<TList1, TList2, T>(
            IReadOnlySublist<TList1, T> list1,
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
            return compare<TList1, T, TList2, T>(list1, list2, comparer.Compare);
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
        public static int Compare<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
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
            return compare<TList1, T1, TList2, T2>(list1, list2, comparison);
        }

        private static int compare<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            return compare<TList1, T1, TList2, T2>(
                list1.List, list1.Offset, list1.Offset + list1.Count,
                list2.List, list2.Offset, list2.Offset + list2.Count,
                comparison);
        }

        private static int compare<TList1, T1, TList2, T2>(
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

        #region Copy

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
        public static CopyResult Copy<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
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
            Tuple<int, int> indexes = copy<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count);
            CopyResult result = new CopyResult()
            {
                SourceOffset = indexes.Item1 - source.Offset,
                DestinationOffset = indexes.Item2 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int> copy<TSourceList, TDestinationList, T>(
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
        public static int Copy<TDestinationList, T>(IEnumerable<T> source, IMutableSublist<TDestinationList, T> destination)
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
            int index = copy<TDestinationList, T>(source, destination.List, destination.Offset, destination.Offset + destination.Count);
            index -= destination.Offset;
            return index;
        }

        private static int copy<TDestinationList, T>(
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

        #region CopyCombined

        /// <summary>
        /// Combines the values from two lists and stores the results in a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="T1">The type of the items in the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="T2">The type of the items in the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="TDest">The type of the items in the destination list.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The destination list.</param>
        /// <param name="combiner">The delegate to use to combine items from the first and second list.</param>
        /// <returns>The index into the destination list after the last combined item.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The combiner is null.</exception>
        public static CopyTwoSourcesResult CopyCombined<TSourceList1, T1, TSourceList2, T2, TDestinationList, TDest>(
            IReadOnlySublist<TSourceList1, T1> source1,
            IReadOnlySublist<TSourceList2, T2> source2,
            IMutableSublist<TDestinationList, TDest> destination,
            Func<T1, T2, TDest> combiner)
            where TSourceList1 : IList<T1>
            where TSourceList2 : IList<T2>
            where TDestinationList : IList<TDest>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (combiner == null)
            {
                throw new ArgumentNullException("combiner");
            }
            Tuple<int, int, int> indexes = copyCombined<TSourceList1, T1, TSourceList2, T2, TDestinationList, TDest>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                combiner);
            CopyTwoSourcesResult result = new CopyTwoSourcesResult()
            {
                SourceOffset1 = indexes.Item1 - source1.Offset,
                SourceOffset2 = indexes.Item2 - source2.Offset,
                DestinationOffset = indexes.Item3 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int, int> copyCombined<TSourceList1, T1, TSourceList2, T2, TDestinationList, TDest>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T1, T2, TDest> combiner)
            where TSourceList1 : IList<T1>
            where TSourceList2 : IList<T2>
            where TDestinationList : IList<TDest>
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

        #region CopyConverted

        /// <summary>
        /// Converts items from a list and stores the results in a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="TDestination">The type of the items in the destination list.</typeparam>
        /// <param name="source">The list of items to convert.</param>
        /// <param name="destination">The list to store the results of the conversion.</param>
        /// <param name="converter">The conversion delegate to convert instances of T to TDest.</param>
        /// <returns>The index into the destination past the last set item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        public static CopyResult CopyConverted<TSourceList, T, TDestinationList, TDestination>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, TDestination> destination,
            Func<T, TDestination> converter)
            where TSourceList : IList<T>
            where TDestinationList : IList<TDestination>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (converter == null)
            {
                throw new ArgumentNullException("converter");
            }
            return copyConverted<TSourceList, T, TDestinationList, TDestination>(source, destination, converter);
        }

        private static CopyResult copyConverted<TSourceList, T, TDestinationList, TDestination>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, TDestination> destination,
            Func<T, TDestination> converter)
            where TSourceList : IList<T>
            where TDestinationList : IList<TDestination>
        {
            Tuple<int, int> indexes = copyConverted<TSourceList, T, TDestinationList, TDestination>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                converter);
            CopyResult result = new CopyResult()
            {
                SourceOffset = indexes.Item1 - source.Offset,
                DestinationOffset = indexes.Item2 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int> copyConverted<TSourceList, T, TDestinationList, TDestination>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, TDestination> converter)
            where TSourceList : IList<T>
            where TDestinationList : IList<TDestination>
        {
            while (first != past && destinationFirst != destinationPast)
            {
                destination[destinationFirst] = converter(source[first]);
                ++first;
                ++destinationFirst;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        #endregion

        #region CopyDifference

        /// <summary>
        /// Copies the items in the first list that are not in the second list to the destination.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <returns>The index into the destination past the last item copies.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted the same and must not contain duplicates.
        /// </remarks>
        public static CopyTwoSourcesResult CopyDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return copyDifference<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Copies the items in the first list that are not in the second list to the destination.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <param name="comparer">The comparer to use to compare items from the first and second lists.</param>
        /// <returns>The index into the destination past the last item copies.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer and must not contain duplicates.
        /// </remarks>
        public static CopyTwoSourcesResult CopyDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return copyDifference<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparer.Compare);
        }

        /// <summary>
        /// Copies the items in the first list that are not in the second list to the destination.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <param name="comparison">The delegate used to compare items from the first and second lists.</param>
        /// <returns>The index into the destination past the last item copies.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate and must not contain duplicates.
        /// </remarks>
        public static CopyTwoSourcesResult CopyDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copyDifference<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparison);
        }

        private static CopyTwoSourcesResult copyDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            Tuple<int, int, int> indexes = copyDifference<TSourceList1, TSourceList2, TDestinationList, T>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            CopyTwoSourcesResult result = new CopyTwoSourcesResult()
            {
                SourceOffset1 = indexes.Item1 - source1.Offset,
                SourceOffset2 = indexes.Item2 - source2.Offset,
                DestinationOffset = indexes.Item3 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int, int> copyDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
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
            Tuple<int, int> indexes = copy<TSourceList1, TDestinationList, T>(
                source1, first1, past1,
                destination, destinationFirst, destinationPast);
            first1 = indexes.Item1;
            destinationFirst = indexes.Item2;
            return new Tuple<int, int, int>(first1, first2, destinationFirst);
        }

        #endregion

        #region CopyGenerated

        /// <summary>
        /// Copies the given value into the list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to fill.</param>
        /// <param name="value">The value to fill the list with.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>If T is a reference type, each item in the list will refer to the same instance.</remarks>
        public static void CopyGenerated<TList, T>(IMutableSublist<TList, T> list, T value)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            copyGenerated<TList, T>(list.List, list.Offset, list.Offset + list.Count, value);
        }

        private static void copyGenerated<TList, T>(TList list, int first, int past, T value)
            where TList : IList<T>
        {
            while (first != past)
            {
                list[first] = value;
                ++first;
            }
        }

        /// <summary>
        /// Copies the result of each call to the generator into the list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to fill.</param>
        /// <param name="generator">The delegate to use to fill the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator delegate is null.</exception>
        /// <remarks>The generator will be called to set each item in the list.</remarks>
        public static void CopyGenerated<TList, T>(IMutableSublist<TList, T> list, Func<T> generator)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            copyGenerated<TList, T>(list.List, list.Offset, list.Offset + list.Count, generator);
        }

        private static void copyGenerated<TList, T>(TList list, int first, int past, Func<T> generator)
            where TList : IList<T>
        {
            while (first != past)
            {
                list[first] = generator();
                ++first;
            }
        }

        /// <summary>
        /// Copies the result of each call to the generator into the list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to fill.</param>
        /// <param name="generator">The delegate to use to fill the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator delegate is null.</exception>
        /// <remarks>
        /// The generator will be called to set each item in the list. 
        /// The relative index of the item is passed with each call to the generator.
        /// </remarks>
        public static void CopyGenerated<TList, T>(IMutableSublist<TList, T> list, Func<int, T> generator)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            copyGenerated<TList, T>(list.List, list.Offset, list.Offset + list.Count, generator);
        }

        private static void copyGenerated<TList, T>(TList list, int first, int past, Func<int, T> generator)
            where TList : IList<T>
        {
            int adjustment = first;
            while (first != past)
            {
                list[first] = generator(first - adjustment);
                ++first;
            }
        }

        #endregion

        #region CopyIf

        /// <summary>
        /// Copies the items from a list satisfying the predicate to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to copy.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <param name="predicate">The delegate used to determine whether to copy an item.</param>
        /// <returns>The index into the destination past the last item copied.</returns>
        public static CopyResult CopyIf<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, bool> predicate)
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
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            Tuple<int, int> indexes = copyIf<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                predicate);
            CopyResult result = new CopyResult()
            {
                SourceOffset = indexes.Item1 - source.Offset,
                DestinationOffset = indexes.Item2 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int> copyIf<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, bool> predicate)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
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

        #endregion

        #region CopyIntersection

        /// <summary>
        /// Copies the items that appear in both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the intersecting items to.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>
        /// The items in the lists must be sorted according to the default ordering of the items. Neither of
        /// the lists can contain duplicate items.
        /// </remarks>
        public static CopyTwoSourcesResult CopyIntersection<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return copyIntersection<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Copies the items that appear in both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the intersecting items to.</param>
        /// <param name="comparer">The comparer to use to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer and must not contain duplicates.
        /// </remarks>
        public static CopyTwoSourcesResult CopyIntersection<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return copyIntersection<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparer.Compare);
        }

        /// <summary>
        /// Copies the items that appear in both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the intersecting items to.</param>
        /// <param name="comparison">The delegate used to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate and must not contain duplicates.
        /// </remarks>
        public static CopyTwoSourcesResult CopyIntersection<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copyIntersection<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparison);
        }

        private static CopyTwoSourcesResult copyIntersection<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            Tuple<int, int, int> indexes = copyIntersection<TSourceList1, TSourceList2, TDestinationList, T>(
                source1.List, source2.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            CopyTwoSourcesResult result = new CopyTwoSourcesResult()
            {
                SourceOffset1 = indexes.Item1 - source1.Offset,
                SourceOffset2 = indexes.Item2 - source2.Offset,
                DestinationOffset = indexes.Item3 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int, int> copyIntersection<TSourceList1, TSourceList2, TDestinationList, T>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
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

        #region CopyMerged

        /// <summary>
        /// Merges the items from two lists such that they remain in sorted order, copying the items
        /// to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list to merge.</param>
        /// <param name="source2">The second list to merge.</param>
        /// <param name="destination">The list to copy the merged items to.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>
        /// The items in the lists must be sorted according to the default ordering of the items.
        /// </remarks>
        public static CopyTwoSourcesResult CopyMerged<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return copyMerged<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Merges the items from two lists such that they remain in sorted order, copying the items
        /// to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list to merge.</param>
        /// <param name="source2">The second list to merge.</param>
        /// <param name="destination">The list to copy the merged items to.</param>
        /// <param name="comparer">The comparer to use to compare items from the lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer.
        /// </remarks>
        public static CopyTwoSourcesResult CopyMerged<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return copyMerged<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparer.Compare);
        }

        /// <summary>
        /// Merges the items from two lists such that they remain in sorted order, copying the items
        /// to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list to merge.</param>
        /// <param name="source2">The second list to merge.</param>
        /// <param name="destination">The list to copy the merged items to.</param>
        /// <param name="comparison">The delegate to use to compare items from the lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate.
        /// </remarks>
        public static CopyTwoSourcesResult CopyMerged<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copyMerged<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparison);
        }

        private static CopyTwoSourcesResult copyMerged<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            Tuple<int, int, int> indexes = copyMerged<TSourceList1, TSourceList2, TDestinationList, T>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            CopyTwoSourcesResult result = new CopyTwoSourcesResult()
            {
                SourceOffset1 = indexes.Item1 - source1.Offset,
                SourceOffset2 = indexes.Item2 - source2.Offset,
                DestinationOffset = indexes.Item3 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int, int> copyMerged<TSourceList1, TSourceList2, TDestinationList, T>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
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
            Tuple<int, int> indexes1 = copy<TSourceList1, TDestinationList, T>(
                source1, first1, past1,
                destination, destinationFirst, destinationPast);
            first1 = indexes1.Item1;
            destinationFirst = indexes1.Item2;
            Tuple<int, int> indexes2 = copy<TSourceList2, TDestinationList, T>(
                source2, first2, past2,
                destination, destinationFirst, destinationPast);
            first2 = indexes2.Item1;
            destinationFirst = indexes2.Item2;
            return new Tuple<int, int, int>(first1, first2, destinationFirst);
        }

        #endregion

        #region CopyPartiallySorted

        /// <summary>
        /// Copies the items of a list to a destination list as if they were sorted prior to being copied.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <returns>The index into the destination past the last item copied.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <remarks>
        /// If the destination is large enough to hold all of the items in the list, this method is equivilent to calling Copy,
        /// followed by a HeapSort.
        /// </remarks>
        public static CopyResult CopyPartiallySorted<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
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
            return copyPartiallySorted<TSourceList, TDestinationList, T>(source, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Copies the items of a list to a destination list as if they were sorted prior to being copied.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="comparer">The comparer to use to compare two items.</param>
        /// <returns>The index into the destination past the last item copied.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// If the destination is large enough to hold all of the items in the list, this method is equivilent to calling Copy,
        /// followed by a HeapSort.
        /// </remarks>
        public static CopyResult CopyPartiallySorted<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
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
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return copyPartiallySorted<TSourceList, TDestinationList, T>(source, destination, comparer.Compare);
        }

        /// <summary>
        /// Copies the items of a list to a destination list as if they were sorted prior to being copied.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="comparison">The comparison delegate to use to compare two items.</param>
        /// <returns>The index into the destination past the last item copied.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// If the destination is large enough to hold all of the items in the list, this method is equivilent to calling Copy,
        /// followed by a HeapSort.
        /// </remarks>
        public static CopyResult CopyPartiallySorted<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
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
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copyPartiallySorted<TSourceList, TDestinationList, T>(source, destination, comparison);
        }

        private static CopyResult copyPartiallySorted<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int index = copyPartiallySorted<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            CopyResult result = new CopyResult()
            {
                SourceOffset = source.Count,
                DestinationOffset = index - destination.Offset,
            };
            return result;
        }

        private static int copyPartiallySorted<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int destinationMiddle = destinationFirst;
            while (first != past && destinationMiddle != destinationPast)
            {
                destination[destinationMiddle] = source[first];
                ++first;
                ++destinationMiddle;
            }
            makeHeap<TDestinationList, T>(destination, destinationFirst, destinationMiddle, comparison);

            int numberOfItems = destinationMiddle - destinationFirst;
            while (first != past)
            {
                if (comparison(source[first], destination[destinationFirst]) < 0)
                {
                    adjustHeap<TDestinationList, T>(destination, destinationFirst, 0, numberOfItems, source[first], comparison);
                }
                ++first;
            }
            heapSort<TDestinationList, T>(destination, destinationFirst, destinationMiddle, comparison);
            return destinationMiddle;
        }

        #endregion

        #region CopyPartitioned

        /// <summary>
        /// Copies the items from a list satisfying the predicate into the first destination list and the remaining
        /// into the second destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList1">The type of the first destination list.</typeparam>
        /// <typeparam name="TDestinationList2">The type of the second destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to partition.</param>
        /// <param name="destination1">The list to copy the items that satisfy the predicate.</param>
        /// <param name="destination2">The list to copy the items that don't satisfy the predicate.</param>
        /// <param name="predicate">The condition an item must satisfy to be copied to the first destination list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The first destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static CopyPartitionedResult CopyPartitioned<TSourceList, TDestinationList1, TDestinationList2, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList1, T> destination1,
            IMutableSublist<TDestinationList2, T> destination2,
            Func<T, bool> predicate)
            where TSourceList : IList<T>
            where TDestinationList1 : IList<T>
            where TDestinationList2 : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination1 == null)
            {
                throw new ArgumentNullException("destination1");
            }
            if (destination2 == null)
            {
                throw new ArgumentNullException("destination2");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            Tuple<int, int, int> indexes = copyPartitioned<TSourceList, TDestinationList1, TDestinationList2, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination1.List, destination1.Offset, destination1.Offset + destination1.Count,
                destination2.List, destination2.Offset, destination2.Offset + destination2.Count,
                predicate);
            CopyPartitionedResult result = new CopyPartitionedResult()
            {
                SourceOffset = indexes.Item1 - source.Offset,
                DestinationOffset1 = indexes.Item2 - destination1.Offset,
                DestinationOffset2 = indexes.Item3 - destination2.Offset
            };
            return result;
        }

        private static Tuple<int, int, int> copyPartitioned<TSourceList, TDestinationList1, TDestinationList2, T>(
            TSourceList source, int first, int past,
            TDestinationList1 destination1, int destinationFirst1, int destinationPast1,
            TDestinationList2 destination2, int destinationFirst2, int destinationPast2,
            Func<T, bool> predicate)
            where TSourceList : IList<T>
            where TDestinationList1 : IList<T>
            where TDestinationList2 : IList<T>
        {
            while (first != past)
            {
                if (predicate(source[first]))
                {
                    if (destinationFirst1 == destinationPast1)
                    {
                        break;
                    }
                    destination1[destinationFirst1] = source[first];
                    ++destinationFirst1;
                }
                else
                {
                    if (destinationFirst2 == destinationPast2)
                    {
                        break;
                    }
                    destination2[destinationFirst2] = source[first];
                    ++destinationFirst2;
                }
                ++first;
            }
            return new Tuple<int, int, int>(first, destinationFirst1, destinationFirst2);
        }

        #endregion

        #region CopyRandomSamples

        /// <summary>
        /// Randomly copies items from a list to fill a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items to randomly choose values from.</param>
        /// <param name="destination">The list to copy items to.</param>
        /// <param name="random">The random number generator to use.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The random number generator is null.</exception>
        /// <remarks>The order that the items appear in the destination is not guaranteed.</remarks>
        public static CopyResult CopyRandomSamples<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Random random)
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
            if (random == null)
            {
                throw new ArgumentNullException("random");
            }
            return copyRandomSamples<TSourceList, TDestinationList, T>(source, destination, random.Next);
        }

        /// <summary>
        /// Randomly copies items from a list to fill a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items to randomly choose values from.</param>
        /// <param name="destination">The list to copy items to.</param>
        /// <param name="generator">The random number generator to use.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The random number generator is null.</exception>
        /// <remarks>The order that the items appear in the destination is not guaranteed.</remarks>
        public static CopyResult CopyRandomSamples<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Func<int> generator)
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
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            return copyRandomSamples<TSourceList, TDestinationList, T>(source, destination, generator);
        }

        private static CopyResult copyRandomSamples<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Func<int> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int index = copyRandomSamples<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                generator);
            CopyResult result = new CopyResult()
            {
                SourceOffset = source.Count,
                DestinationOffset = index - destination.Offset,
            };
            return result;
        }

        private static int copyRandomSamples<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<int> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            for (int index = destinationFirst; first != past && index != destinationPast; ++index)
            {
                destination[index] = source[first];
                ++first;
            }
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

        #region CopyReplaced

        /// <summary>
        /// Copies a list, replacing items satisfying the predicate with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="predicate">The predicate used to determine whether an item should be replaced.</param>
        /// <param name="replacement">The value to use to replace items satisfying the predicate.</param>
        /// <returns>The index into the destination past the last copied item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static CopyResult CopyReplaced<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, bool> predicate,
            T replacement)
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
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            Tuple<int, int> indexes = copyReplaced<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                predicate,
                replacement);
            CopyResult result = new CopyResult()
            {
                SourceOffset = indexes.Item1 - source.Offset,
                DestinationOffset = indexes.Item2 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int> copyReplaced<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, bool> predicate,
            T replacement)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
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

        /// <summary>
        /// Copies a list, replacing items satisfying the predicate with the result of the generator delegate.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="predicate">The predicate used to determine whether an item should be replaced.</param>
        /// <param name="generator">The delegate to use to generate the replacement value.</param>
        /// <returns>The index into the destination past the last copied item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator delegate is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static CopyResult CopyReplaced<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, bool> predicate,
            Func<T, T> generator)
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
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            Tuple<int, int> indexes = copyReplaced<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                predicate,
                generator);
            CopyResult result = new CopyResult()
            {
                SourceOffset = indexes.Item1 - source.Offset,
                DestinationOffset = indexes.Item2 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int> copyReplaced<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, bool> predicate,
            Func<T, T> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
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

        /// <summary>
        /// Copies the items in the source to the destination, replacing all occurrences of the sequence with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequence">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacement">The type of the replacement list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The source of the items to copy to the destination.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The replacement items.</param>
        /// <param name="destination">The list to copy the values to.</param>
        /// <returns>A result holding the indexes into the source and destination where the copying stopped.</returns>
        public static CopyResult CopyReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequence, T> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IMutableSublist<TDestinationList, T> destination)
            where TSourceList : IList<T>
            where TSequence : IList<T>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return copyReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T, T>(
                source,
                sequence,
                replacement,
                destination,
                EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Copies the items in the source to the destination, replacing all occurrences of the sequence with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequence">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacement">The type of the replacement list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The source of the items to copy to the destination.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The replacement items.</param>
        /// <param name="destination">The list to copy the values to.</param>
        /// <param name="comparer">The comparer to use to find the sequences.</param>
        /// <returns>A result holding the indexes into the source and destination where the copying stopped.</returns>
        public static CopyResult CopyReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequence, T> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IMutableSublist<TDestinationList, T> destination,
            IEqualityComparer<T> comparer)
            where TSourceList : IList<T>
            where TSequence : IList<T>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copyReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T, T>(source, sequence, replacement, destination, comparer.Equals);
        }

        /// <summary>
        /// Copies the items in the source to the destination, replacing all occurrences of the sequence with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequenceList">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacement">The type of the replacement list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <typeparam name="TSequence">The type of the items in the sequence.</typeparam>
        /// <param name="source">The source of the items to copy to the destination.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The replacement items.</param>
        /// <param name="destination">The list to copy the values to.</param>
        /// <param name="comparison">The function to use to find the sequences.</param>
        /// <returns>A result holding the indexes into the source and destination where the copying stopped.</returns>
        public static CopyResult CopyReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, TSequence, bool> comparison)
            where TSourceList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copyReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(source, sequence, replacement, destination, comparison);
        }

        private static CopyResult copyReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, TSequence, bool> comparison)
            where TSourceList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            Tuple<int, int> indexes = copyReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
                source.List, source.Offset, source.Offset + source.Count,
                sequence.List, sequence.Offset, sequence.Offset + sequence.Count,
                replacement.List, replacement.Offset, replacement.Offset + replacement.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            CopyResult result = new CopyResult();
            result.SourceOffset = indexes.Item1 - source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }

        private static Tuple<int, int> copyReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
            TSourceList source, int first, int past,
            TSequenceList sequence, int sequenceFirst, int sequencePast,
            TReplacement replacement, int replacementFirst, int replacementPast,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, TSequence, bool> comparison)
            where TSourceList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            int sequenceCount = sequencePast - sequenceFirst;
            int replacementCount = replacementPast - replacementFirst;

            int index = indexOfSequence<TSourceList, T, TSequenceList, TSequence>(source, first, past, sequence, sequenceFirst, sequencePast, comparison);
            Tuple<int, int> indexes = copy<TSourceList, TDestinationList, T>(source, first, index, destination, destinationFirst, destinationPast);
            first = indexes.Item1;
            destinationFirst = indexes.Item2;

            while (index != past && destinationFirst + replacementCount <= destinationPast)
            {
                indexes = copy<TReplacement, TDestinationList, T>(replacement, replacementFirst, replacementPast, destination, destinationFirst, destinationPast);
                destinationFirst = indexes.Item2;
                index += sequenceCount;

                int next = indexOfSequence<TSourceList, T, TSequenceList, TSequence>(source, index, past, sequence, sequenceFirst, sequencePast, comparison);
                indexes = copy<TSourceList, TDestinationList, T>(source, index, next, destination, destinationFirst, destinationPast);
                first = indexes.Item1;
                destinationFirst = indexes.Item2;
                index = next;
            }
            return new Tuple<int,int>(first, destinationFirst);
        }

        #endregion

        #region CopyReversed

        /// <summary>
        /// Copies the items in a list in reverse to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The list to copy from.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <returns>The index into the destination past the last item copied.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <remarks>
        /// If the destination is too small to hold all of the values in the source, then only the items at the beginning
        /// of the source are copied.
        /// </remarks>
        public static CopyResult CopyReversed<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
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
            Tuple<int, int> indexes = CopyReversed<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count);
            CopyResult result = new CopyResult()
            {
                SourceOffset = indexes.Item1 - source.Offset,
                DestinationOffset = indexes.Item2 - destination.Offset,
            };
            return result;
        }

        internal static Tuple<int, int> CopyReversed<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
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

        #endregion

        #region CopyRotatedLeft

        /// <summary>
        /// Copies the items from a list to a destination, rotated to the left by the given shift amount.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="shift">The amount to rotate the items to the left.</param>
        /// <returns>The index into the destination past the last copied item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <remarks>
        /// If the shift is negative, the algoritm simulates rotating the items to the right. If the shift is larger than the number of items, 
        /// the algorithm will simulate a complete rotation as many times as necessary.
        /// </remarks>
        public static CopyResult CopyRotatedLeft<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            int shift)
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
            Tuple<int, int> indexes = copyRotatedLeftUnreduced<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                shift);
            CopyResult result = new CopyResult()
            {
                SourceOffset = indexes.Item1 - source.Offset,
                DestinationOffset = indexes.Item2 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int> copyRotatedLeftUnreduced<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            int shift)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int middle = getReducedOffset<TSourceList, T>(source, first, past, shift);
            middle += first;
            return copyRotatedLeft<TSourceList, TDestinationList, T>(
                source, first, middle, past,
                destination, destinationFirst, destinationPast);
        }

        private static Tuple<int, int> copyRotatedLeft<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int middle, int past,
            TDestinationList destination, int destinationFirst, int destinationPast)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            Tuple<int, int> indexes1 = copy<TSourceList, TDestinationList, T>(
                source, middle, past,
                destination, destinationFirst, destinationPast);
            int position = indexes1.Item1;
            destinationFirst = indexes1.Item2;
            if (position == past)
            {
                Tuple<int, int> indexes2 = copy<TSourceList, TDestinationList, T>(
                    source, first, middle,
                    destination, destinationFirst, destinationPast);
                position = indexes2.Item1;
                destinationFirst = indexes2.Item2;
            }
            return new Tuple<int, int>(position, destinationFirst);
        }

        #endregion

        #region CopySymmetricDifference

        /// <summary>
        /// Copies the items that do not appear in both lists into a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the third list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>
        /// The items in the lists must be sorted according to the default ordering of the items. Neither of
        /// the lists can contain duplicate items.
        /// </remarks>
        public static CopyTwoSourcesResult CopySymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return copySymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Copies the items that do not appear in both lists into a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <param name="comparer">The comparer to use to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer and must not contain duplicates.
        /// </remarks>
        public static CopyTwoSourcesResult CopySymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return copySymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparer.Compare);
        }

        /// <summary>
        /// Copies the items that do not appear in both lists into a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <param name="comparison">The delegate used to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate and must not contain duplicates.
        /// </remarks>
        public static CopyTwoSourcesResult CopySymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copySymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparison);
        }

        private static CopyTwoSourcesResult copySymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            Tuple<int, int, int> indexes = copySymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            CopyTwoSourcesResult result = new CopyTwoSourcesResult()
            {
                SourceOffset1 = indexes.Item1 - source1.Offset,
                SourceOffset2 = indexes.Item2 - source2.Offset,
                DestinationOffset = indexes.Item3 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int, int> copySymmetricDifference<TSourceList1, TSourceList2, TDestinationList, T>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
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
            Tuple<int, int> indexes1 = copy<TSourceList1, TDestinationList, T>(
                source1, first1, past1,
                destination, destinationFirst, destinationPast);
            first1 = indexes1.Item1;
            destinationFirst = indexes1.Item2;
            Tuple<int, int> indexes2 = copy<TSourceList2, TDestinationList, T>(
                source2, first2, past2,
                destination, destinationFirst, destinationPast);
            first2 = indexes2.Item1;
            destinationFirst = indexes2.Item2;
            return new Tuple<int, int, int>(first1, first2, destinationFirst);
        }

        #endregion

        #region CopyUnion

        /// <summary>
        /// Copies the items that are unique across both lists into a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the third list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>
        /// The items in the lists must be sorted according to the default ordering of the items. Neither of
        /// the lists can contain duplicate items.
        /// </remarks>
        public static CopyTwoSourcesResult CopyUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return copyUnion<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Copies the items that are unique across both lists into a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <param name="comparer">The comparer to use to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer and must not contain duplicates.
        /// </remarks>
        public static CopyTwoSourcesResult CopyUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return copyUnion<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparer.Compare);
        }

        /// <summary>
        /// Copies the items that are unique across both lists into a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <param name="comparison">The delegate used to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate and must not contain duplicates.
        /// </remarks>
        public static CopyTwoSourcesResult CopyUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copyUnion<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparison);
        }

        private static CopyTwoSourcesResult copyUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            Tuple<int, int, int> indexes = copyUnion<TSourceList1, TSourceList2, TDestinationList, T>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source1.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            CopyTwoSourcesResult result = new CopyTwoSourcesResult()
            {
                SourceOffset1 = indexes.Item1 - source1.Offset,
                SourceOffset2 = indexes.Item2 - source2.Offset,
                DestinationOffset = indexes.Item3 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int, int> copyUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
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
            Tuple<int, int> indexes1 = copy<TSourceList1, TDestinationList, T>(
                source1, first1, past1,
                destination, destinationFirst, destinationPast);
            first1 = indexes1.Item1;
            destinationFirst = indexes1.Item2;
            Tuple<int, int> indexes2 = copy<TSourceList2, TDestinationList, T>(
                source2, first2, past2,
                destination, destinationFirst, destinationPast);
            first2 = indexes2.Item1;
            destinationFirst = indexes2.Item2;
            return new Tuple<int, int, int>(first1, first2, destinationFirst);
        }

        #endregion

        #region CopyUnique

        /// <summary>
        /// Copies the items from a list that are unique to a destination.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy from.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <returns>The index into the destination past the last copied item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <remarks>The list must be sorted such that equivilent items appear adjacent.</remarks>
        public static CopyResult CopyUnique<TSourceList, TDestinationList, T>(IReadOnlySublist<TSourceList, T> source, IMutableSublist<TDestinationList, T> destination)
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
            return copyUnique<TSourceList, TDestinationList, T>(source, destination, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Copies the items from a list that are unique to a destination.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy from.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="comparer">The compare to use to determine whether two items are equivilent.</param>
        /// <returns>The index into the destination past the last copied item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>The list must be sorted such that equivilent items appear adjacent.</remarks>
        public static CopyResult CopyUnique<TSourceList, TDestinationList, T>(IReadOnlySublist<TSourceList, T> source, IMutableSublist<TDestinationList, T> destination, IEqualityComparer<T> comparer)
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
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return copyUnique<TSourceList, TDestinationList, T>(source, destination, comparer.Equals);
        }

        /// <summary>
        /// Copies the items from a list that are unique to a destination.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy from.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="comparison">The delegate used to determine whether two items are equivilent.</param>
        /// <returns>The index into the destination past the last copied item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>The list must be sorted such that equivilent items appear adjacent.</remarks>
        public static CopyResult CopyUnique<TSourceList, TDestinationList, T>(IReadOnlySublist<TSourceList, T> source, IMutableSublist<TDestinationList, T> destination, Func<T, T, bool> comparison)
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
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copyUnique<TSourceList, TDestinationList, T>(source, destination, comparison);
        }

        private static CopyResult copyUnique<TSourceList, TDestinationList, T>(IReadOnlySublist<TSourceList, T> source, IMutableSublist<TDestinationList, T> destination, Func<T, T, bool> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            Tuple<int, int> indexes = copyUnique<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            CopyResult result = new CopyResult()
            {
                SourceOffset = indexes.Item1 - source.Offset,
                DestinationOffset = indexes.Item2 - destination.Offset,
            };
            return result;
        }

        private static Tuple<int, int> copyUnique<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, T, bool> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
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
        public static int CountIf<TList, T>(IReadOnlySublist<TList, T> list, Func<T, bool> predicate)
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
        public static FindResult Find<TList, T>(IReadOnlySublist<TList, T> list, T value)
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
        public static FindResult Find<TList, T>(IReadOnlySublist<TList, T> list, T value, IEqualityComparer<T> comparer)
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
        public static FindResult Find<TList, T, TSearch>(IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, bool> comparison)
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

        private static FindResult find<TList, T, TSearch>(IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, bool> comparison)
            where TList : IList<T>
        {
            int past = list.Offset + list.Count;
            int index = indexOf<TList, T, TSearch>(list.List, list.Offset, past, value, comparison);
            FindResult result = new FindResult();
            result.Index = index - list.Offset;
            result.Exists = index != past;
            return result;
        }

        private static int indexOf<TList, T, TSearch>(TList list, int first, int past, TSearch value, Func<T, TSearch, bool> comparison)
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
        public static FindResult Find<TList, T>(IReadOnlySublist<TList, T> list, Func<T, bool> predicate)
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
            int index = indexOf<TList, T>(list.List, list.Offset, past, predicate);
            FindResult result = new FindResult();
            result.Index = index - list.Offset;
            result.Exists = index != past;
            return result;
        }

        private static int indexOf<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
            where TList : IList<T>
        {
            while (first != past && !predicate(list[first]))
            {
                ++first;
            }
            return first;
        }

        private static int indexOfNot<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
            where TList : IList<T>
        {
            while (first != past && predicate(list[first]))
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
        public static FindResult FindAny<TList1, TList2, T>(IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
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
        public static FindResult FindAny<TList1, TList2, T>(
            IReadOnlySublist<TList1, T> list1,
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
        public static FindResult FindAny<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
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

        private static FindResult findAny<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            int past = list1.Offset + list1.Count;
            int index = indexOfAny<TList1, T1, TList2, T2>(
                list1.List, list1.Offset, past,
                list2.List, list2.Offset, list2.Offset + list2.Count,
                comparison);
            FindResult result = new FindResult();
            result.Index = index - list1.Offset;
            result.Exists = index != past;
            return result;
        }

        private static int indexOfAny<TList1, T1, TList2, T2>(
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
        public static FindResult FindDuplicates<TList, T>(IReadOnlySublist<TList, T> list)
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
        public static FindResult FindDuplicates<TList, T>(IReadOnlySublist<TList, T> list, IEqualityComparer<T> comparer)
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
        public static FindResult FindDuplicates<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, bool> comparison)
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

        private static FindResult findDuplicates<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, bool> comparison)
            where TList : IList<T>
        {
            int past = list.Offset + list.Count;
            int index = indexOfDuplicates<TList, T>(list.List, list.Offset, past, comparison);
            FindResult result = new FindResult();
            result.Index = index - list.Offset;
            result.Exists = index != past;
            return result;
        }

        private static int indexOfDuplicates<TList, T>(TList list, int first, int past, Func<T, T, bool> comparison)
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
        public static FindResult FindSequence<TList1, TList2, T>(IReadOnlySublist<TList1, T> list, IReadOnlySublist<TList2, T> sequence)
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
        public static FindResult FindSequence<TList1, TList2, T>(
            IReadOnlySublist<TList1, T> list,
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
        public static FindResult FindSequence<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list,
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

        private static FindResult findSequence<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list,
            IReadOnlySublist<TList2, T2> sequence,
            Func<T1, T2, bool> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            int past = list.Offset + list.Count;
            int index = indexOfSequence<TList1, T1, TList2, T2>(
                list.List, list.Offset, past,
                sequence.List, sequence.Offset, sequence.Offset + sequence.Count,
                comparison);
            FindResult result = new FindResult();
            result.Index = index - list.Offset;
            result.Exists = index != past;
            return result;
        }

        private static int indexOfSequence<TList1, T1, TList2, T2>(
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
        public static void ForEach<TList, T>(IReadOnlySublist<TList, T> list, Action<T> action)
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

        #region HeapAdd

        /// <summary>
        /// Moves an item after the end of a heap to its appropriate location.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The heap to add the value to.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>The list must be a max heap.</remarks>
        public static void HeapAdd<TList, T>(IMutableSublist<TList, T> list)
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
        public static void HeapAdd<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
        public static void HeapAdd<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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
        public static void HeapRemove<TList, T>(IMutableSublist<TList, T> list)
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
        public static void HeapRemove<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
        public static void HeapRemove<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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
        public static void HeapSort<TList, T>(IMutableSublist<TList, T> list)
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
        public static void HeapSort<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
        public static void HeapSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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

        /// <summary>
        /// Sorts the list using the insertion sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static void InsertionSort<TList, T>(IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            insertionSort<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Sorts the list using the insertion sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static void InsertionSort<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
            insertionSort<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Sorts the list using the insertion sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static void InsertionSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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
            insertionSort<TList, T>(list, comparison);
        }

        private static void insertionSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            insertionSort<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
        }

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

        #region IsHeap

        /// <summary>
        /// Determines whether the given list is a valid heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <returns>True if the list represents a valid heap; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static bool IsHeap<TList, T>(IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return isHeap<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Determines whether the given list is a valid heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>True if the list represents a valid heap; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static bool IsHeap<TList, T>(IReadOnlySublist<TList, T> list, IComparer<T> comparer)
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
        /// Determines whether the given list is a valid heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>True if the list represents a valid heap; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static bool IsHeap<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
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

        private static bool isHeap<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int past = list.Offset + list.Count;
            int result = isHeapUntil<TList, T>(list.List, list.Offset, past, comparison);
            return result == past;
        }

        #endregion

        #region IsHeapUntil

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
        public static int IsHeapUntil<TList, T>(IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return isHeapUntil<TList, T>(list, Comparer<T>.Default.Compare);
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
        public static int IsHeapUntil<TList, T>(IReadOnlySublist<TList, T> list, IComparer<T> comparer)
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
            return isHeapUntil<TList, T>(list, comparer.Compare);
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
        public static int IsHeapUntil<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
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
            return isHeapUntil<TList, T>(list, comparison);
        }

        private static int isHeapUntil<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int result = isHeapUntil<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
            result -= list.Offset;
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

        #region IsPartitioned

        /// <summary>
        /// Determines whether a list is partitioned.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <param name="predicate">The predicate to check if the list is partitioned.</param>
        /// <returns>True if the list is partitioned by the given predicate; otherwise, false.</returns>
        /// <remarks>The algorithm expects that the list is partitioned such that the items satisfying the predicate appear first.</remarks>
        public static bool IsPartitioned<TList, T>(IReadOnlySublist<TList, T> list, Func<T, bool> predicate)
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
            return isPartitioned<TList, T>(list.List, list.Offset, list.Offset + list.Count, predicate);
        }

        private static bool isPartitioned<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
            where TList : IList<T>
        {
            int index = isPartitionedUntil<TList, T>(list, first, past, predicate);
            return index == past;
        }

        #endregion

        #region IsPartitionedUntil

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
        public static int IsPartitionedUntil<TList, T>(IReadOnlySublist<TList, T> list, Func<T, bool> predicate)
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
            int result = isPartitionedUntil<TList, T>(list.List, list.Offset, list.Offset + list.Count, predicate);
            result -= list.Offset;
            return result;
        }

        private static int isPartitionedUntil<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
            where TList : IList<T>
        {
            first = indexOfNot<TList, T>(list, first, past, predicate);
            first = indexOf<TList, T>(list, first, past, predicate);
            return first;
        }

        #endregion

        #region IsSet

        /// <summary>
        /// Determines whether the list is a valid ordered set.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <returns>True if the list is a valid ordered set; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static bool IsSet<TList, T>(IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return isSet<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Determines whether the list is a valid ordered set.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>True if the list is a valid ordered set; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static bool IsSet<TList, T>(IReadOnlySublist<TList, T> list, IComparer<T> comparer)
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
        /// Determines whether the list is a valid ordered set.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>True if the list is a valid ordered set; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static bool IsSet<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
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

        private static bool isSet<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int past = list.Offset + list.Count;
            int result = isSetUntil<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
            return result == past;
        }

        #endregion

        #region IsSetUntil

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
        public static int IsSetUntil<TList, T>(IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return isSetUntil<TList, T>(list, Comparer<T>.Default.Compare);
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
        public static int IsSetUntil<TList, T>(IReadOnlySublist<TList, T> list, IComparer<T> comparer)
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
            return isSetUntil<TList, T>(list, comparer.Compare);
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
        public static int IsSetUntil<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
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
            return isSetUntil<TList, T>(list, comparison);
        }

        private static int isSetUntil<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int result = isSetUntil<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
            result -= list.Offset;
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

        #region IsSubset

        /// <summary>
        /// Determines whether all of the items in the second list appear in the first list.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <returns>True if all of the items in the second list appear in the first list; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <remarks>
        /// This algorithm assumes that both lists are sorted according to the default order of the items. 
        /// Both lists must contain distinct values.
        /// </remarks>
        public static bool IsSubset<TList1, TList2, T>(IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
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
        /// Determines whether all of the items in the second list appear in the first list.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparer">The comparison delegate to use to compare items from the lists.</param>
        /// <returns>True if all of the items in the second list have an equivilent item in the first list; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// This algorithm assumes that the lists are sorted using a meaningful ordering that applies to both lists and that the
        /// comparison delegate respects that order. Both lists must contain distinct values.
        /// </remarks>
        public static bool IsSubset<TList1, TList2, T>(
            IReadOnlySublist<TList1, T> list1,
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
        /// Determines whether all of the items in the second list appear in the first list.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="T1">The type of the items in the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T2">The type of the items in the second list.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparison">The comparison delegate to use to compare items from the lists.</param>
        /// <returns>True if all of the items in the second list have an equivilent item in the first list; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// This algorithm assumes that the lists are sorted using a meaningful ordering that applies to both lists and that the
        /// comparison delegate respects that order. Both lists must contain distinct values.
        /// </remarks>
        public static bool IsSubset<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
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

        private static bool isSubset<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            int past2 = list2.Offset + list2.Count;
            int index = isSubsetUntil<TList1, T1, TList2, T2>(
                list1.List, list1.Offset, list1.Offset + list1.Count,
                list2.List, list2.Offset, past2,
                comparison);
            return index == past2;
        }

        #endregion

        #region IsSubsetUntil

        /// <summary>
        /// Determines whether all of the items in the second list appear in the first list.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <returns>True if all of the items in the second list appear in the first list; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <remarks>
        /// This algorithm assumes that both lists are sorted according to the default order of the items. 
        /// Both lists must contain distinct values.
        /// </remarks>
        public static int IsSubsetUntil<TList1, TList2, T>(IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
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
            return isSubsetUntil<TList1, T, TList2, T>(list1, list2, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Determines whether all of the items in the second list appear in the first list.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparer">The comparison delegate to use to compare items from the lists.</param>
        /// <returns>True if all of the items in the second list have an equivilent item in the first list; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// This algorithm assumes that the lists are sorted using a meaningful ordering that applies to both lists and that the
        /// comparison delegate respects that order. Both lists must contain distinct values.
        /// </remarks>
        public static int IsSubsetUntil<TList1, TList2, T>(
            IReadOnlySublist<TList1, T> list1,
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
            return isSubsetUntil<TList1, T, TList2, T>(list1, list2, comparer.Compare);
        }

        /// <summary>
        /// Determines whether all of the items in the second list appear in the first list.
        /// </summary>
        /// <typeparam name="TList1">The type of the first list.</typeparam>
        /// <typeparam name="T1">The type of the items in the first list.</typeparam>
        /// <typeparam name="TList2">The type of the second list.</typeparam>
        /// <typeparam name="T2">The type of the items in the second list.</typeparam>
        /// <param name="list1">The first list.</param>
        /// <param name="list2">The second list.</param>
        /// <param name="comparison">The comparison delegate to use to compare items from the lists.</param>
        /// <returns>True if all of the items in the second list have an equivilent item in the first list; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// This algorithm assumes that the lists are sorted using a meaningful ordering that applies to both lists and that the
        /// comparison delegate respects that order. Both lists must contain distinct values.
        /// </remarks>
        public static int IsSubsetUntil<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
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
            return isSubsetUntil<TList1, T1, TList2, T2>(list1, list2, comparison);
        }

        private static int isSubsetUntil<TList1, T1, TList2, T2>(
            IReadOnlySublist<TList1, T1> list1,
            IReadOnlySublist<TList2, T2> list2,
            Func<T1, T2, int> comparison)
            where TList1 : IList<T1>
            where TList2 : IList<T2>
        {
            int index = isSubsetUntil<TList1, T1, TList2, T2>(
                list1.List, list1.Offset, list1.Offset + list1.Count,
                list2.List, list2.Offset, list2.Offset + list2.Count,
                comparison);
            return index - list2.Offset;
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
                    ++first1;
                }
                else if (result > 0)
                {
                    return first2;
                }
                else
                {
                    ++first1;
                    ++first2;
                }
            }
            return first2;
        }

        #endregion

        #region IsSorted

        /// <summary>
        /// Determines whether a list is sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <returns>True if the list is sorted; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static bool IsSorted<TList, T>(IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return isSorted<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Determines whether a list is sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>True if the list is sorted; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static bool IsSorted<TList, T>(IReadOnlySublist<TList, T> list, IComparer<T> comparer)
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
        /// Determines whether a list is sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>True if the list is sorted; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static bool IsSorted<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
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

        private static bool isSorted<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int past = list.Offset + list.Count;
            int result = isSortedUntil<TList, T>(list.List, list.Offset, past, comparison);
            return result == past;
        }

        #endregion

        #region IsSortUntil

        /// <summary>
        /// Finds the index in which the list stops being sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <returns>The index in which the list stops being sorted -or- an index past the end of the list, if the list is sorted.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static int IsSortedUntil<TList, T>(IReadOnlySublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return isSortedUntil<TList, T>(list, Comparer<T>.Default.Compare);
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
        public static int IsSortedUntil<TList, T>(IReadOnlySublist<TList, T> list, IComparer<T> comparer)
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
            return isSortedUntil<TList, T>(list, comparer.Compare);
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
        public static int IsSortedUntil<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
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
            return isSortedUntil<TList, T>(list, comparison);
        }

        private static int isSortedUntil<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int result = isSortedUntil<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
            result -= list.Offset;
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

        #region ItemAt

        /// <summary>
        /// Arranges the items in a list such that the item at the given index is the same had the list been sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to rearrange.</param>
        /// <param name="index">The index into the list to move the expected item.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        public static void ItemAt<TList, T>(IMutableSublist<TList, T> list, int index)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (index < 0 || index >= list.Count)
            {
                throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
            }
            itemAt<TList, T>(list, index, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Arranges the items in a list such that the item at the given index is the same had the list been sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to rearrange.</param>
        /// <param name="index">The index into the list to move the expected item.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static void ItemAt<TList, T>(IMutableSublist<TList, T> list, int index, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (index < 0 || index >= list.Count)
            {
                throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            itemAt<TList, T>(list, index, comparer.Compare);
        }

        /// <summary>
        /// Arranges the items in a list such that the item at the given index is the same had the list been sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to rearrange.</param>
        /// <param name="index">The index into the list to move the expected item.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static void ItemAt<TList, T>(IMutableSublist<TList, T> list, int index, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (index < 0 || index >= list.Count)
            {
                throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            itemAt<TList, T>(list, index, comparison);
        }

        private static void itemAt<TList, T>(IMutableSublist<TList, T> list, int index, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            itemAt<TList, T>(list.List, list.Offset, list.Offset + index, list.Offset + list.Count, comparison);
        }

        private static void itemAt<TList, T>(TList list, int first, int n, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            while (past - first > _sortMax)
            {
                int middle = partition<TList, T>(list, first, past, comparison);
                if (middle <= n)
                {
                    first = middle;
                }
                else
                {
                    past = middle;
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
        public static LowerAndUpperBoundResult LowerAndUpperBound<TList, T>(IReadOnlySublist<TList, T> list, T value)
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
        public static LowerAndUpperBoundResult LowerAndUpperBound<TList, T>(IReadOnlySublist<TList, T> list, T value, IComparer<T> comparer)
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
            IReadOnlySublist<TList, T> list,
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
            LowerAndUpperBoundResult result = new LowerAndUpperBoundResult()
            {
                LowerBound = indexes.Item1 - list.Offset,
                UpperBound = indexes.Item2 - list.Offset
            };
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
        public static int LowerBound<TList, T>(IReadOnlySublist<TList, T> list, T value)
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
        public static int LowerBound<TList, T>(IReadOnlySublist<TList, T> list, T value, IComparer<T> comparer)
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
        public static int LowerBound<TList, T, TSearch>(IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, int> comparison)
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

        /// <summary>
        /// Rearranges the items in a list such that they represent a max heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to rearrange into a heap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static void MakeHeap<TList, T>(IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            makeHeap<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Rearranges the items in a list such that they represent a max heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to rearrange into a heap.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static void MakeHeap<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
            makeHeap<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Rearranges the items in a list such that they represent a max heap.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to rearrange into a heap.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static void MakeHeap<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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
            makeHeap<TList, T>(list, comparison);
        }

        private static void makeHeap<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            makeHeap<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
        }

        private static void makeHeap<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int bottom = past - first;
            for (int hole = bottom / 2; hole > 0; )
            {
                --hole;
                adjustHeap<TList, T>(list, first, hole, bottom, list[first + hole], comparison);
            }
        }

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

        #region MakeSet

        /// <summary>
        /// Makes a list an ordered set.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to make a set.</param>
        /// <returns>The index past the last item in the set.</returns>
        /// <remarks>
        /// This set will be ordered according to the default ordering of the item type. 
        /// Items are not removed from the list. 
        /// Only items with an index less than the return value are part of the set.
        /// The remaining items are garbage.
        /// </remarks>
        public static int MakeSet<TList, T>(IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return makeSet<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Makes a list an ordered set.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to make a set.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>The index past the last item in the set.</returns>
        /// <remarks>
        /// This set will be ordered according to the comparer. 
        /// Items are not removed from the list.
        /// Only items with an index less than the return value are part of the set.
        /// The remaining items are garbage.
        /// </remarks>
        public static int MakeSet<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
            return makeSet<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Makes a list an ordered set.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to make a set.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>The index past the last item in the set.</returns>
        /// <remarks>
        /// This set will be ordered according to the comparer. 
        /// Items are not removed from the list.
        /// Only items with an index less than the return value are part of the set.
        /// The remaining items are garbage.
        /// </remarks>
        public static int MakeSet<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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
            return makeSet<TList, T>(list, comparison);
        }

        private static int makeSet<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int result = makeSet<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
            result -= list.Offset;
            return result;
        }

        private static int makeSet<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (past - first > 1)
            {
                quickSort<TList, T>(list, first, past, past - first, comparison);
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
        public static int Maximum<TList, T>(IReadOnlySublist<TList, T> list)
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
        public static int Maximum<TList, T>(IReadOnlySublist<TList, T> list, IComparer<T> comparer)
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
        public static int Maximum<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
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

        #region MergeSort

        /// <summary>
        /// Sorts a list using the merge sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>
        /// MergeSort uses an underlying buffer that is roughly half the size of the given list.
        /// MergeSort will preserve the order that equivalent items appear in the list.
        /// </remarks>
        public static void MergeSort<TList, T>(IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            mergeSort<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Sorts a list using the merge sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="TBuffer">The type of the buffer.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="buffer">The list to use to act as a temporary buffer.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer is null.</exception>
        /// <remarks>
        /// MergeSort uses the given buffer to merge. 
        /// It never needs to be larger than half the size of the list.
        /// Making it too small will impact performance negatively.
        /// MergeSort will preserve the order that equivalent items appear in the list.
        /// </remarks>
        public static void MergeSort<TList, TBuffer, T>(IMutableSublist<TList, T> list, IMutableSublist<TBuffer, T> buffer)
            where TList : IList<T>
            where TBuffer : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            mergeSort<TList, TBuffer, T>(list, buffer, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Sorts a list using the merge sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// MergeSort uses an underlying buffer that is roughly half the size of the given list.
        /// MergeSort will preserve the order that equivalent items appear in the list.
        /// </remarks>
        public static void MergeSort<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
            mergeSort<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Sorts a list using the merge sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="TBuffer">The type of the buffer.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="buffer">The list to use to act as a temporary buffer.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// MergeSort uses the given buffer to merge. 
        /// It never needs to be larger than half the size of the list.
        /// Making it too small will impact performance negatively.
        /// MergeSort will preserve the order that equivalent items appear in the list.
        /// </remarks>
        public static void MergeSort<TList, TBuffer, T>(IMutableSublist<TList, T> list, IMutableSublist<TBuffer, T> buffer, IComparer<T> comparer)
            where TList : IList<T>
            where TBuffer : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            mergeSort<TList, TBuffer, T>(list, buffer, comparer.Compare);
        }

        /// <summary>
        /// Sorts a list using the merge sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// MergeSort uses an underlying buffer that is roughly half the size of the given list.
        /// MergeSort will preserve the order that equivalent items appear in the list.
        /// </remarks>
        public static void MergeSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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
            mergeSort<TList, T>(list, comparison);
        }

        /// <summary>
        /// Sorts a list using the merge sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="TBuffer">The type of the buffer.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="buffer">The list to use to act as a temporary buffer.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// MergeSort uses the given buffer to merge. 
        /// It never needs to be larger than half the size of the list.
        /// Making it too small will impact performance negatively.
        /// MergeSort will preserve the order that equivalent items appear in the list.
        /// </remarks>
        public static void MergeSort<TList, TBuffer, T>(IMutableSublist<TList, T> list, IMutableSublist<TBuffer, T> buffer, Func<T, T, int> comparison)
            where TList : IList<T>
            where TBuffer : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            mergeSort<TList, TBuffer, T>(list, buffer, comparison);
        }

        private static void mergeSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int first = list.Offset;
            int past = first + list.Count;
            int bufferLength = (past - first) / 2;
            T[] buffer = new T[bufferLength];
            mergeSort<TList, T[], T>(list.List, first, past, buffer, 0, bufferLength, comparison);
        }

        private static void mergeSort<TList, TBuffer, T>(IMutableSublist<TList, T> list, IMutableSublist<TBuffer, T> buffer, Func<T, T, int> comparison)
            where TList : IList<T>
            where TBuffer : IList<T>
        {
            if (buffer.Count < 3)
            {
                insertionSort<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
            }
            else
            {
                mergeSort<TList, TBuffer, T>(
                    list.List, list.Offset, list.Offset + list.Count,
                    buffer.List, buffer.Offset, buffer.Offset + buffer.Count,
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
                    bufferMiddle = copy<TList, TBuffer, T>(list, first1, middle, buffer, bufferFirst, bufferPast).Item2;
                    copyMerged<TBuffer, TList, TList, T>(buffer, bufferFirst, bufferMiddle, list, middle, past1, list, first1, past1, comparison);
                }
                bufferMiddle = copy<TList, TBuffer, T>(list, first1, middle, buffer, bufferFirst, bufferPast).Item2;
                copyMerged<TBuffer, TList, TList, T>(buffer, bufferFirst, bufferMiddle, list, middle, past, list, first1, past, comparison);
            }
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
                    int bufferMiddle = copy<TList, TBuffer, T>(list, first, middle, buffer, bufferFirst, bufferPast).Item2;
                    copyMerged<TBuffer, TList, TList, T>(
                        buffer, bufferFirst, bufferMiddle,
                        list, middle, past,
                        list, first, past,
                        comparison);
                }
                else if (count2 <= bufferCount)
                {
                    int bufferMiddle = copy<TList, TBuffer, T>(list, middle, past, buffer, bufferFirst, bufferPast).Item2;
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
                int bufferMiddle = copy<TList, TBuffer, T>(list, first, middle, buffer, bufferFirst, bufferPast).Item2;
                copy<TList, TList, T>(list, middle, past, list, first, past);
                return copyBackward<TBuffer, TList, T>(buffer, bufferFirst, bufferMiddle, list, first, past);
            }
            else if (count2 <= bufferCount)
            {
                int bufferMiddle = copy<TList, TBuffer, T>(list, middle, past, buffer, bufferFirst, bufferPast).Item2;
                copyBackward<TList, TList, T>(list, first, middle, list, first, past);
                return copy<TBuffer, TList, T>(buffer, bufferFirst, bufferMiddle, list, first, past).Item2;
            }
            else
            {
                rotateLeft<TList, T>(list, first, middle, past);
                return first + count2;
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

        #region Minimum

        /// <summary>
        /// Finds the index of the smallest item in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <returns>The index of the smaller item in the list -or- the index past the end of the list, if it is empty.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static int Minimum<TList, T>(IReadOnlySublist<TList, T> list)
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
        public static int Minimum<TList, T>(IReadOnlySublist<TList, T> list, IComparer<T> comparer)
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
        public static int Minimum<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
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

        #region MimimumMaximum

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
        public static MinimumMaximumResult MinimumMaximum<TList, T>(IReadOnlySublist<TList, T> list)
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
        public static MinimumMaximumResult MinimumMaximum<TList, T>(IReadOnlySublist<TList, T> list, IComparer<T> comparer)
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
        public static MinimumMaximumResult MinimumMaximum<TList, T>(IReadOnlySublist<TList, T> list, Func<T, T, int> comparison)
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
        public static int Mismatch<TList1, TList2, T>(IReadOnlySublist<TList1, T> list1, IReadOnlySublist<TList2, T> list2)
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
            IReadOnlySublist<TList1, T> list1,
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
            IReadOnlySublist<TList1, T1> list1,
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
        public static bool NextPermutation<TList, T>(IMutableSublist<TList, T> list)
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
        public static bool NextPermutation<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
        public static bool NextPermutation<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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
                    reverse<TList, T>(list, previous1, past);
                    return true;
                }

                if (previous == first)
                {
                    reverse<TList, T>(list, first, past);
                    return false;
                }
            }
        }

        #endregion

        #region PartialSort

        /// <summary>
        /// Sorts a list such that the first items appear as if the entire list were sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in a list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="numberOfItems">The number of items to sort.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The number of items is negative -or- larger than the size of the list.</exception>
        public static void PartialSort<TList, T>(IMutableSublist<TList, T> list, int numberOfItems)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (numberOfItems < 0 || numberOfItems > list.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, Resources.IndexOutOfRange);
            }
            partialSort<TList, T>(list, numberOfItems, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Sorts a list such that the first items appear as if the entire list were sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The list to sort.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="numberOfItems">The number of items to sort.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The number of items is negative -or- larger than the size of the list.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static void PartialSort<TList, T>(IMutableSublist<TList, T> list, int numberOfItems, IComparer<T> comparer)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (numberOfItems < 0 || numberOfItems > list.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, Resources.IndexOutOfRange);
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            partialSort<TList, T>(list, numberOfItems, comparer.Compare);
        }

        /// <summary>
        /// Sorts a list such that the first items appear as if the entire list were sorted.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The list to sort.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="numberOfItems">The number of items to sort.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The number of items is negative -or- larger than the size of the list.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static void PartialSort<TList, T>(IMutableSublist<TList, T> list, int numberOfItems, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (numberOfItems < 0 || numberOfItems > list.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, Resources.IndexOutOfRange);
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            partialSort<TList, T>(list, numberOfItems, comparison);
        }

        private static void partialSort<TList, T>(IMutableSublist<TList, T> list, int numberOfItems, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            partialSort<TList, T>(list.List, list.Offset, list.Offset + numberOfItems, list.Offset + list.Count, comparison);
        }

        private static void partialSort<TList, T>(TList list, int first, int middle, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            if (past - first > 1)
            {
                makeHeap<TList, T>(list, first, middle, comparison);
                int numberOfItems = middle - first;
                for (int next = middle; next != past; ++next)
                {
                    if (comparison(list[next], list[first]) < 0)
                    {
                        T value = list[next];
                        list[next] = list[first];
                        adjustHeap<TList, T>(list, first, 0, numberOfItems, value, comparison);
                    }
                }
                heapSort<TList, T>(list, first, middle, comparison);
            }
        }

        #endregion

        #region Partition

        /// <summary>
        /// Partitions a list such that the items satisfying the predicate are moved to the front
        /// and the rest are moved to the back.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to partition.</param>
        /// <param name="predicate">The condition an item must satisfy to be moved to the front.</param>
        /// <returns>The index past the last item to satisfy the predicate.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static int Partition<TList, T>(IMutableSublist<TList, T> list, Func<T, bool> predicate)
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
            int result = partition<TList, T>(list.List, list.Offset, list.Offset + list.Count, predicate);
            result -= list.Offset;
            return result;
        }

        private static int partition<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
            where TList : IList<T>
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
                T temp = list[first];
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
        public static bool PreviousPermutation<TList, T>(IMutableSublist<TList, T> list)
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
        public static bool PreviousPermutation<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
        public static bool PreviousPermutation<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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
                    reverse<TList, T>(list, previous1, past);
                    return true;
                }
                if (previous == first)
                {
                    reverse<TList, T>(list, first, past);
                    return false;
                }
            }
        }

        #endregion

        #region QuickSort

        /// <summary>
        /// Sorts a list using the quick sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static void QuickSort<TList, T>(IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            quickSort<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Sorts a list using the quick sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static void QuickSort<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
            quickSort<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Sorts a list using the quick sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static void QuickSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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
            quickSort<TList, T>(list, comparison);
        }

        private static void quickSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            quickSort<TList, T>(list.List, list.Offset, list.Offset + list.Count, list.Count, comparison);
        }

        private static void quickSort<TList, T>(TList list, int first, int past, int ideal, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            while (past - first > _sortMax && ideal > 0)
            {
                int middle = partition<TList, T>(list, first, past, comparison);
                ideal /= 2;
                ideal += ideal / 2;
                // recursively sort the smaller branch
                if (middle - first < past - middle)
                {
                    quickSort<TList, T>(list, first, middle, ideal, comparison);
                    first = middle;
                }
                else
                {
                    quickSort<TList, T>(list, middle, past, ideal, comparison);
                    past = middle;
                }
            }
            if (past - first > _sortMax)
            {
                makeHeap<TList, T>(list, first, past, comparison);
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

        #endregion

        #region RandomShuffle

        /// <summary>
        /// Rearranges the items in a list randomly.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to shuffle.</param>
        /// <param name="random">The random generator to use to shuffle the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The random generator is null.</exception>
        public static void RandomShuffle<TList, T>(IMutableSublist<TList, T> list, Random random)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (random == null)
            {
                throw new ArgumentNullException("random");
            }
            randomShuffle<TList, T>(list, random.Next);
        }

        /// <summary>
        /// Rearranges the items in a list randomly.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to shuffle.</param>
        /// <param name="generator">The generator to use to shuffle the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator is null.</exception>
        public static void RandomShuffle<TList, T>(IMutableSublist<TList, T> list, Func<int> generator)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            randomShuffle<TList, T>(list, generator);
        }

        private static void randomShuffle<TList, T>(IMutableSublist<TList, T> list, Func<int> generator)
            where TList : IList<T>
        {
            randomShuffle<TList, T>(list.List, list.Offset, list.Offset + list.Count, generator);
        }

        private static void randomShuffle<TList, T>(TList list, int first, int past, Func<int> generator)
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

        #endregion

        #region RemoveDuplicates

        /// <summary>
        /// Overwrites duplicate items in a list with the next non-duplicate.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to overwrite the duplicates in.</param>
        /// <returns>The index past the last non-duplicate item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>
        /// The list is expected to be sorted according to the default order of the items.
        /// Use this algorithm when either the size of the list is fixed
        /// -or- it is more efficient to first move valid items to the front of the list and then remove those remaining from the back.
        /// </remarks>
        public static int RemoveDuplicates<TList, T>(IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            return removeDuplicates<TList, T>(list, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Overwrites duplicate items in a list with the next non-duplicate.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to overwrite the duplicates in.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>The index past the last non-duplicate item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The list is expected to be sorted such that equivalent items appear adjacent to one another.
        /// Use this algorithm when either the size of the list is fixed
        /// -or- it is more efficient to first move valid items to the front of the list and then remove those remaining from the back.
        /// </remarks>
        public static int RemoveDuplicates<TList, T>(IMutableSublist<TList, T> list, IEqualityComparer<T> comparer)
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
            return removeDuplicates<TList, T>(list, comparer.Equals);
        }

        /// <summary>
        /// Overwrites duplicate items in a list with the next non-duplicate.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to overwrite the duplicates in.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>The index past the last non-duplicate item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The list is expected to be sorted such that equivalent items appear adjacent to one another.
        /// Use this algorithm when either the size of the list is fixed
        /// -or- it is more efficient to first move valid items to the front of the list and then remove those remaining from the back.
        /// </remarks>26
        public static int RemoveDuplicates<TList, T>(IMutableSublist<TList, T> list, Func<T, T, bool> comparison)
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
            return removeDuplicates<TList, T>(list, comparison);
        }

        private static int removeDuplicates<TList, T>(IMutableSublist<TList, T> list, Func<T, T, bool> comparison)
            where TList : IList<T>
        {
            int result = removeDuplicates<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
            result -= list.Offset;
            return result;
        }

        private static int removeDuplicates<TList, T>(TList list, int first, int past, Func<T, T, bool> comparison)
            where TList : IList<T>
        {
            first = indexOfDuplicates<TList, T>(list, first, past, comparison);
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

        #region RemoveIf

        /// <summary>
        /// Overwrites the items in a list that satisfy the predicate with items that do not.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to overwrite items in.</param>
        /// <param name="predicate">The condition an item must satisfy to be overwritten.</param>
        /// <returns>The index past the last item not satisfying the predicate.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>
        /// Use this algorithm when either the size of the list is fixed
        /// -or- it is more efficient to first move valid items to the front of the list and then remove those remaining from the back.
        /// </remarks>
        public static int RemoveIf<TList, T>(IMutableSublist<TList, T> list, Func<T, bool> predicate)
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
            int result = removeIf<TList, T>(list.List, list.Offset, list.Offset + list.Count, predicate);
            result -= list.Offset;
            return result;
        }

        private static int removeIf<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
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

        #region RemoveRange

        /// <summary>
        /// Removes all of the items in the range defined by a Sublist.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list containing the items to remove.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static IExpandableSublist<TList, T> RemoveRange<TList, T>(IExpandableSublist<TList, T> list)
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
            first = copy<TList, TList, T>(list, past, list.Count, list, first, list.Count).Item2;
            past = list.Count;
            while (first != past)
            {
                --past;
                list.RemoveAt(past);
            }
        }

        #endregion

        #region Replace

        /// <summary>
        /// Replaces any items in a list that satisfy the predicate with the replacement value.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to replace the items in.</param>
        /// <param name="predicate">The condition an item must satisfy to be replaced.</param>
        /// <param name="replacement">The value to replace the items with.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static void Replace<TList, T>(IMutableSublist<TList, T> list, Func<T, bool> predicate, T replacement)
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
            replace<TList, T>(list.List, list.Offset, list.Offset + list.Count, predicate, replacement);
        }

        private static void replace<TList, T>(TList list, int first, int past, Func<T, bool> predicate, T replacement)
            where TList : IList<T>
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

        /// <summary>
        /// Replaces any items in a list that satisfy the predicate with the results of calling the generator.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to replace the items in.</param>
        /// <param name="predicate">The condition an item must satisfy to be replaced.</param>
        /// <param name="generator">The generator delegate to call to generate the replacement values.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator delegate is null.</exception>
        public static void Replace<TList, T>(IMutableSublist<TList, T> list, Func<T, bool> predicate, Func<T, T> generator)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            replace<TList, T>(list.List, list.Offset, list.Offset + list.Count, predicate, generator);
        }

        private static void replace<TList, T>(TList list, int first, int past, Func<T, bool> predicate, Func<T, T> generator)
            where TList : IList<T>
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

        /// <summary>
        /// Replaces each occurrence of the given sequence with the replacement sequence.
        /// </summary>
        /// <typeparam name="TList">The type of the list to replace the sequences in.</typeparam>
        /// <typeparam name="TSequenceList">The type of the list to find the sequence in.</typeparam>
        /// <typeparam name="TReplacementList">The type of the replacement list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to replace the sequences in.</param>
        /// <param name="sequence">The sequence to search for.</param>
        /// <param name="replacement">The sequence to use as a replacement.</param>
        /// <returns>A new sublist wrapping the list with all of its replacements.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        public static IExpandableSublist<TList, T> Replace<TList, TSequenceList, TReplacementList, T>(
            IExpandableSublist<TList, T> list,
            IReadOnlySublist<TSequenceList, T> sequence,
            IReadOnlySublist<TReplacementList, T> replacement)
            where TList : IList<T>
            where TSequenceList : IList<T>
            where TReplacementList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            return replace<TList, TSequenceList, TReplacementList, T, T>(list, sequence, replacement, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Replaces each occurrence of the given sequence with the replacement sequence.
        /// </summary>
        /// <typeparam name="TList">The type of the list to replace the sequences in.</typeparam>
        /// <typeparam name="TSequenceList">The type of the list to find the sequence in.</typeparam>
        /// <typeparam name="TReplacementList">The type of the replacement list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to replace the sequences in.</param>
        /// <param name="sequence">The sequence to search for.</param>
        /// <param name="replacement">The sequence to use as a replacement.</param>
        /// <param name="comparer">An equality comparer to comparer values in the list and in the sequence.</param>
        /// <returns>A new sublist wrapping the list with all of its replacements.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static IExpandableSublist<TList, T> Replace<TList, TSequenceList, TReplacementList, T>(
            IExpandableSublist<TList, T> list,
            IReadOnlySublist<TSequenceList, T> sequence,
            IReadOnlySublist<TReplacementList, T> replacement,
            IEqualityComparer<T> comparer)
            where TList : IList<T>
            where TSequenceList : IList<T>
            where TReplacementList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return replace<TList, TSequenceList, TReplacementList, T, T>(list, sequence, replacement, comparer.Equals);
        }

        /// <summary>
        /// Replaces each occurrence of the given sequence with the replacement sequence.
        /// </summary>
        /// <typeparam name="TList">The type of the list to replace the sequences in.</typeparam>
        /// <typeparam name="TSequenceList">The type of the list to find the sequence in.</typeparam>
        /// <typeparam name="TReplacementList">The type of the replacement list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <typeparam name="TSequence">The type of the items in the sequence list.</typeparam>
        /// <param name="list">The list to replace the sequences in.</param>
        /// <param name="sequence">The sequence to search for.</param>
        /// <param name="replacement">The sequence to use as a replacement.</param>
        /// <param name="comparison">A function to compare values in the list and in the sequence.</param>
        /// <returns>A new sublist wrapping the list with all of its replacements.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static IExpandableSublist<TList, T> Replace<TList, TSequenceList, TReplacementList, T, TSequence>(
            IExpandableSublist<TList, T> list,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacementList, T> replacement,
            Func<T, TSequence, bool> comparison)
            where TList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacementList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return replace<TList, TSequenceList, TReplacementList, T, TSequence>(list, sequence, replacement, comparison);
        }

        private static IExpandableSublist<TList, T> replace<TList, TSequenceList, TReplacementList, T, TSequence>(
            IExpandableSublist<TList, T> list,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacementList, T> replacement,
            Func<T, TSequence, bool> comparison)
            where TList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacementList : IList<T>
        {
            int past = replace<TList, TSequenceList, TReplacementList, T, TSequence>(
                list.List, list.Offset, list.Offset + list.Count,
                sequence.List, sequence.Offset, sequence.Offset + sequence.Count,
                replacement.List, replacement.Offset, replacement.Offset + replacement.Count,
                comparison);
            return list.Resize(past - list.Offset, true);
        }

        private static int replace<TList, TSequenceList, TReplacementList, T, TSequence>(
            TList list, int first, int past, 
            TSequenceList sequence, int sequenceFirst, int sequencePast, 
            TReplacementList replacement, int replacementFirst, int replacementPast, 
            Func<T, TSequence, bool> comparison)
            where TList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacementList : IList<T>
        {
            int temp = past;
            int sequenceCount = sequencePast - sequenceFirst;
            int replacementCount = replacementPast - replacementFirst;
            first = indexOfSequence<TList, T, TSequenceList, TSequence>(list, first, past, sequence, sequenceFirst, sequencePast, comparison);

            while (first != past)
            {
                if (sequenceCount < replacementCount)
                {
                    int difference = replacementCount - sequenceCount;
                    growAndShift<TList, T>(list, first, difference);
                    past += difference;
                }
                else if (sequenceCount > replacementCount)
                {
                    int index = first + sequenceCount;
                    int difference = sequenceCount - replacementCount;
                    copy<TList, TList, T>(list, index, past, list, index - difference, past);
                    past -= difference;
                }
                first = copy<TReplacementList, TList, T>(replacement, replacementFirst, replacementPast, list, first, past).Item2;
                first = indexOfSequence<TList, T, TSequenceList, TSequence>(list, first, past, sequence, sequenceFirst, sequencePast, comparison);
            }
            if (past < temp)
            {
                removeRange<TList, T>(list, past, temp);
            }
            return past;
        }

        #endregion

        #region Reverse

        /// <summary>
        /// Reverses the items in a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to reverse.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static void Reverse<TList, T>(IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            reverse<TList, T>(list.List, list.Offset, list.Offset + list.Count);
        }

        private static void reverse<TList, T>(TList list, int first, int past)
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

        /// <summary>
        /// Rotates the items in a list the specified amount to the left.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to rotate.</param>
        /// <param name="shift">The amount to shift the items to the left.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>
        /// If the shift is negative, the algoritm simulates rotating the items to the right. If the shift is larger than the number of items, 
        /// the algorithm will simulate a complete rotation as many times as necessary.
        /// </remarks>
        public static void RotateLeft<TList, T>(IMutableSublist<TList, T> list, int shift)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            rotateLeftUnreduced<TList, T>(list.List, list.Offset, list.Offset + list.Count, shift);
        }

        private static void rotateLeftUnreduced<TList, T>(TList list, int first, int past, int shift)
            where TList : IList<T>
        {
            int middle = getReducedOffset<TList, T>(list, first, past, shift);
            middle += first;
            rotateLeft<TList, T>(list, first, middle, past);
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

        #endregion

        #region SelectionSort

        /// <summary>
        /// Sorts a list using the selection sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static void SelectionSort<TList, T>(IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            selectionSort<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Sorts a list using the selection sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static void SelectionSort<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
            selectionSort<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Sorts a list using the selection sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static void SelectionSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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
            selectionSort<TList, T>(list, comparison);
        }

        private static void selectionSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            selectionSort<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
        }

        private static void selectionSort<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            while (first < past - 1)
            {
                int smallest = minimum<TList, T>(list, first, past, comparison);
                if (first != smallest)
                {
                    T temp = list[first];
                    list[first] = list[smallest];
                    list[smallest] = temp;
                }
                ++first;
            }
        }

        #endregion

        #region ShellSort

        /// <summary>
        /// Sorts a list using the selection sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static void ShellSort<TList, T>(IMutableSublist<TList, T> list)
            where TList : IList<T>
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            shellSort<TList, T>(list, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Sorts a list using the selection sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static void ShellSort<TList, T>(IMutableSublist<TList, T> list, IComparer<T> comparer)
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
            shellSort<TList, T>(list, comparer.Compare);
        }

        /// <summary>
        /// Sorts a list using the selection sort algorithm.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        public static void ShellSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
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
            shellSort<TList, T>(list, comparison);
        }

        private static void shellSort<TList, T>(IMutableSublist<TList, T> list, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            shellSort<TList, T>(list.List, list.Offset, list.Offset + list.Count, comparison);
        }

        private static void shellSort<TList, T>(TList list, int first, int past, Func<T, T, int> comparison)
            where TList : IList<T>
        {
            int half = (past - first) / 2;
            while (half > 0)
            {
                for (int i = first + half; i < past; ++i)
                {
                    for (int j = i; j >= first + half && comparison(list[j], list[j - half]) < 0; j -= half)
                    {
                        T temp = list[j];
                        list[j] = list[j - half];
                        list[j - half] = temp;
                    }
                }
                half /= 2;
            }
        }

        #endregion

        #region StablePartition

        /// <summary>
        /// Partitions a list such that the items satisfying the predicate appear in the front of the list, 
        /// retaining their relative order, and those that don't appear at the end, retaining their relative order.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to partition.</param>
        /// <param name="predicate">The condition an item must satisfy to appear at the front of the list.</param>
        /// <returns>
        /// The index of the first item that does not satisfy the predicate 
        /// -or- the index past the end of the list, if every item satisfies the condition.
        /// </returns>
        /// <remarks>
        /// This algorithm requires temporarily storing the items that do not satisfy the predicate in another container.
        /// </remarks>
        public static int StablePartition<TList, T>(IMutableSublist<TList, T> list, Func<T, bool> predicate)
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
            int result = stablePartition<TList, T>(list.List, list.Offset, list.Offset + list.Count, predicate);
            result -= list.Offset;
            return result;
        }

        private static int stablePartition<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
            where TList : IList<T>
        {
            List<T> buffer = new List<T>();
            int next = first;
            while (first != past)
            {
                if (predicate(list[first]))
                {
                    list[next] = list[first];
                    ++next;
                }
                else
                {
                    buffer.Add(list[first]);
                }
                ++first;
            }
            copy<List<T>, TList, T>(buffer, 0, buffer.Count, list, next, past);
            return next;
        }

        #endregion

        #region SwapRanges

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
        public static int SwapRanges<TList1, TList2, T>(IMutableSublist<TList1, T> list1, IMutableSublist<TList2, T> list2)
            where TList1 : IList<T>
            where TList2 : IList<T>
        {
            if (list1 == null)
            {
                throw new ArgumentNullException("source1");
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

        #region TrueForAll

        /// <summary>
        /// Determines whether every item in a list satisfies the predicate.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <param name="predicate">The condition every item must satisfy.</param>
        /// <returns>True if every item in the list satisfies the predicate; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static bool TrueForAll<TList, T>(IReadOnlySublist<TList, T> list, Func<T, bool> predicate)
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
            return trueForAll<TList, T>(list.List, list.Offset, list.Offset + list.Count, predicate);
        }

        private static bool trueForAll<TList, T>(TList list, int first, int past, Func<T, bool> predicate)
            where TList : IList<T>
        {
            while (first != past)
            {
                if (!predicate(list[first]))
                {
                    return false;
                }
                ++first;
            }
            return true;
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
        public static int UpperBound<TList, T>(IReadOnlySublist<TList, T> list, T value)
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
        public static int UpperBound<TList, T>(IReadOnlySublist<TList, T> list, T value, IComparer<T> comparer)
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
        public static int UpperBound<TList, T, TSearch>(IReadOnlySublist<TList, T> list, TSearch value, Func<T, TSearch, int> comparison)
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

        /// <summary>
        /// Implicitly creates a new instance of a Sublist spanning the entirety of a list.
        /// </summary>
        /// <param name="list">The list to wrap with a Sublist.</param>
        /// <returns>A new instance of a Sublist.</returns>
        public static implicit operator Sublist<TList, T>(TList list)
        {
            return new Sublist<TList, T>(list);
        }
    }
}
