using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;

namespace NDex.Test
{
    /// <summary>
    /// Tests the ContainsAny methods.
    /// </summary>
    [TestClass]
    public class ContainsAnyTester
    {
        #region Real World Example

        /// <summary>
        /// We may want to find out if a random string contains any special characters.
        /// </summary>
        [TestMethod]
        public void TestContainsAny_FindSpecialCharacters()
        {
            Random random = new Random();

            // build a random string representing a password
            var password = new List<char>();
            Sublist.Grow(password, 100, () => (char)random.Next(32, 127));
            password.Insert(50, '>'); // insert an invalid character

            // have a list of characters that aren't allowed
            char[] exclusions = { '<', '>', '[', ']', '{', '}', '(', ')' };

            // see if the password contains an invalid character
            bool result = Sublist.ContainsAny(password.ToSublist(), exclusions.ToSublist());
            Assert.IsTrue(result, "Could not find an invalid character.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsAny_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist.ContainsAny(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsAny_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.ContainsAny(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsAny_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.ContainsAny(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsAny_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist.ContainsAny(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsAny_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.ContainsAny(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsAny_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.ContainsAny(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsAny_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.ContainsAny(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsAny_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.ContainsAny(list1, list2, comparison);
        }

        #endregion

        /// <summary>
        /// We shouldn't find anything if there's nothing to find.
        /// </summary>
        [TestMethod]
        public void TestContainsAny_List2Empty_ReturnFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>());
            bool result = Sublist.ContainsAny(list1, list2, EqualityComparer<int>.Default.Equals);
            Assert.IsFalse(result, "Found a match when the second list was empty.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the first item is the first thing we're looking for...
        /// </summary>
        [TestMethod]
        public void TestContainsAny_FirstItem_FirstItem_ReturnTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 1 });
            bool result = Sublist.ContainsAny(list1, list2, EqualityComparer<int>.Default);
            Assert.IsTrue(result, "Did not find a match.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the first item is the last thing we're looking for...
        /// </summary>
        [TestMethod]
        public void TestContainsAny_FirstItem_LastItem_ReturnTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 1 });
            bool result = Sublist.ContainsAny(list1, list2, EqualityComparer<int>.Default.Equals);
            Assert.IsTrue(result, "Did not find a match.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the last item is the first thing we're looking for...
        /// </summary>
        [TestMethod]
        public void TestContainsAny_LastItem_FirstItem_ReturnTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 3 });
            bool result = Sublist.ContainsAny(list1, list2, EqualityComparer<int>.Default.Equals);
            Assert.IsTrue(result, "Did not find a match.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the last item is the last thing we're looking for...
        /// </summary>
        [TestMethod]
        public void TestContainsAny_LastItem_LastItem_ReturnTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 3 });
            bool result = Sublist.ContainsAny(list1, list2, EqualityComparer<int>.Default.Equals);
            Assert.IsTrue(result, "Did not find a match.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }
    }
}
