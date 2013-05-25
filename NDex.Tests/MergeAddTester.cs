using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the MergeAdd methods.
    /// </summary>
    [TestClass]
    public class MergeAddTester
    {
        #region Real World Example

        /// <summary>
        /// Merge can be used to combine two sorted lists maintaining sort order.
        /// </summary>
        [TestMethod]
        public void TestMergeAdd_CombineSortedLists_StaySorted()
        {
            Random random = new Random();

            // builds the first list
            var list1 = new List<int>(50);
            Sublist.Generate(50, i => random.Next(100)).AddTo(list1.ToSublist());

            // builds the second list
            var list2 = new List<int>(50);
            Sublist.Generate(50, i => random.Next(100)).AddTo(list2.ToSublist());

            // merging requires sorted lists
            list1.ToSublist().Sort().InPlace();
            list2.ToSublist().Sort().InPlace();

            // merge the lists
            var destination = new List<int>(100);
            list1.ToSublist().Merge(list2.ToSublist()).AddTo(destination.ToSublist());

            // make sure the destination is still sorted
            bool isSorted = destination.ToSublist().IsSorted();
            Assert.IsTrue(isSorted, "Merge caused the items to become unsorted.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeAdd_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            list1.Merge(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeAdd_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Merge(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeAdd_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Merge(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeAdd_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            list1.Merge(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeAdd_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Merge(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeAdd_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Merge(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeAdd_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            list1.Merge(list2).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeAdd_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Merge(list2, comparer).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeAdd_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Merge(list2, comparison).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeAdd_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = null;
            list1.Merge(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeAdd_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = null;
            list1.Merge(list2, comparison);
        }

        #endregion

        /// <summary>
        /// If we merge two lists, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestMergeAdd_Merges()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 2, 4, 6, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list1.Merge(list2).AddTo(destination);
            Assert.AreEqual(8, destination.Count, "The wrong number of items were added.");
            int[] expected = { 0, 1, 2, 3, 4, 5, 6, 7 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestMergeAdd_HandlesDuplicates()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 2, 4, 6, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, 7 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list1.Merge(list2).AddTo(destination);
            Assert.AreEqual(9, destination.Count, "The wrong number of items were added.");
            int[] expected = { 0, 1, 2, 2, 3, 4, 5, 6, 7 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestMergeAdd_WithComparer_MergeInReverse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 2, 4, 6 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list1.Merge(list2, Comparer<int>.Default).AddTo(destination);
            Assert.AreEqual(7, destination.Count, "The wrong number of items were added.");
            int[] expected = { 1, 2, 3, 4, 5, 6, 7 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestMergeAdd_WithComparison_MergeInReverse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 6, 4, 2 });
            var list2 = TestHelper.Wrap(new List<int>() { 7, 5, 3, 1 });
            var destination = TestHelper.Wrap(new List<int>());
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            destination = list1.Merge(list2, comparison).AddTo(destination);
            Assert.AreEqual(7, destination.Count, "The wrong number of items were added.");
            int[] expected = { 7, 6, 5, 4, 3, 2, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
