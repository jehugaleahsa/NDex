using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the PartitionInPlace method.
    /// </summary>
    [TestClass]
    public class PartitionInPlaceTester
    {
        #region Real World Example

        /// <summary>
        /// We'll use PartitionInPlace to move even items to the front of a list.
        /// </summary>
        [TestMethod]
        public void TestPartitionInPlace_MoveEvenNumbersToTheFront()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next()).AddTo(list.ToSublist());

            // we'll partition the list and make sure evens and odds are separated
            int index = list.ToSublist().Partition(i => i % 2 == 0).InPlace();
            Assert.IsFalse(list.ToSublist(0, index).Find(i => i % 2 != 0), "Odds were discovered in the front partition.");
            Assert.IsFalse(list.ToSublist(index).Find(i => i % 2 == 0), "Evens were discovered in the back partition.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartitionInPlace_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            list.Partition(predicate);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartitionInPlace_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null;
            list.Partition(predicate);
        }

        #endregion

        /// <summary>
        /// If every item in a list satisfies the predicate, then the returned index should equal the count.
        /// </summary>
        [TestMethod]
        public void TestPartitionInPlace_AllItemsSatisfy_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 6, 8, });
            int index = list.Partition(i => i % 2 == 0).InPlace();
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If none of the items in the list satisfies the predicate, then the returned index should equal zero.
        /// </summary>
        [TestMethod]
        public void TestPartitionInPlace_NoItemsSatisfy_ReturnsZero()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7 });
            int index = list.Partition(i => i % 2 == 0).InPlace();
            Assert.AreEqual(0, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If all of the items satisfying the predicate are in the back, then they should be moved forward.
        /// </summary>
        [TestMethod]
        public void TestPartitionInPlace_MatchingItemsInBack_MovedForward()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7, 2, 4, 6, 8 });
            int index = list.Partition(i => i % 2 == 0).InPlace();
            Assert.AreEqual(4, index, "The wrong index was returned.");

            Assert.IsFalse(list.Nest(0, index).Find(i => i % 2 != 0), "Odds were found in the front partition.");
            var evens = new HashSet<int>() { 2, 4, 6, 8 };
            Assert.IsTrue(evens.SetEquals(list.Nest(0, index)), "Not all evens are accounted for.");

            Assert.IsFalse(list.Nest(index).Find(i => i % 2 == 0), "Evens were found in the back partition.");
            var odds = new HashSet<int>() { 1, 3, 5, 7 };
            Assert.IsTrue(odds.SetEquals(list.Nest(index)), "Not all odds are accounted for.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If all of the items satisfying the predicate are in the front, then they should stay there.
        /// </summary>
        [TestMethod]
        public void TestPartitionInPlace_MatchingItemsInFront_StayPut()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 6, 8, 1, 3, 5, 7 });
            int index = list.Partition(i => i % 2 == 0).InPlace();
            Assert.AreEqual(4, index, "The wrong index was returned.");

            Assert.IsFalse(list.Nest(0, index).Find(i => i % 2 != 0), "Odds were found in the front partition.");
            var evens = new HashSet<int>() { 2, 4, 6, 8 };
            Assert.IsTrue(evens.SetEquals(list.Nest(0, index)), "Not all evens are accounted for.");

            Assert.IsFalse(list.Nest(index).Find(i => i % 2 == 0), "Evens were found in the back partition.");
            var odds = new HashSet<int>() { 1, 3, 5, 7 };
            Assert.IsTrue(odds.SetEquals(list.Nest(index)), "Not all odds are accounted for.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If all of the items satisfying the predicate are in the front, then they should stay there.
        /// </summary>
        [TestMethod]
        public void TestPartitionInPlace_MatchingItemsInterweaved_MovedForward()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 });
            int index = list.Partition(i => i % 2 == 0).InPlace();
            Assert.AreEqual(4, index, "The wrong index was returned.");

            Assert.IsFalse(list.Nest(0, index).Find(i => i % 2 != 0), "Odds were found in the front partition.");
            var evens = new HashSet<int>() { 2, 4, 6, 8 };
            Assert.IsTrue(evens.SetEquals(list.Nest(0, index)), "Not all evens are accounted for.");

            Assert.IsFalse(list.Nest(index).Find(i => i % 2 == 0), "Evens were found in the back partition.");
            var odds = new HashSet<int>() { 1, 3, 5, 7 };
            Assert.IsTrue(odds.SetEquals(list.Nest(index)), "Not all odds are accounted for.");

            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
