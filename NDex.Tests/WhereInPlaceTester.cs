using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the WhereInPlace methods.
    /// </summary>
    [TestClass]
    public class WhereInPlaceTester
    {
        #region Real World Example

        /// <summary>
        /// WhereInPlace is needed when you want to remove items from a list
        /// that has a fixed-size, or it is more efficient to move items to the
        /// front of a list and then remove from the back.
        /// </summary>
        [TestMethod]
        public void TestWhereInPlace_RemoveEvensFromList()
        {
            Random random = new Random();

            // build a list
            var array = Enumerable.Range(0, 100).Select(i => random.Next(100)).ToArray();

            // overwrite odd numbers
            int index = array.ToSublist().Where(i => i % 2 == 0).InPlace();

            // now check that every number is even
            var evens = array.ToSublist(0, index);
            Assert.IsFalse(evens.Find(i => i % 2 != 0), "Odds numbers were found.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWhereInPlace_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            list.Where(predicate);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWhereInPlace_NullPredicate_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, bool> predicate = null;
            list.Where(predicate);
        }

        #endregion

        /// <summary>
        /// If every item in the list satisfies the predicate, then zero should be returned.
        /// </summary>
        [TestMethod]
        public void TestWhereInPlace_AllSatisfy_ReturnsZero()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 6, 8 });
            int index = list.Where(i => i % 2 == 0).InPlace();
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If none of the items in the list satisfy the predicate, then the count should be returned.
        /// </summary>
        [TestMethod]
        public void TestWhereInPlace_NoneSatisfy_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7, 9 });
            int index = list.Where(i => i % 2 == 0).InPlace();
            Assert.AreEqual(0, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If items in the back satisfy the predicate, the returned index should reflect them not being included.
        /// </summary>
        [TestMethod]
        public void TestWhereInPlace_BackSatisfies()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 4, 6 });
            int index = list.Where(i => i % 2 == 0).InPlace();
            Assert.AreEqual(2, index, "The wrong index was returned.");
            int[] expected = { 4, 6 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.Nest(0, index)), "The items were not where they were expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If items in the front satisfy the predicate, the returned index should reflect them not being included.
        /// </summary>
        [TestMethod]
        public void TestWhereInPlace_FrontSatisfies()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 1, 3, 5, });
            int index = list.Where(i => i % 2 == 0).InPlace();
            Assert.AreEqual(2, index, "The wrong index was returned.");
            int[] expected = { 2, 4 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.Nest(0, index)), "The items were not where they were expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If items in the middle satisfy the predicate, the returned index should reflect them not being included.
        /// </summary>
        [TestMethod]
        public void TestWhereInPlace_MiddleSatisfies()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            int index = list.Where(i => i % 2 == 0).InPlace();
            Assert.AreEqual(2, index, "The wrong index was returned.");
            int[] expected = { 2, 4 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.Nest(0, index)), "The items were not where they were expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
