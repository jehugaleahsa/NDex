using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the RemoveDuplicates methods.
    /// </summary>
    [TestClass]
    public class RemoveDuplicatesTester
    {
        #region Real World Example

        /// <summary>
        /// We can make a set out of a sorted list of values.
        /// </summary>
        [TestMethod]
        public void TestRemoveDuplicates_MakeSet()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Grow(list, 100, () => random.Next(100));

            // remove duplicates requires the list to be sorted
            Sublist.QuickSort(list.ToSublist());

            Sublist.RemoveDuplicates(list.ToSublist());

            Assert.IsTrue(Sublist.IsSet(list.ToSublist()), "The list was not a set.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemoveDuplicates_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.RemoveDuplicates(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemoveDuplicates_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.RemoveDuplicates(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemoveDuplicates_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.RemoveDuplicates(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemoveDuplicates_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.RemoveDuplicates(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemoveDuplicates_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.RemoveDuplicates(list, comparison);
        }

        #endregion

        /// <summary>
        /// If a list contains no duplicates, nothing gets removed.
        /// </summary>
        [TestMethod]
        public void TestRemoveDuplicates_NoDuplicates_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            Sublist.RemoveDuplicates(list);
            int[] expected = { 1, 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items was not in the expected order.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If a list contains all duplicates, only the first item should remain.
        /// </summary>
        [TestMethod]
        public void TestRemoveDuplicates_AllDuplicates_RemovesAllButOne()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 1, 1, 1 });
            Sublist.RemoveDuplicates(list);
            int[] expected = { 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items was not in the expected order.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the duplicates appear at the end, no items should be moved.
        /// </summary>
        [TestMethod]
        public void TestRemoveDuplicates_DuplicatesInBack()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 5 });
            Sublist.RemoveDuplicates(list, EqualityComparer<int>.Default);
            int[] expected = { 1, 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items was not in the expected order.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the duplicates appear at the front, they should be overwritten by next non-duplicates.
        /// </summary>
        [TestMethod]
        public void TestRemoveDuplicates_DuplicatesInFront()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 2, 3, 4, 5 });
            Sublist.RemoveDuplicates(list, EqualityComparer<int>.Default.Equals);
            int[] expected = { 1, 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items was not in the expected order.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the duplicates appear in the middle, they should be overwritten by next non-duplicates.
        /// </summary>
        [TestMethod]
        public void TestRemoveDuplicates_DuplicatesInMiddle()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 3, 4, 4, 5 });
            Sublist.RemoveDuplicates(list, EqualityComparer<int>.Default.Equals);
            int[] expected = { 1, 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items was not in the expected order.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
