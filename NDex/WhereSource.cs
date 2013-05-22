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
        /// Adds the items from a list that satisfy the predicate to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to conditionally add.</param>
        /// <param name="predicate">The condition an item must satisfy to be added to the destination.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static WhereSource<TSourceList, TSource> Where<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            return new WhereSource<TSourceList, TSource>(source, predicate);
        }

        /// <summary>
        /// Adds the items from a list that satisfy the predicate to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to conditionally add.</param>
        /// <param name="predicate">The condition an item must satisfy to be added to the destination.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static InplaceWhereSource<TSourceList, TSource> Where<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            return new InplaceWhereSource<TSourceList, TSource>(source, predicate);
        }
    }

    #endregion

    #region WhereResult

    /// <summary>
    /// Holds the results of copying the results of a Where operation.
    /// </summary>
    public sealed class WhereResult
    {
        /// <summary>
        /// Initializes a new instance of a WhereResult.
        /// </summary>
        internal WhereResult()
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
        public static implicit operator int(WhereResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region WhereSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class WhereSource<TSourceList, TSource> : Source<TSource, WhereResult>
        where TSourceList : IList<TSource>
    {
        internal WhereSource(IReadOnlySublist<TSourceList, TSource> source, Func<TSource, bool> predicate)
        {
            Source = source;
            Predicate = predicate;
        }

        /// <summary>
        /// Gets the list whose items will be filtered.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the function used to determine whether an item will be kept.
        /// </summary>
        protected Func<TSource, bool> Predicate { get; private set; }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected sealed override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int result = addIf<TDestinationList>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset + destination.Count,
                Predicate);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addIf<TDestinationList>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<TSource, bool> predicate)
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
            Sublist.RotateLeft<TDestinationList, TSource>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected sealed override WhereResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = copyIf<TDestinationList>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Predicate);
            WhereResult result = new WhereResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }

        private static Tuple<int, int> copyIf<TDestinationList>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, bool> predicate)
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
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class InplaceWhereSource<TSourceList, TSource> : WhereSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InplaceWhereSource(IMutableSublist<TSourceList, TSource> source, Func<TSource, bool> predicate)
            : base(source, predicate)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        /// <returns>The index past the last remaining item.</returns>
        public int InPlace()
        {
            int result = removeIf(Source.List, Source.Offset, Source.Offset + Source.Count, Predicate);
            result -= Source.Offset;
            return result;
        }

        private static int removeIf(TSourceList list, int first, int past, Func<TSource, bool> predicate)
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
    }

    #endregion
}
