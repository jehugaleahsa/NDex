using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the StableSortAdd methods.
    /// </summary>
    [TestClass]
    public class StableSortAddTester
    {
        #region Real World Example

        /// <summary>
        /// StableSort is mostly useful when a fast sort is needed and equivalent values need to stay in the same order.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_SortRandomList_KeepEquivalentItemsOrdered()
        {
            Random random = new Random();

            // build a list
            var list = new List<Tuple<int, int>>(100);
            Sublist.Generate(100, i => Tuple.Create(random.Next(100), i)).AddTo(list.ToSublist());

            // sort the list
            var destination = new List<Tuple<int, int>>(100);
            list.ToSublist().StableSort().AddTo(destination.ToSublist());

            // first make sure the list is sorted by the first value
            bool isFirstSorted = destination.ToSublist().IsSorted((t1, t2) => Comparer<int>.Default.Compare(t1.Item1, t2.Item1));
            Assert.IsTrue(isFirstSorted, "The items were not sorted according to the first item.");

            // then make sure the list is sorted by the second value
            // tuples compare by first comparing the first items, then, if necessary, the second items
            bool isSorted = destination.ToSublist().IsSorted();
            Assert.IsTrue(isSorted, "The equivalent items did not remain in the same order.");
        }

        /// <summary>
        /// StableSort is mostly useful when a fast sort is needed and equivalent values need to stay in the same order.
        /// If we restrict the size of the buffer to three, the merge sort will be very slow.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_SortRandomList_TinyBuffer_KeepEquivalentItemsOrdered()
        {
            Random random = new Random();

            // build a list
            var list = new List<Tuple<int, int>>(100);
            Sublist.Generate(100, i => Tuple.Create(random.Next(100), i)).AddTo(list.ToSublist());

            // sort the list
            Tuple<int, int>[] buffer = new Tuple<int, int>[3]; // no space to merge - bad for performance!
            var destination = new List<Tuple<int, int>>(100);
            list.ToSublist().StableSort(buffer.ToSublist()).AddTo(destination.ToSublist());

            // first make sure the list is sorted by the first value
            bool isFirstSorted = destination.ToSublist().IsSorted((t1, t2) => Comparer<int>.Default.Compare(t1.Item1, t2.Item1));
            Assert.IsTrue(isFirstSorted, "The items were not sorted according to the first item.");

            // then make sure the list is sorted by the second value
            // tuples compare by first comparing the first items, then, if necessary, the second items
            bool isSorted = destination.ToSublist().IsSorted();
            Assert.IsTrue(isSorted, "The equivalent items did not remain in the same order.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            list.StableSort();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_WithBuffer_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            IExpandableSublist<List<int>, int> buffer = new List<int>().ToSublist();
            list.StableSort(buffer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_WithComparer_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.StableSort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_WithBuffer_WithComparer_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            IExpandableSublist<List<int>, int> buffer = new List<int>().ToSublist();
            IComparer<int> comparer = Comparer<int>.Default;
            list.StableSort(buffer, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_WithComparison_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.StableSort(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_WithBuffer_WithComparison_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            IExpandableSublist<List<int>, int> buffer = new List<int>().ToSublist();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.StableSort(buffer, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the buffer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_NullBuffer_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> buffer = null;
            list.StableSort(buffer);
        }

        /// <summary>
        /// An exception should be thrown if the buffer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_WithComparer_NullBuffer_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> buffer = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.StableSort(buffer, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the buffer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_WithComparison_NullBuffer_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> buffer = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.StableSort(buffer, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_NullComparer_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list.StableSort(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_WithBuffer_NullComparer_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> buffer = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list.StableSort(buffer, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_NullComparison_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list.StableSort(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_WithBuffer_NullComparison_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> buffer = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list.StableSort(buffer, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStableSortAdd_DestinationNull_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            list.StableSort().AddTo(destination);
        }

        #endregion

        /// <summary>
        /// Sorting an empty list should do nothing.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_EmptyList()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list.StableSort().AddTo(destination);
            Assert.AreEqual(0, destination.Count, "Items were added to the destination.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If someone tries to call StableSort with an empty buffer, InsertionSort is called,
        /// which is a stable sort, although much slower.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_EmptyBuffer_CallsInsertionSort()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list.StableSort(buffer).AddTo(destination);

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a reversed list.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_Reversed()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(200, i => 199 - i).AddTo(TestHelper.Populate(list));
            destination = list.StableSort(Comparer<int>.Default).AddTo(destination);

            bool result = destination.IsSorted(Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a reversed list, even if the buffer is quarter the list's size.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_Reversed_QuarterBuffer()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(100, i => 99 - i).AddTo(list.List.ToSublist());
            buffer = Sublist.Generate(25, 0).AddTo(buffer);
            destination = list.StableSort(buffer, Comparer<int>.Default).AddTo(destination);

            bool result = destination.IsSorted(Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a reversed list, even if the buffer is tiny.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_Reversed_TinyBuffer()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(101, i => 101 - i).AddTo(TestHelper.Populate(list));
            buffer = Sublist.Generate(3, 0).AddTo(buffer);
            destination = list.StableSort(buffer, Comparer<int>.Default).AddTo(destination);

            bool result = destination.IsSorted(Comparer<int>.Default);
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a list whose values ascend and then descend.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_PipeOrganed()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(100, i => i * 2).AddTo(TestHelper.Populate(list));
            list = Sublist.Generate(200, i => 199 - (i - 100) * 2).AddTo(TestHelper.Populate(list));
            destination = list.StableSort(Comparer<int>.Default.Compare).AddTo(destination);

            bool result = destination.IsSorted(Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a list whose values ascend and then descend, even if the buffer is quarter the list's size.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_PipeOrganed_QuarterBuffer()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(50, i => i * 2).AddTo(TestHelper.Populate(list));
            list = Sublist.Generate(100, i => 99 - (i - 50) * 2).AddTo(TestHelper.Populate(list));
            buffer = Sublist.Generate(25, 0).AddTo(buffer);
            destination = list.StableSort(buffer, Comparer<int>.Default.Compare).AddTo(destination);

            bool result = destination.IsSorted(Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a list whose values ascend and then descend, even if the buffer is tiny.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_PipeOrganed_TinyBuffer()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(50, i => i * 2).AddTo(TestHelper.Populate(list));
            list = Sublist.Generate(101, i => 101 - (i - 50) * 2).AddTo(TestHelper.Populate(list));
            buffer = Sublist.Generate(3, 0).AddTo(buffer);
            destination = list.StableSort(buffer, Comparer<int>.Default.Compare).AddTo(destination);

            bool result = destination.IsSorted(Comparer<int>.Default.Compare);
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a list whose values jump between small and large.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_Interweaved()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(200, i => i % 2 == 0 ? i : 199 - (i - 1)).AddTo(TestHelper.Populate(list));
            destination = list.StableSort().AddTo(destination);

            bool result = destination.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a list whose values jump between small and large, even if the buffer is quarter the list's size.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_Interweaved_QuarterBuffer()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(100, i => i % 2 == 0 ? i : 99 - (i - 1)).AddTo(TestHelper.Populate(list));
            buffer = Sublist.Generate(25, 0).AddTo(buffer);
            destination = list.StableSort(buffer).AddTo(destination);

            bool result = destination.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a list whose values jump between small and large, even if the buffer is tiny.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_Interweaved_TinyBuffer()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(101, i => i % 2 == 0 ? i : 101 - (i - 1)).AddTo(TestHelper.Populate(list));
            buffer = Sublist.Generate(3, 0).AddTo(buffer);
            destination = list.StableSort(buffer).AddTo(destination);

            bool result = destination.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a list whose values are sorted except the last value.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_LastMisplaced()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(200, i => i + 1).AddTo(TestHelper.Populate(list));
            list = new int[] { 0 }.AddTo(TestHelper.Populate(list));
            destination = list.StableSort().AddTo(destination);

            bool result = destination.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a list whose values are sorted except the last value, even if the buffer is quarter the list's size.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_LastMisplaced_QuarterBuffer()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(100, i => i).AddTo(TestHelper.Populate(list));
            list = new int[] { -1 }.AddTo(TestHelper.Populate(list));
            buffer = Sublist.Generate(25, 0).AddTo(buffer);
            destination = list.StableSort(buffer).AddTo(destination);

            bool result = destination.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a list whose values are sorted except the last value, even if the buffer is tiny.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_LastMisplaced_TinyBuffer()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(101, i => i).AddTo(TestHelper.Populate(list));
            list = new int[] { -1 }.AddTo(TestHelper.Populate(list));
            buffer = Sublist.Generate(3, 0).AddTo(buffer);
            destination = list.StableSort(buffer).AddTo(destination);

            bool result = destination.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a list whose values are sorted except the first value.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_FirstMisplaced()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = new int[] { 200 }.AddTo(TestHelper.Populate(list));
            list = Sublist.Generate(201, i => i - 1).AddTo(TestHelper.Populate(list));
            destination = list.StableSort().AddTo(destination);

            bool result = destination.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a list whose values are sorted except the first value, even if the buffer is quarter the list's size.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_FirstMisplaced_QuarterBuffer()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = new int[] { 100 }.AddTo(TestHelper.Populate(list));
            list = Sublist.Generate(101, i => i - 1).AddTo(TestHelper.Populate(list));
            buffer = Sublist.Generate(25, 0).AddTo(buffer);
            destination = list.StableSort(buffer).AddTo(destination);

            bool result = destination.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// StableSort should work against a list whose values are sorted except the first value, even if the buffer is tiny.
        /// </summary>
        [TestMethod]
        public void TestStableSortAdd_FirstMisplaced_TinyBuffer()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var buffer = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = new int[] { 100 }.AddTo(TestHelper.Populate(list));
            list = Sublist.Generate(101, i => i - 1).AddTo(TestHelper.Populate(list));
            buffer = Sublist.Generate(3, 0).AddTo(buffer);
            destination = list.StableSort(buffer).AddTo(destination);

            bool result = destination.IsSorted();
            Assert.IsTrue(result, "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(buffer);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
