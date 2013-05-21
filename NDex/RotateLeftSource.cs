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
        #region AddRotatedLeft

        /// <summary>
        /// Adds the items from a list to a destination, rotated to the left by the given shift amount.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="shift">The amount to rotate the items to the left.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <remarks>
        /// If the shift is negative, the algoritm simulates rotating the items to the right. If the shift is larger than the number of items, 
        /// the algorithm will simulate a complete rotation as many times as necessary.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddRotatedLeft<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination, int shift)
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
            int result = addRotatedLeftUnreduced<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                shift);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addRotatedLeftUnreduced<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            int shift)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int middle = getReducedOffset<TSourceList, T>(source, first, past, shift);
            middle += first;
            return addRotatedLeft<TSourceList, TDestinationList, T>(source, first, middle, past, destination, destinationPast);
        }

        private static int addRotatedLeft<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int middle, int past,
            TDestinationList destination, int destinationPast)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            GrowAndShift<TDestinationList, T>(destination, destinationPast, past - first);
            destinationPast = Copy<TSourceList, TDestinationList, T>(source, middle, past, destination, destinationPast, destination.Count).Item2;
            destinationPast = Copy<TSourceList, TDestinationList, T>(source, first, middle, destination, destinationPast, destination.Count).Item2;
            return destinationPast;
        }

        #endregion

        #region CopyRotatedLeft

        /// <summary>
        /// Copies the items from a list to a destination, rotated to the left by the given shift amount.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="shift">The amount to rotate the items to the left.</param>
        /// <returns>The index into the destination past the last copied item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <remarks>
        /// If the shift is negative, the algoritm simulates rotating the items to the right. If the shift is larger than the number of items, 
        /// the algorithm will simulate a complete rotation as many times as necessary.
        /// </remarks>
        public static RotateLeftResult CopyRotatedLeft<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            int shift)
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
            Tuple<int, int> indexes = copyRotatedLeftUnreduced<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                shift);
            RotateLeftResult result = new RotateLeftResult();
            result.SourceOffset = indexes.Item1 - source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }

        private static Tuple<int, int> copyRotatedLeftUnreduced<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            int shift)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int middle = getReducedOffset<TSourceList, T>(source, first, past, shift);
            middle += first;
            return copyRotatedLeft<TSourceList, TDestinationList, T>(
                source, first, middle, past,
                destination, destinationFirst, destinationPast);
        }

        private static Tuple<int, int> copyRotatedLeft<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int middle, int past,
            TDestinationList destination, int destinationFirst, int destinationPast)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            Tuple<int, int> indexes1 = Copy<TSourceList, TDestinationList, T>(
                source, middle, past,
                destination, destinationFirst, destinationPast);
            int position = indexes1.Item1;
            destinationFirst = indexes1.Item2;
            if (position == past)
            {
                Tuple<int, int> indexes2 = Copy<TSourceList, TDestinationList, T>(
                    source, first, middle,
                    destination, destinationFirst, destinationPast);
                position = indexes2.Item1;
                destinationFirst = indexes2.Item2;
            }
            return new Tuple<int, int>(position, destinationFirst);
        }

        #endregion
    }

    #endregion

    #region RotateLeftResult

    /// <summary>
    /// Holds the results of copying the results of a RotateLeft operation.
    /// </summary>
    public sealed class RotateLeftResult
    {
        /// <summary>
        /// Initializes a new instance of a RotateLeftResult.
        /// </summary>
        internal RotateLeftResult()
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
        public static implicit operator int(RotateLeftResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region RotateLeftSource

    internal sealed class RotateLeftSource<TSourceList, TSource> : Source<TSource, RotateLeftResult>
        where TSourceList : IList<TSource>
    {
        private readonly IReadOnlySublist<TSourceList, TSource> source;
        private readonly int shift;

        public RotateLeftSource(IReadOnlySublist<TSourceList, TSource> source, int shift)
        {
            this.source = source;
            this.shift = shift;
        }

        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            throw new NotImplementedException();
        }

        protected override RotateLeftResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
