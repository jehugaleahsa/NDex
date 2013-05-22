using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the FindDuplicates methods.
    /// </summary>
    [TestClass]
    public class FindDuplicatesTester
    {
        #region Real World Example

        /// <summary>
        /// We can use FindDuplicates to find duplicates in a list.
        /// </summary>
        [TestMethod]
        public void TestFindDuplicates_DetectDuplicates()
        {
            Random random = new Random();

            // build a list of random values
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list.ToSublist());

            // duplicates must appear next to each other
            list.ToSublist().QuickSort();

            var result = list.ToSublist().FindDuplicates();
            if (!result.Exists)
            {
                Assert.AreEqual(list.Count, list.GroupBy(i => i).Count(), "Duplicates were not detected.");
            }
            else
            {
                var actual = list.ToSublist(result.Index, 2);
                int[] expected = new int[2] { list[result.Index], list[result.Index] };
                Assert.IsTrue(expected.ToSublist().IsEqualTo(actual), "No duplicates were not found.");
            }
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindDuplicates_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            list.FindDuplicates();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindDuplicates_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list.FindDuplicates(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindDuplicates_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list.FindDuplicates(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindDuplicates_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IEqualityComparer<int> comparer = null;
            list.FindDuplicates(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindDuplicates_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, bool> comparison = null;
            list.FindDuplicates(comparison);
        }

        #endregion

        /// <summary>
        /// An index past the end of the list should be returned if there are no duplicates.
        /// </summary>
        [TestMethod]
        public void TestFindDuplicates_NoDuplicates_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, });
            var result = list.FindDuplicates();
            Assert.AreEqual(list.Count, result.Index, "The wrong index was returned.");
            Assert.IsFalse(result.Exists, "No duplicates should have been found.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find reoccurrences at the beginning of a list.
        /// </summary>
        [TestMethod]
        public void TestFindDuplicates_AtBeginning()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 3, 4, });

            var result = list.FindDuplicates();
            Assert.AreEqual(0, result.Index, "The wrong index was returned.");
            Assert.IsTrue(result.Exists, "Duplicates should have been found.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find reoccurrences at the end of a list.
        /// </summary>
        [TestMethod]
        public void TestFindDuplicates_AtEnd()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 3, });

            var result = list.FindDuplicates(EqualityComparer<int>.Default);
            Assert.AreEqual(2, result.Index, "The wrong index was returned.");
            Assert.IsTrue(result.Exists, "Duplicates should have been found.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find reoccurrences in the middle of a list.
        /// </summary>
        [TestMethod]
        public void TestFindDuplicates_InMiddle()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 3, });

            var result = list.FindDuplicates(EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(1, result.Index, "The wrong index was returned.");
            Assert.IsTrue(result.Exists, "Duplicates should have been found.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// The index of the first duplicates should be returned.
        /// </summary>
        [TestMethod]
        public void TestFindDuplicates_MultipleDuplicates_ReturnsLast()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 3, 4, 4, 5 });

            var result = list.FindDuplicates(EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(1, result.Index, "The wrong index was returned.");
            Assert.IsTrue(result.Exists, "Duplicates should have been found.");

            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
