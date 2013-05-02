using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    /// <summary>
    /// Tests the IsPartitionedUntil methods.
    /// </summary>
    [TestClass]
    public class IsPartitionedUntilTester
    {
        #region Real World Example

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestIsPartitionedUntil_TestSomething()
        {
            
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsPartitionedUntil_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            Sublist.IsPartitionedUntil(list, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsPartitionedUntil_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null;
            Sublist.IsPartitionedUntil(list, predicate);
        }

        #endregion

        /// <summary>
        /// The length of a list should be returned if it is empty.
        /// </summary>
        [TestMethod]
        public void TestIsPartitionedUntil_EmptyList_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>());
            Func<int, bool> predicate = i => true;
            int index = Sublist.IsPartitionedUntil(list, predicate);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with one item is partitioned until the end.
        /// </summary>
        [TestMethod]
        public void TestIsPartitionedUntil_SizeOfOne_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            Func<int, bool> predicate = i => true;
            int index = Sublist.IsPartitionedUntil(list, predicate);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with two items, one satisfying the condition and the other not,
        /// should be partitioned to the end.
        /// </summary>
        [TestMethod]
        public void TestIsPartitionedUntil_FirstSatisfiesSecondDoesNot_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 1 });
            Func<int, bool> predicate = i => i % 2 == 0; // is it even?
            int index = Sublist.IsPartitionedUntil(list, predicate);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with one item, failing the condition, is still partitioned.
        /// </summary>
        [TestMethod]
        public void TestIsPartitionedUntil_FirstFails_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            Func<int, bool> predicate = i => i % 2 == 0; // is it even?
            int index = Sublist.IsPartitionedUntil(list, predicate);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we have two items and the first fails the condition, but the second satisfies, 
        /// the index of the last item should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsPartitionedUntil_FirstFailsSecondSucceeds_ReturnsLastIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2 });
            Func<int, bool> predicate = i => i % 2 == 0; // is it even?
            int index = Sublist.IsPartitionedUntil(list, predicate);
            Assert.AreEqual(1, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
