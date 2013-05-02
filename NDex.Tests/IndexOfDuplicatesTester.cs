using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    /// <summary>
    /// Tests the IndexOfDuplicates methods.
    /// </summary>
    [TestClass]
    public class IndexOfDuplicatesTester
    {
        #region Real World Example

        /// <summary>
        /// We can use IndexOfDuplicates to find duplicates in a list.
        /// </summary>
        [TestMethod]
        public void TestIndexOfDuplicates_DetectDuplicates()
        {
            Random random = new Random();

            // build a list of random values
            var list = new List<int>(100);
            Sublist.AddGenerated(list.ToSublist(), 100, i => random.Next(100));

            // duplicates must appear next to each other
            Sublist.QuickSort(list.ToSublist());

            int index = Sublist.IndexOfDuplicates(list.ToSublist());
            if (index == list.Count)
            {
                Assert.AreEqual(list.Count, list.GroupBy(i => i).Count(), "Duplicates were not detected.");
            }
            else
            {
                var actual = list.ToSublist(index, 2);
                int[] expected = new int[2] { list[index], list[index] };
                Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), actual), "No duplicates were not found.");
            }
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfDuplicates_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.IndexOfDuplicates(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfDuplicates_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.IndexOfDuplicates(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfDuplicates_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.IndexOfDuplicates(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfDuplicates_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.IndexOfDuplicates(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfDuplicates_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.IndexOfDuplicates(list, comparison);
        }

        #endregion

        /// <summary>
        /// An index past the end of the list should be returned if there are no duplicates.
        /// </summary>
        [TestMethod]
        public void TestIndexOfDuplicates_NoDuplicates_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, });
            int index = Sublist.IndexOfDuplicates(list);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find reoccurrences at the beginning of a list.
        /// </summary>
        [TestMethod]
        public void TestIndexOfDuplicates_AtBeginning()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 3, 4, });

            int index = Sublist.IndexOfDuplicates(list);
            Assert.AreEqual(0, index, "The wrong index was returned.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find reoccurrences at the end of a list.
        /// </summary>
        [TestMethod]
        public void TestIndexOfDuplicates_AtEnd()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 3, });

            int index = Sublist.IndexOfDuplicates(list, EqualityComparer<int>.Default);
            Assert.AreEqual(2, index, "The wrong index was returned.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find reoccurrences in the middle of a list.
        /// </summary>
        [TestMethod]
        public void TestIndexOfDuplicates_InMiddle()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 3, });

            int index = Sublist.IndexOfDuplicates(list, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(1, index, "The wrong index was returned.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// The index of the first duplicates should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOfDuplicates_MultipleDuplicates_ReturnsLast()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 3, 4, 4, 5 });

            int index = Sublist.IndexOfDuplicates(list, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(1, index, "The wrong index was returned.");

            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
