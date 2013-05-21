using System;
using System.Collections.Generic;
using NDex.Properties;

namespace NDex
{
    #region Sublist

    /// <summary>
    /// Provides methods for creating and working with instances of Sublist.
    /// </summary>
    public static partial class Sublist
    {
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
            GrowAndShift<TDestinationList, T>(destination, destinationPast, count);
            return copyPartiallySorted<TSourceList, TDestinationList, T>(
                source, first, past,
                destination, destinationPast, destinationPast + count,
                comparison);
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
        public static PartialSortResult CopyPartiallySorted<TSourceList, TDestinationList, T>(
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
        public static PartialSortResult CopyPartiallySorted<TSourceList, TDestinationList, T>(
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
        public static PartialSortResult CopyPartiallySorted<TSourceList, TDestinationList, T>(
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

        private static PartialSortResult copyPartiallySorted<TSourceList, TDestinationList, T>(
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
            PartialSortResult result = new PartialSortResult();
            result.SourceOffset = source.Count;
            result.DestinationOffset = index - destination.Offset;
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
    }

    #endregion

    #region PartialSortResult

    /// <summary>
    /// Holds the results of copying the results of a PartialSort operation.
    /// </summary>
    public sealed class PartialSortResult
    {
        /// <summary>
        /// Initializes a new instance of a PartialSortResult.
        /// </summary>
        internal PartialSortResult()
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
        public static implicit operator int(PartialSortResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region PartialSortSource

    internal sealed class PartialSortSource<TSourceList, TSource> : Source<TSource, PartialSortResult>
        where TSourceList : IList<TSource>
    {
        private readonly IReadOnlySublist<TSourceList, TSource> source;
        private readonly Func<TSource, TSource, int> comparison;

        public PartialSortSource(
            IReadOnlySublist<TSourceList, TSource> source,
            Func<TSource, TSource, int> comparison)
        {
            this.source = source;
            this.comparison = comparison;
        }

        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            // TODO - figure out how pass in numberOfItems
            return Sublist.AddPartiallySorted(source, 0, destination, comparison);
        }

        protected override PartialSortResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            return Sublist.CopyPartiallySorted(source, destination, comparison);
        }
    }

    #endregion
}
