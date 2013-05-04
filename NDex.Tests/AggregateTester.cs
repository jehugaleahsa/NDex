using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the Aggregate methods.
    /// </summary>
    [TestClass]
    public class AggregateTester
    {
        #region Real World Examples

        /// <summary>
        /// We can use aggregate to sum a list of numbers together.
        /// </summary>
        [TestMethod]
        public void TestAggregate_SumValues()
        {
            int[] values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int sum = Sublist.Aggregate(values.ToSublist(), (s, i) => s + i);
            Assert.AreEqual(55, sum, "The values were not aggregated correctly.");
        }

        /// <summary>
        /// We can use aggregate to build a string using a StringBuilder.
        /// </summary>
        [TestMethod]
        public void TestAggregate_BuildString()
        {
            int[] values = new int[] { 1, 2, 3, 4, 5 };
            string actual = Sublist.Aggregate(values.ToSublist(), new StringBuilder(), (s, i) => s.Append(i)).ToString();
            string expected = "12345";
            Assert.AreEqual(expected, actual, "The string wasn't built as expected.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAggregate_ListNull_Throws()
        {
            Sublist<int[], int> sublist = null;
            Func<int, int, int> aggregator = (s, i) => s + i;
            Sublist.Aggregate(sublist, aggregator);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAggregate_Seeded_ListNull_Throws()
        {
            Sublist<int[], int> sublist = null;
            int seed = 0;
            Func<int, int, int> aggregator = (s, i) => s + i;
            Sublist.Aggregate(sublist, seed, aggregator);
        }

        /// <summary>
        /// An exception should be thrown if the list is empty and we don't supply a seed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAggregate_ListEmpty_Throws()
        {
            var sublist = new int[0].ToSublist();
            Func<int, int, int> aggregator = (s, i) => s + i;
            Sublist.Aggregate(sublist, aggregator);
        }

        /// <summary>
        /// An exception should be thrown if the aggregator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAggregate_AggregatorNull_Throws()
        {
            var sublist = new int[1].ToSublist();
            Func<int, int, int> aggregator = null;
            Sublist.Aggregate(sublist, aggregator);
        }

        /// <summary>
        /// An exception should be thrown if the aggregator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAggregate_Seeded_AggregatorNull_Throws()
        {
            var sublist = new int[0].ToSublist();
            int seed = 0;
            Func<int, int, int> aggregator = null;
            Sublist.Aggregate(sublist, seed, aggregator);
        }

        #endregion

        /// <summary>
        /// If we try to aggregate an empty list and provide a seed,
        /// the seed should be returned.
        /// </summary>
        [TestMethod]
        public void TestAggregate_ListEmpty_WithSeed_ReturnsSeed()
        {
            var list = TestHelper.Wrap(new List<int>());
            int seed = 123;
            Func<int, int, int> aggregator = (s, i) => s + i;
            int actual = Sublist.Aggregate(list, seed, aggregator);
            Assert.AreEqual(seed, actual, "The seed was not returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we try to aggregate a list with only one item,
        /// the first item should be returned.
        /// </summary>
        [TestMethod]
        public void TestAggregate_ListOneItem_ReturnsFirstItem()
        {
            var list = TestHelper.Wrap(new List<int>() { 123 });
            Func<int, int, int> aggregator = (s, i) => s + i;
            int actual = Sublist.Aggregate(list, aggregator);
            Assert.AreEqual(list[0], actual, "The seed was not returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
