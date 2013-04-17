using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;

namespace NDex.Test
{
    /// <summary>
    /// Tests the IsSet methods.
    /// </summary>
    [TestClass]
    public class IsSetTester
    {
        #region Real World Examples

        /// <summary>
        /// If we take random numbers, sort them and remove duplicates, the list represents a set.
        /// </summary>
        [TestMethod]
        public void TestIsSet_SortedAndUnique()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(5);
            Sublist.Grow(list, 5, () => random.Next(0, 100));

            bool isSorted = Sublist.IsSorted(list.ToSublist());
            bool hasDuplicates = Sublist.ContainsDuplicates(list.ToSublist());
            bool expected = isSorted && !hasDuplicates;
            bool actual = Sublist.IsSet(list.ToSublist());
            Assert.AreEqual(expected, actual, "A sorted list without duplicates is a set.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSet_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.IsSet(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSet_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.IsSet(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSet_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.IsSet(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSet_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            Sublist.IsSet(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSet_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.IsSet(list, comparison);
        }

        #endregion

        /// <summary>
        /// An empty list is a valid set.
        /// </summary>
        [TestMethod]
        public void TestIsSet_EmptyList_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>());
            bool isSet = Sublist.IsSet(list);
            Assert.IsTrue(isSet, "An empty list should be a valid set.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// An list with one item is a valid set.
        /// </summary>
        [TestMethod]
        public void TestIsSet_SizeOfOne_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            bool isSet = Sublist.IsSet(list);
            Assert.IsTrue(isSet, "A list of size one should be a valid set.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// An list with two unique items in sorted order is a valid set.
        /// </summary>
        [TestMethod]
        public void TestIsSet_SizeOfTwo()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2 });
            bool isSet = Sublist.IsSet(list, Comparer<int>.Default);
            Assert.IsTrue(isSet, "The list should have been a valid set.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we build a set in reverse, it should still be a set with the appropriate comparison.
        /// </summary>
        [TestMethod]
        public void TestIsSet_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 4, 3, 2, 1 });
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            bool isSet = Sublist.IsSet(list, comparison);
            Assert.IsTrue(isSet, "The list should have been a valid set.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we build a list that is not a set, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsSet_ContainsDuplicates_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 3, });
            bool isSet = Sublist.IsSet(list);
            Assert.IsFalse(isSet, "The list was not a valid set.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we build a list that is not a set, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsSet_OutOfOrder_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 4, 3, });
            bool isSet = Sublist.IsSet(list);
            Assert.IsFalse(isSet, "The list was not a valid set.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
