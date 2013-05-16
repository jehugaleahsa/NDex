using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the IsHeap methods.
    /// </summary>
    [TestClass]
    public class IsHeapTester
    {
        // see HeapTester for an additional real world example

        #region Real World Example

        /// <summary>
        /// IsHeap can be useful when you want to save time building a heap from scratch, especially
        /// when you know the list may already partially be a heap.
        /// </summary>
        [TestMethod]
        public void TestIsHeap_BuildHeapFromRemaining()
        {
            Random random = new Random();

            // build a random list
            var list = new List<int>(100);
            Sublist.AddGenerated(list.ToSublist(), 100, i => random.Next());

            // find the first bad value and add the remaining items to the heap
            var result = Sublist.IsHeap(list.ToSublist());
            while (!result.Success)
            {
                var heap = list.ToSublist(0, result.Index + 1);
                Sublist.HeapAdd(heap);
                Assert.IsTrue(Sublist.IsHeap(heap), "After adding an item to the heap, it was no longer valid.");
                result = Sublist.IsHeap(list.ToSublist()); // bypass already valid items - usually more efficient to simply incrememnt
            }
        }

        #endregion

        #region Argument Checks

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsHeap_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.IsHeap(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsHeap_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.IsHeap(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsHeap_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.IsHeap(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsHeap_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            Sublist.IsHeap(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsHeap_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.IsHeap(list, comparison);
        }

        #endregion

        /// <summary>
        /// An empty list is a valid heap, so zero should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsHeap_EmptyList_ReturnsZero()
        {
            var list = TestHelper.Wrap(new List<int>());
            var result = Sublist.IsHeap(list);
            Assert.IsTrue(result.Success, "An empty list should be a valid heap.");
            Assert.AreEqual(list.Count, result.Index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with one item is a valid heap, so an index past the last item should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsHeap_ListOfOne_ReturnsOne()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            var result = Sublist.IsHeap(list);
            Assert.IsTrue(result.Success, "A list with one item should be a valid heap.");
            Assert.AreEqual(list.Count, result.Index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// In a list of two items, it is a valid heap if the first item is larger.
        /// </summary>
        [TestMethod]
        public void TestIsHeap_ListOfTwo_BigToSmall_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 1 });
            var result = Sublist.IsHeap(list, Comparer<int>.Default);
            Assert.IsTrue(result.Success, "The list should have been a heap.");
            Assert.AreEqual(list.Count, result.Index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If there is an item in the list that stops it from being a heap,
        /// its index should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsHeap_InvalidHeap_ReturnsFirstInvalidIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 10, 8, 9, 6, 7, 5, 10, 4, 3 });
            var result = Sublist.IsHeap(list, Comparer<int>.Default.Compare);
            Assert.IsFalse(result.Success, "The list should not have been a heap.");
            Assert.AreEqual(6, result.Index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
