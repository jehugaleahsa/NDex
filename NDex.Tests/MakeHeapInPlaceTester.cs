using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the MakeHeapInPlace methods.
    /// </summary>
    [TestClass]
    public class MakeHeapInPlaceTester
    {
        // See HeapTester for an example

        #region Argument Checker

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeapInPlace_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            list.MakeHeap().InPlace();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeapInPlace_WithComparer_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.MakeHeap(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeapInPlace_WithComparison_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.MakeHeap(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeapInPlace_NullComparer_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list.MakeHeap(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeapInPlace_NullComparison_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list.MakeHeap(comparison);
        }

        #endregion

        /// <summary>
        /// We should be able to heapify an empty list, which should do nothing.
        /// </summary>
        [TestMethod]
        public void TestMakeHeapInPlace_ListEmpty_IsHeap()
        {
            var list = TestHelper.Wrap(new List<int>());

            list.MakeHeap().InPlace();

            Assert.IsTrue(list.IsHeap(), "An empty list should be a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to heapify a list with a single item, which should do nothing.
        /// </summary>
        [TestMethod]
        public void TestMakeHeapInPlace_WithOneItem_IsHeap()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });

            list.MakeHeap().InPlace();

            Assert.IsTrue(list.IsHeap(), "An empty list should be a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to heapify a list with two items, which should move the larger element to the front of the list.
        /// </summary>
        [TestMethod]
        public void TestMakeHeapInPlace_WithTwoItems_IsHeap()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2 });

            list.MakeHeap().InPlace();

            Assert.AreEqual(2, list[0], "The largest element was not first.");
            Assert.IsTrue(list.IsHeap(), "An empty list should be a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to reverse the comparison delegate to create a min heap.
        /// </summary>
        [TestMethod]
        public void TestMakeHeapInPlace_Reversed_CreatesMinHeap()
        {
            var list = TestHelper.Wrap(new List<int>());
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);

            list = Sublist.Generate(100, i => 99 - i).AddTo(list); // largest to smallest
            list.MakeHeap(comparison).InPlace(); // smallest to largest

            Assert.AreEqual(0, list[0], "The largest element was not first.");
            Assert.IsTrue(list.IsHeap(comparison), "An empty list should be a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Regardless of the number of items, heapify should work.
        /// </summary>
        [TestMethod]
        public void TestMakeHeapInPlace_OddNumbered_CreatesHeap()
        {
            var list = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(99, i => i).AddTo(list);
            list.MakeHeap(Comparer<int>.Default).InPlace();

            Assert.AreEqual(98, list[0], "The largest element was not first.");
            Assert.IsTrue(list.IsHeap(Comparer<int>.Default), "An empty list should be a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
