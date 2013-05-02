using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    /// <summary>
    /// Tests the PreviousPermutation methods.
    /// </summary>
    [TestClass]
    public class PreviousPermutationTester
    {
        #region Real World Example

        /// <summary>
        /// PreviousPermutation is needed when you need to look at all possible arrangements of the list.
        /// This is usually helpful during optimization problems. We'll use it to find the highest possible
        /// max sub sum.
        /// </summary>
        [TestMethod]
        public void TestPreviousPermutation_FindsAllPermutations()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(5);
            Sublist.AddGenerated(list.ToSublist(), 5, i => random.Next(0, 10));

            // first, we must sort the items to make sure all permutations are enumerated
            Sublist.BubbleSort(list.ToSublist(), (x, y) => Comparer<int>.Default.Compare(y, x)); // prev. perm. requires reverse order initially

            int permutations = 1;
            while (Sublist.PreviousPermutation(list.ToSublist()))
            {
                ++permutations;
            }
            int expected = countPermutations(list);
            Assert.AreEqual(expected, permutations, "Not all permutations were enumerated.");
        }

        private int countPermutations(List<int> values)
        {
            var groups = values.GroupBy(i => i);
            int nominator = factorial(values.Count);
            int denominator = 1;
            foreach (var group in groups)
            {
                denominator *= factorial(group.Count());
            }
            return nominator / denominator;
        }

        private int factorial(int n)
        {
            int result = n;
            while (n > 1)
            {
                --n;
                result *= n;
            }
            return result;
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPreviousPermutation_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.PreviousPermutation(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPreviousPermutation_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.PreviousPermutation(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPreviousPermutation_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.PreviousPermutation(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPreviousPermutation_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            Sublist.PreviousPermutation(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPreviousPermutation_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.PreviousPermutation(list, comparison);
        }

        #endregion

        /// <summary>
        /// Since an empty list cannot be permutated, the result should always be false.
        /// </summary>
        [TestMethod]
        public void TestPreviousPermutation_EmptyList_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>());
            bool result = Sublist.PreviousPermutation(list);
            Assert.IsFalse(result, "An empty list should have no permutations.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Since a list with one list cannot be permutated, the result should always be false.
        /// </summary>
        [TestMethod]
        public void TestPreviousPermutation_ListOfOne_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            bool result = Sublist.PreviousPermutation(list);
            Assert.IsFalse(result, "A list with one item should have no permutations.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with the same item repeated should have only one permutation.
        /// </summary>
        [TestMethod]
        public void TestPreviousPermutation_AllSameValue_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 1, 1, });
            bool result = Sublist.PreviousPermutation(list, Comparer<int>.Default);
            Assert.IsFalse(result, "A list with one item should have no permutations.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// A list with different values should have n! permutations and they should all be unique.
        /// </summary>
        [TestMethod]
        public void TestPreviousPermutation_DifferentValues_NFactorialPermutations_AllUnique()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 4, 3, 2, 1 }); // initially sorted
            List<int[]> permutations = new List<int[]>() { list.ToArray() };
            while (Sublist.PreviousPermutation(list, Comparer<int>.Default.Compare))
            {
                Assert.IsFalse(permutations.Any(item => Sublist.AreEqual(list, item.ToSublist())), "The same permutation appeared twice.");
                permutations.Add(list.ToArray());
            }
            Assert.AreEqual(120, permutations.Count, "Not all permutations were found.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
