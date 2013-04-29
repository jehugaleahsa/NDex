using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the AddRandomSamples methods.
    /// </summary>
    [TestClass]
    public class AddRandomSamplesTester
    {
        #region Real World Examples

        /// <summary>
        /// We can pick unique values at random.
        /// </summary>
        [TestMethod]
        public void TestAddRandomSamples()
        {
            Random random = new Random();

            // build a list of values
            var list = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100), list.ToSublist());

            // grab 5 values at random
            const int numberOfSamples = 10;
            var samples = new List<int>(numberOfSamples);
            Sublist.AddRandomSamples(list.ToSublist(), numberOfSamples, samples.ToSublist(), random);

            // make sure the same value doesn't occur multiple times
            Sublist.QuickSort(samples.ToSublist());
            Assert.IsFalse(Sublist.ContainsDuplicates(samples.ToSublist()), "The same value was picked multiple times.");

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
        public void TestAddRandomSamples_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int numberOfSamples = 0;
            Sublist<List<int>, int> destination = new List<int>();
            Random random = new Random();
            Sublist.AddRandomSamples(list, numberOfSamples, destination, random);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddRandomSamples_WithGenerator_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int numberOfSamples = 0;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int> generator = () => 0;
            Sublist.AddRandomSamples(list, numberOfSamples, destination, generator);
        }

        /// <summary>
        /// An exception should be thrown if the number of samples is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddRandomSamples_NegativeSamples_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfSamples = -1;
            Sublist<List<int>, int> destination = new List<int>();
            Random random = new Random();
            Sublist.AddRandomSamples(list, numberOfSamples, destination, random);
        }

        /// <summary>
        /// An exception should be thrown if the number of samples is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddRandomSamples_WithGenerator_NegativeSamples_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfSamples = -1;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int> generator = () => 0;
            Sublist.AddRandomSamples(list, numberOfSamples, destination, generator);
        }

        /// <summary>
        /// An exception should be thrown if the number of samples is larger than the list.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddRandomSamples_SamplesTooBig_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfSamples = 1;
            Sublist<List<int>, int> destination = new List<int>();
            Random random = new Random();
            Sublist.AddRandomSamples(list, numberOfSamples, destination, random);
        }

        /// <summary>
        /// An exception should be thrown if the number of samples is larger than the list.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestAddRandomSamples_WithGenerator_SamplesTooBig_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfSamples = 1;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int> generator = () => 0;
            Sublist.AddRandomSamples(list, numberOfSamples, destination, generator);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddRandomSamples_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfSamples = 0;
            Sublist<List<int>, int> destination = null;
            Random random = new Random();
            Sublist.AddRandomSamples(list, numberOfSamples, destination, random);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddRandomSamples_WithGenerator_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfSamples = 0;
            Sublist<List<int>, int> destination = null;
            Func<int> generator = () => 0;
            Sublist.AddRandomSamples(list, numberOfSamples, destination, generator);
        }

        /// <summary>
        /// An exception should be thrown if the random number generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddRandomSamples_NullRandom_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfSamples = 0;
            Sublist<List<int>, int> destination = new List<int>();
            Random random = null;
            Sublist.AddRandomSamples(list, numberOfSamples, destination, random);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddRandomSamples_NullGenerator_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int numberOfSamples = 0;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int> generator = null;
            Sublist.AddRandomSamples(list, numberOfSamples, destination, generator);
        }

        #endregion

        /// <summary>
        /// If we try to add the same number of items as there are in the list,
        /// then we are simply copying.
        /// </summary>
        [TestMethod]
        public void TestAddRandomSamples_SampleSizeEqualsSource_SimplyCopies()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            int numberOfSamples = list.Count;
            var destination = TestHelper.Wrap(new List<int>());
            Func<int> generator = () => 0;
            Sublist.AddRandomSamples(list, numberOfSamples, destination, generator);
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
        public void TestAddRandomSamples_GeneratorWithNegatives()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            int numberOfSamples = 5;
            var destination = TestHelper.Wrap(new List<int>());
            Random random = new Random();
            Func<int> generator = () => random.Next(-5, 6);
            Sublist.AddRandomSamples(list, numberOfSamples, destination, generator);
            Sublist.BubbleSort(destination);
            Assert.IsTrue(Sublist.IsSubset(list, destination), "Not all of the items in the destination exist in the original list.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
