using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ReplaceCopy methods.
    /// </summary>
    [TestClass]
    public class ReplaceCopyTester
    {
        #region Real World Example

        /// <summary>
        /// If you had a list of numbers, and wanted to make sure that they were all positive,
        /// you could replace the negatives with their absolute values.
        /// </summary>
        [TestMethod]
        public void TestReplaceCopy_AbsoluteValue()
        {
            Random random = new Random();

            // build a list of numbers
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(-49, 50)).AddTo(list.ToSublist());

            var destination = new List<int>(100);
            Sublist.Generate(100, 0).AddTo(destination.ToSublist());

            int result = list.ToSublist().Replace(i => i < 0, i => -i).CopyTo(destination.ToSublist());
            Assert.AreEqual(destination.Count, result, "The wrong index was returned.");

            Assert.IsFalse(destination.ToSublist().Find(i => i < 0), "Not all values were positive.");
        }

        /// <summary>
        /// If you want to replace a sequence of values, you can specify the old sequence and a 
        /// replacement sequence.
        /// </summary>
        [TestMethod]
        public void TestReplaceCopy_ReplaceMisspelledWords()
        {
            string source = "This mesage has mispelled wordz.";
            List<char> destination = new List<char>();
            Sublist.Generate(source.Length + 2, '\0').AddTo(destination.ToSublist());
            const string mesage = "mesage";
            const string message = "message";

            var result = source.ToSubstring().Replace(mesage.ToSubstring(), message.ToSubstring()).CopyTo(destination.ToSublist());
            source = new String(destination.ToSublist(0, result.DestinationOffset).ToArray());

            const string mispelled = "mispelled";
            const string misspelled = "misspelled";

            result = source.ToSubstring().Replace(mispelled.ToSubstring(), misspelled.ToSubstring()).CopyTo(destination.ToSublist());
            source = new String(destination.ToSublist(0, result.DestinationOffset).ToArray());

            const string wordz = "wordz";
            const string words = "words";

            result = source.ToSubstring().Replace(wordz.ToSubstring(), words.ToSubstring()).CopyTo(destination.ToSublist());
            source = new String(destination.ToSublist(0, result.DestinationOffset).ToArray());

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
        public void TestReplaceCopy_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            int replacement = 0;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithGenerator_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            int replacement = 0;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, replacement).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithGenerator_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, generator).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_NullPredicate_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int replacement = 0;
            Func<int, bool> predicate = null;
            list.Replace(predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithGenerator_NullPredicate_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = null;
            list.Replace(predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_NullGenerator_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int> generator = null;
            Func<int, bool> predicate = i => true;
            list.Replace(predicate, generator);
        }

        /// <summary>
        /// If we try to pass a null source, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_SourceNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = null;
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            source.Replace(sequence, replacement);
        }

        /// <summary>
        /// If we try to pass a null sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_SequenceNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = null;
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            source.Replace(sequence, replacement);
        }

        /// <summary>
        /// If we try to pass an empty sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestReplaceCopy_SequenceEmpty_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            source.Replace(sequence, replacement);
        }

        /// <summary>
        /// If we try to pass a null replacement, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_ReplacementNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = null;
            source.Replace(sequence, replacement);
        }

        /// <summary>
        /// If we try to pass a null destination, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_DestinationNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            source.Replace(sequence, replacement).CopyTo(destination);
        }

        /// <summary>
        /// If we try to pass a null source, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithComparer_SourceNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = null;
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            source.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass a null sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithComparer_SequenceNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = null;
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            source.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass an empty sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestReplaceCopy_WithComparer_SequenceEmpty_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            source.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass a null replacement, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithComparer_ReplacementNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            source.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass a null destination, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithComparer_DestinationNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            source.Replace(sequence, replacement, comparer).CopyTo(destination);
        }

        /// <summary>
        /// If we try to pass a null comparer, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithComparer_ComparerNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = null;
            source.Replace(sequence, replacement, comparer);
        }

        /// <summary>
        /// If we try to pass a null source, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithComparison_SourceNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = null;
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            source.Replace(sequence, replacement, comparison);
        }

        /// <summary>
        /// If we try to pass a null sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithComparison_SequenceNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = null;
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            source.Replace(sequence, replacement, comparison);
        }

        /// <summary>
        /// If we try to pass an empty sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestReplaceCopy_WithComparison_SequenceEmpty_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            source.Replace(sequence, replacement, comparison);
        }

        /// <summary>
        /// If we try to pass a null replacement, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithComparison_ReplacementNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            source.Replace(sequence, replacement, comparison);
        }

        /// <summary>
        /// If we try to pass a null destination, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithComparison_DestinationNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            source.Replace(sequence, replacement, comparison).CopyTo(destination);
        }

        /// <summary>
        /// If we try to pass a null comparer, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplaceCopy_WithComparison_ComparisonNull_Throws()
        {
            IReadOnlySublist<List<int>, int> source = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> sequence = new List<int>() { 1 }.ToSublist();
            IExpandableSublist<List<int>, int> replacement = new List<int>().ToSublist();
            Func<int, int, bool> comparison = null;
            source.Replace(sequence, replacement, comparison);
        }

        #endregion

        /// <summary>
        /// If the destination is too small to hold all of the values, then the algorithm stops prematurely.
        /// </summary>
        [TestMethod]
        public void TestReplaceCopy_DestinationTooSmall_StopsPrematurely()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            int replacement = 0;
            Func<int, bool> predicate = i => false;
            var result = list.Replace(predicate, replacement).CopyTo(destination);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is too small to hold all of the values, then the algorithm stops prematurely.
        /// </summary>
        [TestMethod]
        public void TestReplaceCopy_WithGenerator_DestinationTooSmall_StopsPrematurely()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => false;
            var result = list.Replace(predicate, generator).CopyTo(destination);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is larger than the source, then there should be space left over at the end.
        /// </summary>
        [TestMethod]
        public void TestReplaceCopy_SourceSmaller_SpaceLeftOver()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            int replacement = 0;
            Func<int, bool> predicate = i => false;
            var result = list.Replace(predicate, replacement).CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offet was wrong.");
            int[] expected = { 1, 2, 0, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is larger than the source, then there should be space left over at the end.
        /// </summary>
        [TestMethod]
        public void TestReplaceCopy_WithGenerator_SourceSmaller_SpaceLeftOver()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => false;
            var result = list.Replace(predicate, generator).CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 0, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the predicate is satified by an element, it should be replaced.
        /// </summary>
        [TestMethod]
        public void TestReplaceCopy_ItemsMatchingPredicateAreReplaced()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 5, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            int replacement = 3;
            Func<int, bool> predicate = i => i % 2 == 0;
            var result = list.Replace(predicate, replacement).CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 3, 3, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the predicate is satified by an element, it should be replaced.
        /// </summary>
        [TestMethod]
        public void TestReplaceCopy_WithGenerator_ItemsMatchingPredicateAreReplaced()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 5, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            Func<int, int> generator = i => i + 1; // make odd by adding one
            Func<int, bool> predicate = i => i % 2 == 0;
            var result = list.Replace(predicate, generator).CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
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
        public void TestReplaceCopy_SequenceEqualsSource_ReplacementEmpty_CopiesNothing()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var replacement = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(0, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 0, 0, 0, 0, 0 };
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
        public void TestReplaceCopy_SequenceInFront_ReplacementEmpty_RemovesFrontOfList()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 4, 5, 0, 0, 0 };
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
        public void TestReplaceCopy_SequenceInBack_ReplacementEmpty_RemovesBackOfList()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 2, 0, 0, 0 };
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
        public void TestReplaceCopy_SequenceInMiddle_ReplacementEmpty_RemovesMiddleOfList()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 5, 0, 0, 0 };
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
        public void TestReplaceCopy_SequenceInMultipleLocations_ReplacementEmpty_RemovesAllOccurrences()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 2, 3 });
            var replacement = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

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
        public void TestReplaceCopy_SequenceEqualsSource_ReplacementSmaller_CopiesReplacement()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 9, 9, 0, 0, 0 };
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
        public void TestReplaceCopy_SequenceInFront_ReplacementSmaller_ReplacesValues()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(4, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 9, 9, 4, 5, 0 };
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
        public void TestReplaceCopy_SequenceInBack_ReplacementSmaller_ReplacesValues()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(4, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 2, 9, 9, 0 };
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
        public void TestReplaceCopy_SequenceInMiddle_ReplacementSmaller_ReplacesValues()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(4, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 9, 9, 5, 0 };
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
        public void TestReplaceCopy_SequenceInMultipleLocations_ReplacementSmaller_ReplacesAllOccurrences()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 9, 9, 5, 9, 9, 5, 2, 3, 1 };
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
        public void TestReplaceCopy_SequenceEqualsSource_ReplacementLarger_CopiesReplacement()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9, 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

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
        public void TestReplaceCopy_SequenceInFront_ReplacementLarger_ReplacesValues()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

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
        public void TestReplaceCopy_SequenceInBack_ReplacementLarger_ReplacesValues()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

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
        public void TestReplaceCopy_SequenceInMiddle_ReplacementLarger_ReplacesValues()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

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
        public void TestReplaceCopy_SequenceInMultipleLocations_ReplacementLarger_ReplacesAllOccurrences()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 2, 3 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 9, 9, 9, 4, 9, 9, 9, 4, 5, 9, 9, 9, 1 };
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
        public void TestReplaceCopy_SequenceEqualsSource_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, sequence).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

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
        public void TestReplaceCopy_SequenceInFront_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, sequence).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

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
        public void TestReplaceCopy_SequenceInBack_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, sequence).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

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
        public void TestReplaceCopy_SequenceInMiddle_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 2, 3, 4 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, sequence).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

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
        public void TestReplaceCopy_SequenceInMultipleLocations_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, sequence).CopyTo(destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            Assert.IsTrue(source.IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is too short to contain a replacement, the algorithm should stop
        /// prematurely.
        /// </summary>
        [TestMethod]
        public void TestReplaceCopy_SequenceInBack_DestinationTooSmall_StopsPrematurely()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 2, 0, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is too short to contain the rest of the source after a replacement, the algorithm should stop
        /// prematurely.
        /// </summary>
        [TestMethod]
        public void TestReplaceCopy_SequenceInFront_DestinationTooSmall_StopsPrematurely()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = source.Replace(sequence, replacement).CopyTo(destination);
            Assert.AreEqual(4, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 9, 9, 9, 9, 4 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is too short to contain the rest of the source after a replacement, the algorithm should stop
        /// prematurely.
        /// </summary>
        [TestMethod]
        public void TestReplaceCopy_SequenceInMiddle_DestinationTooSmall_StopsPrematurely()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;

            var result = source.Replace(sequence, replacement, comparer).CopyTo(destination);
            Assert.AreEqual(4, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 9, 9, 9, 9 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the first occurrence of the sequence occurrence past the length of the destination,
        /// the source should be copied as much as possible.
        /// </summary>
        [TestMethod]
        public void TestReplaceCopy_SequenceFartherThanDestinationLength_StopsPrematurely()
        {
            var source = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5, 6 });
            var sequence = TestHelper.WrapReadOnly(new List<int>() { 4, 5, 6 });
            var replacement = TestHelper.WrapReadOnly(new List<int>() { 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;

            var result = source.Replace(sequence, replacement, comparison).CopyTo(destination);
            Assert.AreEqual(3, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
