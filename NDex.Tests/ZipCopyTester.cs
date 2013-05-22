using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ZipCopy methods.
    /// </summary>
    [TestClass]
    public class ZipCopyTester
    {
        #region Real World Example

        /// <summary>
        /// We'll use combine to generate the products of two lists.
        /// </summary>
        [TestMethod]
        public void TestZipCopy_MultiplyTwoLists()
        {
            Random random = new Random();
            
            // build the first list
            var list1 = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list1.ToSublist());

            // build the second list
            var list2 = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list2.ToSublist());

            var destination = new List<int>(100);
            Sublist.Generate(100, 0).AddTo(destination.ToSublist());

            // multiply the values at each index together
            int destinationIndex = list1.ToSublist().Zip(list2.ToSublist(), (i, j) => i * j).CopyTo(destination.ToSublist());
            Assert.AreEqual(destination.Count, destinationIndex, "Not all the values were multiplied.");

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
        public void TestZipCopy_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> combiner = (i, j) => i + j;
            list1.Zip(list2, combiner);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestZipCopy_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, int> combiner = (i, j) => i + j;
            list1.Zip(list2, combiner);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestZipCopy_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> combiner = (i, j) => i + j;
            list1.Zip(list2, combiner).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestZipCopy_NullCombiner_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> combiner = null;
            list1.Zip(list2, combiner);
        }

        #endregion

        /// <summary>
        /// If the destination is too small, it will be filled as much as possible.
        /// </summary>
        [TestMethod]
        public void TestZipCopy_DestinationTooSmall_StopsPrematurely()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 4, 3, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            var result = list1.Zip(list2, (i, j) => i + j).CopyTo(destination);
            Assert.AreEqual(2, result.SourceOffset1, "The first source index is wrong.");
            Assert.AreEqual(2, result.SourceOffset2, "The second source index is wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong number of items were stored in the destination.");
            var expected = TestHelper.Wrap(new List<int>() { 5, 5, });
            Assert.IsTrue(expected.IsEqualTo(destination), "The items were not combined correctly.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If a list is smaller than the other, the destination will be filled as much as possible.
        /// </summary>
        [TestMethod]
        public void TestZipCopy_ListsDifferentSizes_StopsPrematurely()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 4, 3, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            var result = list1.Zip(list2, (i, j) => i + j).CopyTo(destination);
            Assert.AreEqual(2, result.SourceOffset1, "The first source index is wrong.");
            Assert.AreEqual(2, result.SourceOffset2, "The second source index is wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The wrong number of items were stored in the destination.");
            var expected = TestHelper.Wrap(new List<int>() { 5, 5, 0, }); // the third item should remain untouched.
            Assert.IsTrue(expected.IsEqualTo(destination), "The items were not combined correctly.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
