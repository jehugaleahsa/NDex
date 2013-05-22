﻿using System;
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
        /// Merges the items from two lists such that they remain in sorted order, adding the items
        /// to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list to merge.</param>
        /// <param name="source2">The second list to merge.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <remarks>
        /// The items in the lists must be sorted according to the default ordering of the items.
        /// </remarks>
        public static MergeSource<TSourceList1, TSourceList2, TSource> Merge<TSourceList1, TSourceList2, TSource>(
            this IReadOnlySublist<TSourceList1, TSource> source1,
            IReadOnlySublist<TSourceList2, TSource> source2)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            return new MergeSource<TSourceList1, TSourceList2, TSource>(source1, source2, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Merges the items from two lists such that they remain in sorted order, adding the items
        /// to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list to merge.</param>
        /// <param name="source2">The second list to merge.</param>
        /// <param name="comparer">The comparer to use to compare items from the lists.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer.
        /// </remarks>
        public static MergeSource<TSourceList1, TSourceList2, TSource> Merge<TSourceList1, TSourceList2, TSource>(
            this IReadOnlySublist<TSourceList1, TSource> source1,
            IReadOnlySublist<TSourceList2, TSource> source2,
            IComparer<TSource> comparer)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return new MergeSource<TSourceList1, TSourceList2, TSource>(source1, source2, comparer.Compare);
        }

        /// <summary>
        /// Merges the items from two lists such that they remain in sorted order, adding the items
        /// to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list to merge.</param>
        /// <param name="source2">The second list to merge.</param>
        /// <param name="comparison">The delegate to use to compare items from the lists.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate.
        /// </remarks>
        public static MergeSource<TSourceList1, TSourceList2, TSource> Merge<TSourceList1, TSourceList2, TSource>(
            this IReadOnlySublist<TSourceList1, TSource> source1,
            IReadOnlySublist<TSourceList2, TSource> source2,
            Func<TSource, TSource, int> comparison)
            where TSourceList1 : IList<TSource>
            where TSourceList2 : IList<TSource>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return new MergeSource<TSourceList1, TSourceList2, TSource>(source1, source2, comparison);
        }
    }

    #endregion

    #region MergeResult

    /// <summary>
    /// Holds the results of copying the results of an Merge operation.
    /// </summary>
    public sealed class MergeResult
    {
        /// <summary>
        /// Initializes a new instance of a MergeResult.
        /// </summary>
        internal MergeResult()
        {
        }

        /// <summary>
        /// Gets the index into the first source list where the algorithm stopped.
        /// </summary>
        public int SourceOffset1
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the index into the second source list where the algorithm stopped.
        /// </summary>
        public int SourceOffset2
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
        public static implicit operator int(MergeResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region MergeSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList1">The type of the first source's underlying list.</typeparam>
    /// <typeparam name="TSourceList2">The type of the second source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
    public sealed class MergeSource<TSourceList1, TSourceList2, TSource> : Source<TSource, MergeResult>
        where TSourceList1 : IList<TSource>
        where TSourceList2 : IList<TSource>
    {
        private readonly IReadOnlySublist<TSourceList1, TSource> source1;
        private readonly IReadOnlySublist<TSourceList2, TSource> source2;
        private readonly Func<TSource, TSource, int> comparison;

        internal MergeSource(
            IReadOnlySublist<TSourceList1, TSource> source1, 
            IReadOnlySublist<TSourceList2, TSource> source2, 
            Func<TSource, TSource, int> comparison)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.comparison = comparison;
        }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int result = addMerged<TDestinationList>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addMerged<TDestinationList>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TDestinationList : IList<TSource>
        {
            Sublist.GrowAndShift<TDestinationList, TSource>(destination, destinationPast, (past1 - first1) + (past2 - first2));
            Tuple<int, int, int> indexes = Sublist.CopyMerged<TSourceList1, TSourceList2, TDestinationList, TSource>(
                source1, first1, past1,
                source2, first2, past2,
                destination, destinationPast, destination.Count,
                comparison);
            return indexes.Item3;
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected override MergeResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int, int> indexes = Sublist.CopyMerged<TSourceList1, TSourceList2, TDestinationList, TSource>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            MergeResult result = new MergeResult();
            result.SourceOffset1 = indexes.Item1 - source1.Offset;
            result.SourceOffset2 = indexes.Item2 - source2.Offset;
            result.DestinationOffset = indexes.Item3 - destination.Offset;
            return result;
        }
    }

    #endregion
}
