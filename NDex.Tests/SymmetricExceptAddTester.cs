﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the SymmetricExceptAdd methods.
    /// </summary>
    [TestClass]
    public class SymmetricExceptAddTester
    {
        #region Real World Examples

        /// <summary>
        /// we can find the items that are unique across both lists.
        /// </summary>
        [TestMethod]
        public void TestSymmetricExceptAdd_FindUniqueAcrossLists()
        {
            Random random = new Random();

            // build two lists
            var list1 = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list1.ToSublist());
            var list2 = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list2.ToSublist());

            // make the lists sets
            list1.ToSublist(list1.ToSublist().MakeSet()).Clear();
            list2.ToSublist(list2.ToSublist().MakeSet()).Clear();

            // find the unique values
            var difference = new List<int>();
            list1.ToSublist().SymmetricExcept(list2.ToSublist()).AddTo(difference.ToSublist());

            // this is the opposite of the intersection, so they should share no items
            var intersection = new List<int>();
            list1.ToSublist().Intersect(list2.ToSublist()).AddTo(intersection.ToSublist());

            bool result = intersection.ToSublist().FindAny(difference.ToSublist());
            Assert.IsFalse(result, "Found items in common in the intersection and symmetric difference.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSymmetricExceptAdd_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            list1.SymmetricExcept(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSymmetricExceptAdd_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            list1.SymmetricExcept(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSymmetricExceptAdd_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.SymmetricExcept(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSymmetricExceptAdd_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            list1.SymmetricExcept(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSymmetricExceptAdd_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.SymmetricExcept(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSymmetricExceptAdd_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.SymmetricExcept(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSymmetricExceptAdd_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            list1.SymmetricExcept(list2).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSymmetricExceptAdd_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.SymmetricExcept(list2, comparer).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSymmetricExceptAdd_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.SymmetricExcept(list2, comparison).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSymmetricExceptAdd_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = null;
            list1.SymmetricExcept(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSymmetricExceptAdd_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = null;
            list1.SymmetricExcept(list2, comparison);
        }

        #endregion

        /// <summary>
        /// The symmetric difference of equal sets is nothing.
        /// </summary>
        [TestMethod]
        public void TestSymmetricExceptAdd_EqualLists_AddsNothing()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list.SymmetricExcept(list).AddTo(destination);
            int[] expected = { };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// The symmetric difference of disjoint sets is every item.
        /// </summary>
        [TestMethod]
        public void TestSymmetricExceptAdd_Disjoint_AddsAll()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 2, 4, 6 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list1.SymmetricExcept(list2).AddTo(destination);
            int[] expected = { 1, 2, 3, 4, 5, 6 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the first list is shorter, the remaining items in the second list are copied.
        /// </summary>
        [TestMethod]
        public void TestSymmetricExceptAdd_List1Shorter_RemainingCopied()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list1.SymmetricExcept(list2).AddTo(destination);
            int[] expected = { 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the second list is shorter, the remaining items in the first list are copied.
        /// </summary>
        [TestMethod]
        public void TestSymmetricExceptAdd_List2Shorter_RemainingCopied()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2 });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list1.SymmetricExcept(list2).AddTo(destination);
            int[] expected = { 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we reverse the comparer, the items must match the sort criteria.
        /// </summary>
        [TestMethod]
        public void TestSymmetricExceptAdd_WithComparer()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 2, 3 });
            var destination = TestHelper.Wrap(new List<int>());
            IComparer<int> comparer = Comparer<int>.Default;
            destination = list1.SymmetricExcept(list2, comparer).AddTo(destination);
            int[] expected = { 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we reverse the comparison, the items must match the sort criteria.
        /// </summary>
        [TestMethod]
        public void TestSymmetricExceptAdd_WithComparison()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 3, 2, 1 });
            var list2 = TestHelper.Wrap(new List<int>() { 3, 2 });
            var destination = TestHelper.Wrap(new List<int>());
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            destination = list1.SymmetricExcept(list2, comparison).AddTo(destination);
            int[] expected = { 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}