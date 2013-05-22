using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ReplaceAdd methods.
    /// </summary>
    [TestClass]
    public class ReplaceAddTester
    {
        #region Real World Example

        /// <summary>
        /// If you had a list of numbers, and wanted to make sure that they were all positive,
        /// you could replace the negatives with their absolute values.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_AbsoluteValue()
        {
            Random random = new Random();

            // build a list of numbers
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(-49, 50)).AddTo(list.ToSublist());

            var destination = new List<int>(100);

            list.ToSublist().Replace(i => i < 0, i => -i).AddTo(destination.ToSublist());

            Assert.IsTrue(destination.ToSublist().TrueForAll(i => i >= 0), "Not all values were positive.");
        }

        /// <summary>
        /// If you want to replace a sequence of values, you can specify the old sequence and a 
        /// replacement sequence.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_ReplaceMisspelledWords()
        {
            string source = "This mesage has mispelled wordz.";
            List<char> destination = new List<char>();
            const string mesage = "mesage";
            const string message = "message";

            source.ToSubstring().Replace(mesage.ToSubstring(), message.ToSubstring()).AddTo(destination.ToSublist());
            source = new String(destination.ToArray());

            const string mispelled = "mispelled";
            const string misspelled = "misspelled";

            destination.Clear();
            source.ToSubstring().Replace(mispelled.ToSubstring(), misspelled.ToSubstring()).AddTo(destination.ToSublist());
            source = new String(destination.ToArray());

            const string wordz = "wordz";
            const string words = "words";

            destination.Clear();
            source.ToSubstring().Replace(wordz.ToSubstring(), words.ToSubstring()).AddTo(destination.ToSublist());
            source = new String(destination.ToArray());

            const string expected = "This message has misspelled words.";
            Assert.AreEqual(expected, source, "The words were not replaced as expected.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int replacement = 0;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithGenerator_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            int replacement = 0;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, replacement).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithGenerator_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, generator).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int replacement = 0;
            Func<int, bool> predicate = null;
            list.Replace(predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithGenerator_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = null;
            list.Replace(predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_NullGenerator_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int> generator = null;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, generator);
        }

        /// <summary>
        /// If we try to pass a null source, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_SourceNull_Throws()
        {
            Sublist<List<int>, int> source = null;
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            source.Replace(sequence, replacement);
        }

        /// <summary>
        /// If we try to pass a null sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_SequenceNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = null;
            Sublist<List<int>, int> replacement = new List<int>();
            source.Replace(sequence, replacement);
        }

        /// <summary>
        /// If we try to pass an empty sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestReplaceAdd_SequenceEmpty_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>();
            Sublist<List<int>, int> replacement = new List<int>();
            source.Replace(sequence, replacement);
        }

        /// <summary>
        /// If we try to pass a null replacement, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_ReplacementNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = null;
            source.Replace(sequence, replacement);
        }

        /// <summary>
        /// If we try to pass a null destination, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_DestinationNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = null;
            source.Replace(sequence, replacement).AddTo(destination);
        }

        /// <summary>
        /// If we try to pass a null source, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithComparer_SourceNull_Throws()
        {
            Sublist<List<int>, int> source = null;
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            source.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass a null sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithComparer_SequenceNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = null;
            Sublist<List<int>, int> replacement = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            source.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass an empty sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestReplaceAdd_WithComparer_SequenceEmpty_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>();
            Sublist<List<int>, int> replacement = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            source.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass a null replacement, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithComparer_ReplacementNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            source.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass a null destination, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithComparer_DestinationNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            source.Replace(sequence, replacement, comparer).AddTo(destination);
        }

        /// <summary>
        /// If we try to pass a null comparer, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithComparer_ComparerNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            IEqualityComparer<int> comparer = null;
            source.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass a null source, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithComparison_SourceNull_Throws()
        {
            Sublist<List<int>, int> source = null;
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            source.Replace(sequence, replacement, comparison);
        }

        /// <summary>
        /// If we try to pass a null sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithComparison_SequenceNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = null;
            Sublist<List<int>, int> replacement = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            source.Replace(sequence, replacement, comparison);
        }

        /// <summary>
        /// If we try to pass an empty sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestReplaceAdd_WithComparison_SequenceEmpty_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>();
            Sublist<List<int>, int> replacement = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            source.Replace(sequence, replacement, comparison);
        }

        /// <summary>
        /// If we try to pass a null replacement, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithComparison_ReplacementNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            source.Replace(sequence, replacement, comparison);
        }

        /// <summary>
        /// If we try to pass a null destination, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithComparison_DestinationNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            source.Replace(sequence, replacement, comparison).AddTo(destination);
        }

        /// <summary>
        /// If we try to pass a null comparer, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceAdd_WithComparison_ComparerNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Func<int, int, bool> comparison = null;
            source.Replace(sequence, replacement, comparison);
        }

        #endregion

        /// <summary>
        /// If the predicate is satified by an item, it should be replaced.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_ItemsMatchingPredicateAreReplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, });
            var destination = TestHelper.Wrap(new List<int>());
            int replacement = 3;
            Func<int, bool> predicate = i => i % 2 == 0;
            destination = list.Replace(predicate, replacement).AddTo(destination);
            int[] expected = { 1, 3, 3, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the predicate is satified by an item, it should be replaced.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_WithGenerator_ItemsMatchingPredicateAreReplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, });
            var destination = TestHelper.Wrap(new List<int>());
            Func<int, int> generator = i => i + 1; // make odd by adding one
            Func<int, bool> predicate = i => i % 2 == 0;
            destination = list.Replace(predicate, generator).AddTo(destination);
            int[] expected = { 1, 3, 3, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that equals the source with an empty replacement,
        /// nothing will be added.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceEqualsSource_ReplacementEmpty_AddsNothing()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence appearing at the front of the source with an empty replacement,
        /// the front of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInFront_ReplacementEmpty_RemovesFrontOfList()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence appearing at the back of the source with an empty replacement,
        /// the back of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInBack_ReplacementEmpty_RemovesBackOfList()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 1, 2 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence appearing in the middle of the source with an empty replacement,
        /// the middle of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInMiddle_ReplacementEmpty_RemovesMiddleOfList()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 1, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence appearing in multiple locations of the source with an empty replacement,
        /// all occurrences should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInMultipleLocations_ReplacementEmpty_RemovesAllOccurrences()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 1, 4, 4, 5, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that equals the source with a smaller replacement,
        /// only the replacement will be added.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceEqualsSource_ReplacementSmaller_AddsReplacement()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 9, 9 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence appearing at the front of the source with a smaller replacement,
        /// the front of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInFront_ReplacementSmaller_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 9, 9, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence appearing at the back of the source with a smaller replacement,
        /// the back of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInBack_ReplacementSmaller_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 1, 2, 9, 9 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence appearing in the middle of the source with a smaller replacement,
        /// the middle of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInMiddle_ReplacementSmaller_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 1, 9, 9, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence appearing in multiple locations of the source with a smaller replacement,
        /// all occurrences should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInMultipleLocations_ReplacementSmaller_RemovesAllOccurrences()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 1, 9, 9, 4, 9, 9, 4, 5, 9, 9, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that equals the source with a larger replacement,
        /// only the replacement will be added.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceEqualsSource_ReplacementLarger_AddsReplacement()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            Assert.IsTrue(replacement.IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence appearing at the front of the source with a larger replacement,
        /// the front of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInFront_ReplacementLarger_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 9, 9, 9, 9, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence appearing at the back of the source with a larger replacement,
        /// the back of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInBack_ReplacementLarger_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 1, 2, 9, 9, 9, 9 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence appearing in the middle of the source with a larger replacement,
        /// the middle of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInMiddle_ReplacementLarger_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 1, 9, 9, 9, 9, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence appearing in multiple locations of the source with a larger replacement,
        /// all occurrences should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInMultipleLocations_ReplacementLarger_RemovesAllOccurrences()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, replacement).AddTo(destination);

            int[] expected = new int[] { 1, 9, 9, 9, 9, 4, 9, 9, 9, 9, 4, 5, 9, 9, 9, 9, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that equals the source that equals the replacement,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceEqualsSource_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, sequence).AddTo(destination);

            Assert.IsTrue(sequence.IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that equals the front of the source that equals the replacement,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInFront_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, sequence).AddTo(destination);

            Assert.IsTrue(source.IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that equals the back of the source that equals the replacement,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInBack_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = source.Replace(sequence, sequence).AddTo(destination);

            Assert.IsTrue(source.IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that equals the middle of the source that equals the replacement,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInMiddle_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var destination = TestHelper.Wrap(new List<int>());
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;

            destination = source.Replace(sequence, sequence, comparer).AddTo(destination);

            Assert.IsTrue(source.IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that appears in multiple in the source with itself,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestReplaceAdd_SequenceInMultipleLocations_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3 });
            var destination = TestHelper.Wrap(new List<int>());
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;

            destination = source.Replace(sequence, sequence, comparison).AddTo(destination);

            Assert.IsTrue(source.IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
