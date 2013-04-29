using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the IsSubsetUntil methods.
    /// </summary>
    [TestClass]
    public class IsSubsetUntilTester
    {
        #region Real World Example

        /// <summary>
        /// We should be able to verify that multiples of four are also multiples of two.
        /// </summary>
        [TestMethod]
        public void TestIsSubsetUntil()
        {
            Random random = new Random();

            // build all evens
            var evens = new List<int>();
            Sublist.Add(Enumerable.Range(0, 200).Select(i => i * 2), evens.ToSublist());

            // build multiples of four
            var fours = new List<int>();
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next(100) * 4), fours.ToSublist());
            Sublist.QuickSort(fours.ToSublist()); // items must be sorted
            Sublist.RemoveRange(fours.ToSublist(Sublist.RemoveDuplicates(fours.ToSublist()))); // items must be distinct.

            // there shouldn't be a multiple of four that isn't also a multiple of two
            int index = Sublist.IsSubsetUntil(evens.ToSublist(), fours.ToSublist());
            Assert.AreEqual(fours.Count, index, "Found a multiple of four that wasn't a multiple of two.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubsetUntil_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist.IsSubsetUntil(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubsetUntil_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.IsSubsetUntil(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubsetUntil_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.IsSubsetUntil(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubsetUntil_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist.IsSubsetUntil(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubsetUntil_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.IsSubsetUntil(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubsetUntil_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.IsSubsetUntil(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubsetUntil_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = null;
            Sublist.IsSubsetUntil(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSubsetUntil_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.IsSubsetUntil(list1, list2, comparison);
        }

        #endregion

        /// <summary>
        /// If the second list is empty, then all of its values are in the first list.
        /// </summary>
        [TestMethod]
        public void TestIsSubsetUntil_List2Empty_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>());
            int index = Sublist.IsSubsetUntil(list1, list2, Comparer<int>.Default.Compare);
            Assert.AreEqual(list2.Count, index, "An empty list was not a subset of another.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the second list is empty, then all of its values are in the first list, even if it is empty.
        /// </summary>
        [TestMethod]
        public void TestIsSubsetUntil_BothListsEmpty_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>());
            var list2 = TestHelper.Wrap(new List<int>());
            int index = Sublist.IsSubsetUntil(list1, list2, Comparer<int>.Default.Compare);
            Assert.AreEqual(list2.Count, index, "An empty list was not a subset of another.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but the first item is present.
        /// </summary>
        [TestMethod]
        public void TestIsSubsetUntil_MissingFirst_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int index = Sublist.IsSubsetUntil(list1, list2, Comparer<int>.Default);
            Assert.AreEqual(0, index, "Not all items were in the parent, but still succeeded.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but the last item is present.
        /// </summary>
        [TestMethod]
        public void TestIsSubsetUntil_MissingLast_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int index = Sublist.IsSubsetUntil(list1, list2, Comparer<int>.Default.Compare);
            Assert.AreEqual(2, index, "Not all items were in the parent, but still succeeded.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but a middle item is present.
        /// </summary>
        [TestMethod]
        public void TestIsSubsetUntil_MissingMiddle_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int index = Sublist.IsSubsetUntil(list1, list2, Comparer<int>.Default);
            Assert.AreEqual(1, index, "Not all items were in the parent, but still succeeded.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }
    }
}
