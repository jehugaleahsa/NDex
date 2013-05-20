using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the AddGenerated methods.
    /// </summary>
    [TestClass]
    public class AddGeneratedTester
    {
        #region Real World Examples

        /// <summary>
        /// AddGenerated can be used to populate an array with incrementing values.
        /// </summary>
        [TestMethod]
        public void TestAddGenerated_Counting()
        {
            List<int> values = new List<int>();
            Sublist.AddGenerated(values.ToSublist(), 10, i => i);
            int[] expected = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), values.ToSublist()), "The items were not set as expected.");
        }

        /// <summary>
        /// AddGenerated can be used to populate an array with default values.
        /// </summary>
        [TestMethod]
        public void TestAddGenerated_Defaulting()
        {
            List<DateTime> values = new List<DateTime>(); // defaults to 01/01/0001
            DateTime defaultDate = DateTime.Today;
            Sublist.AddGenerated(values.ToSublist(), 10, defaultDate); // change default to today

            DateTime[] expected = new DateTime[10];
            expected.ToSublist().Select(i => defaultDate).CopyTo(expected.ToSublist()); // replace all with default

            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), values.ToSublist()), "The items were not set as expected.");
        }

        /// <summary>
        /// AddGenerated can be used to populate an array with random values.
        /// </summary>
        [TestMethod]
        public void TestAddGenerated_WithRandomValues()
        {
            Random random = new Random();
            List<int> values = new List<int>();
            Sublist.AddGenerated(values.ToSublist(), 10, i => random.Next(1, 10)); // fixed length version of Grow!
            Assert.IsTrue(Sublist.TrueForAll(values.ToSublist(), i => i != 0), "Not all of the values were filled in.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddGenerated_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int numberOfItems = 0;
            int value = 0;
            Sublist.AddGenerated(list, numberOfItems, value);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddGenerated_WithGenerator_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int numberOfItems = 0;
            Func<int> generator = () => 0;
            Sublist.AddGenerated(list, numberOfItems, generator);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddGenerated_WithIndexedGenerator_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int numberOfItems = 0;
            Func<int, int> generator = i => i;
            Sublist.AddGenerated(list, numberOfItems, generator);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddGenerated_NegativeNumberOfItems_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = -1;
            int value = 0;
            Sublist.AddGenerated(list, numberOfItems, value);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddGenerated_NegativeNumberOfItems_Generator_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = -1;
            Func<int> generator = () => 0;
            Sublist.AddGenerated(list, numberOfItems, generator);
        }

        /// <summary>
        /// An exception should be thrown if the number of items is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddGenerated_NegativeNumberOfItems_IndexedGenerator_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = -1;
            Func<int, int> generator = i => i;
            Sublist.AddGenerated(list, numberOfItems, generator);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddGenerated_NullGenerator_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 10;
            Func<int> generator = null;
            Sublist.AddGenerated(list, numberOfItems, generator);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddGenerated_NullIndexedGenerator_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfItems = 10;
            Func<int, int> generator = null;
            Sublist.AddGenerated(list, numberOfItems, generator);
        }

        #endregion

        /// <summary>
        /// We should be able to set every item to a particular value type.
        /// </summary>
        [TestMethod]
        public void TestAddGenerated_SetValueType()
        {
            var list = TestHelper.Wrap(new List<int>());
            int numberOfItems = 5;
            int defaultValue = 4;

            list = Sublist.AddGenerated(list, numberOfItems, defaultValue);
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
        public void TestAddGenerated_WithGenerator_CallCtor()
        {
            var list = TestHelper.Wrap(new List<int>());
            int numberOfItems = 5;
            list = Sublist.AddGenerated(list, numberOfItems, () => new int());
            int[] expected = { 0, 0, 0, 0, 0, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items were not set as expected.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to set every item to a value relative to its index.
        /// </summary>
        [TestMethod]
        public void TestAddGenerated_IncrementingCount()
        {
            var list = TestHelper.Wrap(new List<int>());
            int numberOfItems = 4;
            list = Sublist.AddGenerated(list, numberOfItems, i => i + 1);
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The items were not set as expected.");

            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
