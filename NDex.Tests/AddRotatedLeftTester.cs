using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the AddRotatedLeft methods.
    /// </summary>
    [TestClass]
    public class AddRotatedLeftTester
    {
        #region Real World Example

        /// <summary>
        /// We can use add rotate left to detect reoccurring patterns.
        /// </summary>
        [TestMethod]
        public void TestAddRotatedLeft()
        {
            Random random = new Random();

            // build list of numbers that divide evenly into 100 and pick one
            var divisors = new List<int>(Enumerable.Range(1, 50).Where(i => 100 % i == 0));
            var samples = new List<int>(1);
            Sublist.AddRandomSamples(divisors.ToSublist(), 1, samples.ToSublist(), random);
            int repeat = samples[0];

            // build a list with the numbers 0-4 reoccurring
            var list = new List<int>(100);
            Sublist.Grow(list, 100, i => i % repeat);

            // try different shifts, looking for reoccurrences
            int shift = 1;
            while (shift != 100)
            {
                var rotated = new List<int>(list.Count);
                Sublist.AddRotatedLeft(list.ToSublist(), rotated.ToSublist(), shift);
                if (Sublist.AreEqual(list.ToSublist(), rotated.ToSublist()))
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
        public void TestAddRotatedLeft_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            int shift = 0;
            Sublist.AddRotatedLeft(list, destination, shift);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddRotatedLeft_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            int shift = 0;
            Sublist.AddRotatedLeft(list, destination, shift);
        }

        #endregion

        /// <summary>
        /// If the destination is too small to hold all of the rotated values, then the first
        /// item should be the item at index shift.
        /// </summary>
        [TestMethod]
        public void TestAddRotatedLeft_PositiveShift_ShiftToLeft()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            var destination = TestHelper.Wrap(new List<int>());
            int shift = 2;
            Sublist.AddRotatedLeft(list, destination, shift);
            int[] expected = { 3, 4, 5, 1, 2 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We can pass a negative number to simulate rotating right.
        /// </summary>
        [TestMethod]
        public void TestAddRotatedLeft_NegativeShift_ShiftsToTheRight()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            var destination = TestHelper.Wrap(new List<int>());
            int shift = -1;
            Sublist.AddRotatedLeft(list, destination, shift);
            int[] expected = { 5, 1, 2, 3, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the shift is greater than the size of the list, we simulate a complete rotation.
        /// </summary>
        [TestMethod]
        public void TestAddRotatedLeft_ShiftLargerThanList_ShiftsFullCircle()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            var destination = TestHelper.Wrap(new List<int>());
            int shift = list.Count + 1;
            Sublist.AddRotatedLeft(list, destination, shift);
            int[] expected = { 2, 3, 4, 5, 1, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
