using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the UnionCopy methods.
    /// </summary>
    [TestClass]
    public class UnionCopyTester
    {
        #region Real World Examples
        
        /// <summary>
        /// Union is used to combine the items that are shared across two sets without introducing duplicates.
        /// </summary>
        [TestMethod]
        public void TestUnionCopy()
        {
            Random random = new Random();

            // build two lists
            var list1 = new List<int>(50);
            Sublist.Generate(50, i => random.Next(100)).AddTo(list1.ToSublist());
            var list2 = new List<int>(50);
            Sublist.Generate(50, i => random.Next(100)).AddTo(list2.ToSublist());

            // we must make both lists sets
            list1.ToSublist(list1.ToSublist().MakeSet().InPlace()).Clear();
            list2.ToSublist(list2.ToSublist().MakeSet().InPlace()).Clear();

            // now we'll build a new set containing all the items
            var destination = new List<int>(100);
            Sublist.Generate(100, 0).AddTo(destination.ToSublist());
            int result = list1.ToSublist().Union(list2.ToSublist()).CopyTo(destination.ToSublist());
            destination.ToSublist(result).Clear();

            // make sure the new set contains both of the original sets
            bool contains1 = list1.ToSublist().IsSubset(destination.ToSublist());
            Assert.IsTrue(contains1, "The union did not contain all the items from the first set.");
            bool contains2 = list2.ToSublist().IsSubset(destination.ToSublist());
            Assert.IsTrue(contains1, "The union did not contain all the items from the second set.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionCopy_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            list1.Union(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionCopy_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Union(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionCopy_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Union(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionCopy_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            list1.Union(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionCopy_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Union(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionCopy_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Union(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionCopy_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            list1.Union(list2).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionCopy_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Union(list2, comparer).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionCopy_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Union(list2, comparison).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionCopy_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = null;
            list1.Union(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionCopy_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = null;
            list1.Union(list2, comparison);
        }

        #endregion

        /// <summary>
        /// The union of equal sets is the same set.
        /// </summary>
        [TestMethod]
        public void TestUnionCopy_EqualLists_CopiesAllItems()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            IComparer<int> comparer = Comparer<int>.Default;

            var result = list.Union(list, comparer).CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 3, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// The union of disjoint sets is every item.
        /// </summary>
        [TestMethod]
        public void TestUnionCopy_Disjoint_CopiesAll()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 2, 4, 6 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0, });

            var result = list1.Union(list2).CopyTo(destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 3, 4, 5, 6 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the first list is shorter, the remaining items in the second list are copied.
        /// </summary>
        [TestMethod]
        public void TestUnionCopy_List1Shorter_RemainingCopied()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = list1.Union(list2).CopyTo(destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(3, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 3, 0, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the second list is shorter, the remaining items in the first list are copied.
        /// </summary>
        [TestMethod]
        public void TestUnionCopy_List2Shorter_RemainingCopied()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = list1.Union(list2).CopyTo(destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(3, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 3, 0, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we reverse the comparison, the items must match the sort criteria.
        /// </summary>
        [TestMethod]
        public void TestUnionCopy_ReversedOrder()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 3, 2, 1 });
            var list2 = TestHelper.Wrap(new List<int>() { 3, 2 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, });
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);

            var result = list1.Union(list2, comparison).CopyTo(destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(3, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 3, 2, 1, 0, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is too small, the algorithm should stop prematurely.
        /// </summary>
        [TestMethod]
        public void TestUnionCopy_DestinationTooSmall_StopsPrematurely()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 2, 4, 6 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });

            var result = list1.Union(list2).CopyTo(destination);
            Assert.AreEqual(2, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(1, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
