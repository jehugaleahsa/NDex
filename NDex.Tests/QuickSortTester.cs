using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the QuickSort methods.
    /// </summary>
    [TestClass]
    public class QuickSortTester
    {
        #region Real World Example

        /// <summary>
        /// QuickSort is useful when a fast sort is needed.
        /// </summary>
        [TestMethod]
        public void TestQuickSort_SortRandomList()
        {
            Random random = new Random();

            // build a list
            int size = random.Next(1000, 10000); // between 1,000 and 10,000 items
            var list = new List<int>(size);
            Sublist.Add(Enumerable.Range(0, size).Select(i => random.Next(size)), list.ToSublist());

            // sort the list
            Sublist.QuickSort(list.ToSublist());

            bool isSorted = Sublist.IsSorted(list.ToSublist());
            Assert.IsTrue(isSorted, "The items were not sorted.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestQuickSort_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.QuickSort(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestQuickSort_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.QuickSort(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestQuickSort_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.QuickSort(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestQuickSort_NullComparer_Throws()
        {
            var list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            Sublist.QuickSort(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestQuickSort_NullComparison_Throws()
        {
            var list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            Sublist.QuickSort(list, comparison);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestQuickSort_EmptyList()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.QuickSort(list);
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// QuickSort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestQuickSort_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Add(Enumerable.Range(0, 200).Select(i => 199 - i), list);
            Sublist.QuickSort(list, Comparer<int>.Default);
            bool result = Sublist.IsSorted(list, Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// QuickSort should work against a list whose values ascend and then descend.
        /// </summary>
        [TestMethod]
        public void TestQuickSort_PipeOrganed()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Add(Enumerable.Range(0, 100).Select(i => i * 2), list);
            list = Sublist.Add(Enumerable.Range(0, 200).Select(i => 199 - (i - 100) * 2), list);
            Sublist.QuickSort(list, Comparer<int>.Default.Compare);
            bool result = Sublist.IsSorted(list, Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// QuickSort should work against a list whose values jump between small and large.
        /// </summary>
        [TestMethod]
        public void TestQuickSort_Interweaved()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Add(Enumerable.Range(0, 200).Select(i => i % 2 == 0 ? i : 199 - (i - 1)), list);
            Sublist.QuickSort(list);
            bool result = Sublist.IsSorted(list);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// QuickSort should work against a list whose values are sorted except the last value.
        /// </summary>
        [TestMethod]
        public void TestQuickSort_LastMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Add(Enumerable.Range(0, 200).Select(i => i + 1), list);
            list = Sublist.Add(new int[] { 0 }, list);
            Sublist.QuickSort(list);
            bool result = Sublist.IsSorted(list);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// QuickSort should work against a list whose values are sorted except the first value.
        /// </summary>
        [TestMethod]
        public void TestQuickSort_FirstMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Add(new int[] { 200 }, list);
            list = Sublist.Add(Enumerable.Range(0, 201).Select(i => i - 1), list);
            Sublist.QuickSort(list);
            bool result = Sublist.IsSorted(list);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
