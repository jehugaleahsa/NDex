using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    /// <summary>
    /// Tests the AddConverted methods.
    /// </summary>
    [TestClass]
    public class AddConvertedTester
    {
        #region Real World Example

        /// <summary>
        /// We can convert a group of numeric-strings.
        /// </summary>
        [TestMethod]
        public void TestAddConverted_BetweenStringAndDouble()
        {
            Random random = new Random();

            // build a list of numbers
            var numbers = new List<double>(100);
            Sublist.AddGenerated(numbers.ToSublist(), 100, i => random.NextDouble() * 100);

            // convert to a list of strings
            var strings = new List<string>(100);
            Sublist.AddConverted(numbers.ToSublist(), strings.ToSublist(), i => i.ToString());

            // convert back to a list of doubles
            var converted = new List<double>(100);
            Sublist.AddConverted(strings.ToSublist(), converted.ToSublist(), s => Double.Parse(s));

            // check that the numbers are mostly the same
            // numbers will change a little due to precision issues
            bool result = Sublist.AreEqual(numbers.ToSublist(), converted.ToSublist(), (n, c) => Math.Abs(n - c) < .0001);
            Assert.IsTrue(result, "Could not convert between strings and numbers.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the source list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddConverted_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int> converter = i => i;
            Sublist.AddConverted(list, destination, converter);
        }

        /// <summary>
        /// An exception should be thrown if the destination list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddConverted_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int> converter = i => i;
            Sublist.AddConverted(list, destination, converter);
        }

        /// <summary>
        /// An exception should be thrown if the conversion delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddConverted_NullConverter_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int> converter = null;
            Sublist.AddConverted(list, destination, converter);
        }

        #endregion

        /// <summary>
        /// We will make sure we can use convert to double a list of numbers.
        /// </summary>
        [TestMethod]
        public void TestAddConverted_DoubleValues()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = Sublist.AddConverted(list, destination, i => i * 2);
            int[] expected = { 2, 4, 6, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "Not all of the items were added as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
