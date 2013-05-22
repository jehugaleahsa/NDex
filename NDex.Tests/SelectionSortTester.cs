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
            Sublist.Generate(100, i => random.Next(100)).AddTo(list.ToSublist());

            // sort the list
            list.ToSublist().SelectionSort();

            bool isSorted = list.ToSublist().IsSorted();
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
            list.SelectionSort();
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
            list.SelectionSort(comparer);
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
            list.SelectionSort(comparison);
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
            list.SelectionSort(comparer);
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
            list.SelectionSort(comparison);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestSelectionSort_EmptyList()
        {
            var list = TestHelper.Wrap(new List<int>());
            list.SelectionSort();
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// SelectionSort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestSelectionSort_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(200, i => 199 - i).AddTo(list);
            list.SelectionSort(Comparer<int>.Default);
            bool result = list.IsSorted(Comparer<int>.Default);
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
            list = Sublist.Generate(100, i => i * 2).AddTo(list);
            list = Sublist.Generate(200, i => 199 - (i - 100) * 2).AddTo(list);
            list.SelectionSort(Comparer<int>.Default.Compare);
            bool result = list.IsSorted(Comparer<int>.Default.Compare);
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
            list = Sublist.Generate(200, i => i % 2 == 0 ? i : 199 - (i - 1)).AddTo(list);
            list.SelectionSort();
            bool result = list.IsSorted();
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
            list = Sublist.Generate(200, i => i + 1).AddTo(list);
            list = new int[] { 0 }.AddTo(list);
            list.SelectionSort();
            bool result = list.IsSorted();
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
            list = new int[] { 200 }.AddTo(list);
            list = Sublist.Generate(201, i => i - 1).AddTo(list);
            list.SelectionSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
