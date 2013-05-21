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
        #region AddRandomSamples

        /// <summary>
        /// Randomly adds the requested number of items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items to randomly choose values from.</param>
        /// <param name="numberOfSamples">The number of items to add to the destination.</param>
        /// <param name="destination">The list to add items to.</param>
        /// <param name="random">The random number generator to use.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The number of samples is negative -or- larger than the size of the list.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">The random number generator is null.</exception>
        /// <remarks>The order that the items appear in the destination is not guaranteed.</remarks>
        public static IExpandableSublist<TDestinationList, T> AddRandomSamples<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            int numberOfSamples,
            IExpandableSublist<TDestinationList, T> destination,
            Random random)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfSamples < 0 || numberOfSamples > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfSamples", numberOfSamples, Resources.IndexOutOfRange);
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (random == null)
            {
                throw new ArgumentNullException("random");
            }
            return addRandomSamples<TSourceList, TDestinationList, T>(source, numberOfSamples, destination, random.Next);
        }

        /// <summary>
        /// Randomly adds the requested number of items from a list to a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items to randomly choose values from.</param>
        /// <param name="numberOfSamples">The number of items to add to the destination.</param>
        /// <param name="destination">The list to add items to.</param>
        /// <param name="generator">The random number generator to use.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The number of samples is negative -or- larger than the size of the list.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">The random number generator is null.</exception>
        /// <remarks>The order that the items appear in the destination is not guaranteed.</remarks>
        public static IExpandableSublist<TDestinationList, T> AddRandomSamples<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            int numberOfSamples,
            IExpandableSublist<TDestinationList, T> destination,
            Func<int> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfSamples < 0 || numberOfSamples > source.Count)
            {
                throw new ArgumentOutOfRangeException("numberOfSamples", numberOfSamples, Resources.IndexOutOfRange);
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            return addRandomSamples<TSourceList, TDestinationList, T>(source, numberOfSamples, destination, generator);
        }

        private static IExpandableSublist<TDestinationList, T> addRandomSamples<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            int numberOfSamples,
            IExpandableSublist<TDestinationList, T> destination,
            Func<int> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addRandomSamples<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                numberOfSamples,
                generator);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addRandomSamples<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            int numberOfSamples,
            Func<int> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            GrowAndShift<TDestinationList, T>(destination, destinationPast, numberOfSamples);
            return copyRandomSamples<TSourceList, TDestinationList, T>(
                source, first, past,
                destination, destinationPast, destinationPast + numberOfSamples,
                generator);
        }

        #endregion

        #region CopyRandomSamples

        /// <summary>
        /// Randomly copies items from a list to fill a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items to randomly choose values from.</param>
        /// <param name="destination">The list to copy items to.</param>
        /// <param name="random">The random number generator to use.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The random number generator is null.</exception>
        /// <remarks>The order that the items appear in the destination is not guaranteed.</remarks>
        public static RandomSamplesResult CopyRandomSamples<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Random random)
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
            if (random == null)
            {
                throw new ArgumentNullException("random");
            }
            return copyRandomSamples<TSourceList, TDestinationList, T>(source, destination, random.Next);
        }

        /// <summary>
        /// Randomly copies items from a list to fill a destination list.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items to randomly choose values from.</param>
        /// <param name="destination">The list to copy items to.</param>
        /// <param name="generator">The random number generator to use.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The random number generator is null.</exception>
        /// <remarks>The order that the items appear in the destination is not guaranteed.</remarks>
        public static RandomSamplesResult CopyRandomSamples<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Func<int> generator)
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
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            return copyRandomSamples<TSourceList, TDestinationList, T>(source, destination, generator);
        }

        private static RandomSamplesResult copyRandomSamples<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Func<int> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            int index = copyRandomSamples<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                generator);
            RandomSamplesResult result = new RandomSamplesResult();
            result.SourceOffset = source.Count;
            result.DestinationOffset = index - destination.Offset;
            return result;
        }

        private static int copyRandomSamples<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<int> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
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

        #endregion
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

    internal sealed class RandomSamplesSource<TSourceList, TSource> : Source<TSource, RandomSamplesResult>
        where TSourceList : IList<TSource>
    {
        private readonly IReadOnlySublist<TSourceList, TSource> source;
        private readonly Func<int> generator;

        public RandomSamplesSource(
            IReadOnlySublist<TSourceList, TSource> source,
            Func<int> generator)
        {
            this.source = source;
            this.generator = generator;
        }

        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            // TODO - figure out how to pass in numberOfItems
            return Sublist.AddRandomSamples(source, 0, destination, generator);
        }

        protected override RandomSamplesResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            return Sublist.CopyRandomSamples(source, destination, generator);
        }
    }

    #endregion
}
