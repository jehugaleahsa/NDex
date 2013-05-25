using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the HeapAdd methods.
    /// </summary>
    [TestClass]
    public class HeapAddTester
    {
        // See HeapTester for an example

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapAdd_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            list.HeapAdd();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapAdd_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.HeapAdd(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapAdd_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.HeapAdd(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapAdd_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            list.HeapAdd(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapAdd_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            list.HeapAdd(comparison);
        }

        #endregion

        /// <summary>
        /// An item by itself is a heap.
        /// </summary>
        [TestMethod]
        public void TestHeapAdd_EmptyList_StaysAHeap()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            IComparer<int> comparer = Comparer<int>.Default;
            list.HeapAdd(comparer);
            Assert.IsTrue(list.IsHeap(comparer), "The list was not a heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// An item by itself is a heap.
        /// </summary>
        [TestMethod]
        public void TestHeapAdd_Reversed_StaysAHeap()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            list = new int[] { 4 }.AddTo(list);
            list.HeapAdd(comparison);
            Assert.IsTrue(list.IsHeap(comparison), "The list was not a heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Adding an item that is bigger than the rest should perculate the value to the top.
        /// </summary>
        [TestMethod]
        public void TestHeapAdd_AddLargest_StaysAHeap()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(10, i => i).AddTo(list); // 0..9
            list.MakeHeap().InPlace();

            list = new int[] { 10 }.AddTo(list);
            list.HeapAdd();

            Assert.IsTrue(list.IsHeap(), "The list was not a heap.");
            Assert.AreEqual(10, list[0], "The value did not move to the top.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Adding an item that is smaller than the rest should essentially do nothing.
        /// </summary>
        [TestMethod]
        public void TestHeapAdd_AddSmallest_StaysAHeap()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(10, i => i + 1).AddTo(list); // 1..10
            list.MakeHeap().InPlace();

            list = new int[] { 0 }.AddTo(list);
            list.HeapAdd();

            Assert.IsTrue(list.IsHeap(), "The list was not a heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Adding an item that is in the middle should cause it to perculate up.
        /// </summary>
        [TestMethod]
        public void TestHeapAdd_AddMedium_StaysAHeap()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(10, i => i + (i > 4 ? 1 : 0)).AddTo(list); // 0..4, 6..10
            list.MakeHeap().InPlace();

            list = new int[] { 5 }.AddTo(list);
            list.HeapAdd();

            Assert.IsTrue(list.IsHeap(), "The list was not a heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
