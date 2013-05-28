using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the RandomSamplesAdd methods.
    /// </summary>
    [TestClass]
    public class RandomSamplesAddTester
    {
        #region Real World Examples

        /// <summary>
        /// We can pick unique values at random.
        /// </summary>
        [TestMethod]
        public void TestRandomSamplesAdd()
        {
            Random random = new Random();

            // build a list of values
            var list = new List<int>(100);
            Sublist.Generate(100, i => i).AddTo(list.ToSublist());

            // grab 5 values at random
            const int numberOfSamples = 10;
            var samples = new List<int>(numberOfSamples);
            list.ToSublist().RandomSamples(numberOfSamples, random).AddTo(samples.ToSublist());

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
        public void TestRandomSamplesAdd_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            int numberOfSamples = 0;
            Random random = new Random();
            list.RandomSamples(numberOfSamples, random);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamplesAdd_WithGenerator_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            int numberOfSamples = 0;
            Func<int> generator = () => 0;
            list.RandomSamples(numberOfSamples, generator);
        }

        /// <summary>
        /// An exception should be thrown if the number of samples is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRandomSamplesAdd_NegativeSamples_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int numberOfSamples = -1;
            Random random = new Random();
            list.RandomSamples(numberOfSamples, random);
        }

        /// <summary>
        /// An exception should be thrown if the number of samples is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRandomSamplesAdd_WithGenerator_NegativeSamples_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int numberOfSamples = -1;
            Func<int> generator = () => 0;
            list.RandomSamples(numberOfSamples, generator);
        }

        /// <summary>
        /// An exception should be thrown if the number of samples is larger than the list.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRandomSamplesAdd_SamplesTooBig_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int numberOfSamples = 1;
            Random random = new Random();
            list.RandomSamples(numberOfSamples, random);
        }

        /// <summary>
        /// An exception should be thrown if the number of samples is larger than the list.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRandomSamplesAdd_WithGenerator_SamplesTooBig_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int numberOfSamples = 1;
            Func<int> generator = () => 0;
            list.RandomSamples(numberOfSamples, generator);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamplesAdd_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int numberOfSamples = 0;
            IExpandableSublist<List<int>, int> destination = null;
            Random random = new Random();
            list.RandomSamples(numberOfSamples, random).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamplesAdd_WithGenerator_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int numberOfSamples = 0;
            IExpandableSublist<List<int>, int> destination = null;
            Func<int> generator = () => 0;
            list.RandomSamples(numberOfSamples, generator).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the random number generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamplesAdd_NullRandom_Throws()
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
        public void TestRandomSamplesAdd_NullGenerator_Throws()
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
        public void TestRandomSamplesAdd_SampleSizeEqualsSource_SimplyCopies()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5 });
            int numberOfSamples = list.Count;
            var destination = TestHelper.Wrap(new List<int>());
            Func<int> generator = () => 0;
            destination = list.RandomSamples(numberOfSamples, generator).AddTo(destination);
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
        public void TestRandomSamplesAdd_GeneratorWithNegatives()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            int numberOfSamples = 5;
            var destination = TestHelper.Wrap(new List<int>());
            Random random = new Random();
            Func<int> generator = () => random.Next(-5, 6);
            destination = list.RandomSamples(numberOfSamples, generator).AddTo(destination);
            destination.Sort().InPlace();
            Assert.IsTrue(destination.IsSubset(list), "Not all of the items in the destination exist in the original list.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
