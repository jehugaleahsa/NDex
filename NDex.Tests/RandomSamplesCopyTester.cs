using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the RandomSamplesCopy methods.
    /// </summary>
    [TestClass]
    public class RandomSamplesCopyTester
    {
        #region Real World Examples

        /// <summary>
        /// We can pick unique values at random.
        /// </summary>
        [TestMethod]
        public void TestRandomSamplesCopy()
        {
            Random random = new Random();

            // build a list of values
            var list = new List<int>(100);
            Sublist.Generate(100, i => i).AddTo(list.ToSublist());

            // grab 5 values at random
            const int numberOfSamples = 10;
            var samples = new List<int>(numberOfSamples);
            Sublist.Generate(numberOfSamples, 0).AddTo(samples.ToSublist());
            int index = list.ToSublist().RandomSamples(numberOfSamples, random).CopyTo(samples.ToSublist());
            Assert.AreEqual(samples.Count, index, "The wrong index was returned.");

            // make sure the same value doesn't occur multiple times
            samples.ToSublist().Sort().InPlace();
            Assert.IsFalse(samples.ToSublist().FindDuplicates(), "The same value was picked multiple times.");

            // make sure the samples are a subset of the original values.
            bool result = samples.ToSublist().IsSubset(list.ToSublist());
            Assert.IsTrue(result, "Some samples were not in the original list.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamplesCopy_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            Random random = new Random();
            list.RandomSamples(0, random);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamplesCopy_WithGenerator_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            int numberOfSamples = 0;
            Func<int> generator = () => 0;
            list.RandomSamples(numberOfSamples, generator);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamplesCopy_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int numberOfSamples = 0;
            IExpandableSublist<List<int>, int> destination = null;
            Random random = new Random();
            list.RandomSamples(numberOfSamples, random).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamplesCopy_WithGenerator_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int numberOfSamples = 0;
            IExpandableSublist<List<int>, int> destination = null;
            Func<int> generator = () => 0;
            list.RandomSamples(numberOfSamples, generator).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the random number generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamplesCopy_NullRandom_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int numberOfSamples = 0;
            Random random = null;
            list.RandomSamples(numberOfSamples, random);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamplesCopy_NullGenerator_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int numberOfSamples = 0;
            Func<int> generator = null;
            list.RandomSamples(numberOfSamples, generator);
        }

        #endregion

        /// <summary>
        /// If we try to add the same number of items as there are in the list,
        /// then we are simply copying.
        /// </summary>
        [TestMethod]
        public void TestRandomSamplesCopy_DestinationSizeEqualsSource_SimplyCopies()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            Func<int> generator = () => 0;
            var result = list.RandomSamples(destination.Count, generator).CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            destination.Sort().InPlace(); // guarantees order -> actually unnecessary
            int[] expected = { 1, 2, 3, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The wrong samples were chosen.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to use a generator that returns negatives.
        /// </summary>
        [TestMethod]
        public void TestRandomSamplesCopy_GeneratorWithNegatives()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            Random random = new Random();
            Func<int> generator = () => random.Next(-5, 6);
            var result = list.RandomSamples(destination.Count, generator).CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            destination.Sort().InPlace();
            Assert.IsTrue(destination.IsSubset(list), "Not all of the items in the destination exist in the original list.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
