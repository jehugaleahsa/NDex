using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the LowerBound methods.
    /// </summary>
    [TestClass]
    public class LowerBoundTester
    {
        #region Real World Example

        /// <summary>
        /// LowerBound is useful for finding the first occurrence of a value in a sorted list.
        /// We'll use LowerBound to build a set.
        /// </summary>
        [TestMethod]
        public void TestLowerBound_BuildSet()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next(100)), list.ToSublist());

            // only add unique items in sorted order
            var set = new List<int>();
            foreach (int value in list)
            {
                int index = Sublist.LowerBound(set.ToSublist(), value);
                if (index == set.Count || set[index] != value)
                {
                    set.Insert(index, value);
                }
            }

            // check that all items are present, sorted and unique
            Sublist.QuickSort(list.ToSublist());
            Assert.IsTrue(Sublist.IsSorted(set.ToSublist()), "The set is not sorted.");
            bool hasValues = Sublist.IsSubset(list.ToSublist(), set.ToSublist());
            Assert.IsTrue(hasValues, "Not all of the values were copied.");
            Assert.IsFalse(Sublist.ContainsDuplicates(set.ToSublist()), "A duplicate was found.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLowerBound_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            Sublist.LowerBound(list, value);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLowerBound_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.LowerBound(list, value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLowerBound_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.LowerBound(list, value, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLowerBound_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int value = 0;
            IComparer<int> comparer = null;
            Sublist.LowerBound(list, value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLowerBound_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int value = 0;
            Func<int, int, int> comparison = null;
            Sublist.LowerBound(list, value, comparison);
        }

        #endregion

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestLowerBound_InMiddle_Exists()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            decimal value = 2;
            int index = Sublist.LowerBound(list, value, (i, d) => Comparer<decimal>.Default.Compare(i, d));
            Assert.AreEqual(1, index, "The value was not found at the expected index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestLowerBound_InMiddle_Missing()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            decimal value = 2.5m;
            int index = Sublist.LowerBound(list, value, (i, d) => Comparer<decimal>.Default.Compare(i, d));
            Assert.AreEqual(2, index, "The value was found or expected at the wrong index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestLowerBound_AtStart_Exists()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            decimal value = 1;
            int index = Sublist.LowerBound(list, value, (i, d) => Comparer<decimal>.Default.Compare(i, d));
            Assert.AreEqual(0, index, "The value was not found at the expected index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestLowerBound_AtStart_Missing()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            decimal value = .5m;
            int index = Sublist.LowerBound(list, value, (i, d) => Comparer<decimal>.Default.Compare(i, d));
            Assert.AreEqual(0, index, "The value was found or expected at the wrong index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestLowerBound_AtEnd_Exists()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            int value = 3;
            int index = Sublist.LowerBound(list, value, Comparer<int>.Default);
            Assert.AreEqual(2, index, "The value was not found at the expected index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestLowerBound_WithComparer_AtEnd_Missing()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            int value = 4;
            int index = Sublist.LowerBound(list, value, Comparer<int>.Default);
            Assert.AreEqual(3, index, "The value was found or expected at the wrong index.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
