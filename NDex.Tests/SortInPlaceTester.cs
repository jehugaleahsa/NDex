using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the SortInPlace methods.
    /// </summary>
    [TestClass]
    public class SortInPlaceTester
    {
        #region Real World Example

        /// <summary>
        /// Sort is useful when a fast sort is needed.
        /// </summary>
        [TestMethod]
        public void TestSortInPlace_SortRandomList()
        {
            Random random = new Random();

            // build a list
            int size = random.Next(1000, 10000); // between 1,000 and 10,000 items
            var list = new List<int>(size);
            Sublist.Generate(size, i => random.Next(size)).AddTo(list.ToSublist());

            // sort the list
            list.ToSublist().Sort().InPlace();

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
        public void TestSortInPlace_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            list.Sort().InPlace();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortInPlace_WithComparer_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.Sort(comparer).InPlace();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortInPlace_WithComparison_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.Sort(comparison).InPlace();
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortInPlace_NullComparer_Throws()
        {
            var list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list.Sort(comparer).InPlace();
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortInPlace_NullComparison_Throws()
        {
            var list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list.Sort(comparison).InPlace();
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestSortInPlace_EmptyList()
        {
            var list = TestHelper.Wrap(new List<int>());
            list.Sort().InPlace();
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Sort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestSortInPlace_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(200, i => 199 - i).AddTo(list);
            list.Sort(Comparer<int>.Default).InPlace();
            bool result = list.IsSorted(Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Sort should work against a list whose values ascend and then descend.
        /// </summary>
        [TestMethod]
        public void TestSortInPlace_PipeOrganed()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(100, i => i * 2).AddTo(list);
            list = Sublist.Generate(200, i => 199 - (i - 100) * 2).AddTo(list);
            list.Sort(Comparer<int>.Default.Compare).InPlace();
            bool result = list.IsSorted(Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Sort should work against a list whose values jump between small and large.
        /// </summary>
        [TestMethod]
        public void TestSortInPlace_Interweaved()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(200, i => i % 2 == 0 ? i : 199 - (i - 1)).AddTo(list);
            list.Sort().InPlace();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Sort should work against a list whose values are sorted except the last value.
        /// </summary>
        [TestMethod]
        public void TestSortInPlace_LastMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(200, i => i + 1).AddTo(list);
            list = new int[] { 0 }.AddTo(list);
            list.Sort().InPlace();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// Sort should work against a list whose values are sorted except the first value.
        /// </summary>
        [TestMethod]
        public void TestSortInPlace_FirstMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = new int[] { 200 }.AddTo(list);
            list = Sublist.Generate(201, i => i - 1).AddTo(list);
            list.Sort().InPlace();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
