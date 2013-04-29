using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the IsSorted methods.
    /// </summary>
    [TestClass]
    public class IsSortedTester
    {
        #region Real World Example

        /// <summary>
        /// If we know a list is likely to be sorted, it can be quicker to check first before blindly calling sort.
        /// We will use IsSorted to find the largest range in a list of random numbers that are sorted.
        /// </summary>
        [TestMethod]
        public void TestIsSorted_FindLongestSortedRange()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next(0, 100)), list.ToSublist());

            int maxIndex = 0;
            int maxLength = 1;
            for (int index = 0; index < list.Count - maxLength; ++index)
            {
                for (int length = maxLength; length < list.Count - index; ++length)
                {
                    var sublist = list.ToSublist(index, length);
                    if (Sublist.IsSorted(sublist))
                    {
                        maxIndex = index;
                        maxLength = length;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Assert.IsTrue(Sublist.IsSorted(list.ToSublist(maxIndex, maxLength)), "The longest range was not sorted.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSorted_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.IsSorted(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSorted_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.IsSorted(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSorted_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.IsSorted(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSorted_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            Sublist.IsSorted(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSorted_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.IsSorted(list, comparison);
        }

        #endregion

        /// <summary>
        /// An empty list is sorted.
        /// </summary>
        [TestMethod]
        public void TestIsSorted_EmptyList_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>());
            bool isSorted = Sublist.IsSorted(list);
            Assert.IsTrue(isSorted, "An empty list should be sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with one item is sorted.
        /// </summary>
        [TestMethod]
        public void TestIsSorted_ListOfOne_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            bool isSorted = Sublist.IsSorted(list);
            Assert.IsTrue(isSorted, "A list with one item should be sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A reversed list is sorted, with the correct comparer.
        /// </summary>
        [TestMethod]
        public void TestIsSorted_Reversed_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 4, 3, 2, 1 });
            bool isSorted = Sublist.IsSorted(list, (x, y) => Comparer<int>.Default.Compare(y, x));
            Assert.IsTrue(isSorted, "A reversed list should be sorted with the correct comparer.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// An unsorted list should cause the method to return false.
        /// </summary>
        [TestMethod]
        public void TestIsSorted_Unsorted_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 4, 2 });
            bool isSorted = Sublist.IsSorted(list, Comparer<int>.Default.Compare);
            Assert.IsFalse(isSorted, "An unsorted list should not be sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
