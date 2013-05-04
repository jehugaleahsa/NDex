using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the CopyReversed methods.
    /// </summary>
    [TestClass]
    public class CopyReversedTester
    {
        #region Real World Example

        /// <summary>
        /// We may want to determine if a list is equal forward and backward.
        /// </summary>
        [TestMethod]
        public void TestCopyReversed_InefficientPalindromeChecker()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 5, 4, 3, 2, 1, };

            var copy = new List<int>(list.Count);
            Sublist.AddGenerated(copy.ToSublist(), list.Count, 0);

            int result = Sublist.CopyReversed(list.ToSublist(), copy.ToSublist());
            Assert.AreEqual(copy.Count, result, "The wrong index was returned.");

            Assert.IsTrue(Sublist.AreEqual(list.ToSublist(), copy.ToSublist()), "The list was not reversed as expected.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReversed_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyReversed(list, destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyReversed_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.CopyReversed(list, destination);
        }

        #endregion

        /// <summary>
        /// If the destination is too small to hold all of the source items, only the first items
        /// should be copied in reverse into the destination.
        /// </summary>
        [TestMethod]
        public void TestCopyReversed_DestinationSmaller_OnlyCopyFirstItems()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            CopyResult result = Sublist.CopyReversed(list, destination);
            Assert.AreEqual(2, result.SourceOffset, "The source offset is wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset is wrong.");
            int[] expected = { 2, 1, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the source list is smaller than the destination, there should be space left over in the destination.
        /// </summary>
        [TestMethod]
        public void TestCopyReversed_SourceSmaller_SpaceLeftOver()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            CopyResult result = Sublist.CopyReversed(list, destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset is wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset is wrong.");
            int[] expected = { 2, 1, 0, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
