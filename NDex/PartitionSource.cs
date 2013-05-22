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
        /// Adds the items from a list satisfying the predicate to the first destination list and the remaining
        /// to the second destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to partition.</param>
        /// <param name="predicate">The condition an item must satisfy to be added to the first destination list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static PartitionSource<TSourceList, TSource> Partition<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            return new PartitionSource<TSourceList, TSource>(source, predicate);
        }

        /// <summary>
        /// Adds the items from a list satisfying the predicate to the first destination list and the remaining
        /// to the second destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to partition.</param>
        /// <param name="predicate">The condition an item must satisfy to be added to the first destination list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static InPlacePartitionSource<TSourceList, TSource> Partition<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            return new InPlacePartitionSource<TSourceList, TSource>(source, predicate);
        }
    }

    #endregion

    #region PartitionResult

    /// <summary>
    /// Holds the results of the AddPartitioned method.
    /// </summary>
    public sealed class AddPartitionedResult<TDestinationList1, TDestinationList2, T>
    {
        /// <summary>
        /// Initializes a new instance of an AddPartitionedResult.
        /// </summary>
        internal AddPartitionedResult()
        {
        }

        /// <summary>
        /// Gets the subset wrapping the first destination list.
        /// </summary>
        public IExpandableSublist<TDestinationList1, T> Destination1
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the subset wrapping the second destination list.
        /// </summary>
        public IExpandableSublist<TDestinationList2, T> Destination2
        {
            get;
            internal set;
        }
    }

    /// <summary>
    /// Holds the result of copying the result of a Partition operation.
    /// </summary>
    public sealed class CopyPartitionedResult
    {
        /// <summary>
        /// Initializes a new instance of a PartitionCopyResult.
        /// </summary>
        internal CopyPartitionedResult()
        {
        }

        /// <summary>
        /// Gets the index into the source list where the items stopped being copied.
        /// </summary>
        public int SourceOffset
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the index into the first destination list.
        /// </summary>
        public int DestinationOffset1
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the index into the second destination list.
        /// </summary>
        public int DestinationOffset2
        {
            get;
            internal set;
        }
    }

    #endregion

    #region PartitionSource

    /// <summary>
    /// Provides the information needed to copy or add items to destination sublists.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the underlying list of the source.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class PartitionSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal PartitionSource(
            IReadOnlySublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate)
        {
            Source = source;
            Predicate = predicate;
        }

        /// <summary>
        /// Gets the list that is the source of the items to partition.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the function that is used to partition the items.
        /// </summary>
        protected Func<TSource, bool> Predicate { get; private set; }


        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination lists.
        /// </summary>
        /// <typeparam name="TDestinationList1">The type of the underlying list to add the items satisfying the predicate to.</typeparam>
        /// <typeparam name="TDestinationList2">The type of the underlying list to add the items not satisfying the predicate to.</typeparam>
        /// <param name="destination1">The list to add the items satisfying the predicate.</param>
        /// <param name="destination2">The list to add the items not satisfying the predicate.</param>
        /// <returns>A result object holding references to the destination sublists wrapping the original sublists and the new items.</returns>
        /// <exception cref="System.ArgumentNullException">The first destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second destination list is null.</exception>
        public AddPartitionedResult<TDestinationList1, TDestinationList2, TSource> AddTo<TDestinationList1, TDestinationList2>(
            IExpandableSublist<TDestinationList1, TSource> destination1,
            IExpandableSublist<TDestinationList2, TSource> destination2)
            where TDestinationList1 : IList<TSource>
            where TDestinationList2 : IList<TSource>
        {
            if (destination1 == null)
            {
                throw new ArgumentNullException("destination1");
            }
            if (destination2 == null)
            {
                throw new ArgumentNullException("destination2");
            }
            Tuple<int, int> indexes = addPartitioned<TDestinationList1, TDestinationList2>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination1.List, destination1.Offset + destination1.Count,
                destination2.List, destination2.Offset + destination2.Count,
                Predicate);
            AddPartitionedResult<TDestinationList1, TDestinationList2, TSource> result = new AddPartitionedResult<TDestinationList1, TDestinationList2, TSource>();
            result.Destination1 = destination1.Resize(indexes.Item1 - destination1.Offset, true);
            result.Destination2 = destination2.Resize(indexes.Item2 - destination2.Offset, true);
            return result;
        }

        private static Tuple<int, int> addPartitioned<TDestinationList1, TDestinationList2>(
            TSourceList source, int first, int past,
            TDestinationList1 destination1, int destinationPast1,
            TDestinationList2 destination2, int destinationPast2,
            Func<TSource, bool> predicate)
            where TDestinationList1 : IList<TSource>
            where TDestinationList2 : IList<TSource>
        {
            int pivot1 = destination1.Count;
            int pivot2 = destination2.Count;
            while (first != past)
            {
                if (predicate(source[first]))
                {
                    destination1.Add(source[first]);
                }
                else
                {
                    destination2.Add(source[first]);
                }
                ++first;
            }
            Sublist.RotateLeft<TDestinationList1, TSource>(destination1, destinationPast1, pivot1, destination1.Count);
            Sublist.RotateLeft<TDestinationList2, TSource>(destination2, destinationPast2, pivot2, destination2.Count);
            destinationPast1 += destination1.Count - pivot1;
            destinationPast2 += destination2.Count - pivot2;
            return new Tuple<int, int>(destinationPast1, destinationPast2);
        }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination lists.
        /// </summary>
        /// <typeparam name="TDestinationList1">The type of the underlying list to copy the items satisfying the predicate to.</typeparam>
        /// <typeparam name="TDestinationList2">The type of the underlying list to add the items not satisfying the predicate to.</typeparam>
        /// <param name="destination1">The list to add the items satisfying the predicate.</param>
        /// <param name="destination2">The list to add the items not satisfying the predicate.</param>
        /// <returns>A result object holding indexes into the lists where the operation stopped.</returns>
        /// <exception cref="System.ArgumentNullException">The first destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second destination list is null.</exception>
        public CopyPartitionedResult CopyTo<TDestinationList1, TDestinationList2>(
            IMutableSublist<TDestinationList1, TSource> destination1,
            IMutableSublist<TDestinationList2, TSource> destination2)
            where TDestinationList1 : IList<TSource>
            where TDestinationList2 : IList<TSource>
        {
            if (destination1 == null)
            {
                throw new ArgumentNullException("destination1");
            }
            if (destination2 == null)
            {
                throw new ArgumentNullException("destination2");
            }
            Tuple<int, int, int> indexes = copyPartitioned<TDestinationList1, TDestinationList2>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination1.List, destination1.Offset, destination1.Offset + destination1.Count,
                destination2.List, destination2.Offset, destination2.Offset + destination2.Count,
                Predicate);
            CopyPartitionedResult result = new CopyPartitionedResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset1 = indexes.Item2 - destination1.Offset;
            result.DestinationOffset2 = indexes.Item3 - destination2.Offset;
            return result;
        }

        private static Tuple<int, int, int> copyPartitioned<TDestinationList1, TDestinationList2>(
            TSourceList source, int first, int past,
            TDestinationList1 destination1, int destinationFirst1, int destinationPast1,
            TDestinationList2 destination2, int destinationFirst2, int destinationPast2,
            Func<TSource, bool> predicate)
            where TDestinationList1 : IList<TSource>
            where TDestinationList2 : IList<TSource>
        {
            while (first != past)
            {
                if (predicate(source[first]))
                {
                    if (destinationFirst1 == destinationPast1)
                    {
                        break;
                    }
                    destination1[destinationFirst1] = source[first];
                    ++destinationFirst1;
                }
                else
                {
                    if (destinationFirst2 == destinationPast2)
                    {
                        break;
                    }
                    destination2[destinationFirst2] = source[first];
                    ++destinationFirst2;
                }
                ++first;
            }
            return new Tuple<int, int, int>(first, destinationFirst1, destinationFirst2);
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to destination sublists, or modifying it in-place.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the underlying list of the source.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class InPlacePartitionSource<TSourceList, TSource> : PartitionSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InPlacePartitionSource(
            IReadOnlySublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate)
            : base(source, predicate)
        {
        }

        /// <summary>
        /// Partitions the items in-place.
        /// </summary>
        public int InPlace()
        {
            int index = partition(Source.List, Source.Offset, Source.Offset + Source.Count, Predicate);
            index -= Source.Offset;
            return index;
        }

        private static int partition(TSourceList list, int first, int past, Func<TSource, bool> predicate)
        {
            while (true)
            {
                while (first != past && predicate(list[first]))
                {
                    ++first;
                }
                if (first == past)
                {
                    break;
                }
                --past;
                while (first != past && !predicate(list[past]))
                {
                    --past;
                }
                if (first == past)
                {
                    break;
                }
                TSource temp = list[first];
                list[first] = list[past];
                list[past] = temp;
                ++first;
            }
            return first;
        }
    }

    #endregion
}
