using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the IsPartitioned methods.
    /// </summary>
    [TestClass]
    public class IsPartitionedTester
    {
        #region Real World Example

        /// <summary>
        /// If we're expecting a partitioned list, it is good to check it first before using it.
        /// </summary>
        [TestMethod]
        public void TestIsPartitioned_VerifyPartitionWorks()
        {
            Random random = new Random();

            // build a list 
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list.ToSublist());

            // partition the odds to the end
            list.ToSublist().Partition(i => i % 2 == 0).InPlace();  // odds to the end

            var result = list.ToSublist().IsPartitioned(i => i % 2 == 0);
            Assert.IsTrue(result.Success, "The list was not partitioned.");
            Assert.AreEqual(list.Count, result.Index, "The wrong index was returned.");

            // force a bad partition
            list[list.Count - 1] = 0;
            result = list.ToSublist().IsPartitioned(i => i % 2 == 0);
            Assert.IsFalse(result.Success, "The list should not have been partitioned.");
            Assert.AreEqual(list.Count - 1, result.Index, "The wrong index was returned.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsPartitioned_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            Sublist.IsPartitioned(list, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsPartitioned_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null;
            Sublist.IsPartitioned(list, predicate);
        }

        #endregion

        /// <summary>
        /// The length of a list should be returned if it is empty.
        /// </summary>
        [TestMethod]
        public void TestIsPartitioned_EmptyList_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>());
            Func<int, bool> predicate = i => i % 2 == 0;
            var result = Sublist.IsPartitioned(list, predicate);
            Assert.IsTrue(result.Success, "An empty list should be partitioned.");
            Assert.AreEqual(list.Count, result.Index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with one item is partitioned until the end.
        /// </summary>
        [TestMethod]
        public void TestIsPartitioned_SizeOfOne_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            Func<int, bool> predicate = i => i % 2 == 0;
            var result = Sublist.IsPartitioned(list, predicate);
            Assert.IsTrue(result.Success, "A list with one item should be partitioned.");
            Assert.AreEqual(list.Count, result.Index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with two items, one satisfying the condition and the other not,
        /// should be partitioned to the end.
        /// </summary>
        [TestMethod]
        public void TestIsPartitioned_FirstSatisfiesSecondDoesNot_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 1 });
            Func<int, bool> predicate = i => i % 2 == 0; // is it even?
            var result = Sublist.IsPartitioned(list, predicate);
            Assert.IsTrue(result.Success, "The list should have been partitioned.");
            Assert.AreEqual(list.Count, result.Index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with one item, failing the condition, is still partitioned.
        /// </summary>
        [TestMethod]
        public void TestIsPartitioned_FirstFails_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            Func<int, bool> predicate = i => i % 2 == 0; // is it even?
            var result = Sublist.IsPartitioned(list, predicate);
            Assert.IsTrue(result.Success, "A list with one item should always be partitioned.");
            Assert.AreEqual(list.Count, result.Index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we have two items and the first fails the condition, but the second satisfies, 
        /// the index of the last item should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsPartitioned_FirstFailsSecondSucceeds_ReturnsLastIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2 });
            Func<int, bool> predicate = i => i % 2 == 0; // is it even?
            var result = Sublist.IsPartitioned(list, predicate);
            Assert.IsFalse(result.Success, "The list should not have been partitioned.");
            Assert.AreEqual(1, result.Index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
