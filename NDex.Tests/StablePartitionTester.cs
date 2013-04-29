using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the StablePartition method.
    /// </summary>
    [TestClass]
    public class StablePartitionTester
    {
        #region Real World Example

        /// <summary>
        /// We'll use StablePartition to move even items to the front of a list and keep them in relative order.
        /// </summary>
        [TestMethod]
        public void TestStablePartition_MoveEvenNumbersToTheFront()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next()), list.ToSublist());

            // now we'll sort them with the expectation that they'll stay sorted
            Sublist.QuickSort(list.ToSublist());

            // we'll partition the list and make sure evens and odds are separated
            int index = Sublist.StablePartition(list.ToSublist(), i => i % 2 == 0);
            Assert.IsTrue(Sublist.TrueForAll(list.ToSublist(0, index), i => i % 2 == 0), "Odds were discovered in the front partition.");
            Assert.IsTrue(Sublist.IsSorted(list.ToSublist(0, index)), "The partition was not stable.");
            Assert.IsTrue(Sublist.TrueForAll(list.ToSublist(index), i => i % 2 != 0), "Evens were discovered in the back partition.");
            Assert.IsTrue(Sublist.IsSorted(list.ToSublist(index)), "The partition was not stable.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStablePartition_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            Sublist.StablePartition(list, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStablePartition_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null;
            Sublist.StablePartition(list, predicate);
        }

        #endregion

        /// <summary>
        /// If every item in a list satisfies the predicate, then the returned index should equal the count.
        /// </summary>
        [TestMethod]
        public void TestStablePartition_AllItemsSatisfy_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 6, 8, });
            int index = Sublist.StablePartition(list, i => i % 2 == 0);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If none of the items in the list satisfies the predicate, then the returned index should equal zero.
        /// </summary>
        [TestMethod]
        public void TestStablePartition_NoItemsSatisfy_ReturnsZero()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7 });
            int index = Sublist.StablePartition(list, i => i % 2 == 0);
            Assert.AreEqual(0, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If all of the items satisfying the predicate are in the back, then they should be moved forward.
        /// </summary>
        [TestMethod]
        public void TestStablePartition_MatchingItemsInBack_MovedForward()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7, 2, 4, 6, 8 });
            int index = Sublist.StablePartition(list, i => i % 2 == 0);
            Assert.AreEqual(4, index, "The wrong index was returned.");

            Assert.IsTrue(Sublist.TrueForAll(list.Nest(0, index), i => i % 2 == 0), "Odds were found in the front partition.");
            var evens = new HashSet<int>() { 2, 4, 6, 8 };
            Assert.IsTrue(evens.SetEquals(list.Nest(0, index)), "Not all evens are accounted for.");

            Assert.IsTrue(Sublist.TrueForAll(list.Nest(index), i => i % 2 != 0), "Evens were found in the back partition.");
            var odds = new HashSet<int>() { 1, 3, 5, 7 };
            Assert.IsTrue(odds.SetEquals(list.Nest(index)), "Not all odds are accounted for.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If all of the items satisfying the predicate are in the front, then they should stay there.
        /// </summary>
        [TestMethod]
        public void TestStablePartition_MatchingItemsInFront_StayPut()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 6, 8, 1, 3, 5, 7 });
            int index = Sublist.StablePartition(list, i => i % 2 == 0);
            Assert.AreEqual(4, index, "The wrong index was returned.");

            Assert.IsTrue(Sublist.TrueForAll(list.Nest(0, index), i => i % 2 == 0), "Odds were found in the front partition.");
            var evens = new HashSet<int>() { 2, 4, 6, 8 };
            Assert.IsTrue(evens.SetEquals(list.Nest(0, index)), "Not all evens are accounted for.");

            Assert.IsTrue(Sublist.TrueForAll(list.Nest(index), i => i % 2 != 0), "Evens were found in the back partition.");
            var odds = new HashSet<int>() { 1, 3, 5, 7 };
            Assert.IsTrue(odds.SetEquals(list.Nest(index)), "Not all odds are accounted for.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If all of the items satisfying the predicate are in the front, then they should stay there.
        /// </summary>
        [TestMethod]
        public void TestStablePartition_MatchingItemsInterweaved_MovedForward()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 });
            int index = Sublist.StablePartition(list, i => i % 2 == 0);
            Assert.AreEqual(4, index, "The wrong index was returned.");

            Assert.IsTrue(Sublist.TrueForAll(list.Nest(0, index), i => i % 2 == 0), "Odds were found in the front partition.");
            var evens = new HashSet<int>() { 2, 4, 6, 8 };
            Assert.IsTrue(evens.SetEquals(list.Nest(0, index)), "Not all evens are accounted for.");

            Assert.IsTrue(Sublist.TrueForAll(list.Nest(index), i => i % 2 != 0), "Evens were found in the back partition.");
            var odds = new HashSet<int>() { 1, 3, 5, 7 };
            Assert.IsTrue(odds.SetEquals(list.Nest(index)), "Not all odds are accounted for.");

            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
