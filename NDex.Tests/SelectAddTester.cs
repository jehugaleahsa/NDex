using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the SelectAdd methods.
    /// </summary>
    [TestClass]
    public class SelectAddTester
    {
        #region Real World Example

        /// <summary>
        /// We can convert a group of numeric-strings.
        /// </summary>
        [TestMethod]
        public void TestSelectAdd_BetweenStringAndDouble()
        {
            Random random = new Random();

            // build a list of numbers
            var numbers = new List<double>(100);
            Sublist.Generate(100, i => random.NextDouble() * 100).AddTo(numbers.ToSublist());

            // convert to a list of strings
            var strings = new List<string>(100);
            numbers.ToSublist().Select(i => i.ToString()).AddTo(strings.ToSublist());

            // convert back to a list of doubles
            var converted = new List<double>(100);
            strings.ToSublist().Select(s => Double.Parse(s)).AddTo(converted.ToSublist());

            // check that the numbers are mostly the same
            // numbers will change a little due to precision issues
            bool result = numbers.ToSublist().IsEqualTo(converted.ToSublist(), (n, c) => Math.Abs(n - c) < .0001);
            Assert.IsTrue(result, "Could not convert between strings and numbers.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the source list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSelectAdd_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            Func<int, int> converter = i => i;
            list.Select(converter);
        }

        /// <summary>
        /// An exception should be thrown if the destination list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSelectAdd_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            Func<int, int> converter = i => i;
            list.Select(converter).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the conversion delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSelectAdd_NullConverter_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int> converter = null;
            list.Select(converter);
        }

        #endregion

        /// <summary>
        /// We will make sure we can use convert to double a list of numbers.
        /// </summary>
        [TestMethod]
        public void TestSelectAdd_DoubleValues()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list.Select(i => i * 2).AddTo(destination);
            int[] expected = { 2, 4, 6, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "Not all of the items were added as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
