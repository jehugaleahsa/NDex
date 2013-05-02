using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    /// <summary>
    /// Tests the RandomShuffle methods.
    /// </summary>
    [TestClass]
    public class RandomShuffleTester
    {
        #region Real World Example

        /// <summary>
        /// RandomShuffle is good for solving brute force problems on a small scale, 
        /// where the same combination might reoccur without causing a problem.
        /// Often NextPermutation is better for large problems where trying the same
        /// combination more than once is unacceptable.
        /// </summary>
        [TestMethod]
        public void TestRandomShuffle_TrySortingSmallList()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(5);
            Sublist.AddGenerated(list.ToSublist(), 5, i => random.Next(5));
            var set = new HashSet<int>(list);

            // try rearranging the items random until it is sorted (may never happen -- bad unit test)
            for (int tries = 0; tries != 100 && !Sublist.IsSorted(list.ToSublist()); ++tries)
            {
                Sublist.RandomShuffle(list.ToSublist(), random);
            }
            Assert.IsTrue(set.SetEquals(list), "Some items were lost during the shuffling.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomShuffle_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Random random = new Random();
            Sublist.RandomShuffle(list, random);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomShuffle_WithGenerator_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int> generator = () => 0;
            Sublist.RandomShuffle(list, generator);
        }

        /// <summary>
        /// An exception should be thrown if the random generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomShuffle_NullRandom_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Random random = null;
            Sublist.RandomShuffle(list, random);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomShuffle_NullGenerator_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int> generator = null;
            Sublist.RandomShuffle(list, generator);
        }

        #endregion

        /// <summary>
        /// If the built in Random class isn't sufficient, then you can use the overload
        /// that simply takes a generator.
        /// </summary>
        [TestMethod]
        public void TestRandomShuffle_ArbitraryGenerator()
        {            
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            var set = new HashSet<int>(list);
            using (RandomNumberGenerator random = RandomNumberGenerator.Create()) // slow
            {
                Func<int> generator = () =>
                    {
                        byte[] data = new byte[4];
                        random.GetBytes(data);
                        return BitConverter.ToInt32(data, 0);
                    };
                Sublist.RandomShuffle(list, generator);
            }
            Assert.IsTrue(set.SetEquals(list), "Some of the items were lost during the shuffling.");
        }
    }
}
