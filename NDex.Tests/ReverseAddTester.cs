using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ReverseAdd methods.
    /// </summary>
    [TestClass]
    public class ReverseAddTester
    {
        #region Real World Example

        /// <summary>
        /// We may want to determine if a list is equal forward and backward.
        /// </summary>
        [TestMethod]
        public void TestReverseAdd_InefficientPalindromeChecker()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 5, 4, 3, 2, 1, };

            var copy = new List<int>(list.Count);

            list.ToSublist().Reverse().AddTo(copy.ToSublist());

            Assert.IsTrue(list.ToSublist().IsEqualTo(copy.ToSublist()), "The list was not reversed as expected.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReverseAdd_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            list.Reverse();
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReverseAdd_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            list.Reverse().AddTo(destination);
        }

        #endregion

        /// <summary>
        /// If the destination is too small to hold all of the source items, only the first items
        /// should be copied in reverse into the destination.
        /// </summary>
        [TestMethod]
        public void TestReverseAdd_DestinationSmaller_OnlyCopyFirstItems()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list.Reverse().AddTo(destination);
            int[] expected = { 3, 2, 1, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
