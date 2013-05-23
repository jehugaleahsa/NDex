using System;
using System.Globalization;
using NDex.Properties;
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
        /// Generates the given value the specified number of items.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="numberOfItems">The number of values to generate.</param>
        /// <param name="value">The value to generate.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">The number of items is negative.</exception>
        /// <remarks>If T is a reference type, each item in the list will refer to the same instance.</remarks>
        public static GenerateWithConstantSource<TSource> Generate<TSource>(int numberOfItems, TSource value)
        {
            if (numberOfItems < 0)
            {
                string message = String.Format(CultureInfo.CurrentCulture, Resources.TooSmall, 0);
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, message);
            }
            return new GenerateWithConstantSource<TSource>(numberOfItems, value);
        }

        /// <summary>
        /// Generates the specified number of items by calling the given generator.
        /// </summary>
        /// <typeparam name="TSource">The type of the values returned by the generator.</typeparam>
        /// <param name="numberOfItems">The number of values to generate.</param>
        /// <param name="generator">A function to generate the values.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">The number of items is negative.</exception>
        /// <exception cref="System.ArgumentNullException">The generator is null.</exception>
        public static GenerateWithGeneratorSource<TSource> Generate<TSource>(int numberOfItems, Func<TSource> generator)
        {
            if (numberOfItems < 0)
            {
                string message = String.Format(CultureInfo.CurrentCulture, Resources.TooSmall, 0);
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, message);
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            return new GenerateWithGeneratorSource<TSource>(numberOfItems, generator);
        }

        /// <summary>
        /// Generates the specified number of items by calling the given generator.
        /// </summary>
        /// <typeparam name="TSource">The type of the values returned by the generator.</typeparam>
        /// <param name="numberOfItems">The number of values to generate.</param>
        /// <param name="generator">A function to generate the values.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">The number of items is negative.</exception>
        /// <exception cref="System.ArgumentNullException">The generator is null.</exception>
        /// <remarks>The generator will be passed integer values starting from 0.</remarks>
        public static GenerateWithIndexedGeneratorSource<TSource> Generate<TSource>(int numberOfItems, Func<int, TSource> generator)
        {
            if (numberOfItems < 0)
            {
                string message = String.Format(CultureInfo.CurrentCulture, Resources.TooSmall, 0);
                throw new ArgumentOutOfRangeException("numberOfItems", numberOfItems, message);
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            return new GenerateWithIndexedGeneratorSource<TSource>(numberOfItems, generator);
        }
    }

    #endregion

    #region GenerateSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSource">The type of the items to generate.</typeparam>
    public sealed class GenerateWithConstantSource<TSource> : Source<TSource, int>
    {
        private readonly int numberOfItems;
        private readonly TSource value;

        internal GenerateWithConstantSource(int numberOfItems, TSource value)
        {
            this.numberOfItems = numberOfItems;
            this.value = value;
        }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int first = destination.Offset + destination.Count;
            int past2 = first + numberOfItems;
            Sublist.AddGenerated<TDestinationList, TSource>(destination.List, first, past2, value);
            return destination.Resize(destination.Count + numberOfItems, true);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected override int SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            int past = Math.Min(destination.Offset + numberOfItems, destination.Offset + destination.Count);
            Sublist.CopyGenerated<TDestinationList, TSource>(destination.List, destination.Offset, past, value);
            return past - destination.Offset;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSource">The type of the items to generate.</typeparam>
    public sealed class GenerateWithGeneratorSource<TSource> : Source<TSource, int>
    {
        private readonly int numberOfItems;
        private readonly Func<TSource> generator;

        internal GenerateWithGeneratorSource(int numberOfItems, Func<TSource> generator)
        {
            this.numberOfItems = numberOfItems;
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
            int past = destination.Offset + destination.Count;
            Sublist.AddGenerated<TDestinationList, TSource>(destination.List, past, past + numberOfItems, generator);
            return destination.Resize(destination.Count + numberOfItems, true);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected override int SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            int past = Math.Min(destination.Offset + destination.Count, destination.Offset + numberOfItems);
            Sublist.CopyGenerated<TDestinationList, TSource>(destination.List, destination.Offset, past, generator);
            return past - destination.Offset;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSource">The type of the items to generate.</typeparam>
    public sealed class GenerateWithIndexedGeneratorSource<TSource> : Source<TSource, int>
    {
        private readonly int numberOfItems;
        private readonly Func<int, TSource> generator;

        internal GenerateWithIndexedGeneratorSource(int numberOfItems, Func<int, TSource> generator)
        {
            this.numberOfItems = numberOfItems;
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
            int past = destination.Offset + destination.Count;
            Sublist.AddGenerated<TDestinationList, TSource>(destination.List, past, past + numberOfItems, generator);
            return destination.Resize(destination.Count + numberOfItems, true);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected override int SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            int past = Math.Min(destination.Offset + destination.Count, destination.Offset + numberOfItems);
            Sublist.CopyGenerated<TDestinationList, TSource>(destination.List, destination.Offset, past, generator);
            return past - destination.Offset;
        }
    }

    #endregion
}
