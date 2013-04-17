using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the RemoveIf methods.
    /// </summary>
    [TestClass]
    public class RemoveIfTester
    {
        #region Real World Example

        /// <summary>
        /// RemoveIf is needed when you want to remove items from a list
        /// that has a fixed-size, or it is more efficient to move items to the
        /// front of a list and then remove from the back.
        /// </summary>
        [TestMethod]
        public void TestRemoveIf_RemoveEvensFromList()
        {
            Random random = new Random();

            // build a list
            var array = new List<int>(100);
            Sublist.Grow(array, 100, () => random.Next(100));

            // remove even numbers
            Sublist.RemoveIf(array.ToSublist(), i => i % 2 == 0);

            // now check that every number is odd
            Assert.IsTrue(Sublist.TrueForAll(array.ToSublist(), i => i % 2 != 0), "Even numbers were found.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemoveIf_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            Sublist.RemoveIf(list, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemoveIf_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null;
            Sublist.RemoveIf(list, predicate);
        }

        #endregion

        /// <summary>
        /// If every item in the list satisfies the predicate, then zero should be returned.
        /// </summary>
        [TestMethod]
        public void TestRemoveIf_AllSatisfy_ReturnsZero()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 6, 8 });
            Sublist.RemoveIf(list, i => i % 2 == 0);
            Assert.AreEqual(0, list.Count, "The items were not removed.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If none of the items in the list satisfy the predicate, then the count should be returned.
        /// </summary>
        [TestMethod]
        public void TestRemoveIf_NoneSatisfy_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7, 9 });
            Sublist.RemoveIf(list, i => i % 2 == 0);
            int[] expected = { 1, 3, 5, 7, 9 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items were not removed as the expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If items in the back satisfy the predicate, the returned index should reflect them not being included.
        /// </summary>
        [TestMethod]
        public void TestRemoveIf_BackSatisfies()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 4, 6 });
            Sublist.RemoveIf(list, i => i % 2 == 0);
            int[] expected = { 1, 3, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items were not removed as the expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If items in the front satisfy the predicate, the returned index should reflect them not being included.
        /// </summary>
        [TestMethod]
        public void TestRemoveIf_FrontSatisfies()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 1, 3, 5, });
            Sublist.RemoveIf(list, i => i % 2 == 0);
            int[] expected = { 1, 3, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items were not removed as the expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If items in the middle satisfy the predicate, the returned index should reflect them not being included.
        /// </summary>
        [TestMethod]
        public void TestRemoveIf_MiddleSatisfies()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            Sublist.RemoveIf(list, i => i % 2 == 0);
            int[] expected = { 1, 3, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items were not removed as the expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
