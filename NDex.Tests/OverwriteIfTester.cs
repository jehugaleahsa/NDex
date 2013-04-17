using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the OverwriteIf methods.
    /// </summary>
    [TestClass]
    public class OverwriteIfTester
    {
        #region Real World Example

        /// <summary>
        /// OverwriteIf is needed when you want to remove items from a list
        /// that has a fixed-size, or it is more efficient to move items to the
        /// front of a list and then remove from the back.
        /// </summary>
        [TestMethod]
        public void TestOverwriteIf_RemoveEvensFromList()
        {
            Random random = new Random();

            // build a list
            var array = new List<int>(100);
            Sublist.Grow(array, 100, () => random.Next(100));

            // overwrite even numbers
            int index = Sublist.OverwriteIf(array.ToSublist(), i => i % 2 == 0);

            // now check that every number is odd
            var odds = array.ToSublist(0, index);
            Assert.IsTrue(Sublist.TrueForAll(odds, i => i % 2 != 0), "Even numbers were found.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverwriteIf_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            Sublist.OverwriteIf(list, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverwriteIf_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null;
            Sublist.OverwriteIf(list, predicate);
        }

        #endregion

        /// <summary>
        /// If every item in the list satisfies the predicate, then zero should be returned.
        /// </summary>
        [TestMethod]
        public void TestOverwriteIf_AllSatisfy_ReturnsZero()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 6, 8 });
            int index = Sublist.OverwriteIf(list, i => i % 2 == 0);
            Assert.AreEqual(0, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If none of the items in the list satisfy the predicate, then the count should be returned.
        /// </summary>
        [TestMethod]
        public void TestOverwriteIf_NoneSatisfy_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7, 9 });
            int index = Sublist.OverwriteIf(list, i => i % 2 == 0);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If items in the back satisfy the predicate, the returned index should reflect them not being included.
        /// </summary>
        [TestMethod]
        public void TestOverwriteIf_BackSatisfies()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 4, 6 });
            int index = Sublist.OverwriteIf(list, i => i % 2 == 0);
            Assert.AreEqual(3, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If items in the front satisfy the predicate, the returned index should reflect them not being included.
        /// </summary>
        [TestMethod]
        public void TestOverwriteIf_FrontSatisfies()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 1, 3, 5, });
            int index = Sublist.OverwriteIf(list, i => i % 2 == 0);
            Assert.AreEqual(3, index, "The wrong index was returned.");
            int[] expected = { 1, 3, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.Nest(0, index)), "The items were not where they were expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If items in the middle satisfy the predicate, the returned index should reflect them not being included.
        /// </summary>
        [TestMethod]
        public void TestOverwriteIf_MiddleSatisfies()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            int index = Sublist.OverwriteIf(list, i => i % 2 == 0);
            Assert.AreEqual(3, index, "The wrong index was returned.");
            int[] expected = { 1, 3, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.Nest(0, index)), "The items were not where they were expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
