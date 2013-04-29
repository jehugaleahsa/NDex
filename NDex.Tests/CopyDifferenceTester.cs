using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the CopyDifference methods.
    /// </summary>
    [TestClass]
    public class CopyDifferenceTester
    {
        #region Real World Example

        /// <summary>
        /// Say we wanted to get a list of odd numbers that aren't divisible by three.
        /// </summary>
        [TestMethod]
        public void TestCopyDifference_EliminateSetIntersections()
        {
            Random random = new Random();

            // build a list of odd numbers
            var odds = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next(0, 50) * 2 + 1), odds.ToSublist()); // max of 99 (49 * 2 + 1)

            // build a list of all of the numbers divisible by three
            var threes = new List<int>(34);
            Sublist.Add(Enumerable.Range(0, 34).Select(i => i * 3), threes.ToSublist()); // max of 99

            // sort and eliminate duplicates from odd list, to make it an ordered set
            Sublist.QuickSort(odds.ToSublist());
            Sublist.RemoveRange(odds.ToSublist(Sublist.RemoveDuplicates(odds.ToSublist())));

            // now remove the threes and make sure none remain
            var destination = new List<int>(100);
            Sublist.Add(Enumerable.Repeat(0, 100), destination.ToSublist());

            int result = Sublist.CopyDifference(odds.ToSublist(), threes.ToSublist(), destination.ToSublist());
            Sublist.RemoveRange(destination.ToSublist(result)); // throw away the back end
            Assert.IsTrue(Sublist.TrueForAll(destination.ToSublist(), i => i % 3 != 0), "Some numbers were still divisible by three.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyDifference_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyDifference(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyDifference_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.CopyDifference(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyDifference_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.CopyDifference(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyDifference_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyDifference(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyDifference_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.CopyDifference(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyDifference_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.CopyDifference(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyDifference_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.CopyDifference(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyDifference_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.CopyDifference(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyDifference_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.CopyDifference(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyDifference_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = null;
            Sublist.CopyDifference(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyDifference_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.CopyDifference(list1, list2, destination, comparison);
        }

        #endregion

        /// <summary>
        /// If there are more items to be copied than can be fit within the destination,
        /// the algorithms stops prematurely.
        /// </summary>
        [TestMethod]
        public void TestCopyDifference_DestinationTooSmall_StopsPrematurely()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            CopyTwoSourcesResult result = Sublist.CopyDifference(list1, list2, destination, comparison);
            Assert.AreEqual(2, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(0, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong index was returned.");
            int[] expected = { 1, 2, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If less items are copied into the destination than will fit, there should be space
        /// left over at the end.
        /// </summary>
        [TestMethod]
        public void TestCopyDifference_DestinationLarger_SpaceLeftOver()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            IComparer<int> comparer = Comparer<int>.Default;
            CopyTwoSourcesResult result = Sublist.CopyDifference(list1, list2, destination, comparer);
            Assert.AreEqual(3, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(2, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(1, result.DestinationOffset, "The wrong destination index was returned.");
            int[] expected = { 3, 0, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Nothing should be copied if the first and second list have the same items.
        /// </summary>
        [TestMethod]
        public void TestCopyDifference_DifferenceEqualsNullSet()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            CopyTwoSourcesResult result = Sublist.CopyDifference(list1, list2, destination);
            Assert.AreEqual(3, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(3, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(0, result.DestinationOffset, "The wrong destination index was returned.");
            int[] expected = { 0, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
