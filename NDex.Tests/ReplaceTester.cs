using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;

namespace NDex.Test
{
    /// <summary>
    /// Tests the Replace methods.
    /// </summary>
    [TestClass]
    public class ReplaceTester
    {
        #region Real World Example

        /// <summary>
        /// If you had a list of numbers, and wanted to make sure that they were all positive,
        /// you could replace the negatives with their absolute values.
        /// </summary>
        [TestMethod]
        public void TestReplace_AbsoluteValue()
        {
            Random random = new Random();

            // build a list of numbers
            var list = new List<int>(100);
            Sublist.Grow(list, 100, () => random.Next(-49, 50));

            Sublist.Replace(list.ToSublist(), i => i < 0, i => -i);

            Assert.IsTrue(Sublist.TrueForAll(list.ToSublist(), i => i >= 0), "Not all values were positive.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int replacement = 0;
            Func<int, bool> predicate = i => true;
            Sublist.Replace(list, predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_WithGenerator_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => true;
            Sublist.Replace(list, predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int replacement = 0;
            Func<int, bool> predicate = null;
            Sublist.Replace(list, predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_WithGenerator_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = null;
            Sublist.Replace(list, predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReplace_NullGenerator_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int> generator = null;
            Func<int, bool> predicate = i => true;
            Sublist.Replace(list, predicate, generator);
        }

        #endregion

        /// <summary>
        /// If the predicate is satified by an item, it should be replaced.
        /// </summary>
        [TestMethod]
        public void TestReplace_ItemsMatchingPredicateAreReplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, });
            int replacement = 3;
            Func<int, bool> predicate = i => i % 2 == 0;
            Sublist.Replace(list, predicate, replacement);
            int[] expected = { 1, 3, 3, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The list did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the predicate is satified by an item, it should be replaced.
        /// </summary>
        [TestMethod]
        public void TestReplace_WithGenerator_ItemsMatchingPredicateAreReplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, });
            Func<int, int> generator = i => i + 1; // make odd by adding one
            Func<int, bool> predicate = i => i % 2 == 0;
            Sublist.Replace(list, predicate, generator);
            int[] expected = { 1, 3, 3, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The list did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
