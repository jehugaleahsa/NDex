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
        /// Combines the items in the lists.
        /// </summary>
        /// <typeparam name="TSourceList1">The type of the first underlying list.</typeparam>
        /// <typeparam name="TSource1">The type of the items in the first underlying list.</typeparam>
        /// <typeparam name="TSourceList2">The type of the second underlying list.</typeparam>
        /// <typeparam name="TSource2">The type of the items in the second underlying list.</typeparam>
        /// <typeparam name="TDestination">The type of the items to copy or add to the destination list.</typeparam>
        /// <param name="source1">The first list.</param>
        /// <param name="source2">The second list.</param>
        /// <param name="combiner">A function that can combine the items from the first and second list.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The first list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The combiner delegate is null.</exception>
        public static Source<TDestination, ZipResult> Zip<TSourceList1, TSource1, TSourceList2, TSource2, TDestination>(
            this IReadOnlySublist<TSourceList1, TSource1> source1,
            IReadOnlySublist<TSourceList2, TSource2> source2,
            Func<TSource1, TSource2, TDestination> combiner)
            where TSourceList1 : IList<TSource1>
            where TSourceList2 : IList<TSource2>
        {
            if (source1 == null)
            {
                throw new ArgumentNullException("source1");
            }
            if (source2 == null)
            {
                throw new ArgumentNullException("source2");
            }
            if (combiner == null)
            {
                throw new ArgumentNullException("combiner");
            }
            return new ZipSource<TSourceList1, TSource1, TSourceList2, TSource2, TDestination>(source1, source2, combiner);
        }
    }

    #endregion

    #region ZipResult

    /// <summary>
    /// Holds the results of copying a Zip operation.
    /// </summary>
    public sealed class ZipResult
    {
        /// <summary>
        /// Initializes a new instance of a ZipResult.
        /// </summary>
        internal ZipResult()
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
        public static implicit operator int(ZipResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region ZipSource

    internal sealed class ZipSource<TSourceList1, TSource1, TSourceList2, TSource2, TDestination> : Source<TDestination, ZipResult>
        where TSourceList1 : IList<TSource1>
        where TSourceList2 : IList<TSource2>
    {
        private readonly IReadOnlySublist<TSourceList1, TSource1> source1;
        private readonly IReadOnlySublist<TSourceList2, TSource2> source2;
        private readonly Func<TSource1, TSource2, TDestination> combiner;

        public ZipSource(
            IReadOnlySublist<TSourceList1, TSource1> source1, 
            IReadOnlySublist<TSourceList2, TSource2> source2,
            Func<TSource1, TSource2, TDestination> combiner)
        {
            this.source1 = source1;
            this.source2 = source2;
            this.combiner = combiner;
        }

        protected override IExpandableSublist<TDestinationList, TDestination> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TDestination> destination)
        {
            int result = Sublist.AddZip<TSourceList1, TSource1, TSourceList2, TSource2, TDestinationList, TDestination>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset + destination.Count,
                combiner);
            return destination.Resize(result - destination.Offset, true);
        }

        protected override ZipResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TDestination> destination)
        {
            Tuple<int, int, int> indexes = Sublist.CopyZip<TSourceList1, TSource1, TSourceList2, TSource2, TDestinationList, TDestination>(
                source1.List, source1.Offset, source1.Offset + source1.Count,
                source2.List, source2.Offset, source2.Offset + source2.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                combiner);
            ZipResult result = new ZipResult();
            result.SourceOffset1 = indexes.Item1 - source1.Offset;
            result.SourceOffset2 = indexes.Item2 - source2.Offset;
            result.DestinationOffset = indexes.Item3 - destination.Offset;
            return result;
        }
    }

    #endregion
}
