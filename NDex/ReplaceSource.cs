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
        #region AddReplaced

        /// <summary>
        /// Adds the items from a list to a destination list, replacing any items satisfying the predicate with the given value.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to add.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="predicate">The condition an item must satisfy to be replaced.</param>
        /// <param name="replacement">The value to replace items satisfying the predicate with.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddReplaced<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, bool> predicate,
            T replacement)
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
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            int result = addReplaced<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                predicate,
                replacement);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addReplaced<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<T, bool> predicate,
            T replacement)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            GrowAndShift<TDestinationList, T>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = copyReplaced<TSourceList, TDestinationList, T>(
                source, first, past,
                destination, destinationPast, destination.Count,
                predicate,
                replacement);
            return indexes.Item2;
        }

        /// <summary>
        /// Adds the items from a list to a destination list, replacing any items satisfying the predicate with the result of the generator.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list of items to add.</param>
        /// <param name="destination">The list to add the items to.</param>
        /// <param name="predicate">The condition an item must satisfy to be replaced.</param>
        /// <param name="generator">The delegate to use to generate the replacements.</param>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddReplaced<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, bool> predicate,
            Func<T, T> generator)
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
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            int result = addReplaced<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset + destination.Count,
                predicate,
                generator);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addReplaced<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationPast,
            Func<T, bool> predicate,
            Func<T, T> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {

            GrowAndShift<TDestinationList, T>(destination, destinationPast, past - first);
            Tuple<int, int> indexes = copyReplaced<TSourceList, TDestinationList, T>(
                source, first, past,
                destination, destinationPast, destination.Count,
                predicate,
                generator);
            return indexes.Item2;
        }

        /// <summary>
        /// Adds the items in the source to the destination, replacing all occurrences of the sequence with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequence">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacement">The type of the replacement list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The source of the items to add to the destination.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The replacement items.</param>
        /// <param name="destination">The list to add the values to.</param>
        /// <returns>A new sublist wrapping the destination and the new items.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequence, T> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IExpandableSublist<TDestinationList, T> destination)
            where TSourceList : IList<T>
            where TSequence : IList<T>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
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
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return addReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T, T>(
                source,
                sequence,
                replacement,
                destination,
                EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Adds the items in the source to the destination, replacing all occurrences of the sequence with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequence">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacement">The type of the replacement list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The source of the items to add to the destination.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The replacement items.</param>
        /// <param name="destination">The list to add the values to.</param>
        /// <param name="comparer">The comparer to use to find the sequences.</param>
        /// <returns>A new sublist wrapping the destination and the new items.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequence, T> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IExpandableSublist<TDestinationList, T> destination,
            IEqualityComparer<T> comparer)
            where TSourceList : IList<T>
            where TSequence : IList<T>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
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
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T, T>(source, sequence, replacement, destination, comparer.Equals);
        }

        /// <summary>
        /// Adds the items in the source to the destination, replacing all occurrences of the sequence with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequenceList">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacement">The type of the replacement list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <typeparam name="TSequence">The type of the items in the sequence.</typeparam>
        /// <param name="source">The source of the items to add to the destination.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The replacement items.</param>
        /// <param name="destination">The list to add the values to.</param>
        /// <param name="comparison">The function to use to find the sequences.</param>
        /// <returns>A new sublist wrapping the destination and the new items.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The sequence is null.</exception>
        /// <exception cref="System.ArgumentException">The sequence is empty.</exception>
        /// <exception cref="System.ArgumentNullException">The replacement list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison is null.</exception>
        public static IExpandableSublist<TDestinationList, T> AddReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, TSequence, bool> comparison)
            where TSourceList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
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
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return addReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(source, sequence, replacement, destination, comparison);
        }

        private static IExpandableSublist<TDestinationList, T> addReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IExpandableSublist<TDestinationList, T> destination,
            Func<T, TSequence, bool> comparison)
            where TSourceList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            int result = addReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
                source.List, source.Offset, source.Offset + source.Count,
                sequence.List, sequence.Offset, sequence.Offset + sequence.Count,
                replacement.List, replacement.Offset, replacement.Offset + replacement.Count,
                destination.List, destination.Offset + destination.Count,
                comparison);
            return destination.Resize(result - destination.Offset, true);
        }

        private static int addReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
            TSourceList source, int first, int past,
            TSequenceList sequence, int sequenceFirst, int sequencePast,
            TReplacement replacement, int replacementFirst, int replacementPast,
            TDestinationList destination, int destinationPast,
            Func<T, TSequence, bool> comparison)
            where TSourceList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            int sequenceCount = sequencePast - sequenceFirst;
            int index = indexOfSequence<TSourceList, T, TSequenceList, TSequence>(source, first, past, sequence, sequenceFirst, sequencePast, comparison);
            destinationPast = Add<TSourceList, TDestinationList, T>(source, first, index, destination, destinationPast);

            while (index != past)
            {
                destinationPast = Add<TReplacement, TDestinationList, T>(replacement, replacementFirst, replacementPast, destination, destinationPast);
                index += sequenceCount;
                int next = indexOfSequence<TSourceList, T, TSequenceList, TSequence>(source, index, past, sequence, sequenceFirst, sequencePast, comparison);
                destinationPast = Add<TSourceList, TDestinationList, T>(source, index, next, destination, destinationPast);
                index = next;
            }
            return destinationPast;
        }

        #endregion

        #region CopyReplaced

        /// <summary>
        /// Copies a list, replacing items satisfying the predicate with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="predicate">The predicate used to determine whether an item should be replaced.</param>
        /// <param name="replacement">The value to use to replace items satisfying the predicate.</param>
        /// <returns>The index into the destination past the last copied item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static ReplaceResult CopyReplaced<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, bool> predicate,
            T replacement)
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
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            Tuple<int, int> indexes = copyReplaced<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                predicate,
                replacement);
            ReplaceResult result = new ReplaceResult();
            result.SourceOffset = indexes.Item1 - source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }

        private static Tuple<int, int> copyReplaced<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, bool> predicate,
            T replacement)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            while (first != past && destinationFirst != destinationPast)
            {
                if (predicate(source[first]))
                {
                    destination[destinationFirst] = replacement;
                }
                else
                {
                    destination[destinationFirst] = source[first];
                }
                ++first;
                ++destinationFirst;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        /// <summary>
        /// Copies a list, replacing items satisfying the predicate with the result of the generator delegate.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The list to copy.</param>
        /// <param name="destination">The list to copy to.</param>
        /// <param name="predicate">The predicate used to determine whether an item should be replaced.</param>
        /// <param name="generator">The delegate to use to generate the replacement value.</param>
        /// <returns>The index into the destination past the last copied item.</returns>
        /// <exception cref="System.ArgumentNullException">The list is null.</exception>
        /// <exception cref="System.ArgumentNullException">The destination is null.</exception>
        /// <exception cref="System.ArgumentNullException">The generator delegate is null.</exception>
        /// <exception cref="System.ArgumentNullException">The predicate is null.</exception>
        public static ReplaceResult CopyReplaced<TSourceList, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, bool> predicate,
            Func<T, T> generator)
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
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            Tuple<int, int> indexes = copyReplaced<TSourceList, TDestinationList, T>(
                source.List, source.Offset, source.Offset + source.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                predicate,
                generator);
            ReplaceResult result = new ReplaceResult();
            result.SourceOffset = indexes.Item1 - source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }

        private static Tuple<int, int> copyReplaced<TSourceList, TDestinationList, T>(
            TSourceList source, int first, int past,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, bool> predicate,
            Func<T, T> generator)
            where TSourceList : IList<T>
            where TDestinationList : IList<T>
        {
            while (first != past && destinationFirst != destinationPast)
            {
                if (predicate(source[first]))
                {
                    destination[destinationFirst] = generator(source[first]);
                }
                else
                {
                    destination[destinationFirst] = source[first];
                }
                ++first;
                ++destinationFirst;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        /// <summary>
        /// Copies the items in the source to the destination, replacing all occurrences of the sequence with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequence">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacement">The type of the replacement list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The source of the items to copy to the destination.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The replacement items.</param>
        /// <param name="destination">The list to copy the values to.</param>
        /// <returns>A result holding the indexes into the source and destination where the copying stopped.</returns>
        public static ReplaceResult CopyReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequence, T> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IMutableSublist<TDestinationList, T> destination)
            where TSourceList : IList<T>
            where TSequence : IList<T>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
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
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            return copyReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T, T>(
                source,
                sequence,
                replacement,
                destination,
                EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Copies the items in the source to the destination, replacing all occurrences of the sequence with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequence">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacement">The type of the replacement list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <param name="source">The source of the items to copy to the destination.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The replacement items.</param>
        /// <param name="destination">The list to copy the values to.</param>
        /// <param name="comparer">The comparer to use to find the sequences.</param>
        /// <returns>A result holding the indexes into the source and destination where the copying stopped.</returns>
        public static ReplaceResult CopyReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequence, T> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IMutableSublist<TDestinationList, T> destination,
            IEqualityComparer<T> comparer)
            where TSourceList : IList<T>
            where TSequence : IList<T>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
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
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copyReplaced<TSourceList, TSequence, TReplacement, TDestinationList, T, T>(source, sequence, replacement, destination, comparer.Equals);
        }

        /// <summary>
        /// Copies the items in the source to the destination, replacing all occurrences of the sequence with the given replacement.
        /// </summary>
        /// <typeparam name="TSourceList">The type of the source list.</typeparam>
        /// <typeparam name="TSequenceList">The type of the sequence list.</typeparam>
        /// <typeparam name="TReplacement">The type of the replacement list.</typeparam>
        /// <typeparam name="TDestinationList">The type of the destination list.</typeparam>
        /// <typeparam name="T">The type of the items in the lists.</typeparam>
        /// <typeparam name="TSequence">The type of the items in the sequence.</typeparam>
        /// <param name="source">The source of the items to copy to the destination.</param>
        /// <param name="sequence">The sequence of items to replace.</param>
        /// <param name="replacement">The replacement items.</param>
        /// <param name="destination">The list to copy the values to.</param>
        /// <param name="comparison">The function to use to find the sequences.</param>
        /// <returns>A result holding the indexes into the source and destination where the copying stopped.</returns>
        public static ReplaceResult CopyReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, TSequence, bool> comparison)
            where TSourceList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
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
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            return copyReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(source, sequence, replacement, destination, comparison);
        }

        private static ReplaceResult copyReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
            IReadOnlySublist<TSourceList, T> source,
            IReadOnlySublist<TSequenceList, TSequence> sequence,
            IReadOnlySublist<TReplacement, T> replacement,
            IMutableSublist<TDestinationList, T> destination,
            Func<T, TSequence, bool> comparison)
            where TSourceList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            Tuple<int, int> indexes = copyReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
                source.List, source.Offset, source.Offset + source.Count,
                sequence.List, sequence.Offset, sequence.Offset + sequence.Count,
                replacement.List, replacement.Offset, replacement.Offset + replacement.Count,
                destination.List, destination.Offset, destination.Offset + destination.Count,
                comparison);
            ReplaceResult result = new ReplaceResult();
            result.SourceOffset = indexes.Item1 - source.Offset;
            result.DestinationOffset = indexes.Item2 - destination.Offset;
            return result;
        }

        private static Tuple<int, int> copyReplaced<TSourceList, TSequenceList, TReplacement, TDestinationList, T, TSequence>(
            TSourceList source, int first, int past,
            TSequenceList sequence, int sequenceFirst, int sequencePast,
            TReplacement replacement, int replacementFirst, int replacementPast,
            TDestinationList destination, int destinationFirst, int destinationPast,
            Func<T, TSequence, bool> comparison)
            where TSourceList : IList<T>
            where TSequenceList : IList<TSequence>
            where TReplacement : IList<T>
            where TDestinationList : IList<T>
        {
            int sequenceCount = sequencePast - sequenceFirst;
            int replacementCount = replacementPast - replacementFirst;

            int index = indexOfSequence<TSourceList, T, TSequenceList, TSequence>(source, first, past, sequence, sequenceFirst, sequencePast, comparison);
            Tuple<int, int> indexes = Copy<TSourceList, TDestinationList, T>(source, first, index, destination, destinationFirst, destinationPast);
            first = indexes.Item1;
            destinationFirst = indexes.Item2;

            while (index != past && destinationFirst + replacementCount <= destinationPast)
            {
                indexes = Copy<TReplacement, TDestinationList, T>(replacement, replacementFirst, replacementPast, destination, destinationFirst, destinationPast);
                destinationFirst = indexes.Item2;
                index += sequenceCount;

                int next = indexOfSequence<TSourceList, T, TSequenceList, TSequence>(source, index, past, sequence, sequenceFirst, sequencePast, comparison);
                indexes = Copy<TSourceList, TDestinationList, T>(source, index, next, destination, destinationFirst, destinationPast);
                first = indexes.Item1;
                destinationFirst = indexes.Item2;
                index = next;
            }
            return new Tuple<int, int>(first, destinationFirst);
        }

        #endregion
    }

    #endregion

    #region ReplaceResult

    /// <summary>
    /// Holds the results of copying the results of a Replace operation.
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

    internal sealed class ReplaceSource<TSourceList, TSource> : Source<TSource, ReplaceResult>
        where TSourceList : IList<TSource>
    {
        private readonly IReadOnlySublist<TSourceList, TSource> source;

        public ReplaceSource(IReadOnlySublist<TSourceList, TSource> source)
        {
            this.source = source;
        }

        protected override IExpandableSublist<TDestinationList, TSource> SafeAddTo<TDestinationList>(IExpandableSublist<TDestinationList, TSource> destination)
        {
            throw new NotImplementedException();
        }

        protected override ReplaceResult SafeCopyTo<TDestinationList>(IMutableSublist<TDestinationList, TSource> destination)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
