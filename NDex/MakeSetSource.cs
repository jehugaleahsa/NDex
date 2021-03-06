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
        /// Makes a sorted set from the items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to turn into a set.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        public static MakeSetSource<TSourceList, TSource> MakeSet<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new MakeSetSource<TSourceList, TSource>(source, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Makes a sorted set from the items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to make a set.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        public static MakeSetSource<TSourceList, TSource> MakeSet<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source, IComparer<TSource> comparer)
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
            return new MakeSetSource<TSourceList, TSource>(source, comparer.Compare);
        }

        /// <summary>
        /// Makes a sorted set from the items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to make a set.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        public static MakeSetSource<TSourceList, TSource> MakeSet<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
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
            return new MakeSetSource<TSourceList, TSource>(source, comparison);
        }

        /// <summary>
        /// Makes a sorted set from the items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to turn into a set.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        public static InPlaceMakeSetSource<TSourceList, TSource> MakeSet<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new InPlaceMakeSetSource<TSourceList, TSource>(source, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Makes a sorted set from the items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to make a set.</param>
        /// <param name="comparer">The comparer to use to compare items in the list.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        public static InPlaceMakeSetSource<TSourceList, TSource> MakeSet<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source, IComparer<TSource> comparer)
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
            return new InPlaceMakeSetSource<TSourceList, TSource>(source, comparer.Compare);
        }

        /// <summary>
        /// Makes a sorted set from the items.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to make a set.</param>
        /// <param name="comparison">The comparison delegate to use to compare items in the list.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        public static InPlaceMakeSetSource<TSourceList, TSource> MakeSet<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
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
            return new InPlaceMakeSetSource<TSourceList, TSource>(source, comparison);
        }
    }

    #endregion

    #region MakeSetResult

    /// <summary>
    /// Holds the results of copying a MakeSet operation.
    /// </summary>
    public sealed class MakeSetResult
    {
        /// <summary>
        /// Initializes a new instance of a MakeSetResult.
        /// </summary>
        internal MakeSetResult()
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
        public static implicit operator int(MakeSetResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region MakeSetSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class MakeSetSource<TSourceList, TSource> : Source<TSource, MakeSetResult>
        where TSourceList : IList<TSource>
    {
        internal MakeSetSource(IReadOnlySublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
        {
            Source = source;
            Comparison = comparison;
        }

        /// <summary>
        /// Gets the list to get the unique items from.
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
        protected sealed override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int past = Sublist.AddMakeSet<TSourceList, TDestinationList, TSource>(
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
        protected sealed override MakeSetResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = Sublist.CopyMakeSet<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Comparison);
            MakeSetResult result = new MakeSetResult();
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
    public sealed class InPlaceMakeSetSource<TSourceList, TSource> : MakeSetSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InPlaceMakeSetSource(IMutableSublist<TSourceList, TSource> source, Func<TSource, TSource, int> comparison)
            : base(source, comparison)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        /// <returns>The integer past the last item in the set.</returns>
        public int InPlace()
        {
            int result = Sublist.MakeSet<TSourceList, TSource>(Source.List, Source.Offset, Source.Offset + Source.Count, Comparison);
            result -= Source.Offset;
            return result;
        }
    }

    #endregion
}
