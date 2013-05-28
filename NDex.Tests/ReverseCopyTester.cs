using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ReverseCopy methods.
    /// </summary>
    [TestClass]
    public class ReverseCopyTester
    {
        #region Real World Example

        /// <summary>
        /// We may want to determine if a list is equal forward and backward.
        /// </summary>
        [TestMethod]
        public void TestReverseCopy_InefficientPalindromeChecker()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 5, 4, 3, 2, 1, };

            var copy = new List<int>(list.Count);
            Sublist.Generate(list.Count, 0).AddTo(copy.ToSublist());

            int result = list.ToSublist().Reverse().CopyTo(copy.ToSublist());
            Assert.AreEqual(copy.Count, result, "The wrong index was returned.");

            Assert.IsTrue(list.ToSublist().IsEqualTo(copy.ToSublist()), "The list was not reversed as expected.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReverseCopy_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            list.Reverse();
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReverseCopy_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            list.Reverse().CopyTo(destination);
        }

        #endregion

        /// <summary>
        /// If the destination is too small to hold all of the source items, only the first items
        /// should be copied in reverse into the destination.
        /// </summary>
        [TestMethod]
        public void TestReverseCopy_DestinationSmaller_OnlyCopyFirstItems()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            var result = list.Reverse().CopyTo(destination);
            Assert.AreEqual(2, result.SourceOffset, "The source offset is wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset is wrong.");
            int[] expected = { 2, 1, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the source list is smaller than the destination, there should be space left over in the destination.
        /// </summary>
        [TestMethod]
        public void TestReverseCopy_SourceSmaller_SpaceLeftOver()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            var result = list.Reverse().CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset is wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset is wrong.");
            int[] expected = { 2, 1, 0, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
