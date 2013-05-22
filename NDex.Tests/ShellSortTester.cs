﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ShellSort methods.
    /// </summary>
    [TestClass]
    public class ShellSortTester
    {
        #region Real World Example

        /// <summary>
        /// Use ShellSort as a slower alternative to QuickSort.
        /// </summary>
        [TestMethod]
        public void TestShellSort_SortRandomList()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list.ToSublist());

            // sort the list
            list.ToSublist().ShellSort();

            bool isSorted = list.ToSublist().IsSorted();
            Assert.IsTrue(isSorted, "The items were not sorted.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestShellSort_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            list.ShellSort();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestShellSort_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.ShellSort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestShellSort_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.ShellSort(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestShellSort_NullComparer_Throws()
        {
            var list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list.ShellSort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestShellSort_NullComparison_Throws()
        {
            var list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list.ShellSort(comparison);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestShellSort_EmptyList()
        {
            var list = TestHelper.Wrap(new List<int>());
            list.ShellSort();
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// ShellSort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestShellSort_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(200, i => 199 - i).AddTo(list);
            list.ShellSort(Comparer<int>.Default);
            bool result = list.IsSorted(Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// ShellSort should work against a list whose values ascend and then descend.
        /// </summary>
        [TestMethod]
        public void TestShellSort_PipeOrganed()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(100, i => i * 2).AddTo(list);
            list = Sublist.Generate(200, i => 199 - (i - 100) * 2).AddTo(list);
            list.ShellSort(Comparer<int>.Default.Compare);
            bool result = list.IsSorted(Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// ShellSort should work against a list whose values jump between small and large.
        /// </summary>
        [TestMethod]
        public void TestShellSort_Interweaved()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(200, i => i % 2 == 0 ? i : 199 - (i - 1)).AddTo(list);
            list.ShellSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// ShellSort should work against a list whose values are sorted except the last value.
        /// </summary>
        [TestMethod]
        public void TestShellSort_LastMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(200, i => i + 1).AddTo(list);
            list = new int[] { 0 }.AddTo(list);
            list.ShellSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// ShellSort should work against a list whose values are sorted except the first value.
        /// </summary>
        [TestMethod]
        public void TestShellSort_FirstMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = new int[] { 200 }.AddTo(list);
            list = Sublist.Generate(201, i => i - 1).AddTo(list);
            list.ShellSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
