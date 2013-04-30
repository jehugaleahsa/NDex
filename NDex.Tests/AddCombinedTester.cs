using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the AddCombined methods.
    /// </summary>
    [TestClass]
    public class AddCombinedTester
    {
        #region Real World Example

        /// <summary>
        /// We'll use combine to generate the products of two lists.
        /// </summary>
        [TestMethod]
        public void TestAddCombined_MultiplyTwoLists()
        {
            Random random = new Random();

            // build the first list
            var list1 = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next(100)), list1.ToSublist());

            // build the second list
            var list2 = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next(100)), list2.ToSublist());

            var destination = new List<int>(100);

            // multiply the values at each index together
            Sublist.AddCombined(list1.ToSublist(), list2.ToSublist(), destination.ToSublist(), (i, j) => i * j);

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
        public void TestAddCombined_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> combiner = (i, j) => i + j;
            Sublist.AddCombined(list1, list2, destination, combiner);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddCombined_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> combiner = (i, j) => i + j;
            Sublist.AddCombined(list1, list2, destination, combiner);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddCombined_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> combiner = (i, j) => i + j;
            Sublist.AddCombined(list1, list2, destination, combiner);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddCombined_NullCombiner_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> combiner = null;
            Sublist.AddCombined(list1, list2, destination, combiner);
        }

        #endregion

        /// <summary>
        /// If a list is smaller than the other, the destination will be filled as much as possible.
        /// </summary>
        [TestMethod]
        public void TestAddCombined_ListsDifferentSizes_StopsPrematurely()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 4, 3, });
            var destination = TestHelper.Wrap(new List<int>());
            destination = Sublist.AddCombined(list1, list2, destination, (i, j) => i + j);
            int[] expected = { 5, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not combined correctly.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
