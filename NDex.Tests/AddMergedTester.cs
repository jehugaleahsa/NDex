using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the AddMerged methods.
    /// </summary>
    [TestClass]
    public class AddMergedTester
    {
        #region Real World Example

        /// <summary>
        /// Merge can be used to combine two sorted lists maintaining sort order.
        /// </summary>
        [TestMethod]
        public void TestAddMerged_CombineSortedLists_StaySorted()
        {
            Random random = new Random();

            // builds the first list
            var list1 = new List<int>(50);
            Sublist.Grow(list1, 50, () => random.Next(100));

            // builds the second list
            var list2 = new List<int>(50);
            Sublist.Grow(list2, 50, () => random.Next(100));

            // merging requires sorted lists
            Sublist.QuickSort(list1.ToSublist());
            Sublist.QuickSort(list2.ToSublist());

            // merge the lists
            var destination = new List<int>(100);
            Sublist.AddMerged(list1.ToSublist(), list2.ToSublist(), destination.ToSublist());

            // make sure the destination is still sorted
            bool isSorted = Sublist.IsSorted(destination.ToSublist());
            Assert.IsTrue(isSorted, "Merge caused the items to become unsorted.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddMerged_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.AddMerged(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddMerged_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.AddMerged(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddMerged_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.AddMerged(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddMerged_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.AddMerged(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddMerged_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.AddMerged(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddMerged_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.AddMerged(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddMerged_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.AddMerged(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddMerged_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.AddMerged(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddMerged_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.AddMerged(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddMerged_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = null;
            Sublist.AddMerged(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddMerged_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.AddMerged(list1, list2, destination, comparison);
        }

        #endregion

        /// <summary>
        /// If we merge two lists, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestAddMerged_Merges()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 2, 4, 6, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7 });
            var destination = TestHelper.Wrap(new List<int>());
            Sublist.AddMerged(list1, list2, destination);
            Assert.AreEqual(8, destination.Count, "The wrong number of items were added.");
            int[] expected = { 0, 1, 2, 3, 4, 5, 6, 7 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestAddMerged_HandlesDuplicates()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 2, 4, 6, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, 7 });
            var destination = TestHelper.Wrap(new List<int>());
            Sublist.AddMerged(list1, list2, destination);
            Assert.AreEqual(9, destination.Count, "The wrong number of items were added.");
            int[] expected = { 0, 1, 2, 2, 3, 4, 5, 6, 7 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestAddMerged_WithComparer_MergeInReverse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 2, 4, 6 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7 });
            var destination = TestHelper.Wrap(new List<int>());
            Sublist.AddMerged(list1, list2, destination, Comparer<int>.Default);
            Assert.AreEqual(7, destination.Count, "The wrong number of items were added.");
            int[] expected = { 1, 2, 3, 4, 5, 6, 7 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestAddMerged_WithComparison_MergeInReverse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 6, 4, 2 });
            var list2 = TestHelper.Wrap(new List<int>() { 7, 5, 3, 1 });
            var destination = TestHelper.Wrap(new List<int>());
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            Sublist.AddMerged(list1, list2, destination, comparison);
            Assert.AreEqual(7, destination.Count, "The wrong number of items were added.");
            int[] expected = { 7, 6, 5, 4, 3, 2, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
