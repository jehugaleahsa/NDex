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
            Sublist.AddGenerated(list.ToSublist(), 100, i => random.Next(100));

            // partition the odds to the end
            Sublist.Partition(list.ToSublist(), i => i % 2 == 0); // odds to the end

            bool isPartitioned = Sublist.IsPartitioned(list.ToSublist(), i => i % 2 == 0);
            Assert.IsTrue(isPartitioned, "The list was not partitioned.");

            // force a bad partition
            list[list.Count - 1] = 0;
            isPartitioned = Sublist.IsPartitioned(list.ToSublist(), i => i % 2 == 0);
            Assert.IsFalse(isPartitioned, "The list should not have been partitioned.");
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
        /// If every item in the list satisfies the predicate, then the list is partitioned.
        /// </summary>
        [TestMethod]
        public void TestIsPartitioned_AllMatch_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, });
            Func<int, bool> predicate = i => true;

            bool isPartitioned = Sublist.IsPartitioned(list, predicate);
            Assert.IsTrue(isPartitioned, "The list should have been partitioned.");
        }

        /// <summary>
        /// If no item in the list satisfies the predicate, then the list is partitioned.
        /// </summary>
        [TestMethod]
        public void TestIsPartitioned_NoMatches_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, });
            Func<int, bool> predicate = i => false;

            bool isPartitioned = Sublist.IsPartitioned(list, predicate);
            Assert.IsTrue(isPartitioned, "The list should have been partitioned.");
        }

        /// <summary>
        /// If the list is partitioned, then the method should return true.
        /// </summary>
        [TestMethod]
        public void TestIsPartitioned_Partitioned_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 0, 2, 1, 3 });
            Func<int, bool> predicate = i => i % 2 == 0;

            bool isPartitioned = Sublist.IsPartitioned(list, predicate);
            Assert.IsTrue(isPartitioned, "The list should have been partitioned.");
        }

        /// <summary>
        /// If an item satisfying the predicate is found after one that doesn't, the list is not partitioned.
        /// </summary>
        [TestMethod]
        public void TestIsPartitioned_NotPartitioned_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 0, 2, 1, 4 });
            Func<int, bool> predicate = i => i % 2 == 0;

            bool isPartitioned = Sublist.IsPartitioned(list, predicate);
            Assert.IsFalse(isPartitioned, "The list should not have been partitioned.");
        }
    }
}
