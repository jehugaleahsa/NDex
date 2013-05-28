using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the SelectCopy methods.
    /// </summary>
    [TestClass]
    public class SelectCopyTester
    {
        #region Real World Example

        /// <summary>
        /// We can convert a group of numeric-strings.
        /// </summary>
        [TestMethod]
        public void TestSelectCopy_BetweenStringAndDouble()
        {
            Random random = new Random();

            // build a list of numbers
            var numbers = new List<double>(100);
            Sublist.Generate(100, i => random.NextDouble() * 100).AddTo(numbers.ToSublist());

            // convert to a list of strings
            var strings = new List<string>(100);
            Sublist.Generate(100, String.Empty).AddTo(strings.ToSublist());
            numbers.ToSublist().Select(i => i.ToString()).CopyTo(strings.ToSublist());

            // convert back to a list of doubles
            var converted = new List<double>(100);
            Sublist.Generate(100, 0d).AddTo(converted.ToSublist());
            strings.ToSublist().Select(s => Double.Parse(s)).CopyTo(converted.ToSublist());

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
        public void TestSelectCopy_NullList_Throws()
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
        public void TestSelectCopy_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            Func<int, int> converter = i => i;
            list.Select(converter).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the conversion delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSelectCopy_NullConverter_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int> converter = null;
            list.Select(converter);
        }

        #endregion

        /// <summary>
        /// The destination should be filled as much as possible.
        /// </summary>
        [TestMethod]
        public void TestSelectCopy_DestinationTooSmall_StopsPrematurely()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0 });
            Func<int, int> converter = i => i;
            var result = list.Select(converter).CopyTo(destination);
            Assert.AreEqual(1, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong number of items were converted.");
            var expected = new int[] { 1 }.ToSublist();
            Assert.IsTrue(destination.IsEqualTo(expected), "The converted values were not stored in the destination.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the source is smaller than the destination, some space should be left in the destination.
        /// </summary>
        [TestMethod]
        public void TestSelectCopy_SourceSmallerThanDestination_StopsPrematurely()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            Func<int, int> converter = i => i;
            var result = list.Select(converter).CopyTo(destination);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The wrong number of items were converted.");
            var expected = new int[] { 1, 2, 0 }.ToSublist();
            Assert.IsTrue(destination.IsEqualTo(expected), "The converted values were not stored in the destination.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
