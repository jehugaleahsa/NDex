using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    /// <summary>
    /// Tests the IsSubset methods.
    /// </summary>
    [TestClass]
    public class IsSubsetTester
    {
        #region Real World Example

        /// <summary>
        /// We should be able to verify that multiples of four are also multiples of two.
        /// </summary>
        [TestMethod]
        public void TestIsSubset()
        {
            Random random = new Random();

            // build all evens
            var evens = new List<int>();
            Sublist.AddGenerated(evens.ToSublist(), 200, i => i * 2);

            // build multiples of four
            var fours = new List<int>();
            Sublist.AddGenerated(fours.ToSublist(), 100, i => random.Next(100) * 4);
            Sublist.QuickSort(fours.ToSublist()); // items must be sorted
            Sublist.RemoveRange(fours.ToSublist(Sublist.RemoveDuplicates(fours.ToSublist()))); // items must be distinct.

            // there shouldn't be a multiple of four that isn't also a multiple of two
            bool result = Sublist.IsSubset(evens.ToSublist(), fours.ToSublist());
            Assert.IsTrue(result, "Found a multiple of four that wasn't a multiple of two.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist.IsSubset(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.IsSubset(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.IsSubset(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist.IsSubset(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.IsSubset(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.IsSubset(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = null;
            Sublist.IsSubset(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubset_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.IsSubset(list1, list2, comparison);
        }

        #endregion

        /// <summary>
        /// If the second list is empty, then all of its values are in the first list.
        /// </summary>
        [TestMethod]
        public void TestIsSubset_List2Empty_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>());
            bool result = Sublist.IsSubset(list1, list2, Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "An empty list was not a subset of another.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the second list is empty, then all of its values are in the first list, even if it is empty.
        /// </summary>
        [TestMethod]
        public void TestIsSubset_BothListsEmpty_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>());
            var list2 = TestHelper.Wrap(new List<int>());
            bool result = Sublist.IsSubset(list1, list2, Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "An empty list was not a subset of another.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but the first item is present.
        /// </summary>
        [TestMethod]
        public void TestIsSubset_MissingFirst_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            bool result = Sublist.IsSubset(list1, list2, Comparer<int>.Default);
            Assert.IsFalse(result, "Not all items were in the parent, but still succeeded.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but the last item is present.
        /// </summary>
        [TestMethod]
        public void TestIsSubset_MissingLast_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            bool result = Sublist.IsSubset(list1, list2, Comparer<int>.Default.Compare);
            Assert.IsFalse(result, "Not all items were in the parent, but still succeeded.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but a middle item is present.
        /// </summary>
        [TestMethod]
        public void TestIsSubset_MissingMiddle_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            bool result = Sublist.IsSubset(list1, list2, Comparer<int>.Default);
            Assert.IsFalse(result, "Not all items were in the parent, but still succeeded.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }
    }
}
