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
            RotateLeft<TDestinationList, T>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
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
        public static WhereResult CopyIf<TSourceList, TDestinationList, T>(
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
            WhereResult result = new WhereResult();
            result.SourceOffset = indexes.Item1 - source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
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

    internal sealed class WhereSource<TSourceList, TSource> : Source<TSource, WhereResult>
        where TSourceList : IList<TSource>
    {
        private readonly IReadOnlySublist<TSourceList, TSource> source;
        private readonly Func<TSource, bool> predicate;

        public WhereSource(IReadOnlySublist<TSourceList, TSource> source, Func<TSource, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            return Sublist.AddIf(source, destination, predicate);
        }

        protected override WhereResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            return Sublist.CopyIf(source, destination, predicate);
        }
    }

    #endregion
}
