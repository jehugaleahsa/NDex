using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;

namespace NDex.Test
{
    /// <summary>
    /// Tests the AreEqual methods.
    /// </summary>
    [TestClass]
    public class AreEqualTester
    {
        #region Real World Example

        /// <summary>
        /// We will determine whether a seeded random number generator generates the same numbers everytime.
        /// </summary>
        [TestMethod]
        public void TestAreEqual_SeededRandom()
        {
            // build the first list
            Random random1 = new Random(1);
            var list1 = new List<int>(100);
            Sublist.Grow(list1, 100, () => random1.Next());

            // build the second list
            Random random2 = new Random(1);
            var list2 = new List<int>(100);
            Sublist.Grow(list2, 100, () => random2.Next());

            bool result = Sublist.AreEqual(list1.ToSublist(), list2.ToSublist());
            Assert.IsTrue(result, "The random number generator did not return the same numbers.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreEqual_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist.AreEqual(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreEqual_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.AreEqual(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreEqual_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.AreEqual(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreEqual_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist.AreEqual(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreEqual_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.AreEqual(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreEqual_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.AreEqual(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreEqual_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.AreEqual(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreEqual_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.AreEqual(list1, list2, comparison);
        }

        #endregion

        /// <summary>
        /// The algorithm is optimized to identify when comparing the same list to itself.
        /// </summary>
        [TestMethod]
        public void TestAreEqual_SameList_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            bool result = Sublist.AreEqual(list, list, EqualityComparer<int>.Default);
            Assert.IsTrue(result, "A list was not equal to itself.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// The algorithm is optimized to identify when comparing the same list to itself.
        /// However, due to sublists, the ranges may be different.
        /// </summary>
        [TestMethod]
        public void TestAreEqual_SameList_DifferentCounts_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });
            var nestedList1 = list.Nest(0, 2);
            var nestedList2 = list.Nest(2, 2);
            bool result = Sublist.AreEqual(nestedList1, nestedList2, EqualityComparer<int>.Default.Equals);
            Assert.IsFalse(result, "Did not handle different offsets into the same list.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Two lists of different size are never equal.
        /// </summary>
        [TestMethod]
        public void TestAreEqual_DifferentCounts_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2 });
            bool result = Sublist.AreEqual(list1, list2);
            Assert.IsFalse(result, "Lists of different sizes were determined equal.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but the last items were equal.
        /// </summary>
        [TestMethod]
        public void TestAreEqual_DifferentLastItems_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 4 });
            bool result = Sublist.AreEqual(list1, list2);
            Assert.IsFalse(result, "Lists were determined equal even though the last items were different.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but the first items were equal.
        /// </summary>
        [TestMethod]
        public void TestAreEqual_DifferentFirstItems_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 2, 3 });
            bool result = Sublist.AreEqual(list1, list2);
            Assert.IsFalse(result, "Lists were determined equal even though the first items were different.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but the middle items were equal.
        /// </summary>
        [TestMethod]
        public void TestAreEqual_DifferentMiddleItems_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 2, 3 });
            bool result = Sublist.AreEqual(list1, list2);
            Assert.IsFalse(result, "Lists were determined equal even though the middle items were different.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All the same items.
        /// </summary>
        [TestMethod]
        public void TestAreEqual_SameItems_SameCount_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            bool result = Sublist.AreEqual(list1, list2);
            Assert.IsTrue(result, "Two equal lists were shown to be not equal.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }
    }
}
