using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the RandomShuffleCopy methods.
    /// </summary>
    [TestClass]
    public class RandomShuffleCopyTester
    {
        #region Real World Example

        /// <summary>
        /// RandomShuffle is good for solving brute force problems on a small scale, 
        /// where the same combination might reoccur without causing a problem.
        /// Often NextPermutation is better for large problems where trying the same
        /// combination more than once is unacceptable.
        /// </summary>
        [TestMethod]
        public void TestRandomShuffleCopy_TrySortingSmallList()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(5);
            Sublist.Generate(5, i => random.Next(5)).AddTo(list.ToSublist());
            var sorted = list.ToSublist().Sort().AddTo(new List<int>().ToSublist());

            var destination = new int[5] { 5, 4, 3, 2, 1 };

            // try rearranging the items randomly until it is sorted (bogo sort -- may never happen)
            for (int tries = 0; tries != 100 && !destination.ToSublist().IsSorted(); ++tries)
            {
                list.ToSublist().RandomShuffle(random).CopyTo(destination.ToSublist());
            }
            destination.ToSublist().Sort().InPlace();  // in case the list wasn't sorted
            Assert.IsTrue(sorted.IsEqualTo(destination.ToSublist()), "Some items were lost during the shuffling.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomShuffleCopy_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Random random = new Random();
            list.RandomShuffle(random);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomShuffleCopy_WithGenerator_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int> generator = () => 0;
            list.RandomShuffle(generator);
        }

        /// <summary>
        /// An exception should be thrown if the random generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomShuffleCopy_NullRandom_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Random random = null;
            list.RandomShuffle(random);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomShuffleCopy_NullGenerator_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int> generator = null;
            list.RandomShuffle(generator);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomShuffleCopy_DestinationNull_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int> generator = () => 0;
            Sublist<List<int>, int> destination = null;
            list.RandomShuffle(generator).CopyTo(destination);
        }

        #endregion

        /// <summary>
        /// If the built in Random class isn't sufficient, then you can use the overload
        /// that simply takes a generator.
        /// </summary>
        [TestMethod]
        public void TestRandomShuffleCopy_ArbitraryGenerator()
        {            
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            using (RandomNumberGenerator random = RandomNumberGenerator.Create()) // slow
            {
                Func<int> generator = () =>
                    {
                        byte[] data = new byte[4];
                        random.GetBytes(data);
                        return BitConverter.ToInt32(data, 0);
                    };
                var result = list.RandomShuffle(generator).CopyTo(destination);
                Assert.AreEqual(destination.Count, result.SourceOffset, "The source offset was wrong.");
                Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            }
            var sorted = list.Nest(0, destination.Count).Sort().AddTo(new List<int>().ToSublist());
            destination.Sort().InPlace();
            Assert.IsTrue(sorted.IsEqualTo(destination), "Some of the items were lost during the shuffling.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
