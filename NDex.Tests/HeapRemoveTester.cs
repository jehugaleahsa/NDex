using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the HeapRemove methods.
    /// </summary>
    [TestClass]
    public class HeapRemoveTester
    {
        // See HeapTester for an example

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapRemove_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.HeapRemove(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapRemove_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.HeapRemove(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapRemove_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.HeapRemove(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapRemove_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            Sublist.HeapRemove(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestHeapRemove_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.HeapRemove(list, comparison);
        }

        #endregion

        /// <summary>
        /// Calling HeapRemove on an empty list does nothing.
        /// </summary>
        [TestMethod]
        public void TestHeapRemove_EmptyList_DoesNothing()
        {
            var list = TestHelper.Wrap(new List<int>());

            Sublist.HeapRemove(list);

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Calling HeapRemove on a list with one item should do nothing.
        /// </summary>
        [TestMethod]
        public void TestHeap_OneItem_DoesNothing()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            Sublist.HeapRemove(list);
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Calling HeapRemove on a list with two items should move the first item to the back.
        /// </summary>
        [TestMethod]
        public void TestHeap_TwoItems_MovesTopToEnd()
        {
            var list = TestHelper.Wrap(new List<int>() { 10, 5 });
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.HeapRemove(list, comparer);
            int[] expected = { 5, 10 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The top item was not moved to the end.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Calling HeapRemove on a list with an even number of items should move the first item to the back.
        /// </summary>
        [TestMethod]
        public void TestHeap_EvenSized_MovesTopToEnd()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Add(Enumerable.Range(0, 100).Select(i => i + 1), list); // 1..100

            Sublist.MakeHeap(list);
            Sublist.HeapRemove(list);

            Assert.AreEqual(100, list[list.Count - 1], "The top item was not moved to the end.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Calling HeapRemove on a list with an odd number of items should move the first item to the back.
        /// </summary>
        [TestMethod]
        public void TestHeap_OddSized_MovesTopToEnd()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Add(Enumerable.Range(0, 99).Select(i => i + 1), list); // 1..99

            Sublist.MakeHeap(list);
            Sublist.HeapRemove(list);

            Assert.AreEqual(99, list[list.Count - 1], "The top item was not moved to the end.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Calling HeapRemove on a min heap should continue to work.
        /// </summary>
        [TestMethod]
        public void TestHeap_Reversed_MovesTopToEnd()
        {
            var list = TestHelper.Wrap(new List<int>());
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            list = Sublist.Add(Enumerable.Range(0, 64).Select(i => i + 1), list); // 1..64

            Sublist.MakeHeap(list, comparison);
            Sublist.HeapRemove(list, comparison);

            Assert.AreEqual(1, list[list.Count - 1], "The top item was not moved to the end.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
