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
        /// Gets the items in reverse order.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to reverse.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static ReverseSource<TSourceList, TSource> Reverse<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new ReverseSource<TSourceList, TSource>(source);
        }

        /// <summary>
        /// Gets the items in reverse order.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to reverse.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static InPlaceReverseSource<TSourceList, TSource> Reverse<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new InPlaceReverseSource<TSourceList, TSource>(source);
        }
    }

    #endregion

    #region ReverseResult

    /// <summary>
    /// Holds the results of copying a Reverse operation.
    /// </summary>
    public sealed class ReverseResult
    {
        /// <summary>
        /// Initializes a new instance of a ReverseResult.
        /// </summary>
        internal ReverseResult()
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
        public static implicit operator int(ReverseResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region ReverseSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist, or perform the action in-place.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class ReverseSource<TSourceList, TSource> : Source<TSource, ReverseResult>
        where TSourceList : IList<TSource>
    {
        internal ReverseSource(IReadOnlySublist<TSourceList, TSource> source)
        {
            Source = source;
        }

        /// <summary>
        /// Gets the list that is the source of the items.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected sealed override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int result = Sublist.AddReversed<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset + destination.Count);
            return destination.Resize(result - destination.Offset, true);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected sealed override ReverseResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = Sublist.CopyReversed<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count);
            ReverseResult result = new ReverseResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist, or perform the action in-place.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class InPlaceReverseSource<TSourceList, TSource> : ReverseSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InPlaceReverseSource(IMutableSublist<TSourceList, TSource> source)
            : base(source)
        {
        }

        /// <summary>
        /// Performs the reverse in-place.
        /// </summary>
        public void InPlace()
        {
            Sublist.Reverse<TSourceList, TSource>(Source.List, Source.Offset, Source.Offset + Source.Count);
        }
    }

    #endregion
}
