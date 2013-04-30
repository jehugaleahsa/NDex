using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the MakeSet methods.
    /// </summary>
    [TestClass]
    public class MakeSetTester
    {
        #region Real World Example

        /// <summary>
        /// Regardless of the input, we can make it a set, so long as the type is sortable.
        /// </summary>
        [TestMethod]
        public void TestMakeSet_FromRandomList()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next()), list.ToSublist());

            // make it a set and remove trailing garbage
            Sublist.RemoveRange(list.ToSublist(Sublist.MakeSet(list.ToSublist())));

            // the set should be sorted and have all unique values
            Assert.IsTrue(Sublist.IsSorted(list.ToSublist()), "The list was not sorted.");
            Assert.IsFalse(Sublist.ContainsDuplicates(list.ToSublist()), "The list had duplicates.");
        }

        #endregion

        #region Argument Checking
        
        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSet_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.MakeSet(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSet_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.MakeSet(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSet_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.MakeSet(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSet_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            Sublist.MakeSet(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSet_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.MakeSet(list, comparison);
        }

        #endregion

        /// <summary>
        /// Since an empty list is the same as an empty set, nothing should be done.
        /// </summary>
        [TestMethod]
        public void TestMakeSet_EmptyList_DoesNothing()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.MakeSet(list);
            Assert.AreEqual(0, list.Count, "Somehow the list grew.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Since a list with one item is a list, nothing should be done.
        /// </summary>
        [TestMethod]
        public void TestMakeSet_ListOfOne_DoesNothing()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            Sublist.MakeSet(list);
            Assert.AreEqual(1, list.Count, "Somehow the list grew.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to make a set out of randomized numbers.
        /// </summary>
        [TestMethod]
        public void TestMakeSet_RandomizedItems_Sorts()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 5, 3, 2, 4, });
            int index = Sublist.MakeSet(list, (x, y) => Comparer<int>.Default.Compare(y, x));
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            int[] expected = { 5, 4, 3, 2, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to make a set out of randomized numbers with duplicates.
        /// </summary>
        [TestMethod]
        public void TestMakeSet_DuplicateItems_RemovesDuplicates()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 5, 3, 2, 4, 2, 1, 5, 4 });
            int index = Sublist.MakeSet(list, Comparer<int>.Default);
            Assert.AreEqual(5, index, "The wrong index was returned.");
            var set = list.Nest(0, index);
            var garbage = list.Nest(index);
            Sublist.RemoveRange(garbage);
            int[] expected = { 1, 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), set), "The duplicates were not removed.");
            TestHelper.CheckHeaderAndFooter(set);
        }
    }
}
