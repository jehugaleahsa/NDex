using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the CopyMerged methods.
    /// </summary>
    [TestClass]
    public class CopyMergedTester
    {
        #region Real World Example

        /// <summary>
        /// Merge can be used to combine two sorted lists maintaining sort order.
        /// </summary>
        [TestMethod]
        public void TestCopyMerged_CombineSortedLists_StaySorted()
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
            Sublist.Grow(destination, 100, 0);
            int result = Sublist.CopyMerged(list1.ToSublist(), list2.ToSublist(), destination.ToSublist());
            Assert.AreEqual(destination.Count, result, "Not all of the items were copied.");

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
        public void TestCopyMerged_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyMerged(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyMerged_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.CopyMerged(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyMerged_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.CopyMerged(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyMerged_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyMerged(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyMerged_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.CopyMerged(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyMerged_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.CopyMerged(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyMerged_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.CopyMerged(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyMerged_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.CopyMerged(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyMerged_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.CopyMerged(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyMerged_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = null;
            Sublist.CopyMerged(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyMerged_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.CopyMerged(list1, list2, destination, comparison);
        }

        #endregion

        /// <summary>
        /// If we merge two lists, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestCopyMerged_Merges()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 2, 4, 6, 8 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 });
            IComparer<int> comparer = Comparer<int>.Default;
            CopyTwoSourcesResult result = Sublist.CopyMerged(list1, list2, destination, comparer);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong number of items were added.");
            int[] expected = { 1, 2, 3, 4, 5, 6, 7, 8 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestCopyMerged_HandlesDuplicates()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 2, 4, 6, 8, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, 7 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            CopyTwoSourcesResult result = Sublist.CopyMerged(list1, list2, destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong number of items were added.");
            int[] expected = { 1, 2, 2, 3, 4, 5, 6, 7, 8 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists in reverse, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestCopyMerged_MergeInReverse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 6, 4, 2 });
            var list2 = TestHelper.Wrap(new List<int>() { 7, 5, 3, 1 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, 0, });
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            CopyTwoSourcesResult result = Sublist.CopyMerged(list1, list2, destination, comparison);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong number of items were added.");
            int[] expected = { 7, 6, 5, 4, 3, 2, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists in reverse, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestCopyMerged_DestinationTooSmall_StopsPrematurely()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 2, 4, 6 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, });
            CopyTwoSourcesResult result = Sublist.CopyMerged(list1, list2, destination);
            Assert.AreEqual(1, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(2, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong number of items were added.");
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
