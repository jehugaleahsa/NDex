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
        /// Gets the items from the list replacing those satisifying the given predicate.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="predicate">The function to use to determine whether an item should be replaced.</param>
        /// <param name="replacement">The value to use as a replacement.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static ReplaceWithConstantSource<TSourceList, TSource> Replace<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate,
            TSource replacement)
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
            return new ReplaceWithConstantSource<TSourceList, TSource>(source, predicate, replacement);
        }

        /// <summary>
        /// Gets the items from the list replacing those satisifying the given predicate with the results of the generator.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="predicate">The function to use to determine whether an item should be replaced.</param>
        /// <param name="generator">A function that creates the replacement values.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator is null.</exception>
        public static ReplaceWithGeneratorSource<TSourceList, TSource> Replace<TSourceList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate,
            Func<TSource, TSource> generator)
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
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            return new ReplaceWithGeneratorSource<TSourceList, TSource>(source, predicate, generator);
        }

        /// <summary>
        /// Gets the items from the list replacing matching sequences with a replacement sequence.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequenceList">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacementList">The type of the replacement list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The sequence to use as a replacement.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The source list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence list is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        public static ReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSource> Replace<TSourceList, TSequenceList, TReplacementList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            IReadOnlySublist<TSequenceList, TSource> sequence,
            IReadOnlySublist<TReplacementList, TSource> replacement)
            where TSourceList : IList<TSource>
            where TSequenceList : IList<TSource>
            where TReplacementList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            return new ReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSource>(
                source,
                sequence,
                replacement,
                EqualityComparer<TSource>.Default.Equals);
        }

        /// <summary>
        /// Gets the items from the list replacing matching sequences with a replacement sequence.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequenceList">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacementList">The type of the replacement list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The sequence to use as a replacement.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The source list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence list is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static ReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSource> Replace<TSourceList, TSequenceList, TReplacementList, TSource>(
            this IReadOnlySublist<TSourceList, TSource> source,
            IReadOnlySublist<TSequenceList, TSource> sequence,
            IReadOnlySublist<TReplacementList, TSource> replacement,
            IEqualityComparer<TSource> comparer)
            where TSourceList : IList<TSource>
            where TSequenceList : IList<TSource>
            where TReplacementList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return new ReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSource>(
                source,
                sequence,
                replacement,
                comparer.Equals);
        }

        /// <summary>
        /// Gets the items from the list replacing matching sequences with a replacement sequence.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequenceList">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacementList">The type of the replacement list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <typeparam name="TSequence">The type of the items in the sequence list.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The sequence to use as a replacement.</param>
        /// <param name="comparison">The comparison to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The source list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence list is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static ReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSequence> Replace<TSourceList, TSequenceList, TReplacementList, TSource, TSequence>(
            this IReadOnlySublist<TSourceList, TSource> source,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacementList, TSource> replacement,
            Func<TSource, TSequence, bool> comparison)
            where TSourceList : IList<TSource>
            where TSequenceList : IList<TSequence>
            where TReplacementList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return new ReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSequence>(
                source,
                sequence,
                replacement,
                comparison);
        }

        /// <summary>
        /// Gets the items from the list replacing those satisifying the given predicate.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="predicate">The function to use to determine whether an item should be replaced.</param>
        /// <param name="replacement">The value to use as a replacement.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static InPlaceReplaceWithConstantSource<TSourceList, TSource> Replace<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate,
            TSource replacement)
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
            return new InPlaceReplaceWithConstantSource<TSourceList, TSource>(source, predicate, replacement);
        }

        /// <summary>
        /// Gets the items from the list replacing those satisifying the given predicate with the results of the generator.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="predicate">The function to use to determine whether an item should be replaced.</param>
        /// <param name="generator">A function that creates the replacement values.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator is null.</exception>
        public static InPlaceReplaceWithGeneratorSource<TSourceList, TSource> Replace<TSourceList, TSource>(
            this IMutableSublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate,
            Func<TSource, TSource> generator)
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
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            return new InPlaceReplaceWithGeneratorSource<TSourceList, TSource>(source, predicate, generator);
        }

        /// <summary>
        /// Gets the items from the list replacing matching sequences with a replacement sequence.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequenceList">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacementList">The type of the replacement list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The sequence to use as a replacement.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The source list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence list is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        public static InPlaceReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSource> Replace<TSourceList, TSequenceList, TReplacementList, TSource>(
            this IExpandableSublist<TSourceList, TSource> source,
            IReadOnlySublist<TSequenceList, TSource> sequence,
            IReadOnlySublist<TReplacementList, TSource> replacement)
            where TSourceList : IList<TSource>
            where TSequenceList : IList<TSource>
            where TReplacementList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            return new InPlaceReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSource>(
                source,
                sequence,
                replacement,
                EqualityComparer<TSource>.Default.Equals);
        }

        /// <summary>
        /// Gets the items from the list replacing matching sequences with a replacement sequence.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequenceList">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacementList">The type of the replacement list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The sequence to use as a replacement.</param>
        /// <param name="comparer">The comparer to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The source list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence list is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static InPlaceReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSource> Replace<TSourceList, TSequenceList, TReplacementList, TSource>(
            this IExpandableSublist<TSourceList, TSource> source,
            IReadOnlySublist<TSequenceList, TSource> sequence,
            IReadOnlySublist<TReplacementList, TSource> replacement,
            IEqualityComparer<TSource> comparer)
            where TSourceList : IList<TSource>
            where TSequenceList : IList<TSource>
            where TReplacementList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return new InPlaceReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSource>(
                source,
                sequence,
                replacement,
                comparer.Equals);
        }

        /// <summary>
        /// Gets the items from the list replacing matching sequences with a replacement sequence.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequenceList">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacementList">The type of the replacement list.</typeparam>
        /// <typeparam name="TSource">The type of the items in the list.</typeparam>
        /// <typeparam name="TSequence">The type of the items in the sequence list.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The sequence to use as a replacement.</param>
        /// <param name="comparison">The comparison to use to compare items.</param>
        /// <returns>An intermediate result that can be copied or added to a destination.</returns>
        /// <exception cref="System.ArgumentNullException">The source list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence list is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static InPlaceReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSequence> Replace<TSourceList, TSequenceList, TReplacementList, TSource, TSequence>(
            this IExpandableSublist<TSourceList, TSource> source,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacementList, TSource> replacement,
            Func<TSource, TSequence, bool> comparison)
            where TSourceList : IList<TSource>
            where TSequenceList : IList<TSequence>
            where TReplacementList : IList<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if (sequence.Count == 0)
            {
                throw new ArgumentException(Resources.ReplaceEmptySequence, "sequence");
            }
            if (replacement == null)
            {
                throw new ArgumentNullException("replacement");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return new InPlaceReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSequence>(
                source,
                sequence,
                replacement,
                comparison);
        }
    }

    #endregion

    #region ReplaceResult

    /// <summary>
    /// Holds the results of copying a Replace operation.
    /// </summary>
    public sealed class ReplaceResult
    {
        /// <summary>
        /// Initializes a new instance of a ReplaceResult.
        /// </summary>
        internal ReplaceResult()
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
        public static implicit operator int(ReplaceResult result)
        {
            return result.DestinationOffset;
        }
    }

    #endregion

    #region ReplaceSource

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class ReplaceWithConstantSource<TSourceList, TSource> : Source<TSource, ReplaceResult>
        where TSourceList : IList<TSource>
    {
        internal ReplaceWithConstantSource(
            IReadOnlySublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate,
            TSource replacement)
        {
            Source = source;
            Predicate = predicate;
            Replacement = replacement;
        }

        /// <summary>
        /// Gets the list to replace the values in.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the function that determines whether an item is replaced.
        /// </summary>
        protected Func<TSource, bool> Predicate { get; private set; }

        /// <summary>
        /// Gets the value to use as a replacement.
        /// </summary>
        protected TSource Replacement { get; private set; }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected sealed override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int result = Sublist.AddReplace<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset + destination.Count,
                Predicate,
                Replacement);
            return destination.Resize(result - destination.Offset, true);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected sealed override ReplaceResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = Sublist.CopyReplace<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Predicate,
                Replacement);
            ReplaceResult result = new ReplaceResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public class ReplaceWithGeneratorSource<TSourceList, TSource> : Source<TSource, ReplaceResult>
        where TSourceList : IList<TSource>
    {
        internal ReplaceWithGeneratorSource(
            IReadOnlySublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate,
            Func<TSource, TSource> generator)
        {
            Source = source;
            Predicate = predicate;
            Generator = generator;
        }

        /// <summary>
        /// Gets the list to replace the values in.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the function that determines whether a value should be replaced.
        /// </summary>
        protected Func<TSource, bool> Predicate { get; private set; }

        /// <summary>
        /// Gets the function used to generate the next replacement.
        /// </summary>
        protected Func<TSource, TSource> Generator { get; private set; }

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected sealed override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int result = Sublist.AddReplace<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset + destination.Count,
                Predicate,
                Generator);
            return destination.Resize(result - destination.Offset, true);
        }

        /// <summary>
        /// Copies the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>Information about the results of the operation.</returns>
        protected sealed override ReplaceResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = Sublist.CopyReplace<TSourceList, TDestinationList, TSource>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Predicate,
                Generator);
            ReplaceResult result = new ReplaceResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSequenceList">The type of the sequence's underlying list.</typeparam>
    /// <typeparam name="TReplacementList">The type of the replacement's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    /// <typeparam name="TSequence">The type of the items in the sequence.</typeparam>
    public class ReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSequence> : Source<TSource, ReplaceResult>
        where TSourceList : IList<TSource>
        where TSequenceList : IList<TSequence>
        where TReplacementList : IList<TSource>
    {
        internal ReplaceWithSequenceSource(
            IReadOnlySublist<TSourceList, TSource> source,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacementList, TSource> replacement,
            Func<TSource, TSequence, bool> comparison)
        {
            Source = source;
            Sequence = sequence;
            Replacement = replacement;
            Comparison = comparison;
        }

        /// <summary>
        /// Gets the list to replace sub-sequences in.
        /// </summary>
        protected IReadOnlySublist<TSourceList, TSource> Source { get; private set; }

        /// <summary>
        /// Gets the sequence to replace.
        /// </summary>
        protected IReadOnlySublist<TSequenceList, TSequence> Sequence { get; private set; }

        /// <summary>
        /// Gets the replacement sequence.
        /// </summary>
        protected IReadOnlySublist<TReplacementList, TSource> Replacement { get; private set; }

        /// <summary>
        /// Gets the function to use to compare items in the list and the sequence.
        /// </summary>
        protected Func<TSource, TSequence, bool> Comparison;

        /// <summary>
        /// Adds the result of the intermediate calculation to the given destination list.
        /// </summary>
        /// <typeparam name="TDestinationList">The type of the underlying list to copy to.</typeparam>
        /// <param name="destination">The sublist to copy the intermediate results to.</param>
        /// <returns>A new sublist wrapping the expanded list, including the added items.</returns>
        protected sealed override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            int result = Sublist.AddReplace<TSourceList, TSequenceList, TReplacementList, TDestinationList, TSource, TSequence>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                Sequence.List, Sequence.Offset, Sequence.Offset + Sequence.Count,
                Replacement.List, Replacement.Offset, Replacement.Offset + Replacement.Count,
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
        protected sealed override ReplaceResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            Tuple<int, int> indexes = Sublist.CopyReplace<TSourceList, TSequenceList, TReplacementList, TDestinationList, TSource, TSequence>(
                Source.List, Source.Offset, Source.Offset + Source.Count,
                Sequence.List, Sequence.Offset, Sequence.Offset + Sequence.Count,
                Replacement.List, Replacement.Offset, Replacement.Offset + Replacement.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                Comparison);
            ReplaceResult result = new ReplaceResult();
            result.SourceOffset = indexes.Item1 - Source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class InPlaceReplaceWithConstantSource<TSourceList, TSource> : ReplaceWithConstantSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InPlaceReplaceWithConstantSource(
            IMutableSublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate,
            TSource replacement)
            : base(source, predicate, replacement)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        public void InPlace()
        {
            Sublist.Replace<TSourceList, TSource>(Source.List, Source.Offset, Source.Offset + Source.Count, Predicate, Replacement);
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    public sealed class InPlaceReplaceWithGeneratorSource<TSourceList, TSource> : ReplaceWithGeneratorSource<TSourceList, TSource>
        where TSourceList : IList<TSource>
    {
        internal InPlaceReplaceWithGeneratorSource(
            IMutableSublist<TSourceList, TSource> source,
            Func<TSource, bool> predicate,
            Func<TSource, TSource> generator)
            : base(source, predicate, generator)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        public void InPlace()
        {
            Sublist.Replace<TSourceList, TSource>(Source.List, Source.Offset, Source.Offset + Source.Count, Predicate, Generator);
        }
    }

    /// <summary>
    /// Provides the information needed to copy or add items to a destination sublist.
    /// </summary>
    /// <typeparam name="TSourceList">The type of the source's underlying list.</typeparam>
    /// <typeparam name="TSequenceList">The type of the sequence's underlying list.</typeparam>
    /// <typeparam name="TReplacementList">The type of the replacement's underlying list.</typeparam>
    /// <typeparam name="TSource">The type of the items in the source.</typeparam>
    /// <typeparam name="TSequence">The type of the items in the sequence.</typeparam>
    public sealed class InPlaceReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSequence> : ReplaceWithSequenceSource<TSourceList, TSequenceList, TReplacementList, TSource, TSequence>
        where TSourceList : IList<TSource>
        where TSequenceList : IList<TSequence>
        where TReplacementList : IList<TSource>
    {
        internal InPlaceReplaceWithSequenceSource(
            IExpandableSublist<TSourceList, TSource> source,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacementList, TSource> replacement,
            Func<TSource, TSequence, bool> comparison)
            : base(source, sequence, replacement, comparison)
        {
        }

        /// <summary>
        /// Performs the operation in-place.
        /// </summary>
        /// <returns>A sublist wrapping the modifying list.</returns>
        public IExpandableSublist<TSourceList, TSource> InPlace()
        {
            int past = Sublist.Replace<TSourceList, TSequenceList, TReplacementList, TSource, TSequence>(
                   Source.List, Source.Offset, Source.Offset + Source.Count,
                   Sequence.List, Sequence.Offset, Sequence.Offset + Sequence.Count,
                   Replacement.List, Replacement.Offset, Replacement.Offset + Replacement.Count,
                   Comparison);
            return new Sublist<TSourceList, TSource>(Source.List, Source.Offset, past - Source.Offset);
        }
    }

    #endregion
}
