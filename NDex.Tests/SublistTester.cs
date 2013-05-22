using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the Sublist class.
    /// </summary>
    [TestClass]
    public class SublistTester
    {
        #region ToSublist

        /// <summary>
        /// If we just call ToSublist with no parameters, it wraps the entire list.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnList_WrapsEntireList()
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            var sublist = list.ToSublist();
            Assert.AreSame(list, sublist.List, "The backing list was not set.");
            Assert.AreEqual(0, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(list.Count, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we just call ToSublist with no parameters, it wraps the entire array.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnArray_WrapsEntireList()
        {
            int[] array = new int[] { 1, 2, 3 };
            var sublist = array.ToSublist();
            Assert.AreSame(array, sublist.List, "The backing array was not set.");
            Assert.AreEqual(0, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(array.Length, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we just call ToSublist with no parameters, it wraps the entire collection.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnCollection_WrapsEntireCollection()
        {
            Collection<int> list = new Collection<int>() { 1, 2, 3 };
            var sublist = list.ToSublist();
            Assert.AreSame(list, sublist.List, "The backing list was not set.");
            Assert.AreEqual(0, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(list.Count, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset, the rest of the list is wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnList_WithOffset_WrapsRemaining()
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            var sublist = list.ToSublist(1);
            Assert.AreSame(list, sublist.List, "The backing list was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(2, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset, the rest of the array is wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnArray_WithOffset_WrapsRemaining()
        {
            int[] array = new int[] { 1, 2, 3 };
            var sublist = array.ToSublist(1);
            Assert.AreSame(array, sublist.List, "The backing array was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(2, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset, the rest of the collection is wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnCollection_WithOffset_WrapsRemaining()
        {
            Collection<int> list = new Collection<int>() { 1, 2, 3 };
            var sublist = list.ToSublist(1);
            Assert.AreSame(list, sublist.List, "The backing list was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(2, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset and count, that portion of the list is returned.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnList_WithOffsetAndCount_WrapsRemaining()
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            var sublist = list.ToSublist(1, 1);
            Assert.AreSame(list, sublist.List, "The backing list was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(1, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset and count, that portion of the list is returned.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnArray_WithOffsetAndCount_WrapsRemaining()
        {
            int[] array = new int[] { 1, 2, 3 };
            var sublist = array.ToSublist(1, 1);
            Assert.AreSame(array, sublist.List, "The backing array was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(1, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset and count, that portion of the collection is returned.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnCollection_WithOffsetAndCount_WrapsRemaining()
        {
            Collection<int> list = new Collection<int>() { 1, 2, 3 };
            var sublist = list.ToSublist(1, 1);
            Assert.AreSame(list, sublist.List, "The backing list was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(1, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist without an offset or count, the entire list should be wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnTypedList_WrapsEntireList()
        {
            ArrayList list = new ArrayList();
            var typed = list.Typed<int>();
            var sublist = typed.ToSublist();
            Assert.AreSame(typed, sublist.List, "The back list was not set.");
            Assert.AreEqual(0, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(typed.Count, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset, everything after the offset should be wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnTypedList_WithOffset_WrapsRemaining()
        {
            ArrayList list = new ArrayList() { 1, 2, 3 };
            var typed = list.Typed<int>();
            var sublist = typed.ToSublist(1);
            Assert.AreSame(typed, sublist.List, "The back list was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(2, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset and count, the range defined should be wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnTypedList_WithOffsetAndCount_WrapsRange()
        {
            ArrayList list = new ArrayList() { 1, 2, 3 };
            var typed = list.Typed<int>();
            var sublist = typed.ToSublist(1, 1);
            Assert.AreSame(typed, sublist.List, "The back list was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(1, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist without an offset or count, the entire list should be wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnReadOnlyList_WrapsEntireList()
        {
            List<int> list = new List<int>();
            var readOnly = list.ReadOnly();
            var sublist = readOnly.ToSublist();
            Assert.AreSame(readOnly, sublist.List, "The back list was not set.");
            Assert.AreEqual(0, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(readOnly.Count, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset, everything after the offset should be wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnReadOnlyList_WithOffset_WrapsRemaining()
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            var readOnly = list.ReadOnly();
            var sublist = readOnly.ToSublist(1);
            Assert.AreSame(readOnly, sublist.List, "The back list was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(2, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset and count, the range defined should be wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnReadOnlyList_WithOffsetAndCount_WrapsRange()
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            var readOnly = list.ReadOnly();
            var sublist = readOnly.ToSublist(1, 1);
            Assert.AreSame(readOnly, sublist.List, "The back list was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(1, sublist.Count, "The count was wrong.");
        }

        #endregion

        #region Ctor

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_NullList_Throws()
        {
            List<int> list = null;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_WithOffset_NullList_Throws()
        {
            List<int> list = null;
            int offset = 0;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_WithOffsetAndCount_NullList_Throws()
        {
            List<int> list = null;
            int offset = 0;
            int count = 0;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_NegativeOffset_Throws()
        {
            List<int> list = new List<int>();
            int offset = -1;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset);
        }

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_WithCount_NegativeOffset_Throws()
        {
            List<int> list = new List<int>();
            int offset = -1;
            int count = 0;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the offset too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_OffsetTooBig_Throws()
        {
            List<int> list = new List<int>();
            int offset = 1;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset);
        }

        /// <summary>
        /// An exception should be thrown if the offset too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_WithCount_OffsetTooBig_Throws()
        {
            List<int> list = new List<int>();
            int offset = 1;
            int count = 0;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the count is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_NegativeCount_Throws()
        {
            List<int> list = new List<int>();
            int offset = 0;
            int count = -1;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the count is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_CountTooBig_Throws()
        {
            List<int> list = new List<int>();
            int offset = 0;
            int count = 1;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset, count);
        }

        /// <summary>
        /// The ctor just taking a list, should wrap the entire list.
        /// </summary>
        [TestMethod]
        public void TestCtor_WrapsEntireList()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4 };
            var sublist = new Sublist<List<int>, int>(list);
            Assert.AreSame(list, sublist.List, "The list was not set as a backing field.");
            Assert.AreEqual(list.Count, sublist.Count, "The sublist had the wrong count.");
            Assert.AreEqual(0, sublist.Offset, "The sublist had the wrong offset.");
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(sublist), "The sublist did not contain the expected items.");
        }

        /// <summary>
        /// The ctor taking an offset defaults the count to the remaining number of items.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithOffset_WrapsRemainingList()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4 };
            var sublist = new Sublist<List<int>, int>(list, 1);
            Assert.AreSame(list, sublist.List, "The list was not set as a backing field.");
            Assert.AreEqual(list.Count - 1, sublist.Count, "The sublist had the wrong count.");
            Assert.AreEqual(1, sublist.Offset, "The sublist had the wrong offset.");
            int[] expected = { 2, 3, 4, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(sublist), "The sublist did not contain the expected items.");
        }

        /// <summary>
        /// The ctor taking a count limits the number of items to a splice of the underlying list.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithOffsetAndCount_CreatesSplice()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4 };
            var sublist = new Sublist<List<int>, int>(list, 1, 2);
            Assert.AreSame(list, sublist.List, "The list was not set as a backing field.");
            Assert.AreEqual(2, sublist.Count, "The sublist had the wrong count.");
            Assert.AreEqual(1, sublist.Offset, "The sublist had the wrong offset.");
            int[] expected = { 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(sublist), "The sublist did not contain the expected items.");
        }

        #endregion

        #region Nest

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_NegativeOffset_Throws()
        {
            var list = new List<int>().ToSublist();
            int offset = -1;
            list.Nest(offset);
        }

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_WithCount_NegativeOffset_Throws()
        {
            var list = new List<int>().ToSublist();
            int offset = -1;
            int count = 0;
            list.Nest(offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the offset is past the end of the sublist.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_OffsetTooBig_Throws()
        {
            var list = new List<int>().ToSublist();
            int offset = 1;
            list.Nest(offset);
        }

        /// <summary>
        /// An exception should be thrown if the offset is past the end of the sublist.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_WithCount_OffsetTooBig_Throws()
        {
            var list = new List<int>().ToSublist();
            int offset = 1;
            int count = 0;
            list.Nest(offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the count is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_NegativeCount_Throws()
        {
            var list = new List<int>().ToSublist();
            int offset = 0;
            int count = -1;
            list.Nest(offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the count is greater than the remaining items.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_CountTooBig_Throws()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist();
            int offset = 1;
            int count = 5; // one too big
            list.Nest(offset, count);
        }

        /// <summary>
        /// If we just change the offset and there's no room to the right, we're essentially popping off the front.
        /// </summary>
        [TestMethod]
        public void TestNest_OffsetToPopFront()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(1);
            int[] expected = { 2, 3, 4, 5, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we just change the offset and there's no room to the right, we're essentially popping off the front.
        /// </summary>
        [TestMethod]
        public void TestNest_ReadOnly_OffsetToPopFront()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(1);
            int[] expected = { 2, 3, 4, 5, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we just change the offset and there's no room to the right, we're essentially popping off the front.
        /// </summary>
        [TestMethod]
        public void TestNest_Sublist_OffsetToPopFront()
        {
            IMutableSublist<List<int>, int> list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(1);
            int[] expected = { 2, 3, 4, 5, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we just change the offset and there's no room to the right, we're essentially popping off the front.
        /// </summary>
        [TestMethod]
        public void TestNest_Expandable_OffsetToPopFront()
        {
            IExpandableSublist<List<int>, int> list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(1);
            int[] expected = { 2, 3, 4, 5, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we nest a nested Sublist, the count should be adjusted as expected.
        /// </summary>
        [TestMethod]
        public void TestNest_DoublyNested_AdjustsCount()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(1).Nest(1);
            int[] expected = { 3, 4, 5, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_CountToPopBack()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(0, list.Count - 1);
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(nested), "The offset did not pop the last item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_ReadOnly_CountToPopBack()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(0, list.Count - 1);
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(nested), "The offset did not pop the last item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_Sublist_CountToPopBack()
        {
            IMutableSublist<List<int>, int> list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(0, list.Count - 1);
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(nested), "The offset did not pop the last item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_Expandable_CountToPopBack()
        {
            IExpandableSublist<List<int>, int> list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(0, list.Count - 1);
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(nested), "The offset did not pop the last item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_ShiftAndShrink()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(1, list.Count - 2); // we want to remove the front and back, two items
            int[] expected = { 2, 3, 4, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(nested), "The offset did not pop the last item.");
        }

        /// <summary>
        /// We can use Sublist to represent partitions within a list.
        /// </summary>
        [TestMethod]
        public void TestNest_RepresentPartitions()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToSublist();
            int partition = list.Partition(i => i % 2 == 0).InPlace(); // put evens in the front
            var evens = list.Nest(0, partition);
            var odds = list.Nest(partition);
            Assert.IsTrue(evens.TrueForAll(i => i % 2 == 0), "Not all evens in the first nested list.");
            Assert.IsTrue(odds.TrueForAll(i => i % 2 != 0), "Not all odds in the second nested list.");
        }

        #endregion

        #region Shift

        /// <summary>
        /// If we try to shift a sublist, making its offset negative, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestShift_MakeOffsetNegative_Throws()
        {
            int[] values = new int[] { 1, 2, 3 };
            var sublist = values.ToSublist();
            sublist.Shift(-1, true);
        }

        /// <summary>
        /// If we try to shift a sublist, making its offset past the end of the list,
        /// an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestShift_MakeOffsetTooBig_Throws()
        {
            int[] values = new int[] { 1, 2, 3 };
            var sublist = values.ToSublist();
            sublist.Shift(values.Length + 1, true);
        }

        /// <summary>
        /// If we try to shift a sublist so that it goes past of the end
        /// of the list, it should be resized automatically.
        /// </summary>
        [TestMethod]
        public void TestShift_PushPastEnd_Resizes()
        {
            int[] values = new int[] { 1, 2, 3 };
            var sublist = values.ToSublist(0, 2);
            sublist = sublist.Shift(2, false);
            Assert.AreEqual(sublist.Offset, 2, "The offset was not updated.");
            Assert.AreEqual(sublist.Count, 1, "The count was not updated.");
        }

        /// <summary>
        /// If we try to shift a sublist so that it still fits in the list,
        /// it should not be resized.
        /// </summary>
        [TestMethod]
        public void TestShift_PushedRightWithinBounds_Shifts()
        {
            int[] values = new int[] { 1, 2, 3 };
            var sublist = values.ToSublist(0, 2);
            sublist = sublist.Shift(1, true);
            Assert.AreEqual(sublist.Offset, 1, "The offset was not updated.");
            Assert.AreEqual(sublist.Count, 2, "The count was updated.");
        }

        /// <summary>
        /// If we try to shift a sublist so that it still fits in the list,
        /// it should not be resized.
        /// </summary>
        [TestMethod]
        public void TestShift_PushedLeftWithinBounds_Shifts()
        {
            int[] values = new int[] { 1, 2, 3 };
            var sublist = values.ToSublist(1, 2);
            sublist = sublist.Shift(-1, true);
            Assert.AreEqual(sublist.Offset, 0, "The offset was not updated.");
            Assert.AreEqual(sublist.Count, 2, "The count was updated.");
        }

        /// <summary>
        /// If we try to shift a sublist so that it goes beyond the bounds
        /// of the list, and we are checking our bounds, an exceptions should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestShift_PushedRightOutOfBounds_Checked_Throws()
        {
            int[] values = new int[] { 1, 2, 3 };
            var sublist = values.ToSublist(1, 2);
            sublist = sublist.Shift(1, true);
        }

        /// <summary>
        /// We should be able to call Shift via the IReadOnlySublist interface.
        /// </summary>
        [TestMethod]
        public void TestShift_ReadOnly()
        {
            IReadOnlySublist<List<int>, int> sublist = new Sublist<List<int>, int>(new List<int>());
            sublist.Shift(0, true);
        }

        /// <summary>
        /// We should be able to call Shift via the IMutableSublist interface.
        /// </summary>
        [TestMethod]
        public void TestShift_Mutable()
        {
            IMutableSublist<List<int>, int> sublist = new Sublist<List<int>, int>(new List<int>());
            sublist.Shift(0, true);
        }

        /// <summary>
        /// We should be able to call Shift via the IExpandableSublist interface.
        /// </summary>
        [TestMethod]
        public void TestShift_Expandable()
        {
            IExpandableSublist<List<int>, int> sublist = new Sublist<List<int>, int>(new List<int>());
            sublist.Shift(0, true);
        }

        #endregion

        #region Resize

        /// <summary>
        /// It is an error to resize to a negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestResize_NegativeSize_Throws()
        {
            int[] values = new int[] { 1, 2, 3 };
            var sublist = values.ToSublist();
            sublist.Resize(-1, true);
        }

        /// <summary>
        /// If we are checking, resizing beyond the length of the
        /// underlying list will result in an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestResize_CountTooBig_Checked_Throws()
        {
            int[] values = new int[] { 1, 2, 3 };
            var sublist = values.ToSublist(0, 0);
            sublist.Resize(values.Length + 1, true);
        }

        /// <summary>
        /// We should be able to shrink a sublist.
        /// </summary>
        [TestMethod]
        public void TestResize_Shrink_ReturnTrue()
        {
            int[] values = new int[] { 1, 2, 3 };
            var sublist = values.ToSublist();
            sublist = sublist.Resize(2, true);
            Assert.AreEqual(0, sublist.Offset, "The offset was updated.");
            Assert.AreEqual(2, sublist.Count, "The count was not updated.");
        }

        /// <summary>
        /// If we try to make the sublist larger than the underlying list,
        /// it will be resized as much as possible.
        /// </summary>
        [TestMethod]
        public void TestResize_ResizeTooBig_ReturnFalse()
        {
            int[] values = new int[] { 1, 2, 3 };
            var sublist = values.ToSublist();
            sublist.Resize(4, false);
            Assert.AreEqual(0, sublist.Offset, "The offset was updated.");
            Assert.AreEqual(values.Length, sublist.Count, "The count was not updated.");
        }

        /// <summary>
        /// If we try to make the sublist larger and it still fits in the list,
        /// it should resize appropriately.
        /// </summary>
        [TestMethod]
        public void TestResize_ResizeToLength_ReturnTrue()
        {
            int[] values = new int[] { 1, 2, 3 };
            var sublist = values.ToSublist(0, 0);
            sublist = sublist.Resize(3, true);
            Assert.AreEqual(0, sublist.Offset, "The offset was updated.");
            Assert.AreEqual(values.Length, sublist.Count, "The count was not updated.");
        }

        /// <summary>
        /// We should be able to call Resize via the IReadOnlySublist interface.
        /// </summary>
        [TestMethod]
        public void TestResize_ReadOnly()
        {
            IReadOnlySublist<List<int>, int> sublist = new Sublist<List<int>, int>(new List<int>());
            sublist.Resize(sublist.Count, true);
        }

        /// <summary>
        /// We should be able to call Resize via the IMutableSublist interface.
        /// </summary>
        [TestMethod]
        public void TestResize_Mutable()
        {
            IMutableSublist<List<int>, int> sublist = new Sublist<List<int>, int>(new List<int>());
            sublist.Resize(sublist.Count, true);
        }

        /// <summary>
        /// We should be able to call Resize via the IExpandableSublist interface.
        /// </summary>
        [TestMethod]
        public void TestResize_Expandable()
        {
            IExpandableSublist<List<int>, int> sublist = new Sublist<List<int>, int>(new List<int>());
            sublist.Resize(sublist.Count, true);
        }

        #endregion

        #region Indexer

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Getter_NegativeIndex_Throws()
        {
            var list = new List<int>().ToSublist();
            int value = list[-1];
        }

        /// <summary>
        /// An exception should be thrown if the index is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Getter_IndexEqualsCount_Throws()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist(0, 2);
            int value = list[list.Count];
        }

        /// <summary>
        /// The indexer should add the given index to the sublist's offset to
        /// grab the value from the underlying list.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_GetsValueAtIndexPlusOffset()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist(1);
            int value = list[1];
            Assert.AreEqual(3, value);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Setter_NegativeIndex_Throws()
        {
            var list = new List<int>().ToSublist();
            list[-1] = 0;
        }

        /// <summary>
        /// An exception should be thrown if the index is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Setter_IndexEqualsCount_Throws()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist(0, 2);
            list[list.Count] = 0;
        }

        /// <summary>
        /// The indexer should add the given index to the sublist's offset to
        /// grab the value from the underlying list.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Setter_SetsValueAtIndexPlusOffset()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist(1);
            list[1] = 0;
            int[] expected = { 1, 2, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()), "The item was not set as expected.");
        }

        #endregion

        #region GetEnumerator

        /// <summary>
        /// Enumeration should move through the items in the sublist.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_GetsItemsInReverse()
        {
            var view = new int[] { 0, 1, 2, 3 }.ToSublist(1, 2);
            var list = new List<int>();
            foreach (int item in view)
            {
                list.Add(item);
            }
            int[] expected = { 1, 2 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.ToSublist()), "The correct values were not enumerated.");
        }

        /// <summary>
        /// Enumeration should move through the items in the sublist.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_Implicit_StillEnumerates()
        {
            IEnumerable view = new int[] { 0, 1, 2, 3 }.ToSublist(1, 2);
            var list = new List<object>();
            foreach (int item in view)
            {
                list.Add(item);
            }
            object[] expected = { 1, 2 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.ToSublist()), "The correct values were not enumerated.");
        }

        #endregion
    }
}
