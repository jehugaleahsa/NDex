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
        /// Gets the unique items from the source list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to get the unique items from.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>The list must be sorted.</remarks>
        public static DistinctSource<TSourceList, TSource> Distinct<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new DistinctSource<TSourceList, TSource>(source, EqualityComparer<TSource>.Default.Equals);
        }

        /// <summary>
        /// Gets the unique items from the source list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to get the unique items from.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>The list must be sorted.</remarks>
        public static DistinctSource<TSourceList, TSource> Distinct<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            IEqualityComparer<TSource> comparer)
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
            return new DistinctSource<TSourceList, TSource>(source, comparer.Equals);
        }

        /// <summary>
        /// Gets the unique items from the source list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to get the unique items from.</param>
        /// <param name="comparison">The comparison to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        /// <remarks>The list must be sorted.</remarks>
        public static DistinctSource<TSourceList, TSource> Distinct<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            Func<TSource, TSource, bool> comparison)
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
            return new DistinctSource<TSourceList, TSource>(source, comparison);
        }

        /// <summary>
        /// Gets the unique items from the source list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to get the unique items from.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>The list must be sorted.</remarks>
        public static InPlaceDistinctSource<TSourceList, TSource> Distinct<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new InPlaceDistinctSource<TSourceList, TSource>(source, EqualityComparer<TSource>.Default.Equals);
        }

        /// <summary>
        /// Gets the unique items from the source list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to get the unique items from.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>The list must be sorted.</remarks>
        public static InPlaceDistinctSource<TSourceList, TSource> Distinct<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            IEqualityComparer<TSource> comparer)
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
            return new InPlaceDistinctSource<TSourceList, TSource>(source, comparer.Equals);
        }

        /// <summary>
        /// Gets the unique items from the source list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to get the unique items from.</param>
        /// <param name="comparison">The comparison to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        /// <remarks>The list must be sorted.</remarks>
        public static InPlaceDistinctSource<TSourceList, TSource> Distinct<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            Func<TSource, TSource, bool> comparison)
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
            return new InPlaceDistinctSource<TSourceList, TSource>(source, comparison);
        }
    }

    #endregion

    #region DistinctResult

    /// <summary>
    /// Holds the results of copying a Distinct operation.
    /// </summary>
    public sealed class DistinctResult
    {
        /// <summary>
        /// Initializes a new instance of a DistinctResult.
        /// </summary>
        internal DistinctResult()
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
        public static implicit operator int(DistinctResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region DistinctSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class DistinctSource<TSourceList, TSource> : Source<TSource, DistinctResult>
        where TSourceList : IList<TSource>
    {
        internal DistinctSource(IReadOnlySublist<TSourceList, TSource> source, Func<TSource, TSource, bool> comparison)
        {
            Source = source;
            Comparison = comparison;
        }

        /// <summary>
        /// Gets the list to get the unique items from.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the comparison function used to detect duplicate items.
        /// </summary>
        protected Func<TSource, TSource, bool> Comparison { get; private set; }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected sealed override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int result = Sublist.AddDistinct<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset + destination.Count,
                Comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected sealed override DistinctResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = Sublist.CopyDistinct<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Comparison);
            DistinctResult result = new DistinctResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class InPlaceDistinctSource<TSourceList, TSource> : DistinctSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InPlaceDistinctSource(IMutableSublist<TSourceList, TSource> source, Func<TSource, TSource, bool> comparison)
            : base(source, comparison)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        /// <returns>The integer past the last unique item.</returns>
        public int InPlace()
        {
            int result = Sublist.Distinct<TSourceList, TSource>(Source.List, Source.Offset, Source.Offset + Source.Count, Comparison);
            result -= Source.Offset;
            return result;
        }
    }

    #endregion
}
