using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;

namespace NDex.Test
{
    /// <summary>
    /// Tests the Contains methods.
    /// </summary>
    [TestClass]
    public class ContainsTester
    {
        #region Real World Example

        /// <summary>
        /// We want to figure out if any of the dates fall on a given day.
        /// </summary>
        [TestMethod]
        public void TestContains_FindDateOnCertainDayOfWeek()
        {
            // build a list of dates.
            var dates = new List<DateTime>();
            Sublist.Grow(dates, 7, day => new DateTime(2010, 12, day + 1));

            // see if any are on a Wednesday
            bool result = Sublist.Contains(dates.ToSublist(), DayOfWeek.Wednesday, (date, day) => date.DayOfWeek == day);
            Assert.IsTrue(result, "Did not find a Wednesday.");
        }

        /// <summary>
        /// Exists is used to determine whether a value or condition exists within a list.
        /// We will use it to determine whether any numbers are divisible by 11.
        /// </summary>
        [TestMethod]
        public void TestExists_DivisibleByEleven()
        {
            Random random = new Random();

            // create a list of random numbers
            var list = new List<int>(100);
            Sublist.Grow(list, 100, () => random.Next(0, 100));
            list.Insert(50, 44); // shove a number divisible by 11 in the middle

            bool result = Sublist.Contains(list.ToSublist(), i => i % 11 == 0);
            Assert.IsTrue(result, "Could not find any numbers divisible by 11.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            Sublist.Contains(list, value);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.Contains(list, value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.Contains(list, value, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains_WithPredicate_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            Sublist.Contains(list, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int value = 0;
            IEqualityComparer<int> comparer = null;
            Sublist.Contains(list, value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int value = 0;
            Func<int, int, bool> comparison = null;
            Sublist.Contains(list, value, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContains_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null;
            Sublist.Contains(list, predicate);
        }

        #endregion

        /// <summary>
        /// We can't find a value in an empty list.
        /// </summary>
        [TestMethod]
        public void TestContains_EmptyList_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>());
            int value = 0;
            bool result = Sublist.Contains(list, value);
            Assert.IsFalse(result, "Found an item in an empty list.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// False should be returned if no items satisfy the predicate.
        /// </summary>
        [TestMethod]
        public void TestContains_WithPredicate_NoMatches_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 7 });
            Func<int, bool> predicate = i => i % 2 == 0;
            bool result = Sublist.Contains(list, predicate);
            Assert.IsFalse(result, "Found a match when there should have been none.");
        }

        /// <summary>
        /// We should be able to find an item if it is the first item.
        /// </summary>
        [TestMethod]
        public void TestContains_AtBeginning()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            int value = 1;
            bool result = Sublist.Contains(list, value, EqualityComparer<int>.Default);
            Assert.IsTrue(result, "Could not find the item.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// True should be returned if the first item satisfies the predicate.
        /// </summary>
        [TestMethod]
        public void TestContains_FirstMatches_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 3, 5, 7 });
            Func<int, bool> predicate = i => i % 2 == 0;
            bool result = Sublist.Contains(list, predicate);
            Assert.IsTrue(result, "Did not find a match.");
        }

        /// <summary>
        /// We should be able to find an item if it is the last item.
        /// </summary>
        [TestMethod]
        public void TestContains_AtEnd()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            int value = 3;
            bool result = Sublist.Contains(list, value);
            Assert.IsTrue(result, "Could not find the item.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// True should be returned if the last item satisfies the predicate.
        /// </summary>
        [TestMethod]
        public void TestContains_LastMatches_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5, 8 });
            Func<int, bool> predicate = i => i % 2 == 0;
            bool result = Sublist.Contains(list, predicate);
            Assert.IsTrue(result, "Did not find a match.");
        }

        /// <summary>
        /// We should be able to find an item if it is in the middle.
        /// </summary>
        [TestMethod]
        public void TestContains_InMiddle()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            int value = 2;
            bool result = Sublist.Contains(list, value);
            Assert.IsTrue(result, "Could not find the item.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// True should be returned if a middle item satisfies the predicate.
        /// </summary>
        [TestMethod]
        public void TestContains_MiddleMatches_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 4, 5 });
            Func<int, bool> predicate = i => i % 2 == 0;
            bool result = Sublist.Contains(list, predicate);
            Assert.IsTrue(result, "Did not find a match.");
        }
    }
}
