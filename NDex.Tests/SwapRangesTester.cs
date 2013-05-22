using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the SwapRanges methods.
    /// </summary>
    [TestClass]
    public class SwapRangesTester
    {
        #region Real World Example

        /// <summary>
        /// SwapRanges is useful when needing to swap items between two lists. We'll partition two lists and
        /// then swap out their items.
        /// </summary>
        [TestMethod]
        public void TestSwapRanges_PartitionAndSwap()
        {
            Random random = new Random();

            // build two lists
            var list1 = new List<int>(50);
            Sublist.Generate(50, i => random.Next(100)).AddTo(list1.ToSublist());
            var list2 = new List<int>(50);
            Sublist.Generate(50, i => random.Next(100)).AddTo(list2.ToSublist());

            // partition by evens and odds
            int index1 = list1.ToSublist().Partition(i => i % 2 == 0).InPlace();
            int index2 = list2.ToSublist().Partition(i => i % 2 != 0).InPlace();

            // grab the ranges not satisfy the predicate and swap them
            var nonEvens = list1.ToSublist(index1);
            var nonOdds = list2.ToSublist(index2);
            int offset = nonEvens.SwapWith(nonOdds);

            // since the lists will probably not be the same size, we'll have to copy the trailing items
            if (offset != nonEvens.Count)
            {
                nonEvens.Nest(offset).AddTo(nonOdds);
                nonEvens.Nest(offset).Clear();
            }
            else if (offset != nonOdds.Count)
            {
                nonOdds.Nest(offset).AddTo(nonEvens);
                nonOdds.Nest(offset).Clear();
            }

            // now make sure both lists are only evens and odds
            Assert.IsTrue(list1.ToSublist().TrueForAll(i => i % 2 == 0), "There were odds remaining in the first list.");
            Assert.IsTrue(list2.ToSublist().TrueForAll(i => i % 2 != 0), "There were evens remaining in the second list.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSwapRanges_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            list1.SwapWith(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSwapRanges_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            list1.SwapWith(list2);
        }

        #endregion

        /// <summary>
        /// If the ranges being swapped are the same size, the returned indexes should both equal the sizes.
        /// </summary>
        [TestMethod]
        public void TestSwapRanges_EqualSizedRanges_ReturnsCounts()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7, });
            var list2 = TestHelper.Wrap(new List<int>() { 2, 4, 6, 8, });
            int offset = list1.SwapWith(list2);
            Assert.AreEqual(list1.Count, offset, "The wrong first index was returned.");
            Assert.AreEqual(list2.Count, offset, "The wrong second index was returned.");
            int[] expected1 = { 2, 4, 6, 8 };
            Assert.IsTrue(expected1.ToSublist().IsEqualTo(list1), "The first items were not swapped as expected.");
            int[] expected2 = { 1, 3, 5, 7 };
            Assert.IsTrue(expected2.ToSublist().IsEqualTo(list2), "The second items were not swapped as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the first range is smaller, the algorithm should stop prematurely.
        /// </summary>
        [TestMethod]
        public void TestSwapRanges_FirstRangeSmaller_StopsPrematurely()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 3, 5, });
            var list2 = TestHelper.Wrap(new List<int>() { 2, 4, 6, 8, });
            int offset = list1.SwapWith(list2);
            Assert.AreEqual(list1.Count, offset, "The wrong first index was returned.");
            Assert.AreEqual(3, offset, "The wrong second index was returned.");
            int[] expected1 = { 2, 4, 6 };
            Assert.IsTrue(expected1.ToSublist().IsEqualTo(list1), "The first items were not swapped as expected.");
            int[] expected2 = { 1, 3, 5, 8 };
            Assert.IsTrue(expected2.ToSublist().IsEqualTo(list2), "The second items were not swapped as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the second range is smaller, the algorithm should stop prematurely.
        /// </summary>
        [TestMethod]
        public void TestSwapRanges_SecondRangeSmaller_StopsPrematurely()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7, });
            var list2 = TestHelper.Wrap(new List<int>() { 2, 4, 6 });
            int offset = list1.SwapWith(list2);
            Assert.AreEqual(3, offset, "The wrong first index was returned.");
            Assert.AreEqual(list2.Count, offset, "The wrong second index was returned.");
            int[] expected1 = { 2, 4, 6, 7 };
            Assert.IsTrue(expected1.ToSublist().IsEqualTo(list1), "The first items were not swapped as expected.");
            int[] expected2 = { 1, 3, 5 };
            Assert.IsTrue(expected2.ToSublist().IsEqualTo(list2), "The second items were not swapped as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }
    }
}
