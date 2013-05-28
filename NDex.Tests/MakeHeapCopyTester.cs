using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the MakeHeapCopy methods.
    /// </summary>
    [TestClass]
    public class MakeHeapCopyTester
    {
        // See HeapTester for an example

        #region Argument Checker

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeapCopy_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            list.MakeHeap();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeapCopy_WithComparer_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.MakeHeap(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeapCopy_WithComparison_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.MakeHeap(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeapCopy_NullComparer_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list.MakeHeap(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeHeapCopy_NullComparison_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list.MakeHeap(comparison);
        }

        #endregion

        /// <summary>
        /// We should be able to heapify an empty list, which should do nothing.
        /// </summary>
        [TestMethod]
        public void TestMakeHeapCopy_ListEmpty_IsHeap()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            var result = list.MakeHeap().CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.IsTrue(destination.IsHeap(), "An empty list should be a valid heap.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to heapify a list with a single item, which should do nothing.
        /// </summary>
        [TestMethod]
        public void TestMakeHeapCopy_WithOneItem_IsHeap()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1 });
            var destination = TestHelper.Wrap(new List<int>() { 0 });

            var result = list.MakeHeap().CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.IsTrue(destination.IsHeap(), "An empty list should be a valid heap.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to heapify a list with two items, which should move the larger element to the front of the list.
        /// </summary>
        [TestMethod]
        public void TestMakeHeapCopy_WithTwoItems_IsHeap()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });

            var result = list.MakeHeap().CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.AreEqual(2, destination[0], "The largest element was not first.");
            Assert.IsTrue(destination.IsHeap(), "An empty list should be a valid heap.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to reverse the comparison delegate to create a min heap.
        /// </summary>
        [TestMethod]
        public void TestMakeHeapCopy_Reversed_CreatesMinHeap()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);

            list = Sublist.Generate(100, i => 99 - i).AddTo(TestHelper.Populate(list)); // largest to smallest
            destination = Sublist.Generate(100, 0).AddTo(destination);
            var result = list.MakeHeap(comparison).CopyTo(destination); // smallest to largest

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.AreEqual(0, destination[0], "The largest element was not first.");
            Assert.IsTrue(destination.IsHeap(comparison), "An empty list should be a valid heap.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Regardless of the number of items, heapify should work.
        /// </summary>
        [TestMethod]
        public void TestMakeHeapCopy_OddNumbered_CreatesHeap()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(99, i => i).AddTo(TestHelper.Populate(list));
            destination = Sublist.Generate(99, 0).AddTo(destination);
            var result = list.MakeHeap(Comparer<int>.Default).CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.AreEqual(98, destination[0], "The largest element was not first.");
            Assert.IsTrue(destination.IsHeap(Comparer<int>.Default), "An empty list should be a valid heap.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is smaller than the source, the largest items should appear in the heap.
        /// </summary>
        [TestMethod]
        public void TestMakeHeapCopy_DestinationSmaller_MovesLargestIntoHeap()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(100, i => i).AddTo(list.List.ToSublist());
            destination = Sublist.Generate(50, 0).AddTo(destination);
            var result = list.MakeHeap(Comparer<int>.Default).CopyTo(destination);

            Assert.AreEqual(50, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.IsTrue(destination.IsHeap(Comparer<int>.Default), "An empty list should be a valid heap.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
