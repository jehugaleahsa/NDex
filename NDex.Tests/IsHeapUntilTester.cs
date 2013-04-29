using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the IsHeapUntil methods.
    /// </summary>
    [TestClass]
    public class IsHeapUntilTester
    {
        // see HeapTester for an additional real world example

        #region Real World Example

        /// <summary>
        /// IsHeapUntil can be useful when you want to save time building a heap from scratch, especially
        /// when you know the list may already partially be a heap.
        /// </summary>
        [TestMethod]
        public void TestIsHeapUntil_BuildHeapFromRemaining()
        {
            Random random = new Random();

            // build a random list
            var list = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next()), list.ToSublist());

            // find the first bad value and add the remaining items to the heap
            int index = Sublist.IsHeapUntil(list.ToSublist());
            while (index != list.Count)
            {
                var heap = list.ToSublist(0, index + 1);
                Sublist.HeapAdd(heap);
                Assert.IsTrue(Sublist.IsHeap(heap), "After adding an item to the heap, it was no longer valid.");
                index = Sublist.IsHeapUntil(list.ToSublist()); // bypass already valid items - usually more efficient to simply incrememnt
            }
        }

        #endregion

        #region Argument Checks

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsHeapUntil_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.IsHeapUntil(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsHeapUntil_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.IsHeapUntil(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsHeapUntil_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.IsHeapUntil(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsHeapUntil_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            Sublist.IsHeapUntil(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsHeapUntil_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.IsHeapUntil(list, comparison);
        }

        #endregion

        /// <summary>
        /// An empty list is a valid heap, so zero should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsHeapUntil_EmptyList_ReturnsZero()
        {
            var list = TestHelper.Wrap(new List<int>());
            int index = Sublist.IsHeapUntil(list);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with one item is a valid heap, so an index past the last item should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsHeapUntil_ListOfOne_ReturnsOne()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            int index = Sublist.IsHeapUntil(list);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// In a list of two items, it is a valid heap if the first item is larger.
        /// </summary>
        [TestMethod]
        public void TestIsHeapUntil_ListOfTwo_BigToSmall_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 1 });
            int index = Sublist.IsHeapUntil(list, Comparer<int>.Default);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If there is an item in the list that stops it from being a heap,
        /// its index should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsHeapUntil_InvalidHeap_ReturnsFirstInvalidIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 10, 8, 9, 6, 7, 5, 10, 4, 3 });
            int index = Sublist.IsHeapUntil(list, Comparer<int>.Default.Compare);
            Assert.AreEqual(6, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
