using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the CopyIf methods.
    /// </summary>
    [TestClass]
    public class CopyIfTester
    {
        #region Real World Examples

        /// <summary>
        /// We will use copy if to remove items we don't want from a list.
        /// </summary>
        [TestMethod]
        public void TestCopyIf_RemoveOddItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6 };
            // only keep the even items
            int result = list.ToSublist().Where(item => item % 2 == 0).CopyTo(list.ToSublist());
            list.RemoveRange(result, list.Count - result);

            int[] expected = { 2, 4, 6 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list.ToSublist()), "The items were not where they were expected.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the source list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyIf_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true; // always true
            list.Where(predicate);
        }

        /// <summary>
        /// An exception should be thrown if the destination list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyIf_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, bool> predicate = i => true; // always true
            list.Where(predicate).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the predicate delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyIf_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null; // always true
            list.Where(predicate);
        }

        #endregion

        /// <summary>
        /// If more items satisfy the predicate than can fit in the destination,
        /// the algorithm should stop prematurely.
        /// </summary>
        [TestMethod]
        public void TestCopyIf_DestinationTooSmall_StopsPrematurely()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            Func<int, bool> predicate = i => true; // always true
            var result = list.Where(predicate).CopyTo(destination);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The result was at the wrong index.");
            int[] expected = { 1, 2 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is bigger than the source, the result should appear in the middle of the destination.
        /// </summary>
        [TestMethod]
        public void TestCopyIf_SourceSmaller_StopsPrematurely()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            Func<int, bool> predicate = i => true; // always true
            var result = list.Where(predicate).CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The result was at the wrong index.");
            int[] expected = { 1, 2, 0 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is too small, the source offset should point to the next item that needs copied.
        /// </summary>
        [TestMethod]
        public void TestCopyIf_DestinationTooSmall_FindsNextMatchingItem()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });
            var destination = TestHelper.Wrap(new List<int>() { 0 });
            Func<int, bool> predicate = i => i % 2 == 0;
            var result = list.Where(predicate).CopyTo(destination);
            Assert.AreEqual(3, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The result was at the wrong index.");
            int[] expected = { 2 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
