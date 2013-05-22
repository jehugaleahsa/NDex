using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the CountIf methods.
    /// </summary>
    [TestClass]
    public class CountIfTester
    {
        #region Real World Example

        /// <summary>
        /// We will count the number of even numbers.
        /// </summary>
        [TestMethod]
        public void TestCountIf_CountEvens()
        {
            Random random = new Random();

            // build list of random evens
            var evens = new List<int>(50);
            Sublist.Generate(50, i => 2 * random.Next(0, 50)).AddTo(evens.ToSublist());

            // build list of random odds
            var odds = new List<int>(50);
            Sublist.Generate(50, i => 1 + 2 * random.Next(0, 50)).AddTo(odds.ToSublist());

            // join the two together and shuffle
            var list = new List<int>(100);
            list.AddRange(evens);
            list.AddRange(odds);
            Sublist.RandomShuffle(list.ToSublist(), random);

            int result = Sublist.CountIf(list.ToSublist(), i => i % 2 == 0);
            Assert.AreEqual(50, result, "Counted the wrong number of evens.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCountIf_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            Sublist.CountIf(list, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCountIf_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null;
            Sublist.CountIf(list, predicate);
        }

        #endregion

        /// <summary>
        /// If all items match the predicate, then the result should equal the count.
        /// </summary>
        [TestMethod]
        public void TestCountIf_AllMatch_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            Func<int, bool> predicate = i => i % 2 != 0;
            int result = Sublist.CountIf(list, predicate);
            Assert.AreEqual(list.Count, result, "The count was wrong.");
        }

        /// <summary>
        /// If no items match the predicate, then the result should equal zero.
        /// </summary>
        [TestMethod]
        public void TestCountIf_NoneMatch_ReturnsZero()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            Func<int, bool> predicate = i => i % 2 == 0;
            int result = Sublist.CountIf(list, predicate);
            Assert.AreEqual(0, result, "The count was wrong.");
        }
    }
}
