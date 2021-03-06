﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the BinarySearch methods.
    /// </summary>
    [TestClass]
    public class BinarySearchTester
    {
        #region Real World Example

        /// <summary>
        /// We will use binary search to build a list of unique values.
        /// </summary>
        [TestMethod]
        public void TestBinarySearch_BuildUnique()
        {
            Random random = new Random();

            // first build a list of random values.
            var values = new List<int>();
            Sublist.Generate(100, i => random.Next(100)).AddTo(values.ToSublist());

            // now build one with just the unique values
            var set = new List<int>();
            foreach (int value in values)
            {
                SearchResult result = set.ToSublist().BinarySearch(value);
                if (!result.Exists)
                {
                    set.Insert(result.Index, value);
                }
            }

            // check that we have every value only once
            values.ToSublist().Sort().InPlace();
            Assert.IsTrue(set.ToSublist().IsSorted(), "The set is not sorted.");
            bool hasValues = set.ToSublist().IsSubset(values.ToSublist());
            Assert.IsTrue(hasValues, "Not all of the values were copied.");
            Assert.IsFalse(set.ToSublist().FindDuplicates(), "A duplicate was found.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBinarySearch_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            int value = 0;
            list.BinarySearch(value);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBinarySearch_WithComparer_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            int value = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            list.BinarySearch(value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBinarySearch_WithComparison_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            int value = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.BinarySearch(value, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBinarySearch_NullComparer_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            int value = 0;
            IComparer<int> comparer = null;
            list.BinarySearch(value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBinarySearch_NullComparison_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            int value = 0;
            Func<int, int, int> comparison = null;
            list.BinarySearch(value, comparison);
        }

        #endregion

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestBinarySearch_InMiddle_Exists()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            decimal value = 2;
            int index = list.BinarySearch(value, (i, d) => Comparer<decimal>.Default.Compare(i, d));
            Assert.AreEqual(1, index, "The value was not found at the expected index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestBinarySearch_InMiddle_Missing()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            decimal value = 2.5m;
            SearchResult result = list.BinarySearch(value, (i, d) => Comparer<decimal>.Default.Compare(i, d));
            Assert.IsFalse(result.Exists, "The value should not have been found.");
            Assert.AreEqual(2, result.Index, "The value was found or expected at the wrong index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestBinarySearch_AtStart_Exists()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            decimal value = 1;
            int index = list.BinarySearch(value, (i, d) => Comparer<decimal>.Default.Compare(i, d));
            Assert.AreEqual(0, index, "The value was not found at the expected index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestBinarySearch_AtStart_Missing()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            decimal value = .5m;
            SearchResult result = list.BinarySearch(value, (i, d) => Comparer<decimal>.Default.Compare(i, d));
            Assert.IsFalse(result.Exists, "The found should not have been found.");
            Assert.AreEqual(0, result.Index, "The value was found or expected at the wrong index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestBinarySearch_AtEnd_Exists()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            int value = 3;
            int index = list.BinarySearch(value, Comparer<int>.Default);
            Assert.AreEqual(2, index, "The value was not found at the expected index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestBinarySearch_AtEnd_Missing()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            int value = 4;
            SearchResult result = list.BinarySearch(value, Comparer<int>.Default);
            Assert.IsFalse(result.Exists, "The value should not have been found.");
            Assert.AreEqual(3, result.Index, "The value was found or expected at the wrong index.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
