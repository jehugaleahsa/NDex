using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ReplaceInPlace methods.
    /// </summary>
    [TestClass]
    public class ReplaceInPlaceTester
    {
        #region Real World Example

        /// <summary>
        /// If you had a list of numbers, and wanted to make sure that they were all positive,
        /// you could replace the negatives with their absolute values.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_AbsoluteValue()
        {
            Random random = new Random();

            // build a list of numbers
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(-49, 50)).AddTo(list.ToSublist());

            list.ToSublist().Replace(i => i < 0, i => -i).InPlace();

            Assert.IsFalse(list.ToSublist().Find(i => i < 0), "Not all values were positive.");
        }

        /// <summary>
        /// If you want to replace a sequence of values, you can specify the old sequence and a 
        /// replacement sequence.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_ReplaceMisspelledWords()
        {
            string source = "This mesage contains mispelled wordz.";
            var list = source.ToList().ToSublist();
            const string mesage = "mesage";
            const string message = "message";

            list = list.Replace(mesage.ToSubstring(), message.ToSubstring()).InPlace();

            const string contains = "contains";
            const string has = "has";

            list = list.Replace(contains.ToSubstring(), has.ToSubstring()).InPlace();

            const string mispelled = "mispelled";
            const string misspelled = "misspelled";

            list = list.Replace(mispelled.ToSubstring(), misspelled.ToSubstring()).InPlace();

            const string wordz = "wordz";
            const string words = "words";

            list = list.Replace(wordz.ToSubstring(), words.ToSubstring()).InPlace();

            const string expected = "This message has misspelled words.";
            string result = new String(list.ToArray());
            Assert.AreEqual(expected, result, "The words were not replaced as expected.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            int replacement = 0;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_WithGenerator_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_NullPredicate_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            int replacement = 0;
            Func<int, bool> predicate = null;
            list.Replace(predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_WithGenerator_NullPredicate_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = null;
            list.Replace(predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_NullGenerator_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int> generator = null;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, generator);
        }

        /// <summary>
        /// If we try to pass a null list, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_ListNull_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            list.Replace(sequence, replacement);
        }

        /// <summary>
        /// If we try to pass a null sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_SequenceNull_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = null;
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            list.Replace(sequence, replacement);
        }

        /// <summary>
        /// If we try to pass an empty sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestReplace_SequenceEmpty_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            list.Replace(sequence, replacement);
        }

        /// <summary>
        /// If we try to pass a null replacement, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_ReplacementNull_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = null;
            list.Replace(sequence, replacement);
        }

        /// <summary>
        /// If we try to pass a null source, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_WithComparer_ListNull_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass a null sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_WithComparer_SequenceNull_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = null;
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass an empty sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestReplace_WithComparer_SequenceEmpty_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass a null replacement, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_WithComparer_ReplacementNull_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass a null comparer, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_WithComparer_ComparerNull_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = null;
            list.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass a null source, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_WithComparison_ListNull_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list.Replace(sequence, replacement, comparison);
        }

        /// <summary>
        /// If we try to pass a null sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_WithComparison_SequenceNull_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = null;
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list.Replace(sequence, replacement, comparison);
        }

        /// <summary>
        /// If we try to pass an empty sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestReplace_WithComparison_SequenceEmpty_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list.Replace(sequence, replacement, comparison);
        }

        /// <summary>
        /// If we try to pass a null replacement, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_WithComparison_ReplacementNull_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list.Replace(sequence, replacement, comparison);
        }

        /// <summary>
        /// If we try to pass a null comparer, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_WithComparison_ComparerNull_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            Func<int, int, bool> comparison = null;
            list.Replace(sequence, replacement, comparison);
        }

        #endregion

        /// <summary>
        /// If the predicate is satified by an item, it should be replaced.
        /// </summary>
        [TestMethod]
        public void TestReplace_ItemsMatchingPredicateAreReplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, });
            int replacement = 3;
            Func<int, bool> predicate = i => i % 2 == 0;
            list.Replace(predicate, replacement).InPlace();
            int[] expected = { 1, 3, 3, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list), "The list did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the predicate is satified by an item, it should be replaced.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_WithGenerator_ItemsMatchingPredicateAreReplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, });
            Func<int, int> generator = i => i + 1; // make odd by adding one
            Func<int, bool> predicate = i => i % 2 == 0;
            list.Replace(predicate, generator).InPlace();
            int[] expected = { 1, 3, 3, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list), "The list did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we replace a sequence that equals the source with an empty replacement,
        /// nothing will be added.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceEqualsSource_ReplacementEmpty_AddsNothing()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>());

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[0];
            Assert.AreEqual(0, source.Count, "The source was not cleared.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence appearing at the front of the source with an empty replacement,
        /// the front of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInFront_ReplacementEmpty_RemovesFrontOfList()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>());

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence appearing at the back of the source with an empty replacement,
        /// the back of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInBack_ReplacementEmpty_RemovesBackOfList()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>());

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 1, 2 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence appearing in the middle of the source with an empty replacement,
        /// the middle of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInMiddle_ReplacementEmpty_RemovesMiddleOfList()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.Wrap(new List<int>());

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 1, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence appearing in multiple locations of the source with an empty replacement,
        /// all occurrences should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInMultipleLocations_ReplacementEmpty_RemovesAllOccurrences()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>());

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 1, 4, 4, 5, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence that equals the source with a smaller replacement,
        /// only the replacement will be added.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceEqualsSource_ReplacementSmaller_AddsReplacement()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 9, 9 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence appearing at the front of the source with a smaller replacement,
        /// the front of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInFront_ReplacementSmaller_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 9, 9, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence appearing at the back of the source with a smaller replacement,
        /// the back of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInBack_ReplacementSmaller_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 1, 2, 9, 9 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence appearing in the middle of the source with a smaller replacement,
        /// the middle of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInMiddle_ReplacementSmaller_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 1, 9, 9, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence appearing in multiple locations of the source with a smaller replacement,
        /// all occurrences should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInMultipleLocations_ReplacementSmaller_RemovesAllOccurrences()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 1, 9, 9, 5, 9, 9, 5, 2, 3, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence that equals the source with a larger replacement,
        /// only the replacement will be added.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceEqualsSource_ReplacementLarger_AddsReplacement()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9, 9, 9 });

            source = source.Replace(sequence, replacement).InPlace();

            Assert.IsTrue(replacement.IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence appearing at the front of the source with a larger replacement,
        /// the front of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInFront_ReplacementLarger_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9 });

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 9, 9, 9, 9, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence appearing at the back of the source with a larger replacement,
        /// the back of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInBack_ReplacementLarger_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9 });

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 1, 2, 9, 9, 9, 9 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence appearing in the middle of the source with a larger replacement,
        /// the middle of the list should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInMiddle_ReplacementLarger_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9 });

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 1, 9, 9, 9, 9, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence appearing in multiple locations of the source with a larger replacement,
        /// all occurrences should be removed.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInMultipleLocations_ReplacementLarger_RemovesAllOccurrences()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9 });

            source = source.Replace(sequence, replacement).InPlace();

            int[] expected = new int[] { 1, 9, 9, 9, 4, 9, 9, 9, 4, 5, 9, 9, 9, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(replacement);
        }

        /// <summary>
        /// If we replace a sequence that equals the source that equals the replacement,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceEqualsSource_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });

            source = source.Replace(sequence, sequence).InPlace();

            Assert.IsTrue(sequence.IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
        }

        /// <summary>
        /// If we replace a sequence that equals the front of the source that equals the replacement,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInFront_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });

            source = source.Replace(sequence, sequence).InPlace();

            int[] expected = new int[] { 1, 2, 3, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
        }

        /// <summary>
        /// If we replace a sequence that equals the back of the source that equals the replacement,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInBack_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });

            source = source.Replace(sequence, sequence).InPlace();

            int[] expected = new int[] { 1, 2, 3, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
        }

        /// <summary>
        /// If we replace a sequence that equals the middle of the source that equals the replacement,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInMiddle_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });

            source = source.Replace(sequence, sequence, EqualityComparer<int>.Default).InPlace();

            int[] expected = new int[] { 1, 2, 3, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
        }

        /// <summary>
        /// If we replace a sequence that appears in multiple in the source with itself,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestReplaceInPlace_SequenceInMultipleLocations_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3 });

            source = source.Replace(sequence, sequence, EqualityComparer<int>.Default.Equals).InPlace();

            int[] expected = new int[] { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(source), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
        }
    }
}
