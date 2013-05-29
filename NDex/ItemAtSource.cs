using System;
using System.Collections.Generic;
using NDex.Properties;

namespace NDex
{
    #region Sublist

    /// <summary>
    /// Provides methods for creating and working with instances of Sublist.
    /// </summary>
    public static partial class Sublist
    {
        /// <summary>
        /// Rearranges the items in the list so that the item at the given index is the same as if the list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to rearrange.</param>
        /// <param name="index">The index into the list to place the desired item.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        public static ItemAtSource<TSourceList, TSource> ItemAt<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            int index)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (index < 0 || index >= source.Count)
            {
                throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
            }
            return new ItemAtSource<TSourceList, TSource>(source, index, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Rearranges the items in the list so that the item at the given index is the same as if the list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to rearrange.</param>
        /// <param name="index">The index into the list to place the desired item.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static ItemAtSource<TSourceList, TSource> ItemAt<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            int index,
            IComparer<TSource> comparer)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (index < 0 || index >= source.Count)
            {
                throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return new ItemAtSource<TSourceList, TSource>(source, index, comparer.Compare);
        }

        /// <summary>
        /// Rearranges the items in the list so that the item at the given index is the same as if the list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to rearrange.</param>
        /// <param name="index">The index into the list to place the desired item.</param>
        /// <param name="comparison">The function to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static ItemAtSource<TSourceList, TSource> ItemAt<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            int index,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (index < 0 || index >= source.Count)
            {
                throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return new ItemAtSource<TSourceList, TSource>(source, index, comparison);
        }

        /// <summary>
        /// Rearranges the items in the list so that the item at the given index is the same as if the list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to rearrange.</param>
        /// <param name="index">The index into the list to place the desired item.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        public static InPlaceItemAtSource<TSourceList, TSource> ItemAt<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source, int index)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (index < 0 || index >= source.Count)
            {
                throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
            }
            return new InPlaceItemAtSource<TSourceList, TSource>(source, index, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Rearranges the items in the list so that the item at the given index is the same as if the list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to rearrange.</param>
        /// <param name="index">The index into the list to place the desired item.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static InPlaceItemAtSource<TSourceList, TSource> ItemAt<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            int index,
            IComparer<TSource> comparer)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (index < 0 || index >= source.Count)
            {
                throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return new InPlaceItemAtSource<TSourceList, TSource>(source, index, comparer.Compare);
        }

        /// <summary>
        /// Rearranges the items in the list so that the item at the given index is the same as if the list was sorted.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to rearrange.</param>
        /// <param name="index">The index into the list to place the desired item.</param>
        /// <param name="comparison">The function to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- outside the bounds of the list.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static InPlaceItemAtSource<TSourceList, TSource> ItemAt<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            int index,
            Func<TSource, TSource, int> comparison)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (index < 0 || index >= source.Count)
            {
                throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return new InPlaceItemAtSource<TSourceList, TSource>(source, index, comparison);
        }
    }

    #endregion

    #region ItemAtResult

    /// <summary>
    /// Holds the results of copying a ItemAt operation.
    /// </summary>
    public sealed class ItemAtResult
    {
        /// <summary>
        /// Initializes a new instance of a ItemAtResult.
        /// </summary>
        internal ItemAtResult()
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
        public static implicit operator int(ItemAtResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region ItemAtSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class ItemAtSource<TSourceList, TSource> : Source<TSource, ItemAtResult>
        where TSourceList : IList<TSource>
    {
        internal ItemAtSource(IReadOnlySublist<TSourceList, TSource> source, int index, Func<TSource, TSource, int> comparison)
        {
            Source = source;
            Index = index;
            Comparison = comparison;
        }

        /// <summary>
        /// Gets the list to get the unique items from.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the index to move the item to.
        /// </summary>
        protected int Index { get; private set; }

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
            int result = Sublist.AddItemAt<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Index, Source.Offset + Source.Count,
                destination.List, destination.Offset + destination.Count,
                Comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected sealed override ItemAtResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = Sublist.CopyItemAt<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Index, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Comparison);
            ItemAtResult result = new ItemAtResult();
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
    public sealed class InPlaceItemAtSource<TSourceList, TSource> : ItemAtSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InPlaceItemAtSource(IMutableSublist<TSourceList, TSource> source, int index, Func<TSource, TSource, int> comparison)
            : base(source, index, comparison)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        /// <returns>The integer past the last unique item.</returns>
        public void InPlace()
        {
            Sublist.ItemAt<TSourceList, TSource>(Source.List, Source.Offset, Source.Offset + Index, Source.Offset + Source.Count, Comparison);
        }
    }

    #endregion
}