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
        /// Gets the items from the list in random order.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to shuffle.</param>
        /// <param name="random">The random generator to use.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The random generator is null.</exception>
        public static RandomShuffleSource<TSourceList, TSource> RandomShuffle<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source, Random random)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (random == null)
            {
                throw new ArgumentNullException("random");
            }
            return new RandomShuffleSource<TSourceList, TSource>(source, random.Next);
        }

        /// <summary>
        /// Gets the items from the list in random order.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to shuffle.</param>
        /// <param name="generator">A function that generates random numbers.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The random generator is null.</exception>
        public static RandomShuffleSource<TSourceList, TSource> RandomShuffle<TSourceList, TSource>(this IReadOnlySublist<TSourceList, TSource> source, Func<int> generator)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            return new RandomShuffleSource<TSourceList, TSource>(source, generator);
        }

        /// <summary>
        /// Gets the items from the list in random order.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to shuffle.</param>
        /// <param name="random">The random generator to use.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The random generator is null.</exception>
        public static InPlaceRandomShuffleSource<TSourceList, TSource> RandomShuffle<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source, Random random)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (random == null)
            {
                throw new ArgumentNullException("random");
            }
            return new InPlaceRandomShuffleSource<TSourceList, TSource>(source, random.Next);
        }

        /// <summary>
        /// Gets the items from the list in random order.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list to shuffle.</param>
        /// <param name="generator">A function that generates random numbers.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The random generator is null.</exception>
        public static InPlaceRandomShuffleSource<TSourceList, TSource> RandomShuffle<TSourceList, TSource>(this IMutableSublist<TSourceList, TSource> source, Func<int> generator)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            return new InPlaceRandomShuffleSource<TSourceList, TSource>(source, generator);
        }
    }

    #endregion

    #region RandomShuffleResult

    /// <summary>
    /// Holds the results of copying a RandomShuffle operation.
    /// </summary>
    public sealed class RandomShuffleResult
    {
        /// <summary>
        /// Initializes a new instance of a RandomShuffleResult.
        /// </summary>
        internal RandomShuffleResult()
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
        public static implicit operator int(RandomShuffleResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region RandomShuffleSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class RandomShuffleSource<TSourceList, TSource> : Source<TSource, RandomShuffleResult>
        where TSourceList : IList<TSource>
    {
        internal RandomShuffleSource(IReadOnlySublist<TSourceList, TSource> source, Func<int> generator)
        {
            Source = source;
            Generator = generator;
        }

        /// <summary>
        /// Gets the list that the items come from.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the random number generator.
        /// </summary>
        protected Func<int> Generator { get; private set; }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int result = Sublist.AddRandomShuffle<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset + destination.Count,
                Generator);
            return destination.Resize(result - destination.Offset, true);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected override RandomShuffleResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = Sublist.CopyRandomShuffle<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Generator);
            RandomShuffleResult result = new RandomShuffleResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist or perform the operation in-place.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class InPlaceRandomShuffleSource<TSourceList, TSource> : RandomShuffleSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InPlaceRandomShuffleSource(IMutableSublist<TSourceList, TSource> source, Func<int> generator)
            : base(source, generator)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        /// <returns>The integer past the last unique item.</returns>
        public void InPlace()
        {
            Sublist.RandomShuffle<TSourceList, TSource>(Source.List, Source.Offset, Source.Offset + Source.Count, Generator);
        }
    }

    #endregion
}
