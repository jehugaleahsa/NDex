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
        /// Copies the given value into the list.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="numberOfItems">The number of items to add.</param>
        /// <param name="value">The value to fill the list with.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
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
        /// Copies the result of each call to the generator into the list.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="numberOfItems">The number of items to add.</param>
        /// <param name="generator">The delegate to use to fill the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator delegate is null.</exception>
        /// <remarks>The generator will be called to set each item in the list.</remarks>
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
        /// Copies the result of each call to the generator into the list.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="numberOfItems">The number of items to add.</param>
        /// <param name="generator">The delegate to use to fill the list.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator delegate is null.</exception>
        /// <remarks>
        /// The generator will be called to set each item in the list. 
        /// The relative index of the item is passed with each call to the generator.
        /// </remarks>
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
    /// <typeparam name="TSource">The type of the destination's underlying list.</typeparam>
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
            addGenerated<TDestinationList>(destination.List, first, past2, value);
            return destination.Resize(destination.Count + numberOfItems, true);
        }

        private static void addGenerated<TSourceList>(TSourceList list, int first, int past, TSource value)
            where TSourceList : IList<TSource>
        {
            while (first != past)
            {
                list.Insert(first, value);
                ++first;
            }
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
            copyGenerated<TDestinationList>(destination.List, destination.Offset, past, value);
            return past - destination.Offset;
        }

        private static void copyGenerated<TSourceList>(TSourceList list, int first, int past, TSource value)
            where TSourceList : IList<TSource>
        {
            while (first != past)
            {
                list[first] = value;
                ++first;
            }
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSource">The type of the destination's underlying list.</typeparam>
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
            addGenerated<TDestinationList>(destination.List, past, past + numberOfItems, generator);
            return destination.Resize(destination.Count + numberOfItems, true);
        }

        private static void addGenerated<TSourceList>(TSourceList list, int first, int past, Func<TSource> generator)
            where TSourceList : IList<TSource>
        {
            while (first != past)
            {
                list.Insert(first, generator());
                ++first;
            }
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
            copyGenerated<TDestinationList>(destination.List, destination.Offset, past, generator);
            return past - destination.Offset;
        }

        private static void copyGenerated<TSourceList>(TSourceList list, int first, int past, Func<TSource> generator)
            where TSourceList : IList<TSource>
        {
            while (first != past)
            {
                list[first] = generator();
                ++first;
            }
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSource">The type of the destination's underlying list.</typeparam>
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
            addGenerated<TDestinationList>(destination.List, past, past + numberOfItems, generator);
            return destination.Resize(destination.Count + numberOfItems, true);
        }

        private static void addGenerated<TSourceList>(TSourceList list, int first, int past, Func<int, TSource> generator)
            where TSourceList : IList<TSource>
        {
            int adjustment = first;
            while (first != past)
            {
                list.Insert(first, generator(first - adjustment));
                ++first;
            }
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
            copyGenerated<TDestinationList>(destination.List, destination.Offset, past, generator);
            return past - destination.Offset;
        }

        private static void copyGenerated<TSourceList>(TSourceList list, int first, int past, Func<int, TSource> generator)
            where TSourceList : IList<TSource>
        {
            int adjustment = first;
            while (first != past)
            {
                list[first] = generator(first - adjustment);
                ++first;
            }
        }
    }

    #endregion
}
