using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the IsEqualTo methods.
    /// </summary>
    [TestClass]
    public class IsEqualToTester
    {
        #region Real World Example

        /// <summary>
        /// We will determine whether a seeded random number generator generates the same numbers everytime.
        /// </summary>
        [TestMethod]
        public void TestIsEqualTo_SeededRandom()
        {
            // build the first list
            Random random1 = new Random(1);
            var list1 = new List<int>(100);
            Sublist.Generate(100, i => random1.Next()).AddTo(list1.ToSublist());

            // build the second list
            Random random2 = new Random(1);
            var list2 = new List<int>(100);
            Sublist.Generate(100, i => random2.Next()).AddTo(list2.ToSublist());

            bool result = list1.ToSublist().IsEqualTo(list2.ToSublist());
            Assert.IsTrue(result, "The random number generator did not return the same numbers.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsEqualTo_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            list1.IsEqualTo(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsEqualTo_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list1.IsEqualTo(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsEqualTo_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list1.IsEqualTo(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsEqualTo_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            list1.IsEqualTo(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsEqualTo_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list1.IsEqualTo(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsEqualTo_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list1.IsEqualTo(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsEqualTo_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = null;
            list1.IsEqualTo(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsEqualTo_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = null;
            list1.IsEqualTo(list2, comparison);
        }

        #endregion

        /// <summary>
        /// The algorithm is optimized to identify when comparing the same list to itself.
        /// </summary>
        [TestMethod]
        public void TestIsEqualTo_SameList_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            bool result = list.IsEqualTo(list, EqualityComparer<int>.Default);
            Assert.IsTrue(result, "A list was not equal to itself.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// The algorithm is optimized to identify when comparing the same list to itself.
        /// However, due to sublists, the ranges may be different.
        /// </summary>
        [TestMethod]
        public void TestIsEqualTo_SameList_DifferentCounts_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });
            var nestedList1 = list.Nest(0, 2);
            var nestedList2 = list.Nest(2, 2);
            bool result = nestedList1.IsEqualTo(nestedList2, EqualityComparer<int>.Default.Equals);
            Assert.IsFalse(result, "Did not handle different offsets into the same list.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Two lists of different size are never equal.
        /// </summary>
        [TestMethod]
        public void TestIsEqualTo_DifferentCounts_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2 });
            bool result = list1.Equals(list2);
            Assert.IsFalse(result, "Lists of different sizes were determined equal.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but the last items were equal.
        /// </summary>
        [TestMethod]
        public void TestIsEqualTo_DifferentLastItems_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 4 });
            bool result = list1.Equals(list2);
            Assert.IsFalse(result, "Lists were determined equal even though the last items were different.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but the first items were equal.
        /// </summary>
        [TestMethod]
        public void TestIsEqualTo_DifferentFirstItems_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 2, 3 });
            bool result = list1.Equals(list2);
            Assert.IsFalse(result, "Lists were determined equal even though the first items were different.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All but the middle items were equal.
        /// </summary>
        [TestMethod]
        public void TestIsEqualTo_DifferentMiddleItems_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 2, 3 });
            bool result = list1.Equals(list2);
            Assert.IsFalse(result, "Lists were determined equal even though the middle items were different.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// All the same items.
        /// </summary>
        [TestMethod]
        public void TestIsEqualTo_SameItems_SameCount_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            bool result = list1.IsEqualTo(list2);
            Assert.IsTrue(result, "Two equal lists were shown to be not equal.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }
    }
}
