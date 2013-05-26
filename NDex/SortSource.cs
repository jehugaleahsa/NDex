using System;
using System.Collections.Generic;

namespace NDex
{
    #region Sublist

    /// <summary>
    /// Provides methods for creating and working with instances of Sublist.
    /// </summary>
    public static partial class Sublist
    {
        /// <summary>
        /// Sorts the items in the list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static SortSource<TSourceList, TSource> Sort<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new SortSource<TSourceList, TSource>(source, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Sorts the items in the list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static SortSource<TSourceList, TSource> Sort<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source, IComparer<TSource> comparer)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return new SortSource<TSourceList, TSource>(source, comparer.Compare);
        }

        /// <summary>
        /// Sorts the items in the list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="comparison">The function to use to compare the items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static SortSource<TSourceList, TSource> Sort<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return new SortSource<TSourceList, TSource>(source, comparison);
        }

        /// <summary>
        /// Sorts the items in the list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static InPlaceSortSource<TSourceList, TSource> Sort<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new InPlaceSortSource<TSourceList, TSource>(source, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Sorts the items in the list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static InPlaceSortSource<TSourceList, TSource> Sort<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source, IComparer<TSource> comparer)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return new InPlaceSortSource<TSourceList, TSource>(source, comparer.Compare);
        }

        /// <summary>
        /// Sorts the items in the list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="comparison">The function to use to compare the items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static InPlaceSortSource<TSourceList, TSource> Sort<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return new InPlaceSortSource<TSourceList, TSource>(source, comparison);
        }
    }

    #endregion

    #region SortResult

    /// <summary>
    /// Holds the results of copying a Sort operation.
    /// </summary>
    public sealed class SortResult
    {
        /// <summary>
        /// Initializes a new instance of a SortResult.
        /// </summary>
        internal SortResult()
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
        public static implicit operator int(SortResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region SortSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class SortSource<TSourceList, TSource> : Source<TSource, SortResult>
        where TSourceList : IList<TSource>
    {
        internal SortSource(IReadOnlySublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
        {
            Source = source;
            Comparison = comparison;
        }

        /// <summary>
        /// Gets the list to sort.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the function to use to compare the items.
        /// </summary>
        protected Func<TSource, TSource, int> Comparison { get; private set; }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int past = Sublist.AddSort<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset + destination.Count,
                Comparison);
            return destination.Resize(past - destination.Offset, true);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected override SortResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = Sublist.CopySort<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Comparison);
            SortResult result = new SortResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist or performs the operation in-place.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class InPlaceSortSource<TSourceList, TSource> : SortSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InPlaceSortSource(IMutableSublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
            : base(source, comparison)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        public void InPlace()
        {
            Sublist.Sort<TSourceList, TSource>(Source.List, Source.Offset, Source.Offset + Source.Count, Source.Count, Comparison);
        }
    }

    #endregion
}
