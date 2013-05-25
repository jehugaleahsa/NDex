using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the SortCopy methods.
    /// </summary>
    [TestClass]
    public class SortCopyTester
    {
        #region Real World Example

        /// <summary>
        /// Sort is useful when a fast sort is needed.
        /// </summary>
        [TestMethod]
        public void TestSortCopy_SortRandomList()
        {
            Random random = new Random();

            // build a list
            int size = random.Next(1000, 10000); // between 1,000 and 10,000 items
            var list = new List<int>(size);
            Sublist.Generate(size, i => random.Next(size)).AddTo(list.ToSublist());

            // sort the list
            var destination = new int[size];
            list.ToSublist().Sort().CopyTo(destination.ToSublist());

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
        public void TestSortCopy_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            list.Sort();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortCopy_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.Sort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortCopy_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.Sort(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortCopy_NullComparer_Throws()
        {
            var list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list.Sort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortCopy_NullComparison_Throws()
        {
            var list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list.Sort(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSortCopy_DestinationNull_Throws()
        {
            var list = new List<int>().ToSublist();
            Sublist<int[], int> destination = null;
            list.Sort().CopyTo(destination);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestSortCopy_EmptyList()
        {
            var list = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            var result = list.Sort().CopyTo(destination);
            Assert.AreEqual(0, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(0, result.DestinationOffset, "The destination offset was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Sort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestSortCopy_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(200, i => 199 - i).AddTo(list);
            destination = Sublist.Generate(200, 0).AddTo(destination);

            var result = list.Sort(Comparer<int>.Default).CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            Assert.IsTrue(destination.IsSorted(Comparer<int>.Default), "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Sort should work against a list whose values ascend and then descend.
        /// </summary>
        [TestMethod]
        public void TestSortCopy_PipeOrganed()
        {
            var list = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(100, i => i * 2).AddTo(list);
            list = Sublist.Generate(200, i => 199 - (i - 100) * 2).AddTo(list);
            destination = Sublist.Generate(300, 0).AddTo(destination);

            var result = list.Sort(Comparer<int>.Default.Compare).CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.IsTrue(destination.IsSorted(Comparer<int>.Default.Compare), "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Sort should work against a list whose values jump between small and large.
        /// </summary>
        [TestMethod]
        public void TestSortCopy_Interweaved()
        {
            var list = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(200, i => i % 2 == 0 ? i : 199 - (i - 1)).AddTo(list);
            destination = Sublist.Generate(200, 0).AddTo(destination);

            var result = list.Sort().CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.IsTrue(destination.IsSorted(), "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Sort should work against a list whose values are sorted except the last value.
        /// </summary>
        [TestMethod]
        public void TestSortCopy_LastMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(200, i => i + 1).AddTo(list);
            list = new int[] { 0 }.AddTo(list);
            destination = Sublist.Generate(201, 0).AddTo(destination);

            var result = list.Sort().CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.IsTrue(destination.IsSorted(), "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Sort should work against a list whose values are sorted except the first value.
        /// </summary>
        [TestMethod]
        public void TestSortCopy_FirstMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = new int[] { 200 }.AddTo(list);
            list = Sublist.Generate(201, i => i - 1).AddTo(list);
            destination = Sublist.Generate(202, 0).AddTo(destination);

            var result = list.Sort().CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.IsTrue(destination.IsSorted(), "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
