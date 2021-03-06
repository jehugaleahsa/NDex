﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the Mismatch methods.
    /// </summary>
    [TestClass]
    public class MismatchTester
    {
        #region Real World Example

        /// <summary>
        /// We will use Mismatch to find where two lists are different.
        /// </summary>
        [TestMethod]
        public void TestMismatch_FindWhereListsDoNotMatch()
        {
            Random random = new Random();

            // build two, small lists
            var list1 = new List<int>(5);
            Sublist.Generate(5, i => random.Next(5)).AddTo(list1.ToSublist());
            var list2 = new List<int>(5);
            Sublist.Generate(5, i => random.Next(5)).AddTo(list2.ToSublist());

            // now find the differences
            int index = list1.ToSublist().Mismatch(list2.ToSublist());
            Assert.IsTrue(index == list1.Count || !EqualityComparer<int>.Default.Equals(list1[index], list2[index]),
                "The wrong index was returned.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMismatch_NullList1_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            list1.Mismatch(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMismatch_WithComparer_NullList1_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list1.Mismatch(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMismatch_WithComparison_NullList1_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list1.Mismatch(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMismatch_NullList2_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            list1.Mismatch(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMismatch_WithComparer_NullList2_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list1.Mismatch(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMismatch_WithComparison_NullList2_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list1.Mismatch(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMismatch_NullComparer_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = null;
            list1.Mismatch(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMismatch_NullComparison_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, bool> comparison = null;
            list1.Mismatch(list2, comparison);
        }

        #endregion

        /// <summary>
        /// If the lists are both empty, the returned index should be past the end of the lists.
        /// </summary>
        [TestMethod]
        public void TestMismatch_EmptyLists_ReturnsCount()
        {
            var list1 = TestHelper.Wrap(new List<int>());
            var list2 = TestHelper.Wrap(new List<int>());
            int index = list1.Mismatch(list2);
            Assert.AreEqual(list1.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the lists are equal, the index will be beyond the end of both.
        /// </summary>
        [TestMethod]
        public void TestMismatch_EqualList_ReturnsCount()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            int index = list1.Mismatch(list2);
            Assert.AreEqual(list1.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the lists are equal up to a point, the index will be beyond the end of the smaller list.
        /// </summary>
        [TestMethod]
        public void TestMismatch_FirstListSmaller_ReturnsFirstListsCount()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            int index = list1.Mismatch(list2);
            Assert.AreEqual(list1.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the lists are equal up to a point, the index will be beyond the end of the smaller list.
        /// </summary>
        [TestMethod]
        public void TestMismatch_SecondListSmaller_ReturnsSecondListsCount()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int index = list1.Mismatch(list2);
            Assert.AreEqual(list2.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the first item is a mismatch, zero should be returned.
        /// </summary>
        [TestMethod]
        public void TestMismatch_FirstItemMismatch_ReturnsZero()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int index = list1.Mismatch(list2, EqualityComparer<int>.Default);
            Assert.AreEqual(0, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the last item is a mismatch, the last index should be returned.
        /// </summary>
        [TestMethod]
        public void TestMismatch_LastItemMismatch_ReturnsIndex()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 4 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int index = list1.Mismatch(list2, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(2, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the last item is a mismatch, the last index should be returned.
        /// </summary>
        [TestMethod]
        public void TestMismatch_MiddleItemMismatch_ReturnsIndex()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 3, 4 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int index = list1.Mismatch(list2);
            Assert.AreEqual(1, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }
    }
}
