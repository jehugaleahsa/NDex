using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the AddUnique methods.
    /// </summary>
    [TestClass]
    public class AddUniqueTester
    {
        #region Real World Example

        /// <summary>
        /// Often when working with set operations, you must first create a set from a list. This can be done
        /// by guaranteeing the uniqueness of each field.
        /// </summary>
        [TestMethod]
        public void TestAddUnique_CreateSet()
        {
            Random random = new Random();

            // build a list of random numbers
            var list = new List<int>(100);
            Sublist.AddGenerated(list.ToSublist(), 100, i => random.Next(100));

            // unique requires that elements be sorted
            Sublist.QuickSort(list.ToSublist());

            // now we create a set from the list
            var destination = new List<int>(100);
            Sublist.AddUnique(list.ToSublist(), destination.ToSublist());

            // check that we have a valid set
            bool isSet = Sublist.IsSet(destination.ToSublist());
            Assert.IsTrue(isSet, "The destinatin was not a valid set.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddUnique_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.AddUnique(list, destination);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddUnique_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.AddUnique(list, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddUnique_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.AddUnique(list, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddUnique_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.AddUnique(list, destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddUnique_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.AddUnique(list, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddUnique_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.AddUnique(list, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddUnique_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.AddUnique(list, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddUnique_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.AddUnique(list, destination, comparison);
        }

        #endregion

        /// <summary>
        /// If all of the items are the same, then only one value should go to the destination.
        /// </summary>
        [TestMethod]
        public void TestAddUnique_AllValuesTheSame_OnlyAddsOne()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 1, 1, 1, });
            var destination = TestHelper.Wrap(new List<int>());
            destination = Sublist.AddUnique(list, destination);
            int[] expected = { 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If all of the items are the same, then only one value should go to the destination.
        /// </summary>
        [TestMethod]
        public void TestAddUnique_AllValuesUnique_AddsAll()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 4, 3, 2, 1 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = Sublist.AddUnique(list, destination, EqualityComparer<int>.Default.Equals);
            int[] expected = { 5, 4, 3, 2, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the last items are the same, then only the first occurrence should go to the destination.
        /// </summary>
        [TestMethod]
        public void TestAddUnique_LastItemsDuplicate()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 4, });
            var destination = TestHelper.Wrap(new List<int>());
            destination = Sublist.AddUnique(list, destination, EqualityComparer<int>.Default);
            int[] expected = { 1, 2, 3, 4 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the first items are the same, then only the first occurrence should go to the destination.
        /// </summary>
        [TestMethod]
        public void TestAddUnique_FirstItemsDuplicate()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 2, 3, 4, });
            var destination = TestHelper.Wrap(new List<int>());
            destination = Sublist.AddUnique(list, destination);
            int[] expected = { 1, 2, 3, 4 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the middle items are the same, then only the first occurrence should go to the destination.
        /// </summary>
        [TestMethod]
        public void TestAddUnique_MiddleItemsDuplicate()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 3, 4, });
            var destination = TestHelper.Wrap(new List<int>());
            destination = Sublist.AddUnique(list, destination);
            int[] expected = { 1, 2, 3, 4 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
