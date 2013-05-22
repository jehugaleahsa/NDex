using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the InsertionSort methods.
    /// </summary>
    [TestClass]
    public class InsertionSortTester
    {
        #region Real World Example

        /// <summary>
        /// Insertion sort is the best algorithm to use when the list size is between 11 and 50 items.
        /// </summary>
        [TestMethod]
        public void TestInsertionSort_RandomizedList()
        {
            Random random = new Random();

            // build the list
            List<int> list = new List<int>();
            Sublist.Generate(50, i => random.Next(50)).AddTo(list.ToSublist());

            // sort the list
            list.ToSublist().InsertionSort();

            // verify it is sorted
            bool result = list.ToSublist().IsSorted();
            Assert.IsTrue(result, "The list was not sorted correctly.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertionSort_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            list.InsertionSort();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertionSort_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.InsertionSort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertionSort_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.InsertionSort(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertionSort_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            list.InsertionSort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertionSort_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            list.InsertionSort(comparison);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestInsertionSort_EmptyList()
        {
            var list = TestHelper.Wrap(new List<int>());
            list.InsertionSort(Comparer<int>.Default.Compare);
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// InsertionSort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestInsertionSort_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>() { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 });
            list.InsertionSort(Comparer<int>.Default);
            bool result = list.IsSorted(Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// InsertionSort should work against a list whose values ascend and then descend.
        /// </summary>
        [TestMethod]
        public void TestInsertionSort_PipeOrganed()
        {
            var list = TestHelper.Wrap(new List<int>() { 0, 2, 4, 6, 8, 9, 7, 5, 3, 1 });
            list.InsertionSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// InsertionSort should work against a list whose values jump between small and large.
        /// </summary>
        [TestMethod]
        public void TestInsertionSort_Interweaved()
        {
            var list = TestHelper.Wrap(new List<int>() { 0, 9, 1, 8, 2, 7, 3, 6, 4, 5 });
            list.InsertionSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// InsertionSort should work against a list whose values are sorted except the last value.
        /// </summary>
        [TestMethod]
        public void TestInsertionSort_LastMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
            list.InsertionSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// InsertionSort should work against a list whose values are sorted except the first value.
        /// </summary>
        [TestMethod]
        public void TestInsertionSort_FirstMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 9, 0, 1, 2, 3, 4, 5, 6, 7, 8 });
            list.InsertionSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
