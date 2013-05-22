using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the Copy methods.
    /// </summary>
    [TestClass]
    public class CopyTester
    {
        #region Real World Example

        /// <summary>
        /// Since the algorithms work against generic instances of IList, Copy can be used to
        /// copy between a List and an Array.
        /// </summary>
        [TestMethod]
        public void TestCopy_FromListToArray()
        {
            var list = new List<int>() { 1, 2, 3 };
            var array = new int[3];
            int result = Sublist.CopyTo(list.ToSublist(), array.ToSublist());
            Assert.AreEqual(array.Length, result, "The result was not at the expected index.");
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), array.ToSublist()), "The items were not copied correctly.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopy_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyTo(list, destination);
        }

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopy_IEnumerable_NullSource_Throws()
        {
            IEnumerable<int> source = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyTo(source, destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopy_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.CopyTo(list, destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopy_IEnumerable_NullDestination_Throws()
        {
            IEnumerable<int> source = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.CopyTo(source, destination);
        }

        #endregion

        /// <summary>
        /// We can use copy to shift items from the right to the left.
        /// </summary>
        [TestMethod]
        public void TestCopy_SameList_FromRightToLeft()
        {
            var list = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3 });
            var from = list.Nest(1, 3);
            var to = list.Nest(0, 3);
            var result = Sublist.CopyTo(from, to);
            Assert.AreEqual(3, result.SourceOffset, "The result had the wrong source index.");
            Assert.AreEqual(3, result.DestinationOffset, "The result had the wrong destination index.");
            int[] expected = { 1, 2, 3, 3, };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the destination cannot fit all of the items, it should stop prematurely.
        /// </summary>
        [TestMethod]
        public void TestCopy_DestinationSmaller_OnlyCopiesFirstItems()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, });
            var result = Sublist.CopyTo(list, destination);
            Assert.AreEqual(2, result.SourceOffset, "The result had the wrong source index.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The result had the wrong destination index.");
            int[] expected = { 1, 2, };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the source is smaller than the destination, then the result should appear in the middle of the destination.
        /// </summary>
        [TestMethod]
        public void TestCopy_SourceSmaller_ResultInMiddleOfDestination()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            var result = Sublist.CopyTo(list, destination);
            Assert.AreEqual(2, result.SourceOffset, "The result had the wrong source index.");
            Assert.AreEqual(2, result.DestinationOffset, "The result had the wrong destination index.");
            int[] expected = { 1, 2, 0 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the source collection and the destination are the same size, all the items
        /// should be copied to the destination.
        /// </summary>
        [TestMethod]
        public void TestCopy_IEnumerable_SameSize_OverwritesDestination()
        {
            IEnumerable<int> source = new List<int>() { 1, 2, 3 };
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            int index = Sublist.CopyTo(source, destination);
            Assert.AreEqual(destination.Count, index, "The wrong index was returned.");
            var expected = new int[] { 1, 2, 3 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the source is larger than the destination, the algorithm should stop prematurely.
        /// </summary>
        [TestMethod]
        public void TestCopy_IEnumerable_DestinationSmaller_StopsPrematurely()
        {
            IEnumerable<int> source = new List<int>() { 1, 2, 3, 4, 5, 6 };
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            int index = Sublist.CopyTo(source, destination);
            Assert.AreEqual(destination.Count, index, "The wrong index was returned.");
            var expected = new int[] { 1, 2, 3 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the source is smaller than the destination, the algorithm should stop when
        /// the source items run out.
        /// </summary>
        [TestMethod]
        public void TestCopy_IEnumerable_SourceSmaller_StopsPrematurely()
        {
            IEnumerable<int> source = new List<int>() { 1, 2, 3 };
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0 });
            int index = Sublist.CopyTo(source, destination);
            Assert.AreEqual(3, index, "The wrong index was returned.");
            var expected = new int[] { 1, 2, 3, 0, 0, 0 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), destination), "The items were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
