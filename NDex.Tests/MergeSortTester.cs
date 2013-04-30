using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;
using System.Linq;

namespace NDex.Test
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
            Sublist.Add(Enumerable.Range(0, 100).Select(i => Tuple.Create(random.Next(100), i)), list.ToSublist());

            // sort the list
            Sublist.MergeSort(list.ToSublist());

            // first make sure the list is sorted by the first value
            bool isFirstSorted = Sublist.IsSorted(list.ToSublist(), (t1, t2) => Comparer<int>.Default.Compare(t1.Item1, t2.Item1));
            Assert.IsTrue(isFirstSorted, "The items were not sorted according to the first item.");

            // then make sure the list is sorted by the second value
            // tuples compare by first comparing the first items, then, if necessary, the second items
            bool isSorted = Sublist.IsSorted(list.ToSublist());
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
            Sublist.Add(Enumerable.Range(0, 100).Select(i => Tuple.Create(random.Next(100), i)), list.ToSublist());

            // sort the list
            Tuple<int, int>[] buffer = new Tuple<int, int>[3]; // no space to merge - bad for performance!
            Sublist.MergeSort(list.ToSublist(), buffer.ToSublist());

            // first make sure the list is sorted by the first value
            bool isFirstSorted = Sublist.IsSorted(list.ToSublist(), (t1, t2) => Comparer<int>.Default.Compare(t1.Item1, t2.Item1));
            Assert.IsTrue(isFirstSorted, "The items were not sorted according to the first item.");

            // then make sure the list is sorted by the second value
            // tuples compare by first comparing the first items, then, if necessary, the second items
            bool isSorted = Sublist.IsSorted(list.ToSublist());
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
            Sublist.MergeSort(list);
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
            Sublist.MergeSort(list, buffer);
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
            Sublist.MergeSort(list, comparer);
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
            Sublist.MergeSort(list, buffer, comparer);
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
            Sublist.MergeSort(list, comparison);
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
            Sublist.MergeSort(list, buffer, comparison);
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
            Sublist.MergeSort(list, buffer);
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
            Sublist.MergeSort(list, buffer, comparer);
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
            Sublist.MergeSort(list, buffer, comparison);
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
            Sublist.MergeSort(list, comparer);
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
            Sublist.MergeSort(list, buffer, comparer);
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
            Sublist.MergeSort(list, comparison);
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
            Sublist.MergeSort(list, buffer, comparison);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestMergeSort_EmptyList()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.MergeSort(list);
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
            Sublist.MergeSort(list, buffer);
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
            list = Sublist.Add(Enumerable.Range(0, 200).Select(i => 199 - i), list);
            Sublist.MergeSort(list, Comparer<int>.Default);
            bool result = Sublist.IsSorted(list, Comparer<int>.Default);
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
            list = Sublist.Add(Enumerable.Range(0, 100).Select(i => 99 - i), list);
            buffer = Sublist.Add(Enumerable.Repeat(0, 25), buffer);
            Sublist.MergeSort(list, buffer, Comparer<int>.Default);
            bool result = Sublist.IsSorted(list, Comparer<int>.Default);
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
            list = Sublist.Add(Enumerable.Range(0, 101).Select(i => 101 - i), list);
            buffer = Sublist.Add(Enumerable.Repeat(0, 3), buffer);
            Sublist.MergeSort(list, buffer, Comparer<int>.Default);
            bool result = Sublist.IsSorted(list, Comparer<int>.Default);
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
            list = Sublist.Add(Enumerable.Range(0, 100).Select(i => i * 2), list);
            list = Sublist.Add(Enumerable.Range(0, 200).Select(i => 199 - (i - 100) * 2), list);
            Sublist.MergeSort(list, Comparer<int>.Default.Compare);
            bool result = Sublist.IsSorted(list, Comparer<int>.Default.Compare);
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
            list = Sublist.Add(Enumerable.Range(0, 50).Select(i => i * 2), list);
            list = Sublist.Add(Enumerable.Range(0, 100).Select(i => 99 - (i - 50) * 2), list);
            buffer = Sublist.Add(Enumerable.Repeat(0, 25), buffer);
            Sublist.MergeSort(list, buffer, Comparer<int>.Default.Compare);
            bool result = Sublist.IsSorted(list, Comparer<int>.Default.Compare);
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
            list = Sublist.Add(Enumerable.Range(0, 50).Select(i => i * 2), list);
            list = Sublist.Add(Enumerable.Range(0, 101).Select(i => 101 - (i - 50) * 2), list);
            buffer = Sublist.Add(Enumerable.Repeat(0, 3), buffer);
            Sublist.MergeSort(list, buffer, Comparer<int>.Default.Compare);
            bool result = Sublist.IsSorted(list, Comparer<int>.Default.Compare);
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
            list = Sublist.Add(Enumerable.Range(0, 200).Select(i => i % 2 == 0 ? i : 199 - (i - 1)), list);
            Sublist.MergeSort(list);
            bool result = Sublist.IsSorted(list);
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
            list = Sublist.Add(Enumerable.Range(0, 100).Select(i => i % 2 == 0 ? i : 99 - (i - 1)), list);
            buffer = Sublist.Add(Enumerable.Repeat(0, 25), buffer);
            Sublist.MergeSort(list, buffer);
            bool result = Sublist.IsSorted(list);
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
            list = Sublist.Add(Enumerable.Range(0, 101).Select(i => i % 2 == 0 ? i : 101 - (i - 1)), list);
            buffer = Sublist.Add(Enumerable.Repeat(0, 3), buffer);
            Sublist.MergeSort(list, buffer);
            bool result = Sublist.IsSorted(list);
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
            list = Sublist.Add(Enumerable.Range(0, 200).Select(i => i + 1), list);
            list = Sublist.Add(new int[] { 0 }, list);
            Sublist.MergeSort(list);
            bool result = Sublist.IsSorted(list);
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
            list = Sublist.Add(Enumerable.Range(0, 100), list);
            list = Sublist.Add(new int[] { -1 }, list);
            buffer = Sublist.Add(Enumerable.Repeat(0, 25), buffer);
            Sublist.MergeSort(list, buffer);
            bool result = Sublist.IsSorted(list);
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
            list = Sublist.Add(Enumerable.Range(0, 101), list);
            list = Sublist.Add(new int[] { -1 }, list);
            buffer = Sublist.Add(Enumerable.Repeat(0, 3), buffer);
            Sublist.MergeSort(list, buffer);
            bool result = Sublist.IsSorted(list);
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
            list = Sublist.Add(new int[] { 200 }, list);
            list = Sublist.Add(Enumerable.Range(0, 201).Select(i => i - 1), list);
            Sublist.MergeSort(list);
            bool result = Sublist.IsSorted(list);
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
            list = Sublist.Add(new int[] { 100 }, list);
            list = Sublist.Add(Enumerable.Range(0, 101).Select(i => i - 1), list);
            buffer = Sublist.Add(Enumerable.Repeat(0, 25), buffer);
            Sublist.MergeSort(list, buffer);
            bool result = Sublist.IsSorted(list);
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
            list = Sublist.Add(new int[] { 100 }, list);
            list = Sublist.Add(Enumerable.Range(0, 101).Select(i => i - 1), list);
            buffer = Sublist.Add(Enumerable.Repeat(0, 3), buffer);
            Sublist.MergeSort(list, buffer);
            bool result = Sublist.IsSorted(list);
            Assert.IsTrue(result, "The list was not sorted.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
        }
    }
}
