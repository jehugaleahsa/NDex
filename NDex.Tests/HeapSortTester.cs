﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the HeapSort methods.
    /// </summary>
    [TestClass]
    public class HeapSortTester
    {
        // look at HeapTester for an example

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapSort_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            list.HeapSort();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapSort_WithComparer_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.HeapSort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapSort_WithComparison_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.HeapSort(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapSort_NullComparer_Throws()
        {
            var list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list.HeapSort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapSort_NullComparison_Throws()
        {
            var list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list.HeapSort(comparison);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestHeapSort_EmptyList()
        {
            var list = TestHelper.Wrap(new List<int>());
            list.HeapSort();
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// HeapSort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestHeapSort_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>() { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 });
            list.MakeHeap(Comparer<int>.Default).InPlace(); // can only sort a heap
            list.HeapSort(Comparer<int>.Default);
            bool result = list.IsSorted(Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// HeapSort should work against a list whose values ascend and then descend.
        /// </summary>
        [TestMethod]
        public void TestHeapSort_PipeOrganed()
        {
            var list = TestHelper.Wrap(new List<int>() { 0, 2, 4, 6, 8, 9, 7, 5, 3, 1 });
            list.MakeHeap(Comparer<int>.Default.Compare).InPlace(); // can only sort a heap
            list.HeapSort(Comparer<int>.Default.Compare);
            bool result = list.IsSorted(Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// HeapSort should work against a list whose values jump between small and large.
        /// </summary>
        [TestMethod]
        public void TestHeapSort_Interweaved()
        {
            var list = TestHelper.Wrap(new List<int>() { 0, 9, 1, 8, 2, 7, 3, 6, 4, 5 });
            list.MakeHeap().InPlace(); // can only sort a heap
            list.HeapSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// HeapSort should work against a list whose values are sorted except the last value.
        /// </summary>
        [TestMethod]
        public void TestHeapSort_LastMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
            list.MakeHeap().InPlace(); // can only sort a heap
            list.HeapSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// HeapSort should work against a list whose values are sorted except the first value.
        /// </summary>
        [TestMethod]
        public void TestHeapSort_FirstMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            list.MakeHeap().InPlace(); // can only sort a heap
            list.HeapSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
