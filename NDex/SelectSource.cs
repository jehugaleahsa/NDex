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
        /// Converts the items in the given list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the underlying source list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the source list.</typeparam>
        /// <typeparam name="TDestination">The type of the items in the destination list.</typeparam>
        /// <param name="source">The list of items to convert.</param>
        /// <param name="selector">A function for converting items in the source list to values for the destination list.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The selector is null.</exception>
        public static Source<TDestination, SelectResult> Select<TSourceList, TSource, TDestination>(
            this IReadOnlySublist<TSourceList, TSource> source,
            Func<TSource, TDestination> selector)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            return new SelectSource<TSourceList, TSource, TDestination>(source, selector);
        }
    }

    #endregion

    #region SelectResult

    /// <summary>
    /// Holds the results of copying a select operation.
    /// </summary>
    public sealed class SelectResult
    {
        /// <summary>
        /// Initializes a new instance of a SelectResult.
        /// </summary>
        internal SelectResult()
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
        public static implicit operator int(SelectResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region SelectSource

    internal sealed class SelectSource<TSourceList, TSource, TDestination> : Source<TDestination, SelectResult>
        where TSourceList : IList<TSource>
    {
        private readonly IReadOnlySublist<TSourceList, TSource> source;
        private readonly Func<TSource, TDestination> selector;

        public SelectSource(IReadOnlySublist<TSourceList, TSource> source, Func<TSource, TDestination> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        protected override IExpandableSublist<TDestinationList, TDestination> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TDestination> destination)
        {
            int result = addConverted<TDestinationList>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                selector);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addConverted<TDestinationList>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<TSource, TDestination> selector)
            where TDestinationList : IList<TDestination>
        {
            Sublist.GrowAndShift<TDestinationList, TDestination>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = copyConverted<TDestinationList>(
                source, first, past,
                destination, destinationPast, destination.Count,
                selector);
            return indexes.Item2;
        }

        protected override SelectResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TDestination> destination) 
        {
            Tuple<int, int> indexes = copyConverted<TDestinationList>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                selector);
            SelectResult result = new SelectResult();
            result.SourceOffset = indexes.Item1 - source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }

        private static Tuple<int, int> copyConverted<TDestinationList>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TDestination> selector)
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
    }

    #endregion
}
