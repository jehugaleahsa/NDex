using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;
using System.Linq;

namespace NDex.Test
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
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next(100)), list.ToSublist());

            // sort the list
            Sublist.ShellSort(list.ToSublist());

            bool isSorted = Sublist.IsSorted(list.ToSublist());
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
            Sublist.ShellSort(list);
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
            Sublist.ShellSort(list, comparer);
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
            Sublist.ShellSort(list, comparison);
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
            Sublist.ShellSort(list, comparer);
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
            Sublist.ShellSort(list, comparison);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestShellSort_EmptyList()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.ShellSort(list);
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// ShellSort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestShellSort_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Add(Enumerable.Range(0, 200).Select(i => 199 - i), list);
            Sublist.ShellSort(list, Comparer<int>.Default);
            bool result = Sublist.IsSorted(list, Comparer<int>.Default);
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
            list = Sublist.Add(Enumerable.Range(0, 100).Select(i => i * 2), list);
            list = Sublist.Add(Enumerable.Range(0, 200).Select(i => 199 - (i - 100) * 2), list);
            Sublist.ShellSort(list, Comparer<int>.Default.Compare);
            bool result = Sublist.IsSorted(list, Comparer<int>.Default.Compare);
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
            list = Sublist.Add(Enumerable.Range(0, 200).Select(i => i % 2 == 0 ? i : 199 - (i - 1)), list);
            Sublist.ShellSort(list);
            bool result = Sublist.IsSorted(list);
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
            list = Sublist.Add(Enumerable.Range(0, 200).Select(i => i + 1), list);
            list = Sublist.Add(new int[] { 0 }, list);
            Sublist.ShellSort(list);
            bool result = Sublist.IsSorted(list);
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
            list = Sublist.Add(new int[] { 200 }, list);
            list = Sublist.Add(Enumerable.Range(0, 201).Select(i => i - 1), list);
            Sublist.ShellSort(list);
            bool result = Sublist.IsSorted(list);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
