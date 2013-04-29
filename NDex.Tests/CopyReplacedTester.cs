using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;
using System.Linq;

namespace NDex.Test
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
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next(-49, 50)), list.ToSublist());

            var destination = new List<int>(100);
            Sublist.Add(Enumerable.Repeat(0, 100), destination.ToSublist());

            int result = Sublist.CopyReplaced(list.ToSublist(), destination.ToSublist(), i => i < 0, i => -i);
            Assert.AreEqual(destination.Count, result, "The wrong index was returned.");

            Assert.IsTrue(Sublist.TrueForAll(destination.ToSublist(), i => i >= 0), "Not all values were positive.");
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
            CopyResult result = Sublist.CopyReplaced(list, destination, predicate, replacement);
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
            CopyResult result = Sublist.CopyReplaced(list, destination, predicate, generator);
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
            CopyResult result = Sublist.CopyReplaced(list, destination, predicate, replacement);
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
            CopyResult result = Sublist.CopyReplaced(list, destination, predicate, generator);
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
            CopyResult result = Sublist.CopyReplaced(list, destination, predicate, replacement);
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
            CopyResult result = Sublist.CopyReplaced(list, destination, predicate, generator);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 3, 3, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
