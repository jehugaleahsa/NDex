using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the CopyReplaced methods.
    /// </summary>
    [TestClass]
    public class CopyReplacedTester
    {
        #region Real World Example

        /// <summary>
        /// If you had a list of numbers, and wanted to make sure that they were all positive,
        /// you could replace the negatives with their absolute values.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_AbsoluteValue()
        {
            Random random = new Random();

            // build a list of numbers
            var list = new List<int>(100);
            Sublist.AddGenerated(list.ToSublist(), 100, i => random.Next(-49, 50));

            var destination = new List<int>(100);
            Sublist.AddGenerated(destination.ToSublist(), 100, 0);

            int result = Sublist.CopyReplaced(list.ToSublist(), destination.ToSublist(), i => i < 0, i => -i);
            Assert.AreEqual(destination.Count, result, "The wrong index was returned.");

            Assert.IsTrue(Sublist.TrueForAll(destination.ToSublist(), i => i >= 0), "Not all values were positive.");
        }

        /// <summary>
        /// If you want to replace a sequence of values, you can specify the old sequence and a 
        /// replacement sequence.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_ReplaceMisspelledWords()
        {
            string source = "This mesage has mispelled wordz.";
            List<char> destination = new List<char>();
            Sublist.AddGenerated(destination.ToSublist(), source.Length + 2, '\0');
            const string mesage = "mesage";
            const string message = "message";

            var result = Sublist.CopyReplaced(source.ToSubstring(), mesage.ToSubstring(), message.ToSubstring(), destination.ToSublist());
            source = new String(destination.ToSublist(0, result.DestinationOffset).ToArray());

            const string mispelled = "mispelled";
            const string misspelled = "misspelled";

            result = Sublist.CopyReplaced(source.ToSubstring(), mispelled.ToSubstring(), misspelled.ToSubstring(), destination.ToSublist());
            source = new String(destination.ToSublist(0, result.DestinationOffset).ToArray());

            const string wordz = "wordz";
            const string words = "words";

            result = Sublist.CopyReplaced(source.ToSubstring(), wordz.ToSubstring(), words.ToSubstring(), destination.ToSublist());
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
        public void TestCopyReplaced_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            int replacement = 0;
            Func<int, bool> predicate = i => true;
            Sublist.CopyReplaced(list, destination, predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithGenerator_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => true;
            Sublist.CopyReplaced(list, destination, predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            int replacement = 0;
            Func<int, bool> predicate = i => true;
            Sublist.CopyReplaced(list, destination, predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithGenerator_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => true;
            Sublist.CopyReplaced(list, destination, predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            int replacement = 0;
            Func<int, bool> predicate = null;
            Sublist.CopyReplaced(list, destination, predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithGenerator_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = null;
            Sublist.CopyReplaced(list, destination, predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_NullGenerator_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int> generator = null;
            Func<int, bool> predicate = i => true;
            Sublist.CopyReplaced(list, destination, predicate, generator);
        }

        /// <summary>
        /// If we try to pass a null source, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_SourceNull_Throws()
        {
            Sublist<List<int>, int> source = null;
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyReplaced(source, sequence, replacement, destination);
        }

        /// <summary>
        /// If we try to pass a null sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_SequenceNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = null;
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyReplaced(source, sequence, replacement, destination);
        }

        /// <summary>
        /// If we try to pass an empty sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCopyReplaced_SequenceEmpty_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>();
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyReplaced(source, sequence, replacement, destination);
        }

        /// <summary>
        /// If we try to pass a null replacement, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_ReplacementNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyReplaced(source, sequence, replacement, destination);
        }

        /// <summary>
        /// If we try to pass a null destination, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_DestinationNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.CopyReplaced(source, sequence, replacement, destination);
        }

        /// <summary>
        /// If we try to pass a null source, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithComparer_SourceNull_Throws()
        {
            Sublist<List<int>, int> source = null;
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.CopyReplaced(source, sequence, replacement, destination, comparer);
        }

        /// <summary>
        /// If we try to pass a null sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithComparer_SequenceNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = null;
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.CopyReplaced(source, sequence, replacement, destination, comparer);
        }

        /// <summary>
        /// If we try to pass an empty sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCopyReplaced_WithComparer_SequenceEmpty_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>();
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.CopyReplaced(source, sequence, replacement, destination, comparer);
        }

        /// <summary>
        /// If we try to pass a null replacement, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithComparer_ReplacementNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = null;
            Sublist<List<int>, int> destination = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.CopyReplaced(source, sequence, replacement, destination, comparer);
        }

        /// <summary>
        /// If we try to pass a null destination, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithComparer_DestinationNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.CopyReplaced(source, sequence, replacement, destination, comparer);
        }

        /// <summary>
        /// If we try to pass a null comparer, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithComparer_ComparerNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.CopyReplaced(source, sequence, replacement, destination, comparer);
        }

        /// <summary>
        /// If we try to pass a null source, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithComparison_SourceNull_Throws()
        {
            Sublist<List<int>, int> source = null;
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.CopyReplaced(source, sequence, replacement, destination, comparison);
        }

        /// <summary>
        /// If we try to pass a null sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithComparison_SequenceNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = null;
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.CopyReplaced(source, sequence, replacement, destination, comparison);
        }

        /// <summary>
        /// If we try to pass an empty sequence, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCopyReplaced_WithComparison_SequenceEmpty_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>();
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.CopyReplaced(source, sequence, replacement, destination, comparison);
        }

        /// <summary>
        /// If we try to pass a null replacement, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithComparison_ReplacementNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.CopyReplaced(source, sequence, replacement, destination, comparison);
        }

        /// <summary>
        /// If we try to pass a null destination, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithComparison_DestinationNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.CopyReplaced(source, sequence, replacement, destination, comparison);
        }

        /// <summary>
        /// If we try to pass a null comparer, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReplaced_WithComparison_ComparerNull_Throws()
        {
            Sublist<List<int>, int> source = new List<int>();
            Sublist<List<int>, int> sequence = new List<int>() { 1 };
            Sublist<List<int>, int> replacement = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.CopyReplaced(source, sequence, replacement, destination, comparison);
        }

        #endregion

        /// <summary>
        /// If the destination is too small to hold all of the values, then the algorithm stops prematurely.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_DestinationTooSmall_StopsPrematurely()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            int replacement = 0;
            Func<int, bool> predicate = i => false;
            var result = Sublist.CopyReplaced(list, destination, predicate, replacement);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is too small to hold all of the values, then the algorithm stops prematurely.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_WithGenerator_DestinationTooSmall_StopsPrematurely()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => false;
            var result = Sublist.CopyReplaced(list, destination, predicate, generator);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is larger than the source, then there should be space left over at the end.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_SourceSmaller_SpaceLeftOver()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            int replacement = 0;
            Func<int, bool> predicate = i => false;
            var result = Sublist.CopyReplaced(list, destination, predicate, replacement);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offet was wrong.");
            int[] expected = { 1, 2, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is larger than the source, then there should be space left over at the end.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_WithGenerator_SourceSmaller_SpaceLeftOver()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => false;
            var result = Sublist.CopyReplaced(list, destination, predicate, generator);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the predicate is satified by an element, it should be replaced.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_ItemsMatchingPredicateAreReplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            int replacement = 3;
            Func<int, bool> predicate = i => i % 2 == 0;
            var result = Sublist.CopyReplaced(list, destination, predicate, replacement);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 3, 3, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the predicate is satified by an element, it should be replaced.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_WithGenerator_ItemsMatchingPredicateAreReplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            Func<int, int> generator = i => i + 1; // make odd by adding one
            Func<int, bool> predicate = i => i % 2 == 0;
            var result = Sublist.CopyReplaced(list, destination, predicate, generator);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 3, 3, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that equals the source with an empty replacement,
        /// nothing will be added.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_SequenceEqualsSource_ReplacementEmpty_CopiesNothing()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(0, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 0, 0, 0, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceInFront_ReplacementEmpty_RemovesFrontOfList()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 4, 5, 0, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceInBack_ReplacementEmpty_RemovesBackOfList()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 2, 0, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceInMiddle_ReplacementEmpty_RemovesMiddleOfList()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 5, 0, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceInMultipleLocations_ReplacementEmpty_RemovesAllOccurrences()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 4, 4, 5, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceEqualsSource_ReplacementSmaller_CopiesReplacement()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 9, 9, 0, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceInFront_ReplacementSmaller_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(4, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 9, 9, 4, 5, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceInBack_ReplacementSmaller_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(4, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 2, 9, 9, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceInMiddle_ReplacementSmaller_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(4, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 9, 9, 5, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceInMultipleLocations_ReplacementSmaller_ReplacesAllOccurrences()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 9, 9, 5, 9, 9, 5, 2, 3, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceEqualsSource_ReplacementLarger_CopiesReplacement()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            Assert.IsTrue(Sublist.AreEqual(replacement, destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceInFront_ReplacementLarger_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 9, 9, 9, 9, 4, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceInBack_ReplacementLarger_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 2, 9, 9, 9, 9 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceInMiddle_ReplacementLarger_ReplacesValues()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 9, 9, 9, 9, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceInMultipleLocations_ReplacementLarger_ReplacesAllOccurrences()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 9, 9, 9, 4, 9, 9, 9, 4, 5, 9, 9, 9, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
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
        public void TestCopyReplaced_SequenceEqualsSource_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, sequence, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            Assert.IsTrue(Sublist.AreEqual(sequence, destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that equals the front of the source that equals the replacement,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_SequenceInFront_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, sequence, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            Assert.IsTrue(Sublist.AreEqual(source, destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that equals the back of the source that equals the replacement,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_SequenceInBack_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, sequence, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            Assert.IsTrue(Sublist.AreEqual(source, destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that equals the middle of the source that equals the replacement,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_SequenceInMiddle_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, sequence, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            Assert.IsTrue(Sublist.AreEqual(source, destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we replace a sequence that appears in multiple in the source with itself,
        /// the destination should equal the source.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_SequenceInMultipleLocations_ReplacementEqualsSequence_NoChange()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 2, 3, 4, 5, 2, 3, 1 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, sequence, destination);
            Assert.AreEqual(source.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            Assert.IsTrue(Sublist.AreEqual(source, destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is too short to contain a replacement, the algorithm should stop
        /// prematurely.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_SequenceInBack_DestinationTooSmall_StopsPrematurely()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 3, 4, 5 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 2, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is too short to contain the rest of the source after a replacement, the algorithm should stop
        /// prematurely.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_SequenceInFront_DestinationTooSmall_StopsPrematurely()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination);
            Assert.AreEqual(4, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 9, 9, 9, 9, 4 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is too short to contain the rest of the source after a replacement, the algorithm should stop
        /// prematurely.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_SequenceInMiddle_DestinationTooSmall_StopsPrematurely()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var sequence = TestHelper.Wrap(new List<int>() { 2, 3, 4 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination, comparer);
            Assert.AreEqual(4, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 9, 9, 9, 9 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the first occurrence of the sequence occurrence past the length of the destination,
        /// the source should be copied as much as possible.
        /// </summary>
        [TestMethod]
        public void TestCopyReplaced_SequenceFartherThanDestinationLength_StopsPrematurely()
        {
            var source = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6 });
            var sequence = TestHelper.Wrap(new List<int>() { 4, 5, 6 });
            var replacement = TestHelper.Wrap(new List<int>() { 9, 9, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;

            var result = Sublist.CopyReplaced(source, sequence, replacement, destination, comparison);
            Assert.AreEqual(3, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = new int[] { 1, 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong values were added to the destination.");
            TestHelper.CheckHeaderAndFooter(source);
            TestHelper.CheckHeaderAndFooter(sequence);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
