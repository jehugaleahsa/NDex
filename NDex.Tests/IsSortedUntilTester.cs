using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the IsSortedUntil methods.
    /// </summary>
    [TestClass]
    public class IsSortedUntilTester
    {
        #region Real World Example

        /// <summary>
        /// We'll use IsSortedUntil to break a list into sorted ranges, then we'll merge the values to sort the entire list.
        /// </summary>
        [TestMethod]
        public void TestIsSortedUntil_BreakListIntoSortedRanges_ThenMerge()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Grow(list, 100, () => random.Next(0, 100));

            // find all of the sorted ranges
            int index = 0;
            var ranges = new List<IMutableSublist<List<int>, int>>();
            while (index != list.Count)
            {
                var remaining = list.ToSublist(index);
                int nextIndex = Sublist.IsSortedUntil(remaining);
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
                    int count = Sublist.CopyMerged(firstRange, nextRange, buffer.ToSublist()); // merge into buffer
                    Sublist.Copy(buffer.ToSublist(0, count), list.ToSublist()); // move back to original list, sorted
                }
            }
            Assert.IsTrue(Sublist.IsSorted(list.ToSublist()), "The list was not sorted.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSortedUntil_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.IsSortedUntil(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSortedUntil_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.IsSortedUntil(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSortedUntil_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.IsSortedUntil(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSortedUntil_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            Sublist.IsSortedUntil(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSortedUntil_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.IsSortedUntil(list, comparison);
        }

        #endregion

        /// <summary>
        /// An empty list is sorted.
        /// </summary>
        [TestMethod]
        public void TestIsSortedUntil_EmptyList_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>());
            int index = Sublist.IsSortedUntil(list);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with one item is sorted.
        /// </summary>
        [TestMethod]
        public void TestIsSortedUntil_ListOfOne_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            int index = Sublist.IsSortedUntil(list);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A reversed list is sorted, with the correct comparer.
        /// </summary>
        [TestMethod]
        public void TestIsSortedUntil_Reversed_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 4, 3, 2, 1 });
            int index = Sublist.IsSortedUntil(list, (x, y) => Comparer<int>.Default.Compare(y, x));
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// An unsorted list should cause the method to return false.
        /// </summary>
        [TestMethod]
        public void TestIsSortedUntil_Unsorted()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 4, 2 }); // 4 is the problem, not 5!
            int index = Sublist.IsSortedUntil(list, Comparer<int>.Default);
            Assert.AreEqual(3, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
