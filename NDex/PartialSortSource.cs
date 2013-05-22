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
        /// <summary>
        /// Adds the given number of items from a list to a destination list as if the source list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to be added.</param>
        /// <param name="numberOfItems">The number of items to add to the destination.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The specified number of items is larger than the source list.</exception>
        /// <remarks>
        /// The items in the list will be sorted according to the default ordering of the items.
        /// </remarks>
        public static PartialSortSource<TSourceList, TSource> PartialSort<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            int numberOfItems)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfItems < 0 || numberOfItems > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, Resources.IndexOutOfRange);
            }
            return new PartialSortSource<TSourceList, TSource>(source, numberOfItems, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Adds the given number of items from a list to a destination list as if the source list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to be added.</param>
        /// <param name="numberOfItems">The number of items to add to the destination.</param>
        /// <param name="comparer">The comparer to use to compare items in the source list.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The specified number of items is larger than the source list.</exception>
        public static PartialSortSource<TSourceList, TSource> PartialSort<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            int numberOfItems,
            IComparer<TSource> comparer)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfItems < 0 || numberOfItems > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, Resources.IndexOutOfRange);
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return new PartialSortSource<TSourceList, TSource>(source, numberOfItems, comparer.Compare);
        }

        /// <summary>
        /// Adds the given number of items from a list to a destination list as if the source list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to be added.</param>
        /// <param name="numberOfItems">The number of items to add to the destination.</param>
        /// <param name="comparison">The delegate used to compare items in the source list.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The specified number of items is larger than the source list.</exception>
        public static PartialSortSource<TSourceList, TSource> PartialSort<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            int numberOfItems,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfItems < 0 || numberOfItems > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, Resources.IndexOutOfRange);
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return new PartialSortSource<TSourceList, TSource>(source, numberOfItems, comparison);
        }

        /// <summary>
        /// Adds the given number of items from a list to a destination list as if the source list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to be added.</param>
        /// <param name="numberOfItems">The number of items to add to the destination.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The specified number of items is larger than the source list.</exception>
        /// <remarks>
        /// The items in the list will be sorted according to the default ordering of the items.
        /// </remarks>
        public static InPlacePartialSortSource<TSourceList, TSource> PartialSort<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            int numberOfItems)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfItems < 0 || numberOfItems > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, Resources.IndexOutOfRange);
            }
            return new InPlacePartialSortSource<TSourceList, TSource>(source, numberOfItems, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Adds the given number of items from a list to a destination list as if the source list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to be added.</param>
        /// <param name="numberOfItems">The number of items to add to the destination.</param>
        /// <param name="comparer">The comparer to use to compare items in the source list.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The specified number of items is larger than the source list.</exception>
        public static InPlacePartialSortSource<TSourceList, TSource> PartialSort<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            int numberOfItems,
            IComparer<TSource> comparer)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfItems < 0 || numberOfItems > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, Resources.IndexOutOfRange);
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return new InPlacePartialSortSource<TSourceList, TSource>(source, numberOfItems, comparer.Compare);
        }

        /// <summary>
        /// Adds the given number of items from a list to a destination list as if the source list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to be added.</param>
        /// <param name="numberOfItems">The number of items to add to the destination.</param>
        /// <param name="comparison">The delegate used to compare items in the source list.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The specified number of items is larger than the source list.</exception>
        public static InPlacePartialSortSource<TSourceList, TSource> PartialSort<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            int numberOfItems,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfItems < 0 || numberOfItems > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, Resources.IndexOutOfRange);
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return new InPlacePartialSortSource<TSourceList, TSource>(source, numberOfItems, comparison);
        }
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

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class PartialSortSource<TSourceList, TSource> : Source<TSource, PartialSortResult>
        where TSourceList : IList<TSource>
    {
        internal PartialSortSource(
            IReadOnlySublist<TSourceList, TSource> source,
            int numberOfItems,
            Func<TSource, TSource, int> comparison)
        {
            Source = source;
            NumberOfItems = numberOfItems;
            Comparison = comparison;
        }

        /// <summary>
        /// Gets the list that will be sorted.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the number of items to sort.
        /// </summary>
        protected int NumberOfItems { get; private set; }

        /// <summary>
        /// Gets the function to use to compare items.
        /// </summary>
        protected Func<TSource, TSource, int> Comparison { get; private set; }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected sealed override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int result = addPartiallySorted<TDestinationList>(
                Source.List, Source.Offset, Source.Offset + NumberOfItems, Source.Offset + Source.Count,
                destination.List, destination.Offset + destination.Count,
                Comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addPartiallySorted<TDestinationList>(
            TSourceList source, int first, int middle, int past,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TDestinationList : IList<TSource>
        {
            int count = middle - first;
            Sublist.GrowAndShift<TDestinationList, TSource>(destination, destinationPast, count);
            return copyPartiallySorted<TDestinationList>(
                source, first, past,
                destination, destinationPast, destinationPast + count,
                comparison);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected sealed override PartialSortResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            int index = copyPartiallySorted<TDestinationList>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + NumberOfItems,
                Comparison);
            PartialSortResult result = new PartialSortResult();
            result.SourceOffset = Source.Count;
            result.DestinationOffset = index - destination.Offset;
            return result;
        }

        private static int copyPartiallySorted<TDestinationList>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TDestinationList : IList<TSource>
        {
            int destinationMiddle = destinationFirst;
            while (first != past && destinationMiddle != destinationPast)
            {
                destination[destinationMiddle] = source[first];
                ++first;
                ++destinationMiddle;
            }
            Sublist.MakeHeap<TDestinationList, TSource>(destination, destinationFirst, destinationMiddle, comparison);

            int numberOfItems = destinationMiddle - destinationFirst;
            while (first != past)
            {
                if (comparison(source[first], destination[destinationFirst]) < 0)
                {
                    Sublist.AdjustHeap<TDestinationList, TSource>(destination, destinationFirst, 0, numberOfItems, source[first], comparison);
                }
                ++first;
            }
            Sublist.HeapSort<TDestinationList, TSource>(destination, destinationFirst, destinationMiddle, comparison);
            return destinationMiddle;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class InPlacePartialSortSource<TSourceList, TSource> : PartialSortSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InPlacePartialSortSource(
            IMutableSublist<TSourceList, TSource> source,
            int numberOfItems,
            Func<TSource, TSource, int> comparison)
            : base(source, numberOfItems, comparison)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        public void InPlace()
        {
            partialSort(Source.List, Source.Offset, Source.Offset + NumberOfItems, Source.Offset + Source.Count, Comparison);
        }

        private static void partialSort(TSourceList list, int first, int middle, int past, Func<TSource, TSource, int> comparison)
        {
            if (past - first > 1)
            {
                Sublist.MakeHeap<TSourceList, TSource>(list, first, middle, comparison);
                int numberOfItems = middle - first;
                for (int next = middle; next != past; ++next)
                {
                    if (comparison(list[next], list[first]) < 0)
                    {
                        TSource value = list[next];
                        list[next] = list[first];
                        Sublist.AdjustHeap<TSourceList, TSource>(list, first, 0, numberOfItems, value, comparison);
                    }
                }
                Sublist.HeapSort<TSourceList, TSource>(list, first, middle, comparison);
            }
        }
    }

    #endregion
}
