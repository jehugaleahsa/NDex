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
        /// Gets the items in the list so they form a heap.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to turn into a heap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static MakeHeapSource<TSourceList, TSource> MakeHeap<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new MakeHeapSource<TSourceList, TSource>(source, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Gets the items in the list so they form a heap.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to turn into a heap.</param>
        /// <param name="comparer">The comparer to use to compare the items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static MakeHeapSource<TSourceList, TSource> MakeHeap<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source, IComparer<TSource> comparer)
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
            return new MakeHeapSource<TSourceList, TSource>(source, comparer.Compare);
        }

        /// <summary>
        /// Gets the items in the list so they form a heap.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to turn into a heap.</param>
        /// <param name="comparison">The function to use to compare the items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static MakeHeapSource<TSourceList, TSource> MakeHeap<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
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
            return new MakeHeapSource<TSourceList, TSource>(source, comparison);
        }

        /// <summary>
        /// Gets the items in the list so they form a heap.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to turn into a heap.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        public static InPlaceMakeHeapSource<TSourceList, TSource> MakeHeap<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new InPlaceMakeHeapSource<TSourceList, TSource>(source, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Gets the items in the list so they form a heap.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to turn into a heap.</param>
        /// <param name="comparer">The comparer to use to compare the items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static InPlaceMakeHeapSource<TSourceList, TSource> MakeHeap<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source, IComparer<TSource> comparer)
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
            return new InPlaceMakeHeapSource<TSourceList, TSource>(source, comparer.Compare);
        }

        /// <summary>
        /// Gets the items in the list so they form a heap.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to turn into a heap.</param>
        /// <param name="comparison">The function to use to compare the items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static InPlaceMakeHeapSource<TSourceList, TSource> MakeHeap<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
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
            return new InPlaceMakeHeapSource<TSourceList, TSource>(source, comparison);
        }
    }

    #endregion

    #region MakeHeapResult

    /// <summary>
    /// Holds the results of copying a Distinct operation.
    /// </summary>
    public sealed class MakeHeapResult
    {
        /// <summary>
        /// Initializes a new instance of a MakeHeapResult.
        /// </summary>
        internal MakeHeapResult()
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
        public static implicit operator int(MakeHeapResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region MakeHeapSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class MakeHeapSource<TSourceList, TSource> : Source<TSource, MakeHeapResult>
        where TSourceList : IList<TSource>
    {
        internal MakeHeapSource(IReadOnlySublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
        {
            Source = source;
            Comparison = comparison;
        }

        /// <summary>
        /// Gets the list to make a heap from.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the comparison function used to detect duplicate items.
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
            int past = Sublist.AddMakeHeap<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
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
        protected override MakeHeapResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = Sublist.CopyMakeHeap<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Comparison);
            MakeHeapResult result = new MakeHeapResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist or performs the operation in-place.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class InPlaceMakeHeapSource<TSourceList, TSource> : MakeHeapSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InPlaceMakeHeapSource(IMutableSublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
            : base(source, comparison)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        /// <returns>The integer past the last unique item.</returns>
        public void InPlace()
        {
            Sublist.MakeHeap<TSourceList, TSource>(Source.List, Source.Offset, Source.Offset + Source.Count, Comparison);
        }
    }

    #endregion
}
