using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the SelectionSort methods.
    /// </summary>
    [TestClass]
    public class SelectionSortTester
    {
        #region Real World Example

        /// <summary>
        /// Use SelectionSort as an alternative to BubbleSort.
        /// </summary>
        [TestMethod]
        public void TestSelectionSort_SortRandomList()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.AddGenerated(list.ToSublist(), 100, i => random.Next(100));

            // sort the list
            Sublist.SelectionSort(list.ToSublist());

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
        public void TestSelectionSort_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.SelectionSort(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSelectionSort_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.SelectionSort(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSelectionSort_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.SelectionSort(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSelectionSort_NullComparer_Throws()
        {
            var list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            Sublist.SelectionSort(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSelectionSort_NullComparison_Throws()
        {
            var list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            Sublist.SelectionSort(list, comparison);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestSelectionSort_EmptyList()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.SelectionSort(list);
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// SelectionSort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestSelectionSort_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.AddGenerated(list, 200, i => 199 - i);
            Sublist.SelectionSort(list, Comparer<int>.Default);
            bool result = Sublist.IsSorted(list, Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// SelectionSort should work against a list whose values ascend and then descend.
        /// </summary>
        [TestMethod]
        public void TestSelectionSort_PipeOrganed()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.AddGenerated(list, 100, i => i * 2);
            list = Sublist.AddGenerated(list, 200, i => 199 - (i - 100) * 2);
            Sublist.SelectionSort(list, Comparer<int>.Default.Compare);
            bool result = Sublist.IsSorted(list, Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// SelectionSort should work against a list whose values jump between small and large.
        /// </summary>
        [TestMethod]
        public void TestSelectionSort_Interweaved()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.AddGenerated(list, 200, i => i % 2 == 0 ? i : 199 - (i - 1));
            Sublist.SelectionSort(list);
            bool result = Sublist.IsSorted(list);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// SelectionSort should work against a list whose values are sorted except the last value.
        /// </summary>
        [TestMethod]
        public void TestSelectionSort_LastMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.AddGenerated(list, 200, i => i + 1);
            list = Sublist.Add(new int[] { 0 }, list);
            Sublist.SelectionSort(list);
            bool result = Sublist.IsSorted(list);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// SelectionSort should work against a list whose values are sorted except the first value.
        /// </summary>
        [TestMethod]
        public void TestSelectionSort_FirstMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Add(new int[] { 200 }, list);
            list = Sublist.AddGenerated(list, 201, i => i - 1);
            Sublist.SelectionSort(list);
            bool result = Sublist.IsSorted(list);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
