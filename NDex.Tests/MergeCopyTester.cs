using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the MergeCopy methods.
    /// </summary>
    [TestClass]
    public class MergeCopyTester
    {
        #region Real World Example

        /// <summary>
        /// Merge can be used to combine two sorted lists maintaining sort order.
        /// </summary>
        [TestMethod]
        public void TestMergeCopy_CombineSortedLists_StaySorted()
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
            Sublist.Generate(100, 0).AddTo(destination.ToSublist());
            int result = list1.ToSublist().Merge(list2.ToSublist()).CopyTo(destination.ToSublist());
            Assert.AreEqual(destination.Count, result, "Not all of the items were copied.");

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
        public void TestMergeCopy_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            list1.Merge(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeCopy_WithComparer_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Merge(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeCopy_WithComparison_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Merge(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeCopy_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            list1.Merge(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeCopy_WithComparer_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Merge(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeCopy_WithComparison_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Merge(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeCopy_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            list1.Merge(list2).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeCopy_WithComparer_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Merge(list2, comparer).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeCopy_WithComparison_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Merge(list2, comparison).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeCopy_NullComparer_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list1.Merge(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeCopy_NullComparison_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list1.Merge(list2, comparison);
        }

        #endregion

        /// <summary>
        /// If we merge two lists, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestMergeCopy_Merges()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 2, 4, 6, 8 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 1, 3, 5, 7 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 });
            IComparer<int> comparer = Comparer<int>.Default;
            var result = list1.Merge(list2, comparer).CopyTo(destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong number of items were added.");
            int[] expected = { 1, 2, 3, 4, 5, 6, 7, 8 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestMergeCopy_HandlesDuplicates()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 2, 4, 6, 8, });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 5, 7 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            var result = list1.Merge(list2).CopyTo(destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong number of items were added.");
            int[] expected = { 1, 2, 2, 3, 4, 5, 6, 7, 8 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists in reverse, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestMergeCopy_MergeInReverse()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 6, 4, 2 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 7, 5, 3, 1 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, 0, });
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            var result = list1.Merge(list2, comparison).CopyTo(destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong number of items were added.");
            int[] expected = { 7, 6, 5, 4, 3, 2, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists in reverse, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestMergeCopy_DestinationTooSmall_StopsPrematurely()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 2, 4, 6 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 1, 3, 5 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, });
            var result = list1.Merge(list2).CopyTo(destination);
            Assert.AreEqual(1, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(2, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong number of items were added.");
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
