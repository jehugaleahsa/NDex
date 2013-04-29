using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace NDex.Test
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
            Sublist.HeapAdd(list);
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
            Sublist.HeapAdd(list, comparer);
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
            Sublist.HeapAdd(list, comparison);
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
            Sublist.HeapAdd(list, comparer);
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
            Sublist.HeapAdd(list, comparison);
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
            Sublist.HeapAdd(list, comparer);
            Assert.IsTrue(Sublist.IsHeap(list, comparer), "The list was not a heap.");
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
            list.Add(4);
            Sublist.HeapAdd(list, comparison);
            Assert.IsTrue(Sublist.IsHeap(list, comparison), "The list was not a heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Adding an item that is bigger than the rest should perculate the value to the top.
        /// </summary>
        [TestMethod]
        public void TestHeapAdd_AddLargest_StaysAHeap()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.Add(Enumerable.Range(0, 10), list); // 0..9
            Sublist.MakeHeap(list);

            list.Add(10);
            Sublist.HeapAdd(list);

            Assert.IsTrue(Sublist.IsHeap(list), "The list was not a heap.");
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
            Sublist.Add(Enumerable.Range(0, 10).Select(i => i + 1), list); // 1..10
            Sublist.MakeHeap(list);

            list.Add(0);
            Sublist.HeapAdd(list);

            Assert.IsTrue(Sublist.IsHeap(list), "The list was not a heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Adding an item that is in the middle should cause it to perculate up.
        /// </summary>
        [TestMethod]
        public void TestHeapAdd_AddMedium_StaysAHeap()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.Add(Enumerable.Range(0, 10).Select(i => i + (i > 4 ? 1 : 0)), list); // 0..4, 6..10
            Sublist.MakeHeap(list);

            list.Add(5);
            Sublist.HeapAdd(list);

            Assert.IsTrue(Sublist.IsHeap(list), "The list was not a heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
