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
        #region AddUnique

        /// <summary>
        /// Adds the unique items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to add.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>The items in the list must be sorted according to the default ordering of the items.</remarks>
        public static IExpandableSublist<TDestinationList, T> AddUnique<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return addUnique<TSourceList, TDestinationList, T>(source, destination, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Adds the unique items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to add.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>The list must be sorted.</remarks>
        public static IExpandableSublist<TDestinationList, T> AddUnique<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination,
            IEqualityComparer<T> comparer)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return addUnique<TSourceList, TDestinationList, T>(source, destination, comparer.Equals);
        }

        /// <summary>
        /// Adds the unique items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to add.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="comparison">The comparison delegate to use to compare items.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <remarks>The list must be sorted.</remarks>
        public static IExpandableSublist<TDestinationList, T> AddUnique<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, bool> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addUnique<TSourceList, TDestinationList, T>(source, destination, comparison);
        }

        private static IExpandableSublist<TDestinationList, T> addUnique<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, T, bool> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addUnique<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addUnique<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<T, T, bool> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int pivot = destination.Count;
            if (first != past)
            {
                destination.Add(source[first]);
                for (int next = first + 1; next != past; first = next, ++next)
                {
                    if (!comparison(source[first], source[next]))
                    {
                        destination.Add(source[next]);
                    }
                }
            }
            RotateLeft<TDestinationList, T>(destination, destinationPast, pivot, destination.Count);
            return destinationPast + (destination.Count - pivot);
        }

        #endregion

        #region CopyUnique

        /// <summary>
        /// Copies the items from a list that are unique to a destination.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy from.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <returns>The index into the destination past the last copied item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <remarks>The list must be sorted such that equivilent items appear adjacent.</remarks>
        public static DistinctResult CopyUnique<TSourceList, TDestinationList, T>(IReadOnlySublist<TSourceList, T> source, IMutableSublist<TDestinationList, T> destination)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return copyUnique<TSourceList, TDestinationList, T>(source, destination, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Copies the items from a list that are unique to a destination.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy from.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="comparer">The compare to use to determine whether two items are equivilent.</param>
        /// <returns>The index into the destination past the last copied item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <remarks>The list must be sorted such that equivilent items appear adjacent.</remarks>
        public static DistinctResult CopyUnique<TSourceList, TDestinationList, T>(IReadOnlySublist<TSourceList, T> source, IMutableSublist<TDestinationList, T> destination, IEqualityComparer<T> comparer)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return copyUnique<TSourceList, TDestinationList, T>(source, destination, comparer.Equals);
        }

        /// <summary>
        /// Copies the items from a list that are unique to a destination.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy from.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="comparison">The delegate used to determine whether two items are equivilent.</param>
        /// <returns>The index into the destination past the last copied item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison delegate is null.</exception>
        /// <remarks>The list must be sorted such that equivilent items appear adjacent.</remarks>
        public static DistinctResult CopyUnique<TSourceList, TDestinationList, T>(IReadOnlySublist<TSourceList, T> source, IMutableSublist<TDestinationList, T> destination, Func<T, T, bool> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copyUnique<TSourceList, TDestinationList, T>(source, destination, comparison);
        }

        private static DistinctResult copyUnique<TSourceList, TDestinationList, T>(IReadOnlySublist<TSourceList, T> source, IMutableSublist<TDestinationList, T> destination, Func<T, T, bool> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            Tuple<int, int> indexes = copyUnique<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            DistinctResult result = new DistinctResult();
            result.SourceOffset = indexes.Item1 - source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }

        private static Tuple<int, int> copyUnique<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, T, bool> comparison)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (first != past && destinationFirst != destinationPast)
            {
                destination[destinationFirst] = source[first];
                ++destinationFirst;
                for (int next = first + 1; next != past; first = next, ++next)
                {
                    if (!comparison(source[first], source[next]))
                    {
                        if (destinationFirst == destinationPast)
                        {
                            break;
                        }
                        destination[destinationFirst] = source[next];
                        ++destinationFirst;
                    }
                }
                ++first;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        #endregion
    }

    #endregion

    #region DistinctResult

    /// <summary>
    /// Holds the results of copying the results of a Distinct operation.
    /// </summary>
    public sealed class DistinctResult
    {
        /// <summary>
        /// Initializes a new instance of a DistinctResult.
        /// </summary>
        internal DistinctResult()
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
        public static implicit operator int(DistinctResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region DistinctSource

    internal sealed class DistinctSource<TSourceList, TSource> : Source<TSource, DistinctResult>
        where TSourceList : IList<TSource>
    {
        private readonly IReadOnlySublist<TSourceList, TSource> source;
        private readonly Func<TSource, TSource, bool> comparison;

        public DistinctSource(IReadOnlySublist<TSourceList, TSource> source, Func<TSource, TSource, bool> comparison)
        {
            this.source = source;
            this.comparison = comparison;
        }

        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            return Sublist.AddUnique(source, destination, comparison);
        }

        protected override DistinctResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            return Sublist.CopyUnique(source, destination, comparison);
        }
    }

    #endregion
}
