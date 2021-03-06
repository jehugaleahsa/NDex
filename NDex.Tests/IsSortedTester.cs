﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the IsSorted methods.
    /// </summary>
    [TestClass]
    public class IsSortedTester
    {
        #region Real World Example

        /// <summary>
        /// We'll use IsSorted to break a list into sorted ranges, then we'll merge the values to sort the entire list.
        /// </summary>
        [TestMethod]
        public void TestIsSorted_BreakListIntoSortedRanges_ThenMerge()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(0, 100)).AddTo(list.ToSublist());

            // find all of the sorted ranges
            int index = 0;
            var ranges = new List<IMutableSublist<List<int>, int>>();
            while (index != list.Count)
            {
                var remaining = list.ToSublist(index);
                int nextIndex = remaining.IsSorted();
                var range = list.ToSublist(index, nextIndex);
                ranges.Add(range);
                index += nextIndex;
            }

            // now merge the ranges to sort the list
            int[] buffer = new int[list.Count];
            if (ranges.Count > 1)
            {
                for (int next = 1; next != ranges.Count; ++next)
                {
                    var nextRange = ranges[next];
                    var firstRange = list.ToSublist(0, nextRange.Offset);
                    int count = firstRange.Merge(nextRange).CopyTo(buffer.ToSublist());  // merge into buffer
                    buffer.ToSublist(0, count).CopyTo(list.ToSublist()); // move back to original list, sorted
                }
            }
            Assert.IsTrue(list.ToSublist().IsSorted(), "The list was not sorted.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSorted_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            list.IsSorted();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSorted_WithComparer_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.IsSorted(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSorted_WithComparison_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.IsSorted(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSorted_NullComparer_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list.IsSorted(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSorted_NullComparison_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list.IsSorted(comparison);
        }

        #endregion

        /// <summary>
        /// An empty list is sorted.
        /// </summary>
        [TestMethod]
        public void TestIsSorted_EmptyList_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>());
            var result = list.IsSorted();
            Assert.AreEqual(list.Count, result.Index, "The wrong index was returned.");
            Assert.IsTrue(result.Success, "An empty list should be sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with one item is sorted.
        /// </summary>
        [TestMethod]
        public void TestIsSorted_ListOfOne_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            var result = list.IsSorted();
            Assert.AreEqual(list.Count, result.Index, "The wrong index was returned.");
            Assert.IsTrue(result.Success, "A list with one item should be sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A reversed list is sorted, with the correct comparer.
        /// </summary>
        [TestMethod]
        public void TestIsSorted_Reversed_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 4, 3, 2, 1 });
            var result = list.IsSorted((x, y) => Comparer<int>.Default.Compare(y, x));
            Assert.AreEqual(list.Count, result.Index, "The wrong index was returned.");
            Assert.IsTrue(result.Success, "A list should have been sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// An unsorted list should cause the method to return false.
        /// </summary>
        [TestMethod]
        public void TestIsSorted_Unsorted()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 4, 2 }); // 4 is the problem, not 5!
            var result = list.IsSorted(Comparer<int>.Default);
            Assert.AreEqual(3, result.Index, "The wrong index was returned.");
            Assert.IsFalse(result.Success, "A list should not have been sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
