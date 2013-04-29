using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the LowerAndBound methods.
    /// </summary>
    [TestClass]
    public class LowerAndUpperBoundTester
    {
        #region Real World Example

        /// <summary>
        /// LowerAndUpperBound finds the range of items that are equal within a sorted list.
        /// We could use this to remove duplicates, in a rather inefficient way.
        /// </summary>
        [TestMethod]
        public void TestLowerAndBound_RemoveDuplicates()
        {
            Random random = new Random();

            // build a list of random values, then sort it
            var list = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next(100)), list.ToSublist());
            Sublist.QuickSort(list.ToSublist());

            // now detect each duplicate and remove all but the first
            for (int index = 0; index != list.Count; ++index)
            {
                var sublist = list.ToSublist(index);
                int value = list[index];
                LowerAndUpperBoundResult bounds = Sublist.LowerAndUpperBound(sublist, value);
                var range = sublist.Nest(bounds.LowerBound, bounds.UpperBound - bounds.LowerBound).Nest(1); // leave the first item alone
                Sublist.RemoveRange(range);
            }

            // at this point, all of the items should be unique and sorted, a.k.a, an ordered set.
            Assert.IsTrue(Sublist.IsSet(list.ToSublist()), "Found a duplicate in the list.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLowerAndUpperBound_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            Sublist.LowerAndUpperBound(list, value);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLowerAndUpperBound_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.LowerAndUpperBound(list, value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLowerAndUpperBound_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.LowerAndUpperBound(list, value, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLowerAndUpperBound_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int value = 0;
            IComparer<int> comparer = null;
            Sublist.LowerAndUpperBound(list, value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLowerAndUpperBound_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int value = 0;
            Func<int, int, int> comparison = null;
            Sublist.LowerAndUpperBound(list, value, comparison);
        }

        #endregion

        /// <summary>
        /// If we try to find the lower and upper bound in a empty list, an empty
        /// Sublist should be returned.
        /// </summary>
        [TestMethod]
        public void TestLowerAndUpperBound_EmptyList_ReturnsEmptySublist()
        {
            var list = TestHelper.Wrap(new List<int>());
            int value = 0;
            LowerAndUpperBoundResult range = Sublist.LowerAndUpperBound(list, value);
            Assert.AreEqual(0, range.UpperBound - range.LowerBound, "The size of the range was not zero.");
        }

        /// <summary>
        /// If the value makes up the entire list, a new Sublist over the entire list should be returned.
        /// </summary>
        [TestMethod]
        public void TestLowerAndUpperBound_SpansEntireList_ReturnsNewSublist()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 1, });
            int value = 1;
            LowerAndUpperBoundResult range = Sublist.LowerAndUpperBound(list, value);
            Assert.AreNotSame(list, range, "The same Sublist was returned.");
            int[] expected = { 1, 1, 1, };
            var sublist = list.Nest(range.LowerBound, range.UpperBound - range.LowerBound);
            Assert.IsTrue(Sublist.AreEqual(list, sublist), "The returned range did not contain the expected values.");
        }

        /// <summary>
        /// If the value makes up the beginning of the list, a new Sublist over the items should be returned.
        /// </summary>
        [TestMethod]
        public void TestLowerAndUpperBound_SpansStartOfList_ReturnsSublist()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 2, });
            int value = 1;
            LowerAndUpperBoundResult range = Sublist.LowerAndUpperBound(list, value);
            int[] expected = { 1, 1, };
            var sublist = list.Nest(range.LowerBound, range.UpperBound - range.LowerBound);
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), sublist), "The returned range did not contain the expected values.");
        }

        /// <summary>
        /// If the value makes up the end of the list, a new Sublist over the items should be returned.
        /// </summary>
        [TestMethod]
        public void TestLowerAndUpperBound_SpansEndOfList_ReturnsSublist()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, });
            int value = 2;
            LowerAndUpperBoundResult range = Sublist.LowerAndUpperBound(list, value);
            int[] expected = { 2, 2, };
            var sublist = list.Nest(range.LowerBound, range.UpperBound - range.LowerBound);
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), sublist), "The returned range did not contain the expected values.");
        }

        /// <summary>
        /// If the value makes up the middle of the list, a new Sublist over the items should be returned.
        /// </summary>
        [TestMethod]
        public void TestLowerAndUpperBound_SpansMiddleOfList_ReturnsSublist()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 3 });
            int value = 2;
            LowerAndUpperBoundResult range = Sublist.LowerAndUpperBound(list, value);
            int[] expected = { 2, 2, };
            var sublist = list.Nest(range.LowerBound, range.UpperBound - range.LowerBound);
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), sublist), "The returned range did not contain the expected values.");
        }

        /// <summary>
        /// We can use a different comparer if the list is reversed.
        /// </summary>
        [TestMethod]
        public void TestLowerAndUpperBound_ReversedAndExists_ReturnsSublist()
        {
            var list = TestHelper.Wrap(new List<int>() { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 });
            int value = 8;
            LowerAndUpperBoundResult range = Sublist.LowerAndUpperBound(list, value, (x, y) => Comparer<int>.Default.Compare(y, x));
            int[] expected = { 8 };
            var sublist = list.Nest(range.LowerBound, range.UpperBound - range.LowerBound);
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), sublist), "The returned range did not contain the expected values.");
        }

        /// <summary>
        /// If the value belongs in the middle, but doesn't exist, the range should exist in the middle somewhere.
        /// </summary>
        [TestMethod]
        public void TestLowerAndUpperBound_MissingInMiddle_ReturnsEmptySublistWithCorrectOffset()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 4, 5, 6, 7, 8, 9, 10 });
            int value = 3;
            LowerAndUpperBoundResult range = Sublist.LowerAndUpperBound(list, value, Comparer<int>.Default);
            Assert.AreEqual(0, range.UpperBound - range.LowerBound, "The count was not zero, even though the value was missing.");
            Assert.AreEqual(2, range.LowerBound, "The offset was not correct.");
        }
    }
}
