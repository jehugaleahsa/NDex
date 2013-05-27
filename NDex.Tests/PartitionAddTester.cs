using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the PartitionAdd methods.
    /// </summary>
    [TestClass]
    public class PartitionAddTester
    {
        #region Real World Example

        /// <summary>
        /// We can break out even and odd numbers.
        /// </summary>
        [TestMethod]
        public void TestPartitionAdd_BreakApartEvensAndOdds()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(0, 100)).AddTo(list.ToSublist());

            // partition into two
            var evens = new List<int>();
            var odds = new List<int>();
            list.ToSublist().Partition(i => i % 2 == 0).AddTo(evens.ToSublist(), odds.ToSublist());

            // sort all three lists -- we need to check if all values were added
            list.ToSublist().Sort().InPlace();
            evens.ToSublist().Sort().InPlace();
            odds.ToSublist().Sort().InPlace();

            bool hasOdd = evens.ToSublist().Find(i => i % 2 != 0);
            Assert.IsFalse(hasOdd, "Some odds were added to the wrong list.");

            bool hasEven = odds.ToSublist().Find(i => i % 2 == 0);
            Assert.IsFalse(hasEven, "Some evens were added to the wrong list.");

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
        public void TestPartitionAdd_NullList_Throws()
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
        public void TestPartitionAdd_NullDestination1_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination1 = null;
            Sublist<List<int>, int> destination2 = new List<int>();
            Func<int, bool> predicate = i => true;
            list.Partition(predicate).AddTo(destination1, destination2);
        }

        /// <summary>
        /// An exception should be thrown if the second destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartitionAdd_NullDestination2_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination1 = new List<int>();
            Sublist<List<int>, int> destination2 = null;
            Func<int, bool> predicate = i => true;
            list.Partition(predicate).AddTo(destination1, destination2);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestPartitionAdd_NullPredicate_Throws()
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
        public void TestPartitionAdd()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var evens = TestHelper.Wrap(new List<int>());
            var odds = TestHelper.Wrap(new List<int>());
            var result = list.Partition(i => i % 2 == 0).AddTo(evens, odds);
            evens = result.Destination1;
            int[] expectedEvens = { 2, 4, 6, 8 };
            Assert.IsTrue(expectedEvens.ToSublist().IsEqualTo(evens), "Not all the evens were partitioned out.");
            odds = result.Destination2;
            int[] expectedOdds = { 1, 3, 5, 7, 9 };
            Assert.IsTrue(expectedOdds.ToSublist().IsEqualTo(odds), "Not all the odds were partitioned out.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(evens);
            TestHelper.CheckHeaderAndFooter(odds);
        }
    }
}
