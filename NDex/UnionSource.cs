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
        #region AddUnion

        /// <summary>
        /// Adds the items that are unique across both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the third list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>
        /// The items in the lists must be sorted according to the default ordering of the items. Neither of
        /// the lists can contain duplicate items.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return addUnion<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Adds the items that are unique across both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="comparer">The comparer to use to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer and must not contain duplicates.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return addUnion<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparer.Compare);
        }

        /// <summary>
        /// Adds the items that are unique across both lists to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="comparison">The delegate used to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate and must not contain duplicates.
        /// </remarks>
        public static IExpandableSublist<TDestinationList, T> AddUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addUnion<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparison);
        }

        private static IExpandableSublist<TDestinationList, T> addUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addUnion<TSourceList1, TSourceList2, TDestinationList, T>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            int pivot = destination.Count;
            while (first1 != past1 && first2 != past2)
            {
                int result = comparison(source1[first1], source2[first2]);
                if (result < 0)
                {
                    destination.Add(source1[first1]);
                    ++first1;
                }
                else if (result > 0)
                {
                    destination.Add(source2[first2]);
                    ++first2;
                }
                else
                {
                    destination.Add(source1[first1]);
                    ++first1;
                    ++first2;
                }
            }
            Add<TSourceList1, TDestinationList, T>(source1, first1, past1, destination, destination.Count);
            Add<TSourceList2, TDestinationList, T>(source2, first2, past2, destination, destination.Count);
            RotateLeft<TDestinationList, T>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        #endregion

        #region CopyUnion

        /// <summary>
        /// Copies the items that are unique across both lists into a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the third list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>
        /// The items in the lists must be sorted according to the default ordering of the items. Neither of
        /// the lists can contain duplicate items.
        /// </remarks>
        public static UnionResult CopyUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return copyUnion<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Copies the items that are unique across both lists into a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <param name="comparer">The comparer to use to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparer and must not contain duplicates.
        /// </remarks>
        public static UnionResult CopyUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            IComparer<T> comparer)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return copyUnion<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparer.Compare);
        }

        /// <summary>
        /// Copies the items that are unique across both lists into a destination list.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="destination">The list to copy the items to.</param>
        /// <param name="comparison">The delegate used to compare items from the first and second lists.</param>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>
        /// The first and second lists must be sorted according to the comparison delegate and must not contain duplicates.
        /// </remarks>
        public static UnionResult CopyUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copyUnion<TSourceList1, TSourceList2, TDestinationList, T>(source1, source2, destination, comparison);
        }

        private static UnionResult copyUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            IReadOnlySublist<TSourceList1, T> source1,
            IReadOnlySublist<TSourceList2, T> source2,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            Tuple<int, int, int> indexes = copyUnion<TSourceList1, TSourceList2, TDestinationList, T>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source1.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            UnionResult result = new UnionResult();
            result.SourceOffset1 = indexes.Item1 - source1.Offset;
            result.SourceOffset2 = indexes.Item2 - source2.Offset;
            result.DestinationOffset = indexes.Item3 - destination.Offset;
            return result;
        }

        private static Tuple<int, int, int> copyUnion<TSourceList1, TSourceList2, TDestinationList, T>(
            TSourceList1 source1, int first1, int past1,
            TSourceList2 source2, int first2, int past2,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, T, int> comparison)
            where TSourceList1 : IList<T>
            where TSourceList2 : IList<T>
            where TDestinationList : IList<T>
        {
            while (first1 != past1 && first2 != past2 && destinationFirst != destinationPast)
            {
                int result = comparison(source1[first1], source2[first2]);
                if (result < 0)
                {
                    destination[destinationFirst] = source1[first1];
                    ++first1;
                    ++destinationFirst;
                }
                else if (result > 0)
                {
                    destination[destinationFirst] = source2[first2];
                    ++first2;
                    ++destinationFirst;
                }
                else
                {
                    destination[destinationFirst] = source1[first1];
                    ++first1;
                    ++first2;
                    ++destinationFirst;
                }
            }
            Tuple<int, int> indexes1 = Copy<TSourceList1, TDestinationList, T>(
                source1, first1, past1,
                destination, destinationFirst, destinationPast);
            first1 = indexes1.Item1;
            destinationFirst = indexes1.Item2;
            Tuple<int, int> indexes2 = Copy<TSourceList2, TDestinationList, T>(
                source2, first2, past2,
                destination, destinationFirst, destinationPast);
            first2 = indexes2.Item1;
            destinationFirst = indexes2.Item2;
            return new Tuple<int, int, int>(first1, first2, destinationFirst);
        }

        #endregion
    }

    #endregion

    #region UnionResult

    /// <summary>
    /// Holds the results of copying the results of an Union operation.
    /// </summary>
    public sealed class UnionResult
    {
        /// <summary>
        /// Initializes a new instance of a UnionResult.
        /// </summary>
        internal UnionResult()
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
        public static implicit operator int(UnionResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region UnionSource

    internal sealed class UnionSource<TSourceList1, TSourceList2, TSource> : Source<TSource, UnionResult>
        where TSourceList1 : IList<TSource>
        where TSourceList2 : IList<TSource>
    {
        private readonly IReadOnlySublist<TSourceList1, TSource> source1;
        private readonly IReadOnlySublist<TSourceList2, TSource> source2;
        private readonly Func<TSource, TSource, int> comparison;

        public UnionSource(
            IReadOnlySublist<TSourceList1, TSource> source1,
            IReadOnlySublist<TSourceList2, TSource> source2,
            Func<TSource, TSource, int> comparison)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.comparison = comparison;
        }

        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            return Sublist.AddUnion(source1, source2, destination, comparison);
        }

        protected override UnionResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            return Sublist.CopyUnion(source1, source2, destination, comparison);
        }
    }

    #endregion
}
