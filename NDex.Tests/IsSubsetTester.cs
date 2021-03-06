﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the IsSubset methods.
    /// </summary>
    [TestClass]
    public class IsSubsetTester
    {
        #region Real World Example

        /// <summary>
        /// We should be able to verify that multiples of four are also multiples of two.
        /// </summary>
        [TestMethod]
        public void TestIsSubset()
        {
            Random random = new Random();

            // build all evens
            var evens = new List<int>();
            Sublist.Generate(200, i => i * 2).AddTo(evens.ToSublist());

            // build multiples of four
            var fours = new List<int>();
            Sublist.Generate(100, i => random.Next(100) * 4).AddTo(fours.ToSublist());
            fours.ToSublist().Sort().InPlace(); // items must be sorted
            int garbageIndex = fours.ToSublist().Distinct().InPlace();
            var garbage = fours.ToSublist(garbageIndex);
            garbage.Clear(); // items must be distinct.

            // there shouldn't be a multiple of four that isn't also a multiple of two
            var result = fours.ToSublist().IsSubset(evens.ToSublist());
            Assert.IsTrue(result.Success, "Found a multiple of four that wasn't a multiple of two.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_NullList1_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            list1.IsSubset(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_WithComparer_NullList1_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IComparer<int> comparer = Comparer<int>.Default;
            list1.IsSubset(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_WithComparison_NullList1_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.IsSubset(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_NullList2_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            list1.IsSubset(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_WithComparer_NullList2_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.IsSubset(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_WithComparison_NullList2_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.IsSubset(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_NullComparer_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list1.IsSubset(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_NullComparison_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list1.IsSubset(list2, comparison);
        }

        #endregion

        /// <summary>
        /// If the second list is empty, then all of its values are in the first list.
        /// </summary>
        [TestMethod]
        public void TestIsSubset_List2Empty_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>());
            var result = list2.IsSubset(list1, Comparer<int>.Default.Compare);
            Assert.AreEqual(list2.Count, result.Index, "An empty list was not a subset of another.");
            Assert.IsTrue(result.Success, "An empty list should always be a valid subset.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the second list is empty, then all of its values are in the first list, even if it is empty.
        /// </summary>
        [TestMethod]
        public void TestIsSubset_BothListsEmpty_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>());
            var list2 = TestHelper.Wrap(new List<int>());
            var result = list2.IsSubset(list1, Comparer<int>.Default.Compare);
            Assert.AreEqual(list2.Count, result.Index, "An empty list was not a subset of another.");
            Assert.IsTrue(result.Success, "An empty list should always be a valid subset.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but the first item is present.
        /// </summary>
        [TestMethod]
        public void TestIsSubset_MissingFirst_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var result = list2.IsSubset(list1, Comparer<int>.Default);
            Assert.AreEqual(0, result.Index, "Not all items were in the parent, but still succeeded.");
            Assert.IsFalse(result.Success, "The list should not have been a valid subset.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but the last item is present.
        /// </summary>
        [TestMethod]
        public void TestIsSubset_MissingLast_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var result = list2.IsSubset(list1, Comparer<int>.Default.Compare);
            Assert.AreEqual(2, result.Index, "Not all items were in the parent, but still succeeded.");
            Assert.IsFalse(result.Success, "The list should not have been a valid subset.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but a middle item is present.
        /// </summary>
        [TestMethod]
        public void TestIsSubset_MissingMiddle_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var result = list2.IsSubset(list1, Comparer<int>.Default);
            Assert.AreEqual(1, result.Index, "Not all items were in the parent, but still succeeded.");
            Assert.IsFalse(result.Success, "The list should not have been a valid subset.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// The subset can be intermingled within the set.
        /// </summary>
        [TestMethod]
        public void TestIsSubset_List2Intermingled_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4, 5, 6 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            var result = list2.IsSubset(list1, Comparer<int>.Default);
            Assert.AreEqual(list2.Count, result.Index, "Not all items were in the parent, but still succeeded.");
            Assert.IsTrue(result.Success, "The list should have been a valid subset.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }
    }
}
