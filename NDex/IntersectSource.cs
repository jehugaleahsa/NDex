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
        /// Adds the items that appear in both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <remarks>
        /// The items in the lists must be sorted according to the default ordering of the items. Neither of
        /// the lists can contain duplicate items.
        /// </remarks>
        public static IntersectSource<TSourceList1, TSourceList2, TSource> Intersect<TSourceList1, TSourceList2, TSource>(
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
            return new IntersectSource<TSourceList1, TSourceList2, TSource>(source1, source2, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Adds the items that appear in both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="comparer">The comparer to use to compare items from the first and second lists.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer and must not contain duplicates.
        /// </remarks>
        public static IntersectSource<TSourceList1, TSourceList2, TSource> Intersect<TSourceList1, TSourceList2, TSource>(
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
            return new IntersectSource<TSourceList1, TSourceList2, TSource>(source1, source2, comparer.Compare);
        }

        /// <summary>
        /// Adds the items that appear in both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="comparison">The delegate used to compare items from the first and second lists.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate and must not contain duplicates.
        /// </remarks>
        public static IntersectSource<TSourceList1, TSourceList2, TSource> Intersect<TSourceList1, TSourceList2, TSource>(
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
            return new IntersectSource<TSourceList1, TSourceList2, TSource>(source1, source2, comparison);
        }
    }

    #endregion

    #region IntersectResult

    /// <summary>
    /// Holds the results of copying the results of an Intersect operation.
    /// </summary>
    public sealed class IntersectResult
    {
        /// <summary>
        /// Initializes a new instance of a IntersectResult.
        /// </summary>
        internal IntersectResult()
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
        public static implicit operator int(IntersectResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region IntersectSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList1">The type of the first source's underlying list.</typeparam>
    /// <typeparam name="TSourceList2">The type of the second source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
    public sealed class IntersectSource<TSourceList1, TSourceList2, TSource> : Source<TSource, IntersectResult>
        where TSourceList1 : IList<TSource>
        where TSourceList2 : IList<TSource>
    {
        private readonly IReadOnlySublist<TSourceList1, TSource> source1;
        private readonly IReadOnlySublist<TSourceList2, TSource> source2;
        private readonly Func<TSource, TSource, int> comparison;

        internal IntersectSource(
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
            int result = addIntersection<TDestinationList>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addIntersection<TDestinationList>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TDestinationList : IList<TSource>
        {
            int pivot = destination.Count;
            while (first1 != past1 && first2 != past2)
            {
                int result = comparison(source1[first1], source2[first2]);
                if (result < 0)
                {
                    ++first1;
                }
                else if (result > 0)
                {
                    ++first2;
                }
                else
                {
                    destination.Add(source1[first1]);
                    ++first1;
                    ++first2;
                }
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
        protected override IntersectResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int, int> indexes = copyIntersection<TDestinationList>(
                source1.List, source2.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            IntersectResult result = new IntersectResult();
            result.SourceOffset1 = indexes.Item1 - source1.Offset;
            result.SourceOffset2 = indexes.Item2 - source2.Offset;
            result.DestinationOffset = indexes.Item3 - destination.Offset;
            return result;
        }

        private static Tuple<int, int, int> copyIntersection<TDestinationList>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<TSource, TSource, int> comparison)
            where TDestinationList : IList<TSource>
        {
            while (first1 != past1 && first2 != past2 && destinationFirst != destinationPast)
            {
                int result = comparison(source1[first1], source2[first2]);
                if (result < 0)
                {
                    ++first1;
                }
                else if (result > 0)
                {
                    ++first2;
                }
                else
                {
                    destination[destinationFirst] = source1[first1];
                    ++first1;
                    ++first2;
                    ++destinationFirst;
                }
            }
            return new Tuple<int, int, int>(first1, first2, destinationFirst);
        }
    }

    #endregion
}
