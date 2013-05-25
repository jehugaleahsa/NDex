using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the PartitionCopy methods.
    /// </summary>
    [TestClass]
    public class PartitionCopyTester
    {
        #region Real World Example

        /// <summary>
        /// We can break out even and odd numbers.
        /// </summary>
        [TestMethod]
        public void TestPartitionCopy_BreakApartEvensAndOdds()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(0, 100)).AddTo(list.ToSublist());

            // partition into two
            var evens = new List<int>(100);
            Sublist.Generate(100, 0).AddTo(evens.ToSublist());
            var odds = new List<int>(100);
            Sublist.Generate(100, 0).AddTo(odds.ToSublist());
            var result = list.ToSublist().Partition(i => i % 2 == 0).CopyTo(evens.ToSublist(), odds.ToSublist());
            evens.ToSublist(result.DestinationOffset1).Clear();
            odds.ToSublist(result.DestinationOffset2).Clear();

            // sort all three lists -- we need to check if all values were added
            list.ToSublist().Sort().InPlace();
            evens.ToSublist().Sort().InPlace();
            odds.ToSublist().Sort().InPlace();

            bool allEven = evens.ToSublist().TrueForAll(i => i % 2 == 0);
            Assert.IsTrue(allEven, "Some odds were added to the wrong list.");

            bool allOdd = odds.ToSublist().TrueForAll(i => i % 2 != 0);
            Assert.IsTrue(allOdd, "Some evens were added to the wrong list.");

            var combined = new List<int>(100);
            evens.ToSublist().AddTo(combined.ToSublist());
            odds.ToSublist().AddTo(combined.ToSublist());
            combined.ToSublist().Sort().InPlace();
            bool hasAllItems = list.ToSublist().IsEqualTo(combined.ToSublist());
            Assert.IsTrue(hasAllItems, "Not all items were partitioned.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartitionCopy_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            list.Partition(predicate);
        }

        /// <summary>
        /// An exception should be thrown if the first destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartitionCopy_NullDestination1_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination1 = null;
            Sublist<List<int>, int> destination2 = new List<int>();
            Func<int, bool> predicate = i => true;
            list.Partition(predicate).CopyTo(destination1, destination2);
        }

        /// <summary>
        /// An exception should be thrown if the second destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartitionCopy_NullDestination2_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination1 = new List<int>();
            Sublist<List<int>, int> destination2 = null;
            Func<int, bool> predicate = i => true;
            list.Partition(predicate).CopyTo(destination1, destination2);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartitionCopy_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null;
            list.Partition(predicate);
        }

        #endregion

        /// <summary>
        /// Partition should partition according to the predicate.
        /// </summary>
        [TestMethod]
        public void TestPartitionCopy()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var evens = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            var odds = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = list.Partition(i => i % 2 == 0).CopyTo(evens, odds);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(evens.Count, result.DestinationOffset1, "The first destination offset was wrong.");
            Assert.AreEqual(odds.Count, result.DestinationOffset2, "The second destination offset was wrong.");

            int[] expectedEvens = { 2, 4, 6, 8 };
            Assert.IsTrue(expectedEvens.ToSublist().IsEqualTo(evens), "Not all the evens were partitioned out.");
            int[] expectedOdds = { 1, 3, 5, 7, 9 };
            Assert.IsTrue(expectedOdds.ToSublist().IsEqualTo(odds), "Not all the odds were partitioned out.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(evens);
            TestHelper.CheckHeaderAndFooter(odds);
        }

        /// <summary>
        /// The algorithm is engineered to stop as soon as one of the destinations is full.
        /// </summary>
        [TestMethod]
        public void TestPartitionCopy_FirstDestinationFull_StopsPrematurely()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 6, 8, });
            var odds = TestHelper.Wrap(new List<int>() { 0 }); // can't hold 2
            var evens = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });

            var result = list.Partition(i => i % 2 != 0).CopyTo(odds, evens);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(1, result.DestinationOffset1, "The first index was wrong.");
            Assert.AreEqual(1, result.DestinationOffset2, "The second index was wrong.");

            int[] expectedOdds = { 1 };
            Assert.IsTrue(expectedOdds.ToSublist().IsEqualTo(odds), "Not all the odds were partitioned out.");
            int[] expectedEvens = { 2, 0, 0, 0 };
            Assert.IsTrue(expectedEvens.ToSublist().IsEqualTo(evens), "Not all the evens were partitioned out.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(evens);
            TestHelper.CheckHeaderAndFooter(odds);
        }

        /// <summary>
        /// The algorithm is engineered to copy as many items as possible into either destination.
        /// </summary>
        [TestMethod]
        public void TestPartitionCopy_SecondDestinationFull_ContinueToFillFirst()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 6, 8, });
            var evens = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0 });
            var odds = TestHelper.Wrap(new List<int>() { 0 }); // can't hold 3

            var result = list.Partition(i => i % 2 == 0).CopyTo(evens, odds);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(1, result.DestinationOffset1, "The first index was wrong.");
            Assert.AreEqual(1, result.DestinationOffset2, "The second index was wrong.");

            int[] expectedEvens = { 2, 0, 0, 0 };
            Assert.IsTrue(expectedEvens.ToSublist().IsEqualTo(evens), "Not all the evens were partitioned out.");
            int[] expectedOdds = { 1 };
            Assert.IsTrue(expectedOdds.ToSublist().IsEqualTo(odds), "Not all the odds were partitioned out.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(evens);
            TestHelper.CheckHeaderAndFooter(odds);
        }
    }
}
