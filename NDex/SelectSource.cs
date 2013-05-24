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
        /// <typeparam name="TSourceList">The type of the underlying list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <typeparam name="TDestination">The type to convert the items to.</typeparam>
        /// <param name="source">The list of items to convert.</param>
        /// <param name="selector">A function for converting items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The selector is null.</exception>
        public static SelectSource<TSourceList, TSource, TDestination> Select<TSourceList, TSource, TDestination>(
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

        /// <summary>
        /// Converts the items in the given list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the underlying list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items to convert.</param>
        /// <param name="selector">A function for converting items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The selector is null.</exception>
        public static InPlaceSelectSource<TSourceList, TSource> Select<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            Func<TSource, TSource> selector)
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
            return new InPlaceSelectSource<TSourceList, TSource>(source, selector);
        }
    }

    #endregion

    #region SelectResult

    /// <summary>
    /// Holds the results of copying a Select operation.
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

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    /// <typeparam name="TDestination">The type of the items in the destination.</typeparam>
    public class SelectSource<TSourceList, TSource, TDestination> : Source<TDestination, SelectResult>
        where TSourceList : IList<TSource>
    {
        internal SelectSource(IReadOnlySublist<TSourceList, TSource> source, Func<TSource, TDestination> selector)
        {
            this.Source = source;
            this.Selector = selector;
        }

        /// <summary>
        /// Gets the list to apply the selector to.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the selector function to apply to the items.
        /// </summary>
        protected Func<TSource, TDestination> Selector { get; private set; }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected override IExpandableSublist<TDestinationList, TDestination> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TDestination> destination)
        {
            int result = Sublist.AddSelect<TSourceList, TSource, TDestinationList, TDestination>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset + destination.Count,
                Selector);
            return destination.Resize(result - destination.Offset, true);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected override SelectResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TDestination> destination) 
        {
            Tuple<int, int> indexes = Sublist.CopySelect<TSourceList, TSource, TDestinationList, TDestination>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Selector);
            SelectResult result = new SelectResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist or perform the operation in-place.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class InPlaceSelectSource<TSourceList, TSource> : SelectSource<TSourceList, TSource, TSource>
        where TSourceList : IList<TSource>
    {
        internal InPlaceSelectSource(IReadOnlySublist<TSourceList, TSource> source, Func<TSource, TSource> selector)
            : base(source, selector)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        public void InPlace()
        {
            Sublist.CopySelect<TSourceList, TSource, TSourceList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                Source.List, Source.Offset, Source.Offset + Source.Count,
                Selector);
        }
    }

    #endregion
}
