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
        /// Adds the items from a list to a destination, rotated to the left by the given shift amount.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="shift">The amount to rotate the items to the left.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>
        /// If the shift is negative, the algoritm simulates rotating the items to the right. If the shift is larger than the number of items, 
        /// the algorithm will simulate a complete rotation as many times as necessary.
        /// </remarks>
        public static RotateLeftSource<TSourceList, TSource> RotateLeft<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            int shift)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new RotateLeftSource<TSourceList, TSource>(source, shift);
        }

        /// <summary>
        /// Adds the items from a list to a destination, rotated to the left by the given shift amount.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="shift">The amount to rotate the items to the left.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <remarks>
        /// If the shift is negative, the algoritm simulates rotating the items to the right. If the shift is larger than the number of items, 
        /// the algorithm will simulate a complete rotation as many times as necessary.
        /// </remarks>
        public static InplaceRotateLeftSource<TSourceList, TSource> RotateLeft<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            int shift)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new InplaceRotateLeftSource<TSourceList, TSource>(source, shift);
        }
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

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class RotateLeftSource<TSourceList, TSource> : Source<TSource, RotateLeftResult>
        where TSourceList : IList<TSource>
    {
        internal RotateLeftSource(IReadOnlySublist<TSourceList, TSource> source, int shift)
        {
            Source = source;
            Shift = shift;
        }

        /// <summary>
        /// Gets the list whose items will be rotated.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the amount to shift the items.
        /// </summary>
        protected int Shift { get; private set; }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected sealed override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int result = addRotatedLeftUnreduced<TDestinationList>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset + destination.Count,
                Shift);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addRotatedLeftUnreduced<TDestinationList>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            int shift)
            where TDestinationList : IList<TSource>
        {
            int middle = Sublist.GetReducedOffset<TSourceList, TSource>(source, first, past, shift);
            middle += first;
            return addRotatedLeft<TDestinationList>(source, first, middle, past, destination, destinationPast);
        }

        private static int addRotatedLeft<TDestinationList>(
            TSourceList source, int first, int middle, int past,
            TDestinationList destination, int destinationPast)
            where TDestinationList : IList<TSource>
        {
            Sublist.GrowAndShift<TDestinationList, TSource>(destination, destinationPast, past - first);
            destinationPast = Sublist.Copy<TSourceList, TDestinationList, TSource>(source, middle, past, destination, destinationPast, destination.Count).Item2;
            destinationPast = Sublist.Copy<TSourceList, TDestinationList, TSource>(source, first, middle, destination, destinationPast, destination.Count).Item2;
            return destinationPast;
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected sealed override RotateLeftResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = copyRotatedLeftUnreduced<TDestinationList>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Shift);
            RotateLeftResult result = new RotateLeftResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }

        private static Tuple<int, int> copyRotatedLeftUnreduced<TDestinationList>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            int shift)
            where TDestinationList : IList<TSource>
        {
            int middle = Sublist.GetReducedOffset<TSourceList, TSource>(source, first, past, shift);
            middle += first;
            return copyRotatedLeft<TDestinationList>(
                source, first, middle, past,
                destination, destinationFirst, destinationPast);
        }

        private static Tuple<int, int> copyRotatedLeft<TDestinationList>(
            TSourceList source, int first, int middle, int past,
            TDestinationList destination, int destinationFirst, int destinationPast)
            where TDestinationList : IList<TSource>
        {
            Tuple<int, int> indexes1 = Sublist.Copy<TSourceList, TDestinationList, TSource>(
                source, middle, past,
                destination, destinationFirst, destinationPast);
            int position = indexes1.Item1;
            destinationFirst = indexes1.Item2;
            if (position == past)
            {
                Tuple<int, int> indexes2 = Sublist.Copy<TSourceList, TDestinationList, TSource>(
                    source, first, middle,
                    destination, destinationFirst, destinationPast);
                position = indexes2.Item1;
                destinationFirst = indexes2.Item2;
            }
            return new Tuple<int, int>(position, destinationFirst);
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class InplaceRotateLeftSource<TSourceList, TSource> : RotateLeftSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InplaceRotateLeftSource(IMutableSublist<TSourceList, TSource> source, int shift)
            : base(source, shift)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        public void InPlace()
        {
            rotateLeftUnreduced(Source.List, Source.Offset, Source.Offset + Source.Count, Shift);
        }

        private static void rotateLeftUnreduced(TSourceList list, int first, int past, int shift)
        {
            int middle = Sublist.GetReducedOffset<TSourceList, TSource>(list, first, past, shift);
            middle += first;
            Sublist.RotateLeft<TSourceList, TSource>(list, first, middle, past);
        }
    }

    #endregion
}
