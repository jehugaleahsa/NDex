using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the ContainsDuplicates methods.
    /// </summary>
    [TestClass]
    public class ContainsDuplicatesTester
    {
        #region Real World Example

        /// <summary>
        /// We can determine if a list has unique values.
        /// </summary>
        [TestMethod]
        public void TestContainsDuplicates_DetectDuplicates()
        {
            Random random = new Random();

            // build a list of random values
            var list = new List<int>(100);
            Sublist.Grow(list, 100, () => random.Next(100));

            // force duplicates
            int[] duplicates = new int[2];
            Sublist.Fill(duplicates.ToSublist(), 4);
            Sublist.Add(duplicates.ToSublist(), list.ToSublist()); // add the duplicates

            // requires list to be sorted
            Sublist.QuickSort(list.ToSublist());

            Assert.IsTrue(Sublist.ContainsDuplicates(list.ToSublist()), "Did not detect the duplicates.");
            Sublist.RemoveDuplicates(list.ToSublist());
            Assert.IsFalse(Sublist.ContainsDuplicates(list.ToSublist()), "Found a duplicate.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsDuplicates_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.ContainsDuplicates(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsDuplicates_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.ContainsDuplicates(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsDuplicates_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.ContainsDuplicates(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsDuplicates_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.ContainsDuplicates(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsDuplicates_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.ContainsDuplicates(list, comparison);
        }

        #endregion

        /// <summary>
        /// False should be returned if there are no duplicates.
        /// </summary>
        [TestMethod]
        public void TestContainsDuplicates_NoDuplicates_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, });
            Assert.IsFalse(Sublist.ContainsDuplicates(list), "Did not find the duplicates.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find reoccurrences at the beginning of a list.
        /// </summary>
        [TestMethod]
        public void TestContainsDuplicates_AtBeginning()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 3, 4, });
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;

            Assert.IsTrue(Sublist.ContainsDuplicates(list, comparer), "Did not find the duplicates.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find reoccurrences at the end of a list.
        /// </summary>
        [TestMethod]
        public void TestContainsDuplicates_AtEnd()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 3, });
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;

            Assert.IsTrue(Sublist.ContainsDuplicates(list, comparison), "Did not find the duplicates.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find reoccurrences in the middle of a list.
        /// </summary>
        [TestMethod]
        public void TestContainsDuplicates_InMiddle()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 3, });

            Assert.IsTrue(Sublist.ContainsDuplicates(list), "Did not find the duplicates.");

            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
