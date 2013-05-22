using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the CopyPartiallySorted methods.
    /// </summary>
    [TestClass]
    public class CopyPartiallySortedTester
    {
        #region Real World Example
        
        /// <summary>
        /// We can use partially sorted to find the top three values in a list without sorting.
        /// </summary>
        [TestMethod]
        public void TestCopyPartiallySorted_GrabTopValues()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list.ToSublist());

            // create a list to hold the top three
            const int numberOfItems = 10;
            var destination = new int[numberOfItems];

            // store the top three results
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            int result = list.ToSublist().PartialSort(numberOfItems, comparison).CopyTo(destination.ToSublist());
            Assert.AreEqual(destination.Length, result, "The wrong number of items were copied.");

            // grab the three largest values from largest to smallest
            var expected = new List<int>(numberOfItems);
            for (int round = 0; round != numberOfItems; ++round)
            {
                int maxIndex = Sublist.Maximum(list.ToSublist());
                expected.Add(list[maxIndex]);
                list.RemoveAt(maxIndex);
            }

            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), destination.ToSublist()), "The top values weren't grabbed.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyPartiallySorted_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int numberOfItems = 0;
            list.PartialSort(numberOfItems);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyPartiallySorted_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int numberOfItems = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            list.PartialSort(numberOfItems, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyPartiallySorted_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int numberOfItems = 0;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.PartialSort(numberOfItems, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyPartiallySorted_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 0;
            Sublist<List<int>, int> destination = null;
            list.PartialSort(numberOfItems).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyPartiallySorted_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 0;
            Sublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.PartialSort(numberOfItems, comparer).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyPartiallySorted_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 0;
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.PartialSort(numberOfItems, comparison).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyPartiallySorted_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 0;
            IComparer<int> comparer = null;
            list.PartialSort(numberOfItems, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyPartiallySorted_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 0;
            Func<int, int, int> comparison = null;
            list.PartialSort(numberOfItems, comparison);
        }

        #endregion

        /// <summary>
        /// If the destination is smaller than the source, the copied items should be the same had the source been sorted.
        /// </summary>
        [TestMethod]
        public void TestCopyPartiallySorted_DestinationSmaller_HasSmallestItems()
        {
            var list = TestHelper.Wrap(new List<int>() { 8, 5, 12, 1, 7 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            IComparer<int> comparer = Comparer<int>.Default;
            var result = list.PartialSort(destination.Count, comparer).CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong destination index was returned.");
            int[] expected = { 1, 5, 7 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the sorce is smaller than the destination, the entire destination should be sorted with some space left over.
        /// </summary>
        [TestMethod]
        public void TestCopyPartiallySorted_SourceSmaller_HeapSort()
        {
            var list = TestHelper.Wrap(new List<int>() { 8, 5, 12, 1, 7 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0 });
            var result = list.PartialSort(list.Count).CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(5, result.DestinationOffset, "The wrong destination index was returned.");
            int[] expected = { 1, 5, 7, 8, 12, 0 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
