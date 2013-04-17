using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the IsHeap methods.
    /// </summary>
    [TestClass]
    public class IsHeapTester
    {
        // see HeapTester for a real world example

        #region Argument Checking

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
        /// An empty list is a valid heap.
        /// </summary>
        [TestMethod]
        public void TestIsHeap_EmptyList_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>());
            bool isHeap = Sublist.IsHeap(list);
            Assert.IsTrue(isHeap, "An empty list should be a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// An list with one item is a valid heap.
        /// </summary>
        [TestMethod]
        public void TestIsHeap_SizeOfOne_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            bool isHeap = Sublist.IsHeap(list);
            Assert.IsTrue(isHeap, "A list of size one should be a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// An list with two item is a valid heap if the first value is larger.
        /// </summary>
        [TestMethod]
        public void TestIsHeap_SizeOfTwo()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 1 });
            bool isHeap = Sublist.IsHeap(list, Comparer<int>.Default);
            Assert.IsTrue(isHeap, "The list should have been a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we build a heap in reverse, it should still be a heap with the appropriate comparison.
        /// </summary>
        [TestMethod]
        public void TestIsHeap_MinHeap()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6, 7, });
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            bool isHeap = Sublist.IsHeap(list, comparison);
            Assert.IsTrue(isHeap, "The list should have been a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we build a list that is not a heap, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsHeap_NotAHeap_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 10, 9, 8, 5, 6, 11, 4 }); // the 11 is the problem
            bool isHeap = Sublist.IsHeap(list);
            Assert.IsFalse(isHeap, "The list was not a valid heap.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
