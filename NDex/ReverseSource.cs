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
            GrowAndShift<TDestinationList, T>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = CopyReversed<TSourceList, TDestinationList, T>(
                source, first, past,
                destination, destinationPast, destination.Count);
            return indexes.Item2;
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
        public static ReverseResult CopyReversed<TSourceList, TDestinationList, T>(
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
            ReverseResult result = new ReverseResult();
            result.SourceOffset = indexes.Item1 - source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
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
    }

    #endregion

    #region ReverseResult

    /// <summary>
    /// Holds the results of copying the results of a Reverse operation.
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

    internal sealed class ReverseSource<TSourceList, TSource> : Source<TSource, ReverseResult>
        where TSourceList : IList<TSource>
    {
        private readonly IReadOnlySublist<TSourceList, TSource> source;

        public ReverseSource(IReadOnlySublist<TSourceList, TSource> source)
        {
            this.source = source;
        }

        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            return Sublist.AddReversed(source, destination);
        }

        protected override ReverseResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            return Sublist.CopyReversed(source, destination);
        }
    }

    #endregion
}
