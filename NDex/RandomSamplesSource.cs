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
        /// Randomly adds the requested number of items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items to randomly choose values from.</param>
        /// <param name="numberOfSamples">The number of items to add to the destination.</param>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The number of samples is negative -or- larger than the size of the list.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">The random number generator is null.</exception>
        /// <remarks>The order that the items appear in the destination is not guaranteed.</remarks>
        public static RandomSamplesSource<TSourceList, TSource> RandomSamples<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            int numberOfSamples,
            Random random)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfSamples < 0 || numberOfSamples > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfSamples", numberOfSamples, Resources.IndexOutOfRange);
            }
            if (random == null)
            {
                throw new ArgumentNullException("random");
            }
            return new RandomSamplesSource<TSourceList, TSource>(source, numberOfSamples, random.Next);
        }

        /// <summary>
        /// Randomly adds the requested number of items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items to randomly choose values from.</param>
        /// <param name="numberOfSamples">The number of items to add to the destination.</param>
        /// <param name="generator">The random number generator to use.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The number of samples is negative -or- larger than the size of the list.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">The random number generator is null.</exception>
        /// <remarks>The order that the items appear in the destination is not guaranteed.</remarks>
        public static RandomSamplesSource<TSourceList, TSource> RandomSamples<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            int numberOfSamples,
            Func<int> generator)
            where TSourceList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfSamples < 0 || numberOfSamples > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfSamples", numberOfSamples, Resources.IndexOutOfRange);
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            return new RandomSamplesSource<TSourceList, TSource>(source, numberOfSamples, generator);
        }
    }

    #endregion

    #region RandomSamplesResult

    /// <summary>
    /// Holds the results of copying the results of a RandomSamples operation.
    /// </summary>
    public sealed class RandomSamplesResult
    {
        /// <summary>
        /// Initializes a new instance of a RandomSamplesResult.
        /// </summary>
        internal RandomSamplesResult()
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
        public static implicit operator int(RandomSamplesResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region RandomSamples

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class RandomSamplesSource<TSourceList, TSource> : Source<TSource, RandomSamplesResult>
        where TSourceList : IList<TSource>
    {
        private readonly IReadOnlySublist<TSourceList, TSource> source;
        private readonly int numberOfSamples;
        private readonly Func<int> generator;

        internal RandomSamplesSource(
            IReadOnlySublist<TSourceList, TSource> source,
            int numberOfSamples,
            Func<int> generator)
        {
            this.source = source;
            this.numberOfSamples = numberOfSamples;
            this.generator = generator;
        }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int result = addRandomSamples<TDestinationList>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                numberOfSamples,
                generator);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addRandomSamples<TDestinationList>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            int numberOfSamples,
            Func<int> generator)
            where TDestinationList : IList<TSource>
        {
            Sublist.GrowAndShift<TDestinationList, TSource>(destination, destinationPast, numberOfSamples);
            return copyRandomSamples<TDestinationList>(
                source, first, past,
                destination, destinationPast, destinationPast + numberOfSamples,
                generator);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected override RandomSamplesResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            int index = copyRandomSamples<TDestinationList>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + numberOfSamples,
                generator);
            RandomSamplesResult result = new RandomSamplesResult();
            result.SourceOffset = source.Count;
            result.DestinationOffset = index - destination.Offset;
            return result;
        }

        private static int copyRandomSamples<TDestinationList>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<int> generator)
            where TDestinationList : IList<TSource>
        {
            for (int index = destinationFirst; first != past && index != destinationPast; ++index)
            {
                destination[index] = source[first];
                ++first;
            }
            int numberOfSamples = destinationPast - destinationFirst;
            int total = numberOfSamples;
            while (first != past)
            {
                ++total;
                int likelihood = generator() % total;
                if (likelihood < 0)
                {
                    likelihood += total;
                }
                if (likelihood < numberOfSamples)
                {
                    destination[destinationFirst + likelihood] = source[first];
                }
                ++first;
            }
            return destinationPast;
        }
    }

    #endregion
}
