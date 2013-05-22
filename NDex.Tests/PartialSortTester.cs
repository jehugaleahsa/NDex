﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the PartialSort methods.
    /// </summary>
    [TestClass]
    public class PartialSortTester
    {
        #region Real World Example

        /// <summary>
        /// PartialSort is useful when you want to know the biggest or smallest items in a list without sorting the entire list.
        /// We'll use it to find the biggest items in the list.
        /// </summary>
        [TestMethod]
        public void TestPartialSort_FindTopItems()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>();
            Sublist.Generate(100, i => random.Next()).AddTo(list.ToSublist());

            // now, we'll find the top 10 largest items
            list.ToSublist().PartialSort(10, (x, y) => Comparer<int>.Default.Compare(y, x)).InPlace();

            // now, we'll loop through each item and make sure it is the largest
            for (int index = 0; index != 10; ++index)
            {
                var sublist = list.ToSublist(index);
                int maxIndex = sublist.Maximum();
                Assert.AreEqual(0, maxIndex, "The list was not sorted.");
            }
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartialSort_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int numberOfItems = 0;
            Sublist.PartialSort(list, numberOfItems);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartialSort_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int numberOfItems = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.PartialSort(list, numberOfItems, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartialSort_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int numberOfItems = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.PartialSort(list, numberOfItems, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestPartialSort_NegativeNumberOfItems_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = -1;
            Sublist.PartialSort(list, numberOfItems);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestPartialSort_WithComparer_NegativeNumberOfItems_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = -1;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.PartialSort(list, numberOfItems, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestPartialSort_WithComparison_NegativeNumberOfItems_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = -1;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.PartialSort(list, numberOfItems, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestPartialSort_NumberOfItemsTooBig_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 1;
            Sublist.PartialSort(list, numberOfItems);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestPartialSort_WithComparer_NumberOfItemsTooBig_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 1;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.PartialSort(list, numberOfItems, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestPartialSort_WithComparison_NumberOfItemsTooBig_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 1;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.PartialSort(list, numberOfItems, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartialSort_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 0;
            IComparer<int> comparer = null;
            Sublist.PartialSort(list, numberOfItems, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartialSort_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 0;
            Func<int, int, int> comparison = null;
            Sublist.PartialSort(list, numberOfItems, comparison);
        }

        #endregion

        /// <summary>
        /// If we try to sort an empty list, nothing should happen.
        /// </summary>
        [TestMethod]
        public void TestPartialSort_EmptyList_DoesNothing()
        {
            var list = TestHelper.Wrap(new List<int>());
            list.PartialSort(0).InPlace();
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we try to sort an empty list, nothing should happen.
        /// </summary>
        [TestMethod]
        public void TestPartialSort_ListOfOne_DoesNothing()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            list.PartialSort(1).InPlace();
            int[] expected = { 1 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list), "Could not sort a list with one item.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we sort the entire list, it should be equivalent to calling heap sort.
        /// </summary>
        [TestMethod]
        public void TestPartialSort_NumberEqualsCount_SortsEntireList()
        {
            var list = TestHelper.Wrap(new List<int>() { 3, 4, 1, 2, 7, 6, 5 });
            list.PartialSort(list.Count).InPlace();
            int[] expected = { 1, 2, 3, 4, 5, 6, 7, };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list), "The items were not in the expected order.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the smallest items are in the back, they should make their way to the front.
        /// </summary>
        [TestMethod]
        public void TestPartialSort_SmallestItemsInBack_PartiallySorts()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 4, 7, 6, 1, 2, 3 });
            list.PartialSort(3).InPlace();
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list.Nest(0, 3)), "The items were not in the expected order.");
            var expectedRemaining = new HashSet<int>() { 4, 5, 6, 7 };
            Assert.IsTrue(expectedRemaining.SetEquals(list.Nest(3)), "The remaining were destroyed.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the smallest items are in the front, they should stay in the front.
        /// </summary>
        [TestMethod]
        public void TestPartialSort_SmallestItemsInFront_PartiallySorts()
        {
            var list = TestHelper.Wrap(new List<int>() { 3, 1, 2, 5, 4, 7, 6, });
            list.PartialSort(3, Comparer<int>.Default).InPlace();
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list.Nest(0, 3)), "The items were not in the expected order.");
            var expectedRemaining = new HashSet<int>() { 4, 5, 6, 7 };
            Assert.IsTrue(expectedRemaining.SetEquals(list.Nest(3)), "The remaining were destroyed.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the smallest items are in the middle, they should move to the front.
        /// </summary>
        [TestMethod]
        public void TestPartialSort_SmallestItemsInMiddle_PartiallySorts()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 4, 3, 1, 2, 7, 6, });
            list.PartialSort(3, Comparer<int>.Default.Compare).InPlace();
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list.Nest(0, 3)), "The items were not in the expected order.");
            var expectedRemaining = new HashSet<int>() { 4, 5, 6, 7 };
            Assert.IsTrue(expectedRemaining.SetEquals(list.Nest(3)), "The remaining were destroyed.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the smallest items are dispersed, they should move to the front.
        /// </summary>
        [TestMethod]
        public void TestPartialSort_SmallestItemsAllOver_PartiallySorts()
        {
            var list = TestHelper.Wrap(new List<int>() { 3, 5, 4, 1, 7, 6, 2, });
            list.PartialSort(3).InPlace();
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list.Nest(0, 3)), "The items were not in the expected order.");
            var expectedRemaining = new HashSet<int>() { 4, 5, 6, 7 };
            Assert.IsTrue(expectedRemaining.SetEquals(list.Nest(3)), "The remaining were destroyed.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
