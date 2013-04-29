using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the Fill methods.
    /// </summary>
    [TestClass]
    public class FillTester
    {
        #region Real World Examples

        /// <summary>
        /// Fill can be used to populate an array with incrementing values.
        /// </summary>
        [TestMethod]
        public void TestFill_Counting()
        {
            int[] values = new int[10];
            Sublist.Fill(values.ToSublist(), i => i);
            int[] expected = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), values.ToSublist()), "The items were not set as expected.");
        }

        /// <summary>
        /// Fill can be used to populate an array with default values.
        /// </summary>
        [TestMethod]
        public void TestFill_Defaulting()
        {
            DateTime[] values = new DateTime[10]; // defaults to 01/01/0001
            DateTime defaultDate = DateTime.Today;
            Sublist.Fill(values.ToSublist(), defaultDate); // change default to today

            DateTime[] expected = new DateTime[10];
            Sublist.CopyConverted(expected.ToSublist(), expected.ToSublist(), i => defaultDate); // replace all with default

            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), values.ToSublist()), "The items were not set as expected.");
        }

        /// <summary>
        /// Fill can be used to populate an array with random values.
        /// </summary>
        [TestMethod]
        public void TestFill_WithRandomValues()
        {
            Random random = new Random();
            int[] values = new int[10];
            Sublist.Fill(values.ToSublist(), i => random.Next(1, 10)); // fixed length version of Grow!
            Assert.IsTrue(Sublist.TrueForAll(values.ToSublist(), i => i != 0), "Not all of the values were filled in.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFill_NullList_Throws()
        {
            Sublist<int[], int> list = null;
            int value = 0;
            Sublist.Fill(list, value);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFill_WithGenerator_NullList_Throws()
        {
            Sublist<int[], int> list = null;
            Func<int> generator = () => 0;
            Sublist.Fill(list, generator);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFill_WithIndexedGenerator_NullList_Throws()
        {
            Sublist<int[], int> list = null;
            Func<int, int> generator = i => i;
            Sublist.Fill(list, generator);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFill_NullGenerator_Throws()
        {
            Sublist<int[], int> list = new int[10];
            Func<int> generator = null;
            Sublist.Fill(list, generator);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFill_NullIndexedGenerator_Throws()
        {
            Sublist<int[], int> list = new int[10];
            Func<int, int> generator = null;
            Sublist.Fill(list, generator);
        }

        #endregion

        /// <summary>
        /// We should be able to set every item to a particular value type.
        /// </summary>
        [TestMethod]
        public void TestFill_SetValueType()
        {
            var list = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            int defaultValue = 4;

            Sublist.Fill(list, defaultValue);
            int[] expected = { 4, 4, 4, 4, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items were not set as expected.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we want every item in a list to be a new instance, we have to call new each time.
        /// We can use the generator overload to accomplish this. This is especially useful for
        /// reference types.
        /// </summary>
        [TestMethod]
        public void TestFill_WithGenerator_CallCtor()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 1, 1, 1 });
            Sublist.Fill(list, () => new int());
            int[] expected = { 0, 0, 0, 0, 0, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items were not set as expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to set every item to a value relative to its index.
        /// </summary>
        [TestMethod]
        public void TestFill_IncrementingCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });

            Sublist.Fill(list, i => i + 1);
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items were not set as expected.");

            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
