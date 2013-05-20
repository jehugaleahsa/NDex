using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ExceptCopy methods.
    /// </summary>
    [TestClass]
    public class ExceptCopyTester
    {
        #region Real World Example

        /// <summary>
        /// Say we wanted to get a list of odd numbers that aren't divisible by three.
        /// </summary>
        [TestMethod]
        public void TestExceptCopy_EliminateSetIntersections()
        {
            Random random = new Random();

            // build a list of odd numbers
            var odds = new List<int>(100);
            Sublist.AddGenerated(odds.ToSublist(), 100, i => random.Next(0, 50) * 2 + 1); // max of 99 (49 * 2 + 1)

            // build a list of all of the numbers divisible by three
            var threes = new List<int>(34);
            Sublist.AddGenerated(threes.ToSublist(), 34, i => i * 3); // max of 99

            // sort and eliminate duplicates from odd list, to make it an ordered set
            Sublist.QuickSort(odds.ToSublist());
            Sublist.RemoveRange(odds.ToSublist(Sublist.RemoveDuplicates(odds.ToSublist())));

            // now remove the threes and make sure none remain
            var destination = new List<int>(100);
            Sublist.AddGenerated(destination.ToSublist(), 100, 0);

            int result = odds.ToSublist().Except(threes.ToSublist()).CopyTo(destination.ToSublist());
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
        public void TestExceptCopy_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            list1.Except(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptCopy_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Except(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptCopy_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Except(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptCopy_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            list1.Except(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptCopy_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Except(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptCopy_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Except(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptCopy_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            list1.Except(list2).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptCopy_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Except(list2, comparer).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptCopy_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Except(list2, comparison).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptCopy_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = null;
            list1.Except(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptCopy_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = null;
            list1.Except(list2, comparison);
        }

        #endregion

        /// <summary>
        /// If there are more items to be copied than can be fit within the destination,
        /// the algorithms stops prematurely.
        /// </summary>
        [TestMethod]
        public void TestExceptCopy_DestinationTooSmall_StopsPrematurely()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            var result = list1.Except(list2, comparison).CopyTo(destination);
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
        public void TestExceptCopy_DestinationLarger_SpaceLeftOver()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            IComparer<int> comparer = Comparer<int>.Default;
            var result = list1.Except(list2, comparer).CopyTo(destination);
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
        public void TestExceptCopy_DifferenceEqualsNullSet()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            var result = list1.Except(list2).CopyTo(destination);
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
