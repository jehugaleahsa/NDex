using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the RandomShuffle methods.
    /// </summary>
    [TestClass]
    public class RandomShuffleInPlaceTester
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
            Sublist.Generate(5, i => random.Next(5)).AddTo(list.ToSublist());
            var sorted = list.ToSublist().Sort().AddTo(new List<int>().ToSublist());

            // try rearranging the items random until it is sorted (may never happen -- bad unit test)
            for (int tries = 0; tries != 100 && !list.ToSublist().IsSorted(); ++tries)
            {
                list.ToSublist().RandomShuffle(random).InPlace();
            }
            list.ToSublist().Sort().InPlace();  // in case the list wasn't sorted
            Assert.IsTrue(sorted.IsEqualTo(list.ToSublist()), "Some items were lost during the shuffling.");
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
            IExpandableSublist<List<int>, int> list = null;
            Random random = new Random();
            list.RandomShuffle(random);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomShuffle_WithGenerator_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            Func<int> generator = () => 0;
            list.RandomShuffle(generator);
        }

        /// <summary>
        /// An exception should be thrown if the random generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomShuffle_NullRandom_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            Random random = null;
            list.RandomShuffle(random);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomShuffle_NullGenerator_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int> generator = null;
            list.RandomShuffle(generator);
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
            var sorted = list.Sort().AddTo(new List<int>().ToSublist());
            using (RandomNumberGenerator random = RandomNumberGenerator.Create()) // slow
            {
                Func<int> generator = () =>
                    {
                        byte[] data = new byte[4];
                        random.GetBytes(data);
                        return BitConverter.ToInt32(data, 0);
                    };
                list.RandomShuffle(generator).InPlace();
            }
            list.Sort().InPlace();
            Assert.IsTrue(sorted.IsEqualTo(list), "Some of the items were lost during the shuffling.");
        }
    }
}
