using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the RotateLeft methods.
    /// </summary>
    [TestClass]
    public class RotateLeftTester
    {
        #region Real World Example

        /// <summary>
        /// We can use add rotate left to move items to their appropriate location in a sorted list.
        /// </summary>
        [TestMethod]
        public void TestRotateLeft_SlowSort()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list.ToSublist());

            for (int index = 0; index != list.Count; ++index)
            {
                var sortedRange = list.ToSublist(0, index);
                int position = Sublist.LowerBound(sortedRange, list[index]);
                var rotationRange = sortedRange.Nest(position);
                rotationRange = rotationRange.Resize(rotationRange.Count + 1, false);
                rotationRange.RotateLeft(-1).InPlace();  // move the last item to the front
            }

            Assert.IsTrue(Sublist.IsSorted(list.ToSublist()), "The list was not sorted.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRotateLeft_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int shift = 0;
            Sublist.RotateLeft(list, shift);
        }

        #endregion

        /// <summary>
        /// If we shift to the left, the items in the front should be moved to the back.
        /// </summary>
        [TestMethod]
        public void TestRotateLeft_PositiveShift_ShiftToLeft()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            int shift = 2;
            list.RotateLeft(shift).InPlace();
            int[] expected = { 3, 4, 5, 1, 2 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list), "The values were not rotated as expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can pass a negative number to simulate rotating right.
        /// </summary>
        [TestMethod]
        public void TestRotateLeft_NegativeShift_ShiftsToTheRight()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            int shift = -1;
            list.RotateLeft(shift).InPlace();
            int[] expected = { 5, 1, 2, 3, 4, };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list), "The values were not rotated as expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the shift is greater than the size of the list, we simulate a complete rotation.
        /// </summary>
        [TestMethod]
        public void TestRotateLeft_ShiftLargerThanList_ShiftsFullCircle()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            int shift = list.Count + 1;
            list.RotateLeft(shift).InPlace();
            int[] expected = { 2, 3, 4, 5, 1, };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list), "The values were not rotated as expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
