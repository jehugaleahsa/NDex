using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the IndexOf methods.
    /// </summary>
    [TestClass]
    public class IndexOfTester
    {
        #region Real World Example

        /// <summary>
        /// IndexOf supports finding a value that is a different type than the items in the list.
        /// This can be helpful for finding user defined types based on the value of a property.
        /// We will use this ability to find the first day that falls on a Wednesday.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_FindAWednesday_ByValue()
        {
            // generate a week starting from today
            DateTime[] week = new DateTime[7];
            DateTime today = DateTime.Today;
            Sublist.CopyGenerated(week.ToSublist(), days => today.AddDays(days));

            int index = Sublist.IndexOf(week.ToSublist(), DayOfWeek.Wednesday, (date, day) => date.DayOfWeek == day);
            Assert.AreNotEqual(week.Length, index, "A span of seven days should have included a Wednesday, but none was found.");

            DateTime actual = week[index];
            Assert.AreEqual(DayOfWeek.Wednesday, actual.DayOfWeek, "The date at the returned index was not a Wednesday.");
        }

        /// <summary>
        /// If every item in the list will be compared the same way, as in finding a day in a week, we can often
        /// improve performance and make the code more readable using a predicate function.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_FindAWednesday_WithPredicate()
        {
            // generate a week starting from today
            DateTime[] week = new DateTime[7];
            DateTime today = DateTime.Today;
            Sublist.CopyGenerated(week.ToSublist(), days => today.AddDays(days));

            int index = Sublist.IndexOf(week.ToSublist(), date => date.DayOfWeek == DayOfWeek.Wednesday);
            Assert.AreNotEqual(week.Length, index, "A span of seven days should have included a Wednesday, but none was found.");

            DateTime actual = week[index];
            Assert.AreEqual(DayOfWeek.Wednesday, actual.DayOfWeek, "The date at the returned index was not a Wednesday.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOf_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            Sublist.IndexOf(list, value);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOf_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.IndexOf(list, value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOf_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.IndexOf(list, value, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOf_WithPredicate_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            Sublist.IndexOf(list, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOf_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int value = 0;
            IEqualityComparer<int> comparer = null;
            Sublist.IndexOf(list, value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOf_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int value = 0;
            Func<int, int, bool> comparison = null;
            Sublist.IndexOf(list, value, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOf_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null;
            Sublist.IndexOf(list, predicate);
        }

        #endregion

        /// <summary>
        /// We should be able to find a value at the beginning of the list.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_InFront_ReturnsIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int value = 1;

            int index = Sublist.IndexOf(list, value);
            Assert.AreEqual(0, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find a value in the middle of the list.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_InMiddle_ReturnsIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int value = 2;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;

            int index = Sublist.IndexOf(list, value, comparer);
            Assert.AreEqual(1, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find a value in the back of the list.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_InBack_ReturnsIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int value = 3;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;

            int index = Sublist.IndexOf(list, value, comparison);
            Assert.AreEqual(2, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the value is not in the list, the index past the last item should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_Missing_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int value = 4;

            int index = Sublist.IndexOf(list, value);
            Assert.AreEqual(list.Count, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the value is in the list multiple times, the first index should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_MultipleOccurrences_ReturnsFirstIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 3, 2 });
            int value = 2;

            int index = Sublist.IndexOf(list, value);
            Assert.AreEqual(1, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find a value at the beginning of the list.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_WithPredicate_InFront_ReturnsIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 3, 5 });
            Func<int, bool> predicate = i => i % 2 == 0;

            int index = Sublist.IndexOf(list, predicate);
            Assert.AreEqual(0, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find a value in the middle of the list.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_WithPredicate_InMiddle_ReturnsIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            Func<int, bool> predicate = i => i % 2 == 0;

            int index = Sublist.IndexOf(list, predicate);
            Assert.AreEqual(1, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find a value in the back of the list.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_WithPredicate_InBack_ReturnsIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 4 });
            Func<int, bool> predicate = i => i % 2 == 0;

            int index = Sublist.IndexOf(list, predicate);
            Assert.AreEqual(2, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the value is not in the list, the index past the last item should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_WithPredicate_Missing_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            Func<int, bool> predicate = i => i % 2 == 0;

            int index = Sublist.IndexOf(list, predicate);
            Assert.AreEqual(list.Count, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the value is in the list multiple times, the first index should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_WithPredicate_MultipleOccurrences_ReturnsFirstIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 5, 6 });
            Func<int, bool> predicate = i => i % 2 == 0;

            int index = Sublist.IndexOf(list, predicate);
            Assert.AreEqual(1, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
