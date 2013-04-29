using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the Reverse methods.
    /// </summary>
    [TestClass]
    public class ReverseTester
    {
        #region Real World Example

        /// <summary>
        /// If you need a list in descending order and it's already in ascending order,
        /// it is faster to reverse the list than it is to resort it.
        /// </summary>
        [TestMethod]
        public void TestReverse_ReverseOrder()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next(100)), list.ToSublist());

            // sort the list in ascending order
            Sublist.QuickSort(list.ToSublist());

            // now reverse the order
            Sublist.Reverse(list.ToSublist());
            
            // it should be in reverse order
            Assert.IsTrue(Sublist.IsSorted(list.ToSublist(), (x, y) => Comparer<int>.Default.Compare(y, x)), "The list was not reversed.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReverse_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.Reverse(list);
        }

        #endregion

        /// <summary>
        /// If the list has an odd number of items, it should still be reversed.
        /// </summary>
        [TestMethod]
        public void TestReverse_OddCount_ReversesList()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            Sublist.Reverse(list);
            int[] expected = { 3, 2, 1, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items were not reversed as expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the list has an odd number of items, it should still be reversed.
        /// </summary>
        [TestMethod]
        public void TestReverse_EvenCount_ReversesList()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });
            Sublist.Reverse(list);
            int[] expected = { 4, 3, 2, 1, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items were not reversed as expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
