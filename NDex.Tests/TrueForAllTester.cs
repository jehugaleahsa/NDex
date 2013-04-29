using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the TrueForAll methods.
    /// </summary>
    [TestClass]
    public class TrueForAllTester
    {
        #region Real World Examples

        /// <summary>
        /// TrueForAll can be used to see if every item satisfies a condition.
        /// </summary>
        [TestMethod]
        public void TestTrueForAll_AllItemsAreEvens()
        {
            Random random = new Random();

            // create a list of random numbers
            var list = new List<int>(5);
            Sublist.Add(Enumerable.Range(0, 5).Select(i => random.Next(5)), list.ToSublist());

            // see if they are all even
            bool allEven = Sublist.TrueForAll(list.ToSublist(), i => i % 2 == 0);
            bool anyOdd = Sublist.Contains(list.ToSublist(), i => i % 2 != 0);
            Assert.AreNotEqual(anyOdd, allEven, "Detected odds when saying all items were even.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTrueForAll_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            Sublist.TrueForAll(list, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTrueForAll_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null;
            Sublist.TrueForAll(list, predicate);
        }

        #endregion

        /// <summary>
        /// True should be returned if all items satisfy the predicate.
        /// </summary>
        [TestMethod]
        public void TestTrueForAll_AllMatches_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 6, 8 });
            Func<int, bool> predicate = i => i % 2 == 0;
            bool result = Sublist.TrueForAll(list, predicate);
            Assert.IsTrue(result, "All of the items should have satisfied the predicate.");
        }

        /// <summary>
        /// False should be returned if the first item doesn't satisfy the predicate.
        /// </summary>
        [TestMethod]
        public void TestTrueForAll_FirstDoesntMatch_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 4, 6 });
            Func<int, bool> predicate = i => i % 2 == 0;
            bool result = Sublist.TrueForAll(list, predicate);
            Assert.IsFalse(result, "Some items should not have satisfied the predicate.");
        }

        /// <summary>
        /// False should be returned if the last item doesn't satisfy the predicate.
        /// </summary>
        [TestMethod]
        public void TestTrueForAll_LastDoesntMatch_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 6, 7 });
            Func<int, bool> predicate = i => i % 2 == 0;
            bool result = Sublist.TrueForAll(list, predicate);
            Assert.IsFalse(result, "Some items should not have satisfied the predicate.");
        }

        /// <summary>
        /// False should be returned if an item in the middle doesn't satisfy the predicate.
        /// </summary>
        [TestMethod]
        public void TestTrueForAll_MiddleDoesntMatch_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 4, 5, 6, });
            Func<int, bool> predicate = i => i % 2 == 0;
            bool result = Sublist.TrueForAll(list, predicate);
            Assert.IsFalse(result, "Some items should not have satisfied the predicate.");
        }
    }
}
