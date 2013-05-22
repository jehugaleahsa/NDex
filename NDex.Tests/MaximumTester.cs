using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the Maximum methods.
    /// </summary>
    [TestClass]
    public class MaximumTester
    {
        #region Real World Example

        /// <summary>
        /// Maximum is useful for finding the largest item in a list.
        /// </summary>
        [TestMethod]
        public void TestMaximum_FindLargestItem()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next()).AddTo(list.ToSublist());

            int index = Sublist.Maximum(list.ToSublist());
            Assert.AreNotEqual(list.Count, index, "The index cannot be past the end of the list.");

            int maxValue = list[index];
            Sublist.QuickSort(list.ToSublist(), (x, y) => Comparer<int>.Default.Compare(y, x)); // puts the largest item in the first slot
            int expected = list[0];
            Assert.AreEqual(expected, maxValue, "The wrong index was returned.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMaximum_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.Maximum(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMaximum_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.Maximum(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMaximum_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.Maximum(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMaximum_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            Sublist.Maximum(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMaximum_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.Maximum(list, comparison);
        }

        #endregion

        /// <summary>
        /// If a list is empty, it can't have a maximum. In that case,
        /// an index past the end of the list should be returned.
        /// </summary>
        [TestMethod]
        public void TestMaximum_EmptyList_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>());
            int index = Sublist.Maximum(list);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we use the default comparer, maximum will find the largest item.
        /// </summary>
        [TestMethod]
        public void TestMaximum_FindsLargest()
        {
            var list = TestHelper.Wrap(new List<int>() { 4, 3, 5, 2, 1 });
            int index = Sublist.Maximum(list, Comparer<int>.Default);
            Assert.AreEqual(2, index, "The wrong index was returned.");
        }

        /// <summary>
        /// If we reverse the default comparer, maximum will find the smallest item.
        /// </summary>
        [TestMethod]
        public void TestMaximum_FindsSmallest()
        {
            var list = TestHelper.Wrap(new List<int>() { 4, 3, 1, 2, 5 });
            int index = Sublist.Maximum(list, (x, y) => Comparer<int>.Default.Compare(y, x));
            Assert.AreEqual(2, index, "The wrong index was returned.");
        }

        /// <summary>
        /// We should be able to find the largest item if it is in the front.
        /// </summary>
        [TestMethod]
        public void TestMaximum_InFront()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 4, 3, 2, 1 });
            int index = Sublist.Maximum(list);
            Assert.AreEqual(0, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the largest item if it is in the back.
        /// </summary>
        [TestMethod]
        public void TestMaximum_InBack()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            int index = Sublist.Maximum(list);
            Assert.AreEqual(4, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the largest item if it is in the middle.
        /// </summary>
        [TestMethod]
        public void TestMaximum_InMiddle()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 3, 5, 1, 4 });
            int index = Sublist.Maximum(list);
            Assert.AreEqual(2, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
