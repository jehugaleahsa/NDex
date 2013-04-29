using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the IndexOfAny methods.
    /// </summary>
    [TestClass]
    public class IndexOfAnyTester
    {
        #region Real World Example

        /// <summary>
        /// We may want to find out if a random string contains any special characters.
        /// </summary>
        [TestMethod]
        public void TestIndexOfAny_FindSpecialCharacters()
        {
            Random random = new Random();

            // build a random string representing a password
            var password = new List<char>();
            Sublist.Add(Enumerable.Range(0, 100).Select(i => (char)random.Next(32, 127)), password.ToSublist());
            password.Insert(50, '>'); // insert an invalid character

            // have a list of characters that aren't allowed
            char[] exclusions = { '<', '>', '[', ']', '{', '}', '(', ')' };

            // see if the password contains an invalid character
            int index = Sublist.IndexOfAny(password.ToSublist(), exclusions.ToSublist());
            Assert.AreNotEqual(password.Count, index, "No special characters were found.");

            char actual = password[index];
            Assert.IsTrue(Sublist.Contains(exclusions.ToSublist(), actual), "The character found was not in the exclusions list.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfAny_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist.IndexOfAny(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfAny_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.IndexOfAny(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfAny_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.IndexOfAny(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfAny_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist.IndexOfAny(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfAny_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.IndexOfAny(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfAny_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.IndexOfAny(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfAny_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.IndexOfAny(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfAny_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.IndexOfAny(list1, list2, comparison);
        }

        #endregion

        /// <summary>
        /// We shouldn't find anything if there's nothing to find.
        /// </summary>
        [TestMethod]
        public void TestIndexOfAny_List2Empty_ReturnFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>());
            int index = Sublist.IndexOfAny(list1, list2, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(list1.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the first item is the first thing we're looking for...
        /// </summary>
        [TestMethod]
        public void TestIndexOfAny_FirstItem_FirstItem_ReturnTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 1 });
            int index = Sublist.IndexOfAny(list1, list2, EqualityComparer<int>.Default);
            Assert.AreEqual(0, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the first item is the last thing we're looking for...
        /// </summary>
        [TestMethod]
        public void TestIndexOfAny_FirstItem_LastItem_ReturnTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 1 });
            int index = Sublist.IndexOfAny(list1, list2, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(0, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the last item is the first thing we're looking for...
        /// </summary>
        [TestMethod]
        public void TestIndexOfAny_LastItem_FirstItem_ReturnTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 3 });
            int index = Sublist.IndexOfAny(list1, list2, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(2, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the last item is the last thing we're looking for...
        /// </summary>
        [TestMethod]
        public void TestIndexOfAny_LastItem_LastItem_ReturnTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 3 });
            int index = Sublist.IndexOfAny(list1, list2, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(2, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }
    }
}
