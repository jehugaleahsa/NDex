using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the BubbleSort methods.
    /// </summary>
    [TestClass]
    public class BubbleSortTester
    {
        #region Real World Example

        /// <summary>
        /// Bubble sort is the best algorithm to use when the list size is smaller than 10.
        /// </summary>
        [TestMethod]
        public void TestBubbleSort_Default_RandomizedList()
        {
            Random random = new Random();

            // build the list
            List<int> list = new List<int>();
            Sublist.Generate(10, i => random.Next(10)).AddTo(list.ToSublist());

            // sort the list
            list.ToSublist().BubbleSort();

            // verify it is sorted
            bool result = list.ToSublist().IsSorted();
            Assert.IsTrue(result, "The list was not sorted correctly.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBubbleSort_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            list.BubbleSort();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBubbleSort_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.BubbleSort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBubbleSort_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.BubbleSort(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBubbleSort_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            list.BubbleSort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBubbleSort_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            list.BubbleSort(comparison);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestBubbleSort_EmptyList()
        {
            var list = TestHelper.Wrap(new List<int>());
            list.BubbleSort(Comparer<int>.Default.Compare);
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// BubbleSort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestBubbleSort_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>() { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 });
            list.BubbleSort(Comparer<int>.Default);
            bool result = list.IsSorted(Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// BubbleSort should work against a list whose values ascend and then descend.
        /// </summary>
        [TestMethod]
        public void TestBubbleSort_PipeOrganed()
        {
            var list = TestHelper.Wrap(new List<int>() { 0, 2, 4, 6, 8, 9, 7, 5, 3, 1 });
            list.BubbleSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// BubbleSort should work against a list whose values jump between small and large.
        /// </summary>
        [TestMethod]
        public void TestBubbleSort_Interweaved()
        {
            var list = TestHelper.Wrap(new List<int>() { 0, 9, 1, 8, 2, 7, 3, 6, 4, 5 });
            list.BubbleSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// BubbleSort should work against a list whose values are sorted except the last value.
        /// </summary>
        [TestMethod]
        public void TestBubbleSort_LastMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
            list.BubbleSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// BubbleSort should work against a list whose values are sorted except the first value.
        /// </summary>
        [TestMethod]
        public void TestBubbleSort_FirstMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 9, 0, 1, 2, 3, 4, 5, 6, 7, 8 });
            list.BubbleSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
