using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the MakeHeap methods.
    /// </summary>
    [TestClass]
    public class MakeHeapTester
    {
        // See HeapTester for an example

        #region Argument Checker

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeap_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.MakeHeap(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeap_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.MakeHeap(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeap_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.MakeHeap(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeap_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            Sublist.MakeHeap(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeap_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.MakeHeap(list, comparison);
        }

        #endregion

        /// <summary>
        /// We should be able to heapify an empty list, which should do nothing.
        /// </summary>
        [TestMethod]
        public void TestMakeHeap_ListEmpty_IsHeap()
        {
            var list = TestHelper.Wrap(new List<int>());

            Sublist.MakeHeap(list);

            Assert.IsTrue(Sublist.IsHeap(list), "An empty list should be a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to heapify a list with a single item, which should do nothing.
        /// </summary>
        [TestMethod]
        public void TestMakeHeap_WithOneItem_IsHeap()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });

            Sublist.MakeHeap(list);

            Assert.IsTrue(Sublist.IsHeap(list), "An empty list should be a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to heapify a list with two items, which should move the larger element to the front of the list.
        /// </summary>
        [TestMethod]
        public void TestMakeHeap_WithTwoItems_IsHeap()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2 });

            Sublist.MakeHeap(list);

            Assert.AreEqual(2, list[0], "The largest element was not first.");
            Assert.IsTrue(Sublist.IsHeap(list), "An empty list should be a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to reverse the comparison delegate to create a min heap.
        /// </summary>
        [TestMethod]
        public void TestMakeHeap_Reversed_CreatesMinHeap()
        {
            var list = TestHelper.Wrap(new List<int>());
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);

            Sublist.Add(Enumerable.Range(0, 100).Select(i => 99 - i), list); // largest to smallest
            Sublist.MakeHeap(list, comparison); // smallest to largest

            Assert.AreEqual(0, list[0], "The largest element was not first.");
            Assert.IsTrue(Sublist.IsHeap(list, comparison), "An empty list should be a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Regardless of the number of items, heapify should work.
        /// </summary>
        [TestMethod]
        public void TestMakeHeap_OddNumbered_CreatesHeap()
        {
            var list = TestHelper.Wrap(new List<int>());

            Sublist.Add(Enumerable.Range(0, 99), list);
            Sublist.MakeHeap(list, Comparer<int>.Default);

            Assert.AreEqual(98, list[0], "The largest element was not first.");
            Assert.IsTrue(Sublist.IsHeap(list, Comparer<int>.Default), "An empty list should be a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
