﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the Find methods.
    /// </summary>
    [TestClass]
    public class FindTester
    {
        #region Real World Example

        /// <summary>
        /// Find supports finding a value that is a different type than the items in the list.
        /// This can be helpful for finding user defined types based on the value of a property.
        /// We will use this ability to find the first day that falls on a Wednesday.
        /// </summary>
        [TestMethod]
        public void TestFind_FindWednesday_ByValue()
        {
            // generate a week starting from today
            DateTime[] week = new DateTime[7];
            DateTime today = DateTime.Today;
            Sublist.Generate(7, days => today.AddDays(days)).CopyTo(week.ToSublist());

            var result = Sublist.Find(week.ToSublist(), DayOfWeek.Wednesday, (date, day) => date.DayOfWeek == day);
            Assert.IsTrue(result.Exists, "A span of seven days should have included a Wednesday, but none was found.");

            DateTime actual = week[result.Index];
            Assert.AreEqual(DayOfWeek.Wednesday, actual.DayOfWeek, "The date at the returned index was not a Wednesday.");
        }

        /// <summary>
        /// If every item in the list will be compared the same way, as in finding a day in a week, we can often
        /// improve performance and make the code more readable using a predicate function.
        /// </summary>
        [TestMethod]
        public void TestFind_FindAWednesday_WithPredicate()
        {
            // generate a week starting from today
            DateTime[] week = new DateTime[7];
            DateTime today = DateTime.Today;
            Sublist.Generate(7, days => today.AddDays(days)).CopyTo(week.ToSublist());

            var result = Sublist.Find(week.ToSublist(), date => date.DayOfWeek == DayOfWeek.Wednesday);
            Assert.IsTrue(result.Exists, "A span of seven days should have included a Wednesday, but none was found.");

            DateTime actual = week[result.Index];
            Assert.AreEqual(DayOfWeek.Wednesday, actual.DayOfWeek, "The date at the returned index was not a Wednesday.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFind_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            Sublist.Find(list, value);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFind_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.Find(list, value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFind_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.Find(list, value, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFind_WithPredicate_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, bool> predicate = i => true;
            Sublist.Find(list, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFind_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int value = 0;
            IEqualityComparer<int> comparer = null;
            Sublist.Find(list, value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFind_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int value = 0;
            Func<int, int, bool> comparison = null;
            Sublist.Find(list, value, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFind_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, bool> predicate = null;
            Sublist.Find(list, predicate);
        }

        #endregion

        /// <summary>
        /// We should be able to find a value at the beginning of the list.
        /// </summary>
        [TestMethod]
        public void TestFind_InFront_ReturnsIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int value = 1;

            var result = Sublist.Find(list, value);
            Assert.IsTrue(result.Exists, "The value should have been found.");
            Assert.AreEqual(0, result.Index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find a value in the middle of the list.
        /// </summary>
        [TestMethod]
        public void TestFind_InMiddle_ReturnsIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int value = 2;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;

            var result = Sublist.Find(list, value, comparer);
            Assert.IsTrue(result.Exists, "The value should have been found.");
            Assert.AreEqual(1, result.Index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find a value in the back of the list.
        /// </summary>
        [TestMethod]
        public void TestFind_InBack_ReturnsIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int value = 3;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;

            var result = Sublist.Find(list, value, comparison);
            Assert.IsTrue(result.Exists, "The value should have been found.");
            Assert.AreEqual(2, result.Index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the value is not in the list, the index past the last item should be returned.
        /// </summary>
        [TestMethod]
        public void TestFind_Missing_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            int value = 4;

            var result = Sublist.Find(list, value);
            Assert.IsFalse(result.Exists, "The value should not have been found.");
            Assert.AreEqual(list.Count, result.Index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the value is in the list multiple times, the first index should be returned.
        /// </summary>
        [TestMethod]
        public void TestFind_MultipleOccurrences_ReturnsFirstIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 3, 2 });
            int value = 2;

            var result = Sublist.Find(list, value);
            Assert.IsTrue(result.Exists, "The value should have been found.");
            Assert.AreEqual(1, result.Index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find a value at the beginning of the list.
        /// </summary>
        [TestMethod]
        public void TestFind_WithPredicate_InFront_ReturnsIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 2, 3, 5 });
            Func<int, bool> predicate = i => i % 2 == 0;

            var result = Sublist.Find(list, predicate);
            Assert.IsTrue(result.Exists, "A value matching the predicate should have been found.");
            Assert.AreEqual(0, result.Index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find a value in the middle of the list.
        /// </summary>
        [TestMethod]
        public void TestFind_WithPredicate_InMiddle_ReturnsIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            Func<int, bool> predicate = i => i % 2 == 0;

            var result = Sublist.Find(list, predicate);
            Assert.IsTrue(result.Exists, "A value matching the predicate should have been found.");
            Assert.AreEqual(1, result.Index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find a value in the back of the list.
        /// </summary>
        [TestMethod]
        public void TestFind_WithPredicate_InBack_ReturnsIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 4 });
            Func<int, bool> predicate = i => i % 2 == 0;

            var result = Sublist.Find(list, predicate);
            Assert.IsTrue(result.Exists, "A value matching the predicate should have been found.");
            Assert.AreEqual(2, result.Index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the value is not in the list, the index past the last item should be returned.
        /// </summary>
        [TestMethod]
        public void TestFind_WithPredicate_Missing_ReturnsCount()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            Func<int, bool> predicate = i => i % 2 == 0;

            var result = Sublist.Find(list, predicate);
            Assert.IsFalse(result.Exists, "A value matching the predicate should not have been found.");
            Assert.AreEqual(list.Count, result.Index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the value is in the list multiple times, the first index should be returned.
        /// </summary>
        [TestMethod]
        public void TestFind_WithPredicate_MultipleOccurrences_ReturnsFirstIndex()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 5, 6 });
            Func<int, bool> predicate = i => i % 2 == 0;

            var result = Sublist.Find(list, predicate);
            Assert.IsTrue(result.Exists, "A value matching the predicate should have been found.");
            Assert.AreEqual(1, result.Index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
