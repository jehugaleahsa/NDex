using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the MinimumMaximum methods.
    /// </summary>
    [TestClass]
    public class MinimumMaximumTester
    {
        #region Real World Example

        /// <summary>
        /// MinimumMaximum is useful for finding the smallest and largest item in a list at the same time.
        /// </summary>
        [TestMethod]
        public void TestMinimumMaximum_FindLargestAndSmallestItems()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next()).AddTo(list.ToSublist());

            MinimumMaximumResult result = list.ToSublist().MinimumMaximum();
            Assert.AreNotEqual(list.Count, result.MinimumIndex, "The index cannot be past the end of the list.");
            Assert.AreNotEqual(list.Count, result.MaximumIndex, "The index cannot be past the end of the list.");

            int minValue = list[result.MinimumIndex];
            int maxValue = list[result.MaximumIndex];
            list.ToSublist().QuickSort(Comparer<int>.Default); // puts the largest item in the first slot
            Assert.AreEqual(list[0], minValue, "The wrong index was returned.");
            Assert.AreEqual(list[list.Count - 1], maxValue, "The wrong index was returned.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinimumMaximum_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            list.MinimumMaximum();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinimumMaximum_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.MinimumMaximum(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinimumMaximum_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.MinimumMaximum(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinimumMaximum_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            list.MinimumMaximum(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinimumMaximum_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            list.MinimumMaximum(comparison);
        }

        #endregion

        /// <summary>
        /// If a list is empty, it can't have a MinimumMaximum. In that case,
        /// an index past the end of the list should be returned.
        /// </summary>
        [TestMethod]
        public void TestMinimumMaximum_EmptyList_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>());
            MinimumMaximumResult result = list.MinimumMaximum();
            Assert.AreEqual(list.Count, result.MinimumIndex, "The wrong min index was returned.");
            Assert.AreEqual(list.Count, result.MaximumIndex, "The wrong max index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If there is only one item in the list, min and max should be the same value.
        /// </summary>
        [TestMethod]
        public void TestMinimumMaximum_ListOfOne_MinAndMaxEqual()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            MinimumMaximumResult result = list.MinimumMaximum();
            Assert.AreEqual(result.MinimumIndex, result.MaximumIndex, "The min and max should be equal if the size is one.");
            Assert.AreEqual(0, result.MinimumIndex, "The wrong indexes were returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we use the default comparer, MinimumMaximum will find the smallest and largest items.
        /// </summary>
        [TestMethod]
        public void TestMinimumMaximum_FindsSmallestAndLargest()
        {
            var list = TestHelper.Wrap(new List<int>() { 4, 3, 1, 2, 5 });
            MinimumMaximumResult result = list.MinimumMaximum(Comparer<int>.Default);
            Assert.AreEqual(2, result.MinimumIndex, "The wrong min index was returned.");
            Assert.AreEqual(4, result.MaximumIndex, "The wrong max index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we reverse the default comparer, MinimumMaximum will find the largest, then the smallest items.
        /// </summary>
        [TestMethod]
        public void TestMinimumMaximum_FindsLargestAndSmallest()
        {
            var list = TestHelper.Wrap(new List<int>() { 4, 3, 5, 2, 1 });
            MinimumMaximumResult result = list.MinimumMaximum((x, y) => Comparer<int>.Default.Compare(y, x));
            Assert.AreEqual(2, result.MinimumIndex, "The wrong min index was returned.");
            Assert.AreEqual(4, result.MaximumIndex, "The wrong max index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the smallest item if it is in the front, the largest if it is in the back.
        /// </summary>
        [TestMethod]
        public void TestMinimumMaximum_SmallestInFront_LargestInBack()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            MinimumMaximumResult result = list.MinimumMaximum();
            Assert.AreEqual(0, result.MinimumIndex, "The wrong min index was returned.");
            Assert.AreEqual(4, result.MaximumIndex, "The wrong max index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the smallest item if it is in the back, the largest if it is in the front.
        /// </summary>
        [TestMethod]
        public void TestMinimumMaximum_SmallestInBack_LargestInFront()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 2, 3, 4, 1 });
            MinimumMaximumResult result = list.MinimumMaximum();
            Assert.AreEqual(4, result.MinimumIndex, "The wrong min index was returned.");
            Assert.AreEqual(0, result.MaximumIndex, "The wrong max index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the smallest and largest items if they are in the middle.
        /// </summary>
        [TestMethod]
        public void TestMinimumMaximum_InMiddle()
        {
            var list = TestHelper.Wrap(new List<int>() { 3, 1, 5, 4 });
            MinimumMaximumResult result = list.MinimumMaximum();
            Assert.AreEqual(1, result.MinimumIndex, "The wrong min index was returned.");
            Assert.AreEqual(2, result.MaximumIndex, "The wrong max index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
