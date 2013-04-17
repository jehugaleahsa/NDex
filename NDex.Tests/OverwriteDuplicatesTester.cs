using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the OverwriteDuplicates methods.
    /// </summary>
    [TestClass]
    public class OverwriteDuplicatesTester
    {
        #region Real World Example

        /// <summary>
        /// We can eliminate duplicates in an array by overwritting them with non-duplicates.
        /// </summary>
        [TestMethod]
        public void TestOverwriteDuplicates()
        {
            Random random = new Random();

            // build an array
            var array = new int[100];
            Sublist.Fill(array.ToSublist(), () => random.Next(100));

            // sort the list to put equivalent items next to each other
            Sublist.QuickSort(array.ToSublist());

            // overwrite the duplicates
            int index = Sublist.OverwriteDuplicates(array.ToSublist());

            // create a view over the list, eliminating trailing items
            var set = array.ToSublist(0, index);
            Assert.IsTrue(Sublist.IsSet(set), "Duplicate items still remained.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverwriteDuplicates_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.OverwriteDuplicates(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverwriteDuplicates_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.OverwriteDuplicates(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverwriteDuplicates_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.OverwriteDuplicates(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverwriteDuplicates_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.OverwriteDuplicates(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOverwriteDuplicates_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.OverwriteDuplicates(list, comparison);
        }

        #endregion

        /// <summary>
        /// If a list contains no duplicates, nothing gets overwritten, so the returned index should be past the end of the list.
        /// </summary>
        [TestMethod]
        public void TestOverwriteDuplicates_NoDuplicates_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            int index = Sublist.OverwriteDuplicates(list);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            int[] expected = { 1, 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items was not in the expected order.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If a list contains all duplicates, only the first item should be included.
        /// </summary>
        [TestMethod]
        public void TestOverwriteDuplicates_AllDuplicates_ReturnsOne()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 1, 1, 1 });
            int index = Sublist.OverwriteDuplicates(list);
            Assert.AreEqual(1, index, "The wrong index was returned.");
            int[] expected = { 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.Nest(0, index)), "The items was not in the expected order.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the duplicates appear at the end, no items should be moved.
        /// </summary>
        [TestMethod]
        public void TestOverwriteDuplicates_DuplicatesInBack_ReturnsAfterFirstOccurrence()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 5 });
            int index = Sublist.OverwriteDuplicates(list, EqualityComparer<int>.Default);
            Assert.AreEqual(5, index, "The wrong index was returned.");
            int[] expected = { 1, 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.Nest(0, index)), "The items was not in the expected order.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the duplicates appear at the front, they should be overwritten by next non-duplicates.
        /// </summary>
        [TestMethod]
        public void TestOverwriteDuplicates_DuplicatesInFront_ReturnsAtTrailingItems()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 2, 3, 4, 5 });
            int index = Sublist.OverwriteDuplicates(list, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(5, index, "The wrong index was returned.");
            int[] expected = { 1, 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.Nest(0, index)), "The items was not in the expected order.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the duplicates appear in the middle, they should be overwritten by next non-duplicates.
        /// </summary>
        [TestMethod]
        public void TestOverwriteDuplicates_DuplicatesInMiddle_ReturnsAtTrailingItems()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 3, 4, 4, 5 });
            int index = Sublist.OverwriteDuplicates(list, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(5, index, "The wrong index was returned.");
            int[] expected = { 1, 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.Nest(0, index)), "The items was not in the expected order.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
