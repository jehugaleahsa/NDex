using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the CopyPartitioned methods.
    /// </summary>
    [TestClass]
    public class CopyPartitionedTester
    {
        #region Real World Example

        /// <summary>
        /// We can break out even and odd numbers.
        /// </summary>
        [TestMethod]
        public void TestCopyPartitioned_BreakApartEvensAndOdds()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next(0, 100)), list.ToSublist());

            // partition into two
            var evens = new List<int>(100);
            Sublist.Add(Enumerable.Repeat(0, 100), evens.ToSublist());
            var odds = new List<int>(100);
            Sublist.Add(Enumerable.Repeat(0, 100), odds.ToSublist());
            CopyPartitionedResult result = Sublist.CopyPartitioned(list.ToSublist(), evens.ToSublist(), odds.ToSublist(), i => i % 2 == 0);
            Sublist.RemoveRange(evens.ToSublist(result.DestinationOffset1));
            Sublist.RemoveRange(odds.ToSublist(result.DestinationOffset2));

            // sort all three lists -- we need to check if all values were added
            Sublist.QuickSort(list.ToSublist());
            Sublist.QuickSort(evens.ToSublist());
            Sublist.QuickSort(odds.ToSublist());

            bool allEven = Sublist.TrueForAll(evens.ToSublist(), i => i % 2 == 0);
            Assert.IsTrue(allEven, "Some odds were added to the wrong list.");

            bool allOdd = Sublist.TrueForAll(odds.ToSublist(), i => i % 2 != 0);
            Assert.IsTrue(allOdd, "Some evens were added to the wrong list.");

            var combined = new List<int>(100);
            Sublist.Add(evens.ToSublist(), combined.ToSublist());
            Sublist.Add(odds.ToSublist(), combined.ToSublist());
            Sublist.QuickSort(combined.ToSublist());
            bool hasAllItems = Sublist.AreEqual(list.ToSublist(), combined.ToSublist());
            Assert.IsTrue(hasAllItems, "Not all items were partitioned.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyPartitioned_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination1 = new List<int>();
            Sublist<List<int>, int> destination2 = new List<int>();
            Func<int, bool> predicate = i => true;
            Sublist.CopyPartitioned(list, destination1, destination2, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the first destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyPartitioned_NullDestination1_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination1 = null;
            Sublist<List<int>, int> destination2 = new List<int>();
            Func<int, bool> predicate = i => true;
            Sublist.CopyPartitioned(list, destination1, destination2, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the second destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyPartitioned_NullDestination2_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination1 = new List<int>();
            Sublist<List<int>, int> destination2 = null;
            Func<int, bool> predicate = i => true;
            Sublist.CopyPartitioned(list, destination1, destination2, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyPartitioned_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination1 = new List<int>();
            Sublist<List<int>, int> destination2 = new List<int>();
            Func<int, bool> predicate = null;
            Sublist.CopyPartitioned(list, destination1, destination2, predicate);
        }

        #endregion

        /// <summary>
        /// Partition should partition according to the predicate.
        /// </summary>
        [TestMethod]
        public void TestCopyPartitioned()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var evens = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            var odds = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            CopyPartitionedResult result = Sublist.CopyPartitioned(list, evens, odds, i => i % 2 == 0);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(evens.Count, result.DestinationOffset1, "The first destination offset was wrong.");
            Assert.AreEqual(odds.Count, result.DestinationOffset2, "The second destination offset was wrong.");

            int[] expectedEvens = { 2, 4, 6, 8 };
            Assert.IsTrue(Sublist.AreEqual(expectedEvens.ToSublist(), evens), "Not all the evens were partitioned out.");
            int[] expectedOdds = { 1, 3, 5, 7, 9 };
            Assert.IsTrue(Sublist.AreEqual(expectedOdds.ToSublist(), odds), "Not all the odds were partitioned out.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(evens);
            TestHelper.CheckHeaderAndFooter(odds);
        }

        /// <summary>
        /// The algorithm is engineered to stop as soon as one of the destinations is full.
        /// </summary>
        [TestMethod]
        public void TestCopyPartitioned_FirstDestinationFull_StopsPrematurely()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 6, 8, });
            var odds = TestHelper.Wrap(new List<int>() { 0 }); // can't hold 2
            var evens = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });

            CopyPartitionedResult result = Sublist.CopyPartitioned(list, odds, evens, i => i % 2 != 0);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(1, result.DestinationOffset1, "The first index was wrong.");
            Assert.AreEqual(1, result.DestinationOffset2, "The second index was wrong.");

            int[] expectedOdds = { 1 };
            Assert.IsTrue(Sublist.AreEqual(expectedOdds.ToSublist(), odds), "Not all the odds were partitioned out.");
            int[] expectedEvens = { 2, 0, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expectedEvens.ToSublist(), evens), "Not all the evens were partitioned out.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(evens);
            TestHelper.CheckHeaderAndFooter(odds);
        }

        /// <summary>
        /// The algorithm is engineered to copy as many items as possible into either destination.
        /// </summary>
        [TestMethod]
        public void TestCopyPartitioned_SecondDestinationFull_ContinueToFillFirst()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 6, 8, });
            var evens = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            var odds = TestHelper.Wrap(new List<int>() { 0 }); // can't hold 3

            CopyPartitionedResult result = Sublist.CopyPartitioned(list, evens, odds, i => i % 2 == 0);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(1, result.DestinationOffset1, "The first index was wrong.");
            Assert.AreEqual(1, result.DestinationOffset2, "The second index was wrong.");

            int[] expectedEvens = { 2, 0, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expectedEvens.ToSublist(), evens), "Not all the evens were partitioned out.");
            int[] expectedOdds = { 1 };
            Assert.IsTrue(Sublist.AreEqual(expectedOdds.ToSublist(), odds), "Not all the odds were partitioned out.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(evens);
            TestHelper.CheckHeaderAndFooter(odds);
        }
    }
}
