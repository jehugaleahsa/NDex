using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the CopyRotatedLeft methods.
    /// </summary>
    [TestClass]
    public class CopyRotatedLeftTester
    {
        #region Real World Example

        /// <summary>
        /// We can use copy rotate left to detect reoccurring patterns.
        /// </summary>
        [TestMethod]
        public void TestCopyRotatedLeft()
        {
            Random random = new Random();

            // build list of numbers that divide evenly into 100 and pick one
            var divisors = new List<int>(Enumerable.Range(1, 50).Where(i => 100 % i == 0));
            var samples = new List<int>(1);
            Sublist.AddRandomSamples(divisors.ToSublist(), 1, samples.ToSublist(), random);
            int repeat = samples[0];

            // build a list with the numbers 0-4 reoccurring
            var list = new List<int>(100);
            Sublist.AddGenerated(list.ToSublist(), 100, i => i % repeat);

            // try different shifts, looking for reoccurrences
            int shift = 1;
            while (shift != 100)
            {
                var copy = new List<int>(list.Count);
                Sublist.AddGenerated(copy.ToSublist(), list.Count, 0);
                Sublist.CopyRotatedLeft(list.ToSublist(), copy.ToSublist(), shift);
                if (Sublist.AreEqual(list.ToSublist(), copy.ToSublist()))
                {
                    break;
                }
                ++shift;
            }
            Assert.AreEqual(repeat, shift, "Did not detect the reoccurrence where expected.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyRotatedLeft_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            int shift = 0;
            Sublist.CopyRotatedLeft(list, destination, shift);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyRotatedLeft_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            int shift = 0;
            Sublist.CopyRotatedLeft(list, destination, shift);
        }

        #endregion

        /// <summary>
        /// If the destination is too small to hold all of the rotated values, then the first
        /// item should be the item at index shift.
        /// </summary>
        [TestMethod]
        public void TestCopyRotatedLeft_DestinationTooSmall_CopiesStartingAtShift()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });
            int shift = 2;
            var result = Sublist.CopyRotatedLeft(list, destination, shift);
            Assert.AreEqual(4, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 3, 4 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination can hold more than the second half of the list, only the items in the first
        /// half that fit should be copied.
        /// </summary>
        [TestMethod]
        public void TestCopyRotatedLeft_DestinationLargerThanShift_CopiesBeginningOfSecondHalf()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            int shift = 2;
            var result = Sublist.CopyRotatedLeft(list, destination, shift);
            Assert.AreEqual(1, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 3, 4, 5, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We can pass a negative number to simulate rotating right.
        /// </summary>
        [TestMethod]
        public void TestCopyRotatedLeft_NegativeShift_ShiftsToTheRight()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            int shift = -1;
            var result = Sublist.CopyRotatedLeft(list, destination, shift);
            Assert.AreEqual(4, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 5, 1, 2, 3, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the shift is greater than the size of the list, we simulate a complete rotation.
        /// </summary>
        [TestMethod]
        public void TestCopyRotatedLeft_ShiftLargerThanList_ShiftsFullCircle()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            int shift = list.Count + 1;
            var result = Sublist.CopyRotatedLeft(list, destination, shift);
            Assert.AreEqual(1, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 2, 3, 4, 5, 1, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the shift is zero, the items should just be copied.
        /// </summary>
        [TestMethod]
        public void TestCopyRotatedLeft_ShiftZero_Copies()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            int shift = 0;
            var result = Sublist.CopyRotatedLeft(list, destination, shift);
            Assert.AreEqual(0, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 3, 4, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If there isn't enough room to shift the front items to the back,
        /// the returned index should be the front of the list.
        /// </summary>
        [TestMethod]
        public void TestCopyRotatedLeft_ShiftBackToFront_IndexAtBeginningOfList()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            int shift = 2;
            var result = Sublist.CopyRotatedLeft(list, destination, shift);
            Assert.AreEqual(0, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 3, 4, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
