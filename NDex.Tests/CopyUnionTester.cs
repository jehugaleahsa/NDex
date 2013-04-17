using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;

namespace NDex.Test
{
    /// <summary>
    /// Tests the CopyUnion methods.
    /// </summary>
    [TestClass]
    public class CopyUnionTester
    {
        #region Real World Examples
        
        /// <summary>
        /// Union is used to combine the items that are shared across two sets without introducing duplicates.
        /// </summary>
        [TestMethod]
        public void TestCopyUnion()
        {
            Random random = new Random();

            // build two lists
            var list1 = new List<int>(50);
            Sublist.Grow(list1, 50, () => random.Next(100));
            var list2 = new List<int>(50);
            Sublist.Grow(list2, 50, () => random.Next(100));

            // we must make both lists sets
            Sublist.RemoveRange(list1.ToSublist(Sublist.MakeSet(list1.ToSublist())));
            Sublist.RemoveRange(list2.ToSublist(Sublist.MakeSet(list2.ToSublist())));

            // now we'll build a new set containing all the items
            var destination = new List<int>(100);
            Sublist.Grow(destination, 100, 0);
            int result = Sublist.CopyUnion(list1.ToSublist(), list2.ToSublist(), destination.ToSublist());
            Sublist.RemoveRange(destination.ToSublist(result));

            // make sure the new set contains both of the original sets
            bool contains1 = Sublist.IsSubset(destination.ToSublist(), list1.ToSublist());
            Assert.IsTrue(contains1, "The union did not contain all the items from the first set.");
            bool contains2 = Sublist.IsSubset(destination.ToSublist(), list2.ToSublist());
            Assert.IsTrue(contains1, "The union did not contain all the items from the second set.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnion_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyUnion(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnion_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.CopyUnion(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnion_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.CopyUnion(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnion_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyUnion(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnion_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.CopyUnion(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnion_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.CopyUnion(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnion_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.CopyUnion(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnion_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.CopyUnion(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnion_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.CopyUnion(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnion_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = null;
            Sublist.CopyUnion(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnion_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.CopyUnion(list1, list2, destination, comparison);
        }

        #endregion

        /// <summary>
        /// The union of equal sets is the same set.
        /// </summary>
        [TestMethod]
        public void TestCopyUnion_EqualLists_CopiesAllItems()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            IComparer<int> comparer = Comparer<int>.Default;

            CopyTwoSourcesResult result = Sublist.CopyUnion(list, list, destination, comparer);
            Assert.AreEqual(list.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 3, 4, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// The union of disjoint sets is every item.
        /// </summary>
        [TestMethod]
        public void TestCopyUnion_Disjoint_CopiesAll()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 2, 4, 6 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, });

            CopyTwoSourcesResult result = Sublist.CopyUnion(list1, list2, destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 3, 4, 5, 6 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the first list is shorter, the remaining items in the second list are copied.
        /// </summary>
        [TestMethod]
        public void TestCopyUnion_List1Shorter_RemainingCopied()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            CopyTwoSourcesResult result = Sublist.CopyUnion(list1, list2, destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(3, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 3, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the second list is shorter, the remaining items in the first list are copied.
        /// </summary>
        [TestMethod]
        public void TestCopyUnion_List2Shorter_RemainingCopied()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            CopyTwoSourcesResult result = Sublist.CopyUnion(list1, list2, destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(3, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 3, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we reverse the comparison, the items must match the sort criteria.
        /// </summary>
        [TestMethod]
        public void TestCopyUnion_ReversedOrder()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 3, 2, 1 });
            var list2 = TestHelper.Wrap(new List<int>() { 3, 2 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, });
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);

            CopyTwoSourcesResult result = Sublist.CopyUnion(list1, list2, destination, comparison);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(3, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 3, 2, 1, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is too small, the algorithm should stop prematurely.
        /// </summary>
        [TestMethod]
        public void TestCopyUnion_DestinationTooSmall_StopsPrematurely()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 2, 4, 6 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });

            CopyTwoSourcesResult result = Sublist.CopyUnion(list1, list2, destination);
            Assert.AreEqual(2, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(1, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
