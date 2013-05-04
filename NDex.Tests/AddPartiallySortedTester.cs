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
            Sublist.AddGenerated(list.ToSublist(), 100, i => random.Next(100));

            // create a list to hold the top three
            const int numberOfItems = 10;
            var destination = new List<int>(numberOfItems);

            // store the top three results
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            Sublist.AddPartiallySorted(list.ToSublist(), numberOfItems, destination.ToSublist(), comparison);

            // grab the three largest values from largest to smallest
            var expected = new List<int>(numberOfItems);
            for (int round = 0; round != numberOfItems; ++round)
            {
                int maxIndex = Sublist.Maximum(list.ToSublist());
                expected.Add(list[maxIndex]);
                list.RemoveAt(maxIndex);
            }

            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination.ToSublist()), "The top values weren't grabbed.");
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
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.AddPartiallySorted(list, numberOfItems, destination);
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
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.AddPartiallySorted(list, numberOfItems, destination, comparer);
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
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.AddPartiallySorted(list, numberOfItems, destination, comparison);
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
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.AddPartiallySorted(list, numberOfItems, destination);
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
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.AddPartiallySorted(list, numberOfItems, destination, comparer);
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
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.AddPartiallySorted(list, numberOfItems, destination, comparison);
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
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.AddPartiallySorted(list, numberOfItems, destination);
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
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.AddPartiallySorted(list, numberOfItems, destination, comparer);
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
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.AddPartiallySorted(list, numberOfItems, destination, comparison);
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
            Sublist.AddPartiallySorted(list, numberOfItems, destination);
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
            Sublist.AddPartiallySorted(list, numberOfItems, destination, comparer);
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
            Sublist.AddPartiallySorted(list, numberOfItems, destination, comparison);
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
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = null;
            Sublist.AddPartiallySorted(list, numberOfItems, destination, comparer);
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
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.AddPartiallySorted(list, numberOfItems, destination, comparison);
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
            destination = Sublist.AddPartiallySorted(list, numberOfItems, destination);
            int[] expected = { 1, 5, 7 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");
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
            destination = Sublist.AddPartiallySorted(list, numberOfItems, destination, comparer);
            int[] expected = { 1, 5, 7 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");
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
            destination = Sublist.AddPartiallySorted(list, numberOfItems, destination, comparison);
            int[] expected = { 12, 8, 7 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
