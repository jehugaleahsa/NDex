using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the MakeSetInPlace methods.
    /// </summary>
    [TestClass]
    public class MakeSetInPlaceTester
    {
        #region Real World Example

        /// <summary>
        /// Regardless of the input, we can make it a set, so long as the type is sortable.
        /// </summary>
        [TestMethod]
        public void TestMakeSetInPlace_FromRandomList()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next()).AddTo(list.ToSublist());

            // make it a set and remove trailing garbage
            list.ToSublist(list.ToSublist().MakeSet().InPlace()).Clear();

            // the set should be sorted and have all unique values
            Assert.IsTrue(list.ToSublist().IsSorted(), "The list was not sorted.");
            Assert.IsFalse(list.ToSublist().FindDuplicates(), "The list had duplicates.");
        }

        #endregion

        #region Argument Checking
        
        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSetInPlace_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            list.MakeSet();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSetInPlace_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.MakeSet(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSetInPlace_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.MakeSet(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSetInPlace_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            list.MakeSet(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSetInPlace_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            list.MakeSet(comparison);
        }

        #endregion

        /// <summary>
        /// Since an empty list is the same as an empty set, nothing should be done.
        /// </summary>
        [TestMethod]
        public void TestMakeSetInPlace_EmptyList_DoesNothing()
        {
            var list = TestHelper.Wrap(new List<int>());
            list.MakeSet().InPlace();
            Assert.AreEqual(0, list.Count, "Somehow the list grew.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Since a list with one item is a list, nothing should be done.
        /// </summary>
        [TestMethod]
        public void TestMakeSetInPlace_ListOfOne_DoesNothing()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            list.MakeSet().InPlace();
            Assert.AreEqual(1, list.Count, "Somehow the list grew.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to make a set out of randomized numbers.
        /// </summary>
        [TestMethod]
        public void TestMakeSetInPlace_RandomizedItems_Sorts()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 5, 3, 2, 4, });
            int index = list.MakeSet((x, y) => Comparer<int>.Default.Compare(y, x)).InPlace();
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            int[] expected = { 5, 4, 3, 2, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list), "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to make a set out of randomized numbers with duplicates.
        /// </summary>
        [TestMethod]
        public void TestMakeSetInPlace_DuplicateItems_RemovesDuplicates()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 5, 3, 2, 4, 2, 1, 5, 4 });
            int index = list.MakeSet(Comparer<int>.Default).InPlace();
            Assert.AreEqual(5, index, "The wrong index was returned.");
            var set = list.Nest(0, index);
            var garbage = list.Nest(index);
            garbage.Clear();
            int[] expected = { 1, 2, 3, 4, 5, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(set), "The duplicates were not removed.");
            TestHelper.CheckHeaderAndFooter(set);
        }
    }
}
