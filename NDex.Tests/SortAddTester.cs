using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the SortAdd methods.
    /// </summary>
    [TestClass]
    public class SortAddTester
    {
        #region Real World Example

        /// <summary>
        /// Sort is useful when a fast sort is needed.
        /// </summary>
        [TestMethod]
        public void TestSortAdd_SortRandomList()
        {
            Random random = new Random();

            // build a list
            int size = random.Next(1000, 10000); // between 1,000 and 10,000 items
            var list = new List<int>(size);
            Sublist.Generate(size, i => random.Next(size)).AddTo(list.ToSublist());

            // sort the list
            var destination = new List<int>(size);
            list.ToSublist().Sort().AddTo(destination.ToSublist());

            bool isSorted = destination.ToSublist().IsSorted();
            Assert.IsTrue(isSorted, "The items were not sorted.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortAdd_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            list.Sort();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortAdd_WithComparer_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.Sort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortAdd_WithComparison_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.Sort(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortAdd_NullComparer_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list.Sort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortAdd_NullComparison_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list.Sort(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortAdd_DestinationNull_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            list.Sort().AddTo(destination);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestSortAdd_EmptyList()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());
            destination = list.Sort().AddTo(destination);
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Sort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestSortAdd_Reversed()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(200, i => 199 - i).AddTo(list.List.ToSublist());

            destination = list.Sort(Comparer<int>.Default).AddTo(destination);

            bool result = destination.IsSorted(Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Sort should work against a list whose values ascend and then descend.
        /// </summary>
        [TestMethod]
        public void TestSortAdd_PipeOrganed()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(100, i => i * 2).AddTo(list.List.ToSublist());
            list = Sublist.Generate(200, i => 199 - (i - 100) * 2).AddTo(list.List.ToSublist());

            destination = list.Sort(Comparer<int>.Default.Compare).AddTo(destination);

            bool result = destination.IsSorted(Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Sort should work against a list whose values jump between small and large.
        /// </summary>
        [TestMethod]
        public void TestSortAdd_Interweaved()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(200, i => i % 2 == 0 ? i : 199 - (i - 1)).AddTo(list.List.ToSublist());

            destination = list.Sort().AddTo(destination);

            bool result = destination.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Sort should work against a list whose values are sorted except the last value.
        /// </summary>
        [TestMethod]
        public void TestSortAdd_LastMisplaced()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(200, i => i + 1).AddTo(list.List.ToSublist());
            list = new int[] { 0 }.AddTo(list.List.ToSublist());

            destination = list.Sort().AddTo(destination);

            bool result = destination.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Sort should work against a list whose values are sorted except the first value.
        /// </summary>
        [TestMethod]
        public void TestSortAdd_FirstMisplaced()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = new int[] { 200 }.AddTo(list.List.ToSublist());
            list = Sublist.Generate(201, i => i - 1).AddTo(list.List.ToSublist());

            destination = list.Sort().AddTo(destination);

            bool result = destination.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
