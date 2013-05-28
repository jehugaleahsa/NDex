using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the FindAny methods.
    /// </summary>
    [TestClass]
    public class FindAnyTester
    {
        #region Real World Example

        /// <summary>
        /// We may want to find out if a random string contains any special characters.
        /// </summary>
        [TestMethod]
        public void TestFindAny_FindSpecialCharacters()
        {
            Random random = new Random();

            // build a random string representing a password
            var password = new List<char>();
            Sublist.Generate(100, i => (char)random.Next(32, 127)).AddTo(password.ToSublist());
            password.Insert(50, '>'); // insert an invalid character

            // have a list of characters that aren't allowed
            char[] exclusions = { '<', '>', '[', ']', '{', '}', '(', ')' };

            // see if the password contains an invalid character
            var result = password.ToSublist().FindAny(exclusions.ToSublist());
            Assert.IsTrue(result.Exists, "No special characters were found.");

            char actual = password[result.Index];
            Assert.IsTrue(exclusions.ToSublist().Find(actual), "The character found was not in the exclusions list.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindAny_NullList1_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            list1.FindAny(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindAny_WithComparer_NullList1_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list1.FindAny(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindAny_WithComparison_NullList1_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list1.FindAny(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindAny_NullList2_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            list1.FindAny(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindAny_WithComparer_NullList2_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list1.FindAny(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindAny_WithComparison_NullList2_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list1.FindAny(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindAny_NullComparer_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = null;
            list1.FindAny(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindAny_NullComparison_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, bool> comparison = null;
            list1.FindAny(list2, comparison);
        }

        #endregion

        /// <summary>
        /// We shouldn't find anything if there's nothing to find.
        /// </summary>
        [TestMethod]
        public void TestFindAny_List2Empty_ReturnFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>());
            var result = list1.FindAny(list2, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(list1.Count, result.Index, "The wrong index was returned.");
            Assert.IsFalse(result.Exists, "Nothing should have been found.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the first item is the first thing we're looking for...
        /// </summary>
        [TestMethod]
        public void TestFindAny_FirstItem_FirstItem_ReturnTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 1 });
            var result = list1.FindAny(list2, EqualityComparer<int>.Default);
            Assert.AreEqual(0, result.Index, "The wrong index was returned.");
            Assert.IsTrue(result.Exists, "A value should have been found.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the first item is the last thing we're looking for...
        /// </summary>
        [TestMethod]
        public void TestFindAny_FirstItem_LastItem_ReturnTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 1 });
            var result = list1.FindAny(list2, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(0, result.Index, "The wrong index was returned.");
            Assert.IsTrue(result.Exists, "A value should have been found.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the last item is the first thing we're looking for...
        /// </summary>
        [TestMethod]
        public void TestFindAny_LastItem_FirstItem_ReturnTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 3 });
            var result = list1.FindAny(list2, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(2, result.Index, "The wrong index was returned.");
            Assert.IsTrue(result.Exists, "A value should have been found.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the last item is the last thing we're looking for...
        /// </summary>
        [TestMethod]
        public void TestFindAny_LastItem_LastItem_ReturnTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 3 });
            var result = list1.FindAny(list2, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(2, result.Index, "The wrong index was returned.");
            Assert.IsTrue(result.Exists, "A value should have been found.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }
    }
}
