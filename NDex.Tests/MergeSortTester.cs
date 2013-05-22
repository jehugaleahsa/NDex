using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the MergeSort methods.
    /// </summary>
    [TestClass]
    public class MergeSortTester
    {
        #region Real World Example

        /// <summary>
        /// MergeSort is mostly useful when a fast sort is needed and equivalent values need to stay in the same order.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_SortRandomList_KeepEquivalentItemsOrdered()
        {
            Random random = new Random();

            // build a list
            var list = new List<Tuple<int, int>>(100);
            Sublist.Generate(100, i => Tuple.Create(random.Next(100), i)).AddTo(list.ToSublist());

            // sort the list
            list.ToSublist().MergeSort();

            // first make sure the list is sorted by the first value
            bool isFirstSorted = list.ToSublist().IsSorted((t1, t2) => Comparer<int>.Default.Compare(t1.Item1, t2.Item1));
            Assert.IsTrue(isFirstSorted, "The items were not sorted according to the first item.");

            // then make sure the list is sorted by the second value
            // tuples compare by first comparing the first items, then, if necessary, the second items
            bool isSorted = list.ToSublist().IsSorted();
            Assert.IsTrue(isSorted, "The equivalent items did not remain in the same order.");
        }

        /// <summary>
        /// MergeSort is mostly useful when a fast sort is needed and equivalent values need to stay in the same order.
        /// If we restrict the size of the buffer to three, the merge sort will be very slow.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_SortRandomList_TinyBuffer_KeepEquivalentItemsOrdered()
        {
            Random random = new Random();

            // build a list
            var list = new List<Tuple<int, int>>(100);
            Sublist.Generate(100, i => Tuple.Create(random.Next(100), i)).AddTo(list.ToSublist());

            // sort the list
            Tuple<int, int>[] buffer = new Tuple<int, int>[3]; // no space to merge - bad for performance!
            list.ToSublist().MergeSort(buffer.ToSublist());

            // first make sure the list is sorted by the first value
            bool isFirstSorted = list.ToSublist().IsSorted((t1, t2) => Comparer<int>.Default.Compare(t1.Item1, t2.Item1));
            Assert.IsTrue(isFirstSorted, "The items were not sorted according to the first item.");

            // then make sure the list is sorted by the second value
            // tuples compare by first comparing the first items, then, if necessary, the second items
            bool isSorted = list.ToSublist().IsSorted();
            Assert.IsTrue(isSorted, "The equivalent items did not remain in the same order.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            list.MergeSort();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_WithBuffer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> buffer = new List<int>();
            list.MergeSort(buffer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.MergeSort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_WithBuffer_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> buffer = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            list.MergeSort(buffer, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.MergeSort(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_WithBuffer_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> buffer = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.MergeSort(buffer, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the buffer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_NullBuffer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> buffer = null;
            list.MergeSort(buffer);
        }

        /// <summary>
        /// An exception should be thrown if the buffer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_WithComparer_NullBuffer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> buffer = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.MergeSort(buffer, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the buffer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_WithComparison_NullBuffer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> buffer = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.MergeSort(buffer, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_NullComparer_Throws()
        {
            var list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list.MergeSort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_WithBuffer_NullComparer_Throws()
        {
            var list = new List<int>().ToSublist();
            Sublist<List<int>, int> buffer = new List<int>();
            IComparer<int> comparer = null;
            list.MergeSort(buffer, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_NullComparison_Throws()
        {
            var list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list.MergeSort(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSort_WithBuffer_NullComparison_Throws()
        {
            var list = new List<int>().ToSublist();
            Sublist<List<int>, int> buffer = new List<int>();
            Func<int, int, int> comparison = null;
            list.MergeSort(buffer, comparison);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_EmptyList()
        {
            var list = TestHelper.Wrap(new List<int>());
            list.MergeSort();
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If someone tries to call MergeSort with an empty buffer, InsertionSort is called,
        /// which is a stable sort, although much slower.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_EmptyBuffer_CallsInsertionSort()
        {
            var list = TestHelper.Wrap(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            list.MergeSort(buffer);
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
        }

        /// <summary>
        /// MergeSort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(200, i => 199 - i).AddTo(list);
            list.MergeSort(Comparer<int>.Default);
            bool result = list.IsSorted(Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// MergeSort should work against a reversed list, even if the buffer is quarter the list's size.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_Reversed_QuarterBuffer()
        {
            var list = TestHelper.Wrap(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(100, i => 99 - i).AddTo(list);
            buffer = Sublist.Generate(25, 0).AddTo(buffer);
            list.MergeSort(buffer, Comparer<int>.Default);
            bool result = list.IsSorted(Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
        }

        /// <summary>
        /// MergeSort should work against a reversed list, even if the buffer is tiny.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_Reversed_TinyBuffer()
        {
            var list = TestHelper.Wrap(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(101, i => 101 - i).AddTo(list);
            buffer = Sublist.Generate(3, 0).AddTo(buffer);
            list.MergeSort(buffer, Comparer<int>.Default);
            bool result = list.IsSorted(Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
        }

        /// <summary>
        /// MergeSort should work against a list whose values ascend and then descend.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_PipeOrganed()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(100, i => i * 2).AddTo(list);
            list = Sublist.Generate(200, i => 199 - (i - 100) * 2).AddTo(list);
            list.MergeSort(Comparer<int>.Default.Compare);
            bool result = list.IsSorted(Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// MergeSort should work against a list whose values ascend and then descend, even if the buffer is quarter the list's size.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_PipeOrganed_QuarterBuffer()
        {
            var list = TestHelper.Wrap(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(50, i => i * 2).AddTo(list);
            list = Sublist.Generate(100, i => 99 - (i - 50) * 2).AddTo(list);
            buffer = Sublist.Generate(25, 0).AddTo(buffer);
            list.MergeSort(buffer, Comparer<int>.Default.Compare);
            bool result = list.IsSorted(Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
        }

        /// <summary>
        /// MergeSort should work against a list whose values ascend and then descend, even if the buffer is tiny.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_PipeOrganed_TinyBuffer()
        {
            var list = TestHelper.Wrap(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(50, i => i * 2).AddTo(list);
            list = Sublist.Generate(101, i => 101 - (i - 50) * 2).AddTo(list);
            buffer = Sublist.Generate(3, 0).AddTo(buffer);
            list.MergeSort(buffer, Comparer<int>.Default.Compare);
            bool result = list.IsSorted(Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
        }

        /// <summary>
        /// MergeSort should work against a list whose values jump between small and large.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_Interweaved()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(200, i => i % 2 == 0 ? i : 199 - (i - 1)).AddTo(list);
            list.MergeSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// MergeSort should work against a list whose values jump between small and large, even if the buffer is quarter the list's size.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_Interweaved_QuarterBuffer()
        {
            var list = TestHelper.Wrap(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(100, i => i % 2 == 0 ? i : 99 - (i - 1)).AddTo(list);
            buffer = Sublist.Generate(25, 0).AddTo(buffer);
            list.MergeSort(buffer);
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
        }

        /// <summary>
        /// MergeSort should work against a list whose values jump between small and large, even if the buffer is tiny.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_Interweaved_TinyBuffer()
        {
            var list = TestHelper.Wrap(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(101, i => i % 2 == 0 ? i : 101 - (i - 1)).AddTo(list);
            buffer = Sublist.Generate(3, 0).AddTo(buffer);
            list.MergeSort(buffer);
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
        }

        /// <summary>
        /// MergeSort should work against a list whose values are sorted except the last value.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_LastMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(200, i => i + 1).AddTo(list);
            list = new int[] { 0 }.AddTo(list);
            list.MergeSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// MergeSort should work against a list whose values are sorted except the last value, even if the buffer is quarter the list's size.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_LastMisplaced_QuarterBuffer()
        {
            var list = TestHelper.Wrap(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(100, i => i).AddTo(list);
            list = new int[] { -1 }.AddTo(list);
            buffer = Sublist.Generate(25, 0).AddTo(buffer);
            list.MergeSort(buffer);
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
        }

        /// <summary>
        /// MergeSort should work against a list whose values are sorted except the last value, even if the buffer is tiny.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_LastMisplaced_TinyBuffer()
        {
            var list = TestHelper.Wrap(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(101, i => i).AddTo(list);
            list = new int[] { -1 }.AddTo(list);
            buffer = Sublist.Generate(3, 0).AddTo(buffer);
            list.MergeSort(buffer);
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
        }

        /// <summary>
        /// MergeSort should work against a list whose values are sorted except the first value.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_FirstMisplaced()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = new int[] { 200 }.AddTo(list);
            list = Sublist.Generate(201, i => i - 1).AddTo(list);
            list.MergeSort();
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// MergeSort should work against a list whose values are sorted except the first value, even if the buffer is quarter the list's size.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_FirstMisplaced_QuarterBuffer()
        {
            var list = TestHelper.Wrap(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            list = new int[] { 100 }.AddTo(list);
            list = Sublist.Generate(101, i => i - 1).AddTo(list);
            buffer = Sublist.Generate(25, 0).AddTo(buffer);
            list.MergeSort(buffer);
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
        }

        /// <summary>
        /// MergeSort should work against a list whose values are sorted except the first value, even if the buffer is tiny.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_FirstMisplaced_TinyBuffer()
        {
            var list = TestHelper.Wrap(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            list = new int[] { 100 }.AddTo(list);
            list = Sublist.Generate(101, i => i - 1).AddTo(list);
            buffer = Sublist.Generate(3, 0).AddTo(buffer);
            list.MergeSort(buffer);
            bool result = list.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
        }
    }
}
