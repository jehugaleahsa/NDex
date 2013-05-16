using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the CopyRandomSamples methods.
    /// </summary>
    [TestClass]
    public class CopyRandomSamplesTester
    {
        #region Real World Examples

        /// <summary>
        /// We can pick unique values at random.
        /// </summary>
        [TestMethod]
        public void TestCopyRandomSamples()
        {
            Random random = new Random();

            // build a list of values
            var list = new List<int>(100);
            Sublist.AddGenerated(list.ToSublist(), 100, i => i);

            // grab 5 values at random
            const int numberOfSamples = 10;
            var samples = new List<int>(numberOfSamples);
            Sublist.AddGenerated(samples.ToSublist(), numberOfSamples, 0);
            int index = Sublist.CopyRandomSamples(list.ToSublist(), samples.ToSublist(), random);
            Assert.AreEqual(samples.Count, index, "The wrong index was returned.");

            // make sure the same value doesn't occur multiple times
            Sublist.QuickSort(samples.ToSublist());
            Assert.IsFalse(Sublist.FindDuplicates(samples.ToSublist()), "The same value was picked multiple times.");

            // make sure the samples are a subset of the original values.
            bool result = Sublist.IsSubset(list.ToSublist(), samples.ToSublist());
            Assert.IsTrue(result, "Some samples were not in the original list.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyRandomSamples_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Random random = new Random();
            Sublist.CopyRandomSamples(list, destination, random);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyRandomSamples_WithGenerator_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int> generator = () => 0;
            Sublist.CopyRandomSamples(list, destination, generator);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyRandomSamples_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Random random = new Random();
            Sublist.CopyRandomSamples(list, destination, random);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyRandomSamples_WithGenerator_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int> generator = () => 0;
            Sublist.CopyRandomSamples(list, destination, generator);
        }

        /// <summary>
        /// An exception should be thrown if the random number generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyRandomSamples_NullRandom_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Random random = null;
            Sublist.CopyRandomSamples(list, destination, random);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyRandomSamples_NullGenerator_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int> generator = null;
            Sublist.CopyRandomSamples(list, destination, generator);
        }

        #endregion

        /// <summary>
        /// If we try to add the same number of items as there are in the list,
        /// then we are simply copying.
        /// </summary>
        [TestMethod]
        public void TestCopyRandomSamples_DestinationSizeEqualsSource_SimplyCopies()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            Func<int> generator = () => 0;
            CopyResult result = Sublist.CopyRandomSamples(list, destination, generator);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Sublist.QuickSort(destination); // guarantees order -> actually unnecessary
            int[] expected = { 1, 2, 3, 4, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The wrong samples were chosen.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to use a generator that returns negatives.
        /// </summary>
        [TestMethod]
        public void TestCopyRandomSamples_GeneratorWithNegatives()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            Random random = new Random();
            Func<int> generator = () => random.Next(-5, 6);
            CopyResult result = Sublist.CopyRandomSamples(list, destination, generator);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Sublist.BubbleSort(destination);
            Assert.IsTrue(Sublist.IsSubset(list, destination), "Not all of the items in the destination exist in the original list.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
