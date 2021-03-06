﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the IntersectCopy methods.
    /// </summary>
    [TestClass]
    public class IntersectCopyTester
    {
        #region Real Worl Example

        /// <summary>
        /// We can determine all of the numbers that are divisible by both 2 and 3.
        /// </summary>
        [TestMethod]
        public void TestIntersectCopy_FindNumbersDivisibleByTwoAndThree()
        {
            Random random = new Random();

            // build all multiples of two
            var list1 = new List<int>(50);
            Sublist.Generate(50, i => random.Next(50) * 2).AddTo(list1.ToSublist());

            // build all multiples of three
            var list2 = new List<int>(33);
            Sublist.Generate(33, i => random.Next(33) * 3).AddTo(list2.ToSublist());

            // make sets
            list1.ToSublist(list1.ToSublist().MakeSet().InPlace()).Clear();
            list2.ToSublist(list2.ToSublist().MakeSet().InPlace()).Clear();

            // find the intersection
            var destination = new List<int>(83);
            Sublist.Generate(83, 0).AddTo(destination.ToSublist());
            int index = list1.ToSublist().Intersect(list2.ToSublist()).CopyTo(destination.ToSublist());
            destination.ToSublist(index).Clear();

            // make sure all values are divisible by two and three
            bool result = destination.ToSublist().Find(i => i % 2 != 0 || i % 3 != 0);
            Assert.IsFalse(result, "Some of the items didn't meet the criteria.");

            // the result should be all multiple of six
            var expected = new List<int>(17); // space for zero
            Sublist.Generate(17, i => i * 6).AddTo(expected.ToSublist());
            bool containsAll = destination.ToSublist().IsSubset(expected.ToSublist());
            Assert.IsTrue(containsAll, "Some of the items weren't multiples of six.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersectCopy_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            list1.Intersect(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersectCopy_WithComparer_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Intersect(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersectCopy_WithComparison_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Intersect(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersectCopy_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            list1.Intersect(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersectCopy_WithComparer_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Intersect(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersectCopy_WithComparison_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Intersect(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersectCopy_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            list1.Intersect(list2).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersectCopy_WithComparer_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Intersect(list2, comparer).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersectCopy_WithComparison_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Intersect(list2, comparison).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersectCopy_NullComparer_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list1.Intersect(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersectCopy_NullComparison_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list1.Intersect(list2, comparison);
        }

        #endregion

        /// <summary>
        /// The intersection of disjoint sets is the empty set.
        /// </summary>
        [TestMethod]
        public void TestIntersectCopy_Disjoint_CopiesNothing()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 1, 3, 5 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 2, 4, 6 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            IComparer<int> comparer = Comparer<int>.Default;
            var result = list1.Intersect(list2, comparer).CopyTo(destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(2, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(0, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 0, 0, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the first list is shorter, the operation should stop prematurely.
        /// </summary>
        [TestMethod]
        public void TestIntersectCopy_List1Shorter_StopsPrematurely()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 1, 2 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });
            var result = list1.Intersect(list2).CopyTo(destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(2, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 0, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the second list is shorter, the operation should stop prematurely.
        /// </summary>
        [TestMethod]
        public void TestIntersectCopy_List2Shorter_StopsPrematurely()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 1, 2 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, });
            var result = list1.Intersect(list2).CopyTo(destination);
            Assert.AreEqual(2, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(2, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 1, 2, 0, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we merge two lists in reverse, the destination should hold all of the values.
        /// </summary>
        [TestMethod]
        public void TestIntersectCopy_DestinationTooSmall_StopsPrematurely()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, 4 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, });
            var result = list1.Intersect(list2).CopyTo(destination);
            Assert.AreEqual(3, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(3, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result, "The wrong number of items were added.");
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items weren't merged as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we reverse the comparison, the items must match the sort criteria.
        /// </summary>
        [TestMethod]
        public void TestIntersectCopy_ReversedOrder()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 3, 2, 1 });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 3, 2 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0});
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            var result = list1.Intersect(list2, comparison).CopyTo(destination);
            Assert.AreEqual(2, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(2, result, "The wrong number of items were added.");
            int[] expected = { 3, 2, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
