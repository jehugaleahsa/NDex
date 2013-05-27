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
        /// Sorts the items in the list, maintaining the relative positions of equal items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static StableSortSource<TSourceList, TSource[], TSource> StableSort<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            var buffer = new TSource[source.Count / 2].ToSublist();
            return new StableSortSource<TSourceList, TSource[], TSource>(source, buffer, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Sorts the items in the list, maintaining the relative positions of equal items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TBufferList">The type of the buffer.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="buffer">The buffer to use.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer is null.</exception>
        public static StableSortSource<TSourceList, TBufferList, TSource> StableSort<TSourceList, TBufferList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            IMutableSublist<TBufferList, TSource> buffer)
            where TSourceList : IList<TSource>
            where TBufferList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            return new StableSortSource<TSourceList, TBufferList, TSource>(source, buffer, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Sorts the items in the list, maintaining the relative positions of equal items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static StableSortSource<TSourceList, TSource[], TSource> StableSort<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source, IComparer<TSource> comparer)
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
            var buffer = new TSource[source.Count / 2].ToSublist();
            return new StableSortSource<TSourceList, TSource[], TSource>(source, buffer, comparer.Compare);
        }

        /// <summary>
        /// Sorts the items in the list, maintaining the relative positions of equal items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TBufferList">The type of the buffer.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="buffer">The buffer to use.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static StableSortSource<TSourceList, TBufferList, TSource> StableSort<TSourceList, TBufferList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source, 
            IMutableSublist<TBufferList, TSource> buffer,
            IComparer<TSource> comparer)
            where TSourceList : IList<TSource>
            where TBufferList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return new StableSortSource<TSourceList, TBufferList, TSource>(source, buffer, comparer.Compare);
        }

        /// <summary>
        /// Sorts the items in the list, maintaining the relative positions of equal items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="comparison">The function to use to compare the items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static StableSortSource<TSourceList, TSource[], TSource> StableSort<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
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
            var buffer = new TSource[source.Count / 2].ToSublist();
            return new StableSortSource<TSourceList, TSource[], TSource>(source, buffer, comparison);
        }

        /// <summary>
        /// Sorts the items in the list, maintaining the relative positions of equal items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TBufferList">The type of the buffer.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="buffer">The buffer to use.</param>
        /// <param name="comparison">The function to use to compare the items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static StableSortSource<TSourceList, TBufferList, TSource> StableSort<TSourceList, TBufferList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source, 
            IMutableSublist<TBufferList, TSource> buffer,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TBufferList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return new StableSortSource<TSourceList, TBufferList, TSource>(source, buffer, comparison);
        }

        /// <summary>
        /// Sorts the items in the list, maintaining the relative positions of equal items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static InPlaceStableSortSource<TSourceList, TSource[], TSource> StableSort<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            var buffer = new TSource[source.Count / 2].ToSublist();
            return new InPlaceStableSortSource<TSourceList, TSource[], TSource>(source, buffer, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Sorts the items in the list, maintaining the relative positions of equal items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TBufferList">The type of the buffer.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="buffer">The buffer to use.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer is null.</exception>
        public static InPlaceStableSortSource<TSourceList, TBufferList, TSource> StableSort<TSourceList, TBufferList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            IMutableSublist<TBufferList, TSource> buffer)
            where TSourceList : IList<TSource>
            where TBufferList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            return new InPlaceStableSortSource<TSourceList, TBufferList, TSource>(source, buffer, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Sorts the items in the list, maintaining the relative positions of equal items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static InPlaceStableSortSource<TSourceList, TSource[], TSource> StableSort<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source, IComparer<TSource> comparer)
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
            var buffer = new TSource[source.Count / 2].ToSublist();
            return new InPlaceStableSortSource<TSourceList, TSource[], TSource>(source, buffer, comparer.Compare);
        }

        /// <summary>
        /// Sorts the items in the list, maintaining the relative positions of equal items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TBufferList">The type of the buffer.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="buffer">The buffer to use.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static InPlaceStableSortSource<TSourceList, TBufferList, TSource> StableSort<TSourceList, TBufferList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            IMutableSublist<TBufferList, TSource> buffer,
            IComparer<TSource> comparer)
            where TSourceList : IList<TSource>
            where TBufferList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return new InPlaceStableSortSource<TSourceList, TBufferList, TSource>(source, buffer, comparer.Compare);
        }

        /// <summary>
        /// Sorts the items in the list, maintaining the relative positions of equal items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="comparison">The function to use to compare the items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static InPlaceStableSortSource<TSourceList, TSource[], TSource> StableSort<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
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
            var buffer = new TSource[source.Count / 2].ToSublist();
            return new InPlaceStableSortSource<TSourceList, TSource[], TSource>(source, buffer, comparison);
        }

        /// <summary>
        /// Sorts the items in the list, maintaining the relative positions of equal items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TBufferList">The type of the buffer.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to sort.</param>
        /// <param name="buffer">The buffer to use.</param>
        /// <param name="comparison">The function to use to compare the items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static InPlaceStableSortSource<TSourceList, TBufferList, TSource> StableSort<TSourceList, TBufferList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            IMutableSublist<TBufferList, TSource> buffer,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
            where TBufferList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return new InPlaceStableSortSource<TSourceList, TBufferList, TSource>(source, buffer, comparison);
        }
    }

    #endregion

    #region StableSortResult

    /// <summary>
    /// Holds the results of copying a StableSort operation.
    /// </summary>
    public sealed class StableSortResult
    {
        /// <summary>
        /// Initializes a new instance of a StableSortResult.
        /// </summary>
        internal StableSortResult()
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
        public static implicit operator int(StableSortResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region StableSortSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TBufferList">The type of the buffer's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class StableSortSource<TSourceList, TBufferList, TSource> : Source<TSource, StableSortResult>
        where TSourceList : IList<TSource>
        where TBufferList : IList<TSource>
    {
        internal StableSortSource(
            IReadOnlySublist<TSourceList, TSource> source, 
            IMutableSublist<TBufferList, TSource> buffer,
            Func<TSource, TSource, int> comparison)
        {
            Source = source;
            Buffer = buffer;
            Comparison = comparison;
        }

        /// <summary>
        /// Gets the list to sort.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the buffer to use.
        /// </summary>
        protected IMutableSublist<TBufferList, TSource> Buffer { get; private set; }

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
            int past = Sublist.AddStableSort<TSourceList, TBufferList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                Buffer.List, Buffer.Offset, Buffer.Offset + Buffer.Count,
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
        protected override StableSortResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = Sublist.CopyStableSort<TSourceList, TBufferList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                Buffer.List, Buffer.Offset, Buffer.Offset + Buffer.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Comparison);
            StableSortResult result = new StableSortResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist or performs the operation in-place.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TBufferList">The type of the buffer's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class InPlaceStableSortSource<TSourceList, TBufferList, TSource> : StableSortSource<TSourceList, TBufferList, TSource>
        where TSourceList : IList<TSource>
        where TBufferList : IList<TSource>
    {
        internal InPlaceStableSortSource(
            IMutableSublist<TSourceList, TSource> source, 
            IMutableSublist<TBufferList, TSource> buffer,
            Func<TSource, TSource, int> comparison)
            : base(source, buffer, comparison)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        public void InPlace()
        {
            Sublist.StableSort<TSourceList, TBufferList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                Buffer.List, Buffer.Offset, Buffer.Offset + Buffer.Count,
                Comparison);
        }
    }

    #endregion
}
