using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ReverseInPlace methods.
    /// </summary>
    [TestClass]
    public class ReverseInPlaceTester
    {
        #region Real World Example

        /// <summary>
        /// If you need a list in descending order and it's already in ascending order,
        /// it is faster to reverse the list than it is to resort it.
        /// </summary>
        [TestMethod]
        public void TestReverseInPlace_ReverseOrder()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list.ToSublist());

            // sort the list in ascending order
            list.ToSublist().Sort().InPlace();

            // now reverse the order
            list.ToSublist().Reverse().InPlace();
            
            // it should be in reverse order
            Assert.IsTrue(list.ToSublist().IsSorted((x, y) => Comparer<int>.Default.Compare(y, x)), "The list was not reversed.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReverseInPlace_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            list.Reverse();
        }

        #endregion

        /// <summary>
        /// If the list has an odd number of items, it should still be reversed.
        /// </summary>
        [TestMethod]
        public void TestReverseInPlace_OddCount_ReversesList()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            list.Reverse().InPlace();
            int[] expected = { 3, 2, 1, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list), "The items were not reversed as expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the list has an odd number of items, it should still be reversed.
        /// </summary>
        [TestMethod]
        public void TestReverseInPlace_EvenCount_ReversesList()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });
            list.Reverse().InPlace();
            int[] expected = { 4, 3, 2, 1, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list), "The items were not reversed as expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
