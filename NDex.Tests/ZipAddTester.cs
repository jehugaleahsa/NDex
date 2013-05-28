using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the Zip AddTo methods.
    /// </summary>
    [TestClass]
    public class ZipAddTester
    {
        #region Real World Example

        /// <summary>
        /// We'll use combine to generate the products of two lists.
        /// </summary>
        [TestMethod]
        public void TestZipAddTo_MultiplyTwoLists()
        {
            Random random = new Random();

            // build the first list
            var list1 = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list1.ToSublist());

            // build the second list
            var list2 = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list2.ToSublist());

            var destination = new List<int>(100);

            // multiply the values at each index together
            list1.ToSublist().Zip(list2.ToSublist(), (i, j) => i * j).AddTo(destination.ToSublist());

            // check that each value in the destination is the product
            for (int index = 0; index != destination.Count; ++index)
            {
                int product = list1[index] * list2[index];
                Assert.AreEqual(product, destination[index], "The destination did not hold the product at index = {0}.", index);
            }
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestZipAddTo_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> combiner = (i, j) => i + j;
            list1.Zip(list2, combiner);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestZipAddTo_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            Func<int, int, int> combiner = (i, j) => i + j;
            list1.Zip(list2, combiner);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestZipAddTo_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            Func<int, int, int> combiner = (i, j) => i + j;
            list1.Zip(list2, combiner).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestZipAddTo_NullCombiner_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> combiner = null;
            list1.Zip(list2, combiner);
        }

        #endregion

        /// <summary>
        /// If a list is smaller than the other, the destination will be filled as much as possible.
        /// </summary>
        [TestMethod]
        public void TestZipAddTo_ListsDifferentSizes_StopsPrematurely()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 4, 3, });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list1.Zip(list2, (i, j) => i + j).AddTo(destination);
            int[] expected = { 5, 5, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not combined correctly.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
