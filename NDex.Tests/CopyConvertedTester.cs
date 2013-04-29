﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the CopyConverted methods.
    /// </summary>
    [TestClass]
    public class CopyConvertedTester
    {
        #region Real World Example

        /// <summary>
        /// We can convert a group of numeric-strings.
        /// </summary>
        [TestMethod]
        public void TestCopyConverted_BetweenStringAndDouble()
        {
            Random random = new Random();

            // build a list of numbers
            var numbers = new List<double>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.NextDouble() * 100), numbers.ToSublist());

            // convert to a list of strings
            var strings = new List<string>(100);
            Sublist.Add(Enumerable.Repeat(String.Empty, 100), strings.ToSublist());
            Sublist.CopyConverted(numbers.ToSublist(), strings.ToSublist(), i => i.ToString());

            // convert back to a list of doubles
            var converted = new List<double>(100);
            Sublist.Add(Enumerable.Repeat(0d, 100), converted.ToSublist());
            Sublist.CopyConverted(strings.ToSublist(), converted.ToSublist(), s => Double.Parse(s));

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
        public void TestCopyConverted_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int> converter = i => i;
            Sublist.CopyConverted(list, destination, converter);
        }

        /// <summary>
        /// An exception should be thrown if the destination list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyConverted_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int> converter = i => i;
            Sublist.CopyConverted(list, destination, converter);
        }

        /// <summary>
        /// An exception should be thrown if the conversion delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyConverted_NullConverter_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int> converter = null;
            Sublist.CopyConverted(list, destination, converter);
        }

        #endregion

        /// <summary>
        /// The destination should be filled as much as possible.
        /// </summary>
        [TestMethod]
        public void TestCopyConverted_DestinationTooSmall_StopsPrematurely()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0 });
            Func<int, int> converter = i => i;
            CopyResult result = Sublist.CopyConverted(list, destination, converter);
            Assert.AreEqual(1, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong number of items were converted.");
            var expected = new int[] { 1 }.ToSublist();
            Assert.IsTrue(Sublist.AreEqual(destination, expected), "The converted values were not stored in the destination.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the source is smaller than the destination, some space should be left in the destination.
        /// </summary>
        [TestMethod]
        public void TestCopyConverted_SourceSmallerThanDestination_StopsPrematurely()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            Func<int, int> converter = i => i;
            CopyResult result = Sublist.CopyConverted(list, destination, converter);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The wrong number of items were converted.");
            var expected = new int[] { 1, 2, 0 }.ToSublist();
            Assert.IsTrue(Sublist.AreEqual(destination, expected), "The converted values were not stored in the destination.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
