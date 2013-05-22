using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
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
            list.HeapRemove();
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
            list.HeapRemove(comparer);
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
            list.HeapRemove(comparison);
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
            list.HeapRemove(comparer);
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
            list.HeapRemove(comparison);
        }

        #endregion

        /// <summary>
        /// Calling HeapRemove on an empty list does nothing.
        /// </summary>
        [TestMethod]
        public void TestHeapRemove_EmptyList_DoesNothing()
        {
            var list = TestHelper.Wrap(new List<int>());

            list.HeapRemove();

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Calling HeapRemove on a list with one item should do nothing.
        /// </summary>
        [TestMethod]
        public void TestHeap_OneItem_DoesNothing()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            list.HeapRemove();
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
            list.HeapRemove(comparer);
            int[] expected = { 5, 10 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list), "The top item was not moved to the end.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Calling HeapRemove on a list with an even number of items should move the first item to the back.
        /// </summary>
        [TestMethod]
        public void TestHeap_EvenSized_MovesTopToEnd()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(100, i => i + 1).AddTo(list); // 1..100

            list.MakeHeap();
            list.HeapRemove();

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
            list = Sublist.Generate(99, i => i + 1).AddTo(list); // 1..99

            list.MakeHeap();
            list.HeapRemove();

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
            list = Sublist.Generate(64, i => i + 1).AddTo(list); // 1..64

            list.MakeHeap(comparison);
            list.HeapRemove(comparison);

            Assert.AreEqual(1, list[list.Count - 1], "The top item was not moved to the end.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
