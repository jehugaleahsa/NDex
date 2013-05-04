using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the Minimum methods.
    /// </summary>
    [TestClass]
    public class MinimumTester
    {
        #region Real World Example

        /// <summary>
        /// Minimum is useful for finding the smallest item in a list.
        /// </summary>
        [TestMethod]
        public void TestMinimum_FindSmallestItem()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.AddGenerated(list.ToSublist(), 100, i => random.Next());

            int index = Sublist.Minimum(list.ToSublist());
            Assert.AreNotEqual(list.Count, index, "The index cannot be past the end of the list.");

            int minValue = list[index];
            Sublist.QuickSort(list.ToSublist(), Comparer<int>.Default); // puts the largest item in the first slot
            int expected = list[0];
            Assert.AreEqual(expected, minValue, "The wrong index was returned.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinimum_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.Minimum(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinimum_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.Minimum(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinimum_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.Minimum(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinimum_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            Sublist.Minimum(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinimum_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.Minimum(list, comparison);
        }

        #endregion

        /// <summary>
        /// If a list is empty, it can't have a minimum. In that case,
        /// an index past the end of the list should be returned.
        /// </summary>
        [TestMethod]
        public void TestMinimum_EmptyList_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>());
            int index = Sublist.Minimum(list);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we use the default comparer, Minimum will find the largest item.
        /// </summary>
        [TestMethod]
        public void TestMinimum_FindsSmallest()
        {
            var list = TestHelper.Wrap(new List<int>() { 4, 3, 1, 2, 5 });
            int index = Sublist.Minimum(list, Comparer<int>.Default);
            Assert.AreEqual(2, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we reverse the default comparer, Minimum will find the largest item.
        /// </summary>
        [TestMethod]
        public void TestMinimum_FindsLargest()
        {
            var list = TestHelper.Wrap(new List<int>() { 4, 3, 5, 2, 1 });
            int index = Sublist.Minimum(list, (x, y) => Comparer<int>.Default.Compare(y, x));
            Assert.AreEqual(2, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the smallest item if it is in the front.
        /// </summary>
        [TestMethod]
        public void TestMinimum_InFront()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            int index = Sublist.Minimum(list);
            Assert.AreEqual(0, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the smallest item if it is in the back.
        /// </summary>
        [TestMethod]
        public void TestMinimum_InBack()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 3, 4, 5, 1 });
            int index = Sublist.Minimum(list);
            Assert.AreEqual(4, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the smallest item if it is in the middle.
        /// </summary>
        [TestMethod]
        public void TestMinimum_InMiddle()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 3, 1, 5, 4 });
            int index = Sublist.Minimum(list);
            Assert.AreEqual(2, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
