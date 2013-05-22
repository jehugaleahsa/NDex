using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the AddPartiallySorted methods.
    /// </summary>
    [TestClass]
    public class AddPartiallySortedTester
    {
        #region Real World Example
        
        /// <summary>
        /// We can use partially sorted to find the top three values in a list without sorting.
        /// </summary>
        [TestMethod]
        public void TestAddPartiallySorted_GrabTopValues()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list.ToSublist());

            // create a list to hold the top three
            const int numberOfItems = 10;
            var destination = new List<int>(numberOfItems);

            // store the top three results
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            list.ToSublist().PartialSort(numberOfItems, comparison).AddTo(destination.ToSublist());

            // grab the three largest values from largest to smallest
            var expected = new List<int>(numberOfItems);
            for (int round = 0; round != numberOfItems; ++round)
            {
                int maxIndex = list.ToSublist().Maximum();
                expected.Add(list[maxIndex]);
                list.RemoveAt(maxIndex);
            }

            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination.ToSublist()), "The top values weren't grabbed.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddPartiallySorted_NullList_Throws()
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
        public void TestAddPartiallySorted_WithComparer_NullList_Throws()
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
        public void TestAddPartiallySorted_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int numberOfItems = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.PartialSort(numberOfItems, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPartiallySorted_NegativeNumberOfItems_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = -1;
            list.PartialSort(numberOfItems);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPartiallySorted_WithComparer_NegativeNumberOfItems_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = -1;
            IComparer<int> comparer = Comparer<int>.Default;
            list.PartialSort(numberOfItems, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPartiallySorted_WithComparison_NegativeNumberOfItems_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = -1;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.PartialSort(numberOfItems, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is larger than the source.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPartiallySorted_NumberOfItemsTooBig_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 1;
            list.PartialSort(numberOfItems);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is larger than the source.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPartiallySorted_WithComparer_NumberOfItemsTooBig_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 1;
            IComparer<int> comparer = Comparer<int>.Default;
            list.PartialSort(numberOfItems, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is larger than the source.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddPartiallySorted_WithComparison_NumberOfItemsTooBig_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 1;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.PartialSort(numberOfItems, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddPartiallySorted_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 0;
            Sublist<List<int>, int> destination = null;
            list.PartialSort(numberOfItems).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddPartiallySorted_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 0;
            Sublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.PartialSort(numberOfItems, comparer).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddPartiallySorted_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 0;
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.PartialSort(numberOfItems, comparison).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddPartiallySorted_NullComparer_Throws()
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
        public void TestAddPartiallySorted_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 0;
            Func<int, int, int> comparison = null;
            list.PartialSort(numberOfItems, comparison);
        }

        #endregion

        /// <summary>
        /// If the number of items is smaller than the source, the added items should be the same had the source been sorted.
        /// </summary>
        [TestMethod]
        public void TestAddPartiallySorted_NumberOfItemsSmaller_HasSmallestItems()
        {
            var list = TestHelper.Wrap(new List<int>() { 8, 5, 12, 1, 7 });
            int numberOfItems = 3;
            var destination = TestHelper.Wrap(new List<int>());
            destination = list.PartialSort(numberOfItems).AddTo(destination);
            int[] expected = { 1, 5, 7 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the number of items is smaller than the source, the added items should be the same had the source been sorted.
        /// </summary>
        [TestMethod]
        public void TestAddPartiallySorted_WithComparer_NumberOfItemsSmaller_HasLargestItems()
        {
            var list = TestHelper.Wrap(new List<int>() { 8, 5, 12, 1, 7 });
            int numberOfItems = 3;
            var destination = TestHelper.Wrap(new List<int>());
            IComparer<int> comparer = Comparer<int>.Default;
            destination = list.PartialSort(numberOfItems, comparer).AddTo(destination);
            int[] expected = { 1, 5, 7 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the number of items is smaller than the source, the added items should be the same had the source been sorted.
        /// </summary>
        [TestMethod]
        public void TestAddPartiallySorted_WithComparison_NumberOfItemsSmaller_HasLargestItems()
        {
            var list = TestHelper.Wrap(new List<int>() { 8, 5, 12, 1, 7 });
            int numberOfItems = 3;
            var destination = TestHelper.Wrap(new List<int>());
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            destination = list.PartialSort(numberOfItems, comparison).AddTo(destination);
            int[] expected = { 12, 8, 7 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
