using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the UnionAdd methods.
    /// </summary>
    [TestClass]
    public class UnionAddTester
    {
        #region Real World Examples
        
        /// <summary>
        /// Union is used to combine the items that are shared across two sets without introducing duplicates.
        /// </summary>
        [TestMethod]
        public void TestUnionAdd()
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
            var destination = new List<int>();
            list1.ToSublist().Union(list2.ToSublist()).AddTo(destination.ToSublist());

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
        public void TestUnionAdd_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            list1.Union(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionAdd_WithComparer_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Union(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionAdd_WithComparison_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Union(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionAdd_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            list1.Union(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionAdd_WithComparer_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Union(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionAdd_WithComparison_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Union(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionAdd_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            list1.Union(list2).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionAdd_WithComparer_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Union(list2, comparer).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionAdd_WithComparison_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Union(list2, comparison).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionAdd_NullComparer_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list1.Union(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnionAdd_NullComparison_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list1.Union(list2, comparison);
        }

        #endregion

        /// <summary>
        /// The union of equal sets is the same set.
        /// </summary>
        [TestMethod]
        public void TestUnionAdd_EqualLists_AddsAllItems()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list.Union(list).AddTo(destination);
            int[] expected = { 1, 2, 3, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// The union of disjoint sets is every item.
        /// </summary>
        [TestMethod]
        public void TestUnionAdd_Disjoint_AddsAll()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 1, 3, 5 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 2, 4, 6 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list1.Union(list2).AddTo(destination);
            int[] expected = { 1, 2, 3, 4, 5, 6 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the first list is shorter, the remaining items in the second list are copied.
        /// </summary>
        [TestMethod]
        public void TestUnionAdd_List1Shorter_RemainingCopied()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 1, 2 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list1.Union(list2).AddTo(destination);
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the second list is shorter, the remaining items in the first list are copied.
        /// </summary>
        [TestMethod]
        public void TestUnionAdd_List2Shorter_RemainingCopied()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 1, 2 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list1.Union(list2).AddTo(destination);
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we reverse the comparer, the items must match the sort criteria.
        /// </summary>
        [TestMethod]
        public void TestUnionAdd_WithComparer()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 2, 3 });
            var destination = TestHelper.Wrap(new List<int>());
            IComparer<int> comparer = Comparer<int>.Default;
            destination = list1.Union(list2, comparer).AddTo(destination);
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we reverse the comparison, the items must match the sort criteria.
        /// </summary>
        [TestMethod]
        public void TestUnionAdd_WithComparison()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 3, 2, 1 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 3, 2 });
            var destination = TestHelper.Wrap(new List<int>());
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            destination = list1.Union(list2, comparison).AddTo(destination);
            int[] expected = { 3, 2, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
