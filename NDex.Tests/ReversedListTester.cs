using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ReversedList class.
    /// </summary>
    [TestClass]
    public class ReversedListTester
    {
        #region Extension Methods

        /// <summary>
        /// If the IReadOnlySublist is null, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReversed_Readonly_NullSublist_Throws()
        {
            IReadOnlySublist<StringAdapter, char> substring = null;
            substring.Reversed();
        }

        /// <summary>
        /// If the IMutableSublist is null, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReversed_Mutable_NullSublist_Throws()
        {
            IMutableSublist<int[], int> sublist = null;
            sublist.Reversed();
        }

        /// <summary>
        /// If the IExpandableSublist is null, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReversed_Expandable_NullSublist_Throws()
        {
            IExpandableSublist<List<int>, int> sublist = null;
            sublist.Reversed();
        }

        /// <summary>
        /// If the Sublist is null, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReversed_NullSublist_Throws()
        {
            Sublist<List<int>, int> sublist = null;
            sublist.Reversed();
        }

        /// <summary>
        /// If we reverse a read-only sublist, we should see the items backwards.
        /// </summary>
        [TestMethod]
        public void TestReversed_Readonly_Reversed()
        {
            IReadOnlySublist<StringAdapter, char> substring = "Hello".ToSubstring(1, 3); // ell
            var reversed = substring.Reversed();
            Assert.AreEqual(substring[0], reversed[2], "The first item was wrong.");
            Assert.AreEqual(substring[1], reversed[1], "The second item was wrong.");
            Assert.AreEqual(substring[2], reversed[0], "The third item was wrong.");
        }

        /// <summary>
        /// If we reverse a mutable sublist, we should see the items backwards.
        /// </summary>
        [TestMethod]
        public void TestReversed_Mutable_Reversed()
        {
            IMutableSublist<int[], int> substring = new int[] { 1, 2, 3, 4, 5 }.ToSublist(1, 3);
            var reversed = substring.Reversed();
            Assert.AreEqual(substring[0], reversed[2], "The first item was wrong.");
            Assert.AreEqual(substring[1], reversed[1], "The second item was wrong.");
            Assert.AreEqual(substring[2], reversed[0], "The third item was wrong.");
        }

        /// <summary>
        /// If we reverse a expandable sublist, we should see the items backwards.
        /// </summary>
        [TestMethod]
        public void TestReversed_Expandable_Reversed()
        {
            IExpandableSublist<List<int>, int> substring = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(1, 3);
            var reversed = substring.Reversed();
            Assert.AreEqual(substring[0], reversed[2], "The first item was wrong.");
            Assert.AreEqual(substring[1], reversed[1], "The second item was wrong.");
            Assert.AreEqual(substring[2], reversed[0], "The third item was wrong.");
        }

        /// <summary>
        /// If we reverse a sublist, we should see the items backwards.
        /// </summary>
        [TestMethod]
        public void TestReversed_Reversed()
        {
            Sublist<List<int>, int> substring = new Sublist<List<int>,int>(new List<int>() { 1, 2, 3, 4, 5 }, 1, 3);
            var reversed = substring.Reversed();
            Assert.AreEqual(substring[0], reversed[2], "The first item was wrong.");
            Assert.AreEqual(substring[1], reversed[1], "The second item was wrong.");
            Assert.AreEqual(substring[2], reversed[0], "The third item was wrong.");
        }

        /// <summary>
        /// Reversing a reversed list that is null will result in an error.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReversed_OnReversed_NullList_Throws()
        {
            ReversedList<int[], int> reversed = null;
            reversed.Reversed();
        }

        /// <summary>
        /// Reversing a reversed list simply returned the original list.
        /// </summary>
        [TestMethod]
        public void TestReversed_OnReversed_ReturnsUnderlyingList()
        {
            var original = new int[] { 1, 2, 3, 4, 5 };
            var reversed = original.Reversed();
            var rereversed = reversed.Reversed();
            Assert.AreSame(original, rereversed, "The original was not extracted.");
        }

        /// <summary>
        /// If we try to reverse a null reversed sublist, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReversed_OnReversedSublist_ReadOnly_NullSublist_Throws()
        {
            IReadOnlySublist<ReversedList<List<int>, int>, int> original = null;
            original.Reversed();
        }

        /// <summary>
        /// If a reversed, read-only sublist is reversed, the original sublist should be returned.
        /// </summary>
        [TestMethod]
        public void TestReversed_OnReversedSublist_ReadOnly_ReturnsOriginal()
        {
            var original = "Hello".ToSubstring(1, 3);
            var reversed = original.Reversed();
            var rereversed = reversed.Reversed();
            Assert.IsTrue(original.IsEqualTo(rereversed), "The original and re-reversed were not the same.");
        }

        /// <summary>
        /// If we try to reverse a null reversed sublist, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReversed_OnReversedSublist_Mutable_NullSublist_Throws()
        {
            IMutableSublist<ReversedList<List<int>, int>, int> original = null;
            original.Reversed();
        }

        /// <summary>
        /// If a reversed, mutable sublist is reversed, the original sublist should be returned.
        /// </summary>
        [TestMethod]
        public void TestReversed_OnReversedSublist_Mutable_ReturnsOriginal()
        {
            var original = new int[] { 1, 2, 3, 4, 5 }.ToSublist(1, 3);
            var reversed = original.Reversed();
            var rereversed = reversed.Reversed();
            Assert.IsTrue(original.IsEqualTo(rereversed), "The original and re-reversed were not the same.");
        }

        /// <summary>
        /// If we try to reverse a null reversed sublist, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReversed_OnReversedSublist_Expandable_NullSublist_Throws()
        {
            IExpandableSublist<ReversedList<List<int>, int>, int> original = null;
            original.Reversed();
        }

        /// <summary>
        /// If a reversed, expandable sublist is reversed, the original sublist should be returned.
        /// </summary>
        [TestMethod]
        public void TestReversed_OnReversedSublist_Expandable_ReturnsOriginal()
        {
            var original = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(1, 3);
            var reversed = original.Reversed();
            var rereversed = reversed.Reversed();
            Assert.IsTrue(original.IsEqualTo(rereversed), "The original and re-reversed were not the same.");
        }

        /// <summary>
        /// If we try to reverse a null reversed sublist, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReversed_OnReversedSublist_NullSublist_Throws()
        {
            Sublist<ReversedList<List<int>, int>, int> original = null;
            original.Reversed();
        }

        /// <summary>
        /// If a reversed, expandable sublist is reversed, the original sublist should be returned.
        /// </summary>
        [TestMethod]
        public void TestReversed_OnReversedSublist_ReturnsOriginal()
        {
            var original = new Sublist<List<int>, int>(new List<int>() { 1, 2, 3, 4, 5 }, 1, 3);
            var reversed = original.Reversed();
            var rereversed = reversed.Reversed();
            Assert.IsTrue(original.IsEqualTo(rereversed), "The original and re-reversed were not the same.");
        }

        /// <summary>
        /// If we try to reverse a null sublist, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReversed_OnSublist_NullSublist_Throws()
        {
            IExpandableSublist<List<int>, int> sublist = null;
            sublist.Reversed();
        }

        /// <summary>
        /// If a reversed, sublist is reversed, the original sublist should be returned.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReversed_OnReversedSublist_NullSublist_ReturnsOriginal()
        {
            IExpandableSublist<ReversedList<List<int>, int>, int> reversed = null;
            reversed.Reversed();
        }

        /// <summary>
        /// If we call Reversed on a collection, it should wrap the collection.
        /// </summary>
        [TestMethod]
        public void TestReversed_Collection_Wraps()
        {
            Collection<int> collection = new Collection<int>();
            var reversed = collection.Reversed();
            Assert.AreSame(collection, reversed.List, "The collection was not wrapped.");
        }

        #endregion

        #region Ctor, List, Count & IsReadOnly Properties

        /// <summary>
        /// The list passed to the constructor must not be null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_NullList_Throws()
        {
            List<int> list = null;
            new ReversedList<List<int>, int>(list);
        }

        /// <summary>
        /// The list passed to the constructor should be accessible from the List property,
        /// and the Count and IsReadOnly properties should be the same.
        /// </summary>
        [TestMethod]
        public void TestCtor_SetsList_Count_IsReadOnly_Properties()
        {
            List<int> list = new List<int>();
            var view = new ReversedList<List<int>, int>(list);
            Assert.AreSame(list, view.List, "The List value was not set.");
            Assert.AreEqual(list.Count, view.Count, "The Count value was wrong.");
            Assert.AreEqual(((IList<int>)list).IsReadOnly, ((IList<int>)view).IsReadOnly, "The IsReadOnly value was wrong.");
        }

        #endregion

        #region BaseIndex

        /// <summary>
        /// The underlying index equivalent to zero is the index of the last item.
        /// </summary>
        [TestMethod]
        public void TestBaseIndex_Zero_ReturnsListCountMinusOne()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.Reversed();
            int index = list.BaseIndex(0);
            Assert.AreEqual(list.Count - 1, index, "The wrong index was returned.");
        }

        /// <summary>
        /// The underlying index equivilent to the index of the last item is zero.
        /// </summary>
        [TestMethod]
        public void TestBaseIndex_CountMinusOne_ReturnsZero()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.Reversed();
            int index = list.BaseIndex(list.Count - 1);
            Assert.AreEqual(0, index, "The wrong index was returned.");
        }

        /// <summary>
        /// The index returned should find the same value.
        /// </summary>
        [TestMethod]
        public void TestBaseIndex_IndexInMiddle_ReturnsUnderlyingIndex()
        {
            var list = new List<int>() { 1, 2, 3, 4 }.Reversed();
            Assert.AreEqual(list.List.IndexOf(3), list.BaseIndex(list.IndexOf(3)), "The wrong index was returned.");
        }

        /// <summary>
        /// The index returned should find the same value.
        /// </summary>
        [TestMethod]
        public void TestBaseIndex_IndexEqualsCount_ReturnsNegativeOne()
        {
            var list = new List<int>() { 1, 2, 3, 4 }.Reversed();
            int index = list.BaseIndex(list.Count);
            Assert.AreEqual(-1, index, "The wrong index was returned.");
        }

        /// <summary>
        /// The index returned should find the same value.
        /// </summary>
        [TestMethod]
        public void TestBaseIndex_IndexEqualsNegativeOne_ReturnsCount()
        {
            var list = new List<int>() { 1, 2, 3, 4 }.Reversed();
            int index = list.BaseIndex(-1);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
        }

        #endregion

        #region IndexOf

        /// <summary>
        /// IndexOf should return negative one if the given value isn't found.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_ValueNotFound_ReturnsNegativeOne()
        {
            var list = new List<int>() { 0, 1, 2 }.Reversed();
            int index = list.IndexOf(3);
            Assert.AreEqual(-1, index);
        }

        /// <summary>
        /// IndexOf should return 0 if the last item is a match.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_LastValue_ReturnsZero()
        {
            var list = new List<int>() { 0, 1, 2 }.Reversed();
            int index = list.IndexOf(2);
            Assert.AreEqual(0, index);
        }

        /// <summary>
        /// IndexOf should return list.Count - 1 if the first item is a match.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_FirstValue_ReturnsCountMinusOne()
        {
            var list = new List<int>() { 0, 1, 2 }.Reversed();
            int index = list.IndexOf(0);
            Assert.AreEqual(list.Count - 1, index);
        }

        /// <summary>
        /// IndexOf should return the index of the expected value, as if the list were reversed.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_MiddleValue_ReturnsExpectedIndex()
        {
            var list = new List<int>() { 0, 1, 2, 3 }.Reversed();
            int index = list.IndexOf(1);
            Assert.AreEqual(2, index);
        }

        /// <summary>
        /// IndexOf should return the index of the last occurrence of the value in the
        /// underlying list.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_MultipleOccurrences_ReturnsLastIndex()
        {
            var list = new List<int>() { 0, 1, 0, 1 }.Reversed();
            int index = list.IndexOf(0);
            Assert.AreEqual(1, index);
        }

        #endregion

        #region Insert

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInsert_NegativeIndex_Throws()
        {
            var list = new List<int>() { 1, 2, 3 }.Reversed();
            list.Insert(-1, 4);
        }

        /// <summary>
        /// An exception should be thrown if the index is larger than list.Count.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInsert_IndexTooBig_Throws()
        {
            var list = new List<int>() { 1, 2, 3 }.Reversed();
            list.Insert(4, 0);
        }

        /// <summary>
        /// Adding an item to the back of a reversed list should add it to the front of the wrapped list.
        /// </summary>
        [TestMethod]
        public void TestInsert_IndexAtEnd_InsertsInFront()
        {
            var list = new List<int>() { 1, 2, 3 }.Reversed();
            list.Insert(3, 0);
            int[] expected = { 0, 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()));
        }

        /// <summary>
        /// Adding an item to the front of a reversed list should add it to the back of the wrapped list.
        /// </summary>
        [TestMethod]
        public void TestInsert_IndexAtBeginning_InsertsInBack()
        {
            var list = new List<int>() { 1, 2, 3 }.Reversed();
            list.Insert(0, 4);
            int[] expected = { 1, 2, 3, 4 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()));
        }

        /// <summary>
        /// Adding an item to the middle of a reversed list should add it to the middle of the wrapped list.
        /// </summary>
        [TestMethod]
        public void TestInsert_IndexInMiddle_InsertsInMiddle()
        {
            var list = new List<int>() { 1, 2, 4, 5 }.Reversed();
            list.Insert(2, 3);
            int[] expected = { 1, 2, 3, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()));
        }

        #endregion

        #region RemoveAt

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRemoveAt_NegativeIndex_Throws()
        {
            var list = new List<int>() { 1, 2, 3 }.Reversed();
            list.RemoveAt(-1);
        }

        /// <summary>
        /// An exception should be thrown if the index is larger than list.Count.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRemoveAt_IndexTooBig_Throws()
        {
            var list = new List<int>() { 1, 2, 3 }.Reversed();
            list.RemoveAt(3);
        }

        /// <summary>
        /// Removing an item from the back of a reversed list should remove it from the front of the wrapped list.
        /// </summary>
        [TestMethod]
        public void TestRemoveAt_IndexAtEnd_RemovesFromFront()
        {
            var list = new List<int>() { 1, 2, 3 }.Reversed();
            list.RemoveAt(2);
            int[] expected = { 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()));
        }

        /// <summary>
        /// Removing an item from the front of a reversed list should remove it from the back of the wrapped list.
        /// </summary>
        [TestMethod]
        public void TestRemoveAt_IndexAtBeginning_RemovesFromBack()
        {
            var list = new List<int>() { 1, 2, 3 }.Reversed();
            list.RemoveAt(0);
            int[] expected = { 1, 2 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()));
        }

        /// <summary>
        /// Removing an item from the middle of a reversed list should remove it from the middle of the wrapped list.
        /// </summary>
        [TestMethod]
        public void TestRemoveAt_IndexInMiddle_RemovesFromMiddle()
        {
            var list = new List<int>() { 1, 2, 3, 4 }.Reversed();
            list.RemoveAt(2);
            int[] expected = { 1, 3, 4 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()));
        }

        #endregion

        #region Indexer

        /// <summary>
        /// A negative index on the getter should throw an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Getter_NegativeIndex_Throws()
        {
            var list = new List<int>() { 1, 2, 3, 4, }.Reversed();
            int value = list[-1];
        }

        /// <summary>
        /// A negative index on the setter should throw an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Setter_NegativeIndex_Throws()
        {
            var list = new List<int>() { 1, 2, 3, 4, }.Reversed();
            list[-1] = 0;
        }

        /// <summary>
        /// A negative index on the getter should throw an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Getter_IndexTooBig_Throws()
        {
            var list = new List<int>() { 1, 2, 3, }.Reversed();
            int value = list[3];
        }

        /// <summary>
        /// A negative index on the setter should throw an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Setter_IndexTooBig_Throws()
        {
            var list = new List<int>() { 1, 2, 3, }.Reversed();
            list[3] = 0;
        }

        /// <summary>
        /// If we get or set the first item, we're really working with the last item
        /// in the underlying list.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_Setter_FirstIndex_ReallyLastItem()
        {
            var list = new List<int>() { 1, 2, 3, 4, }.Reversed();
            Assert.AreEqual(4, list[0], "The wrong item was selected.");
            list[0] = 5;
            int[] expected = { 1, 2, 3, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()), "The item was not set as expected.");
        }

        /// <summary>
        /// If we get or set the last item, we're really working with the first item
        /// in the underlying list.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_Setter_LastIndex_ReallyFirstItem()
        {
            var list = new List<int>() { 1, 2, 3, 4, }.Reversed();
            Assert.AreEqual(1, list[3], "The wrong item was selected.");
            list[3] = 0;
            int[] expected = { 0, 2, 3, 4 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()), "The item was not set as expected.");
        }

        /// <summary>
        /// If we get or set the middle item, we're really working with the middle item
        /// in the underlying list.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_Setter_MiddleIndex_ReallyMiddleItem()
        {
            var list = new List<int>() { 1, 2, 3, 4, }.Reversed();
            Assert.AreEqual(2, list[2], "The wrong item was selected.");
            list[2] = 0;
            int[] expected = { 1, 0, 3, 4 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()), "The item was not set as expected.");
        }

        #endregion

        #region Add

        /// <summary>
        /// Adding to a ReversedList should insert the item at the first index.
        /// </summary>
        [TestMethod]
        public void TestAdd_InsertsAtBeginning()
        {
            var list = new List<int>() { 1, 2, 3 }.Reversed();
            list.Add(0);
            int[] expected = { 0, 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()), "The item was not added as expected.");
        }

        #endregion

        #region Clear

        /// <summary>
        /// Clear should remove all items from the underlying list.
        /// </summary>
        [TestMethod]
        public void TestClear_RemovesAllItems()
        {
            var list = new List<int>() { 1, 2, 3 }.Reversed();
            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        #endregion

        #region Contains

        /// <summary>
        /// Contains should return true if the item is in the list.
        /// </summary>
        [TestMethod]
        public void TestContains_ValueExists_ReturnsTrue()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.Reversed();
            bool result = list.Contains(3);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Contains should return false if the item is not in the list.
        /// </summary>
        [TestMethod]
        public void TestContains_ValueDoesNotExists_ReturnsFalse()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.Reversed();
            bool result = list.Contains(6);
            Assert.IsFalse(result);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// An exception should be thrown if the array is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyTo_NullArray_Throws()
        {
            var list = new int[0].Reversed();
            int[] array = null;
            int arrayIndex = 0;
            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// An exception should be thrown if the arrayIndex is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCopyTo_NegativeArrayIndex_Throws()
        {
            var list = new int[0].Reversed();
            int[] array = new int[0];
            int arrayIndex = -1;
            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// An exception should be thrown if the space remaining in the array is less than the size of the list.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCopyTo_NotEnoughSpaceRemaining_Throws()
        {
            var list = new int[1].Reversed();
            int[] array = new int[1];
            int arrayIndex = 1;
            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copying should be done in reverse.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_CopiesInReverse()
        {
            var list = new int[] { 0, 1, 2, 3, 4, }.Reversed();
            int[] array = new int[5];
            int arrayIndex = 0;
            list.CopyTo(array, arrayIndex);
            int[] expected = { 4, 3, 2, 1, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(array.ToSublist()), "The items were not copied as expected.");
        }

        /// <summary>
        /// Copying should be done in reverse, starting at the arrayIndex.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_CopiesInReverse_StartingAtArrayIndex()
        {
            var list = new int[] { 1, 2, 3, 4, 5 }.Reversed();
            int[] array = new int[9];
            int arrayIndex = 2;
            list.CopyTo(array, arrayIndex);
            int[] expected = { 0, 0, 5, 4, 3, 2, 1, 0, 0, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(array.ToSublist()), "The items were not copied as expected.");
        }

        #endregion

        #region Remove

        /// <summary>
        /// Remove should return false if the given value isn't found.
        /// </summary>
        [TestMethod]
        public void TestRemove_ValueNotFound_ReturnsFalse()
        {
            var list = new List<int>() { 0, 1, 2 }.Reversed();
            bool result = list.Remove(3);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Remove should remove an item if it is a match.
        /// </summary>
        [TestMethod]
        public void TestRemove_FindsValue_ReturnsTrue()
        {
            var list = new List<int>() { 0, 1, 2 }.Reversed();
            bool result = list.Remove(0);
            Assert.IsTrue(result, "The value was not found.");
            int[] expected = { 1, 2, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()), "The item was not removed from the list.");
        }

        /// <summary>
        /// Remove should remove the last occurrence of the value.
        /// </summary>
        [TestMethod]
        public void TestRemove_MultipleOccurrences_RemovedLastOccurrence()
        {
            var list = new List<int>() { 0, 1, 0, 1 }.Reversed();
            bool result = list.Remove(0);
            Assert.IsTrue(result, "The value was not found.");
            int[] expected = { 0, 1, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()), "The item was not removed from the list.");
        }

        #endregion

        #region Implicit Conversion

        /// <summary>
        /// We should be able to create a ReversedList implicitly from any ISublist.
        /// </summary>
        [TestMethod]
        public void TestImplicitConversion()
        {
            int[] list = { 1, 2, 3 };
            ReversedList<int[], int> view = list;
            Assert.AreSame(list, view.List);
        }

        #endregion

        #region GetEnumerator

        /// <summary>
        /// Enumeration should move through the items in reverse.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_GetsItemsInReverse()
        {
            var view = new int[] { 0, 1, 2, 3 }.Reversed();
            var list = new List<int>();
            foreach (int item in view)
            {
                list.Add(item);
            }
            int[] expected = { 3, 2, 1, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.ToSublist()), "The values were not enumerated in reverse.");
        }

        /// <summary>
        /// Enumeration should move through the items in reverse.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_Implicit_StillEnumerates()
        {
            IEnumerable view = new int[] { 0, 1, 2, 3 }.Reversed();
            var list = new List<object>();
            foreach (int item in view)
            {
                list.Add(item);
            }
            object[] expected = { 3, 2, 1, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.ToSublist()), "The values were not enumerated in reverse.");
        }

        /// <summary>
        /// An exception should be thrown if the list is edited during enumeration.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetEnumerable_ModifyDuringEnumeration_Throws()
        {
            var view = new List<int> { 0, 1, 2, 3 }.Reversed();
            var list = new List<int>();
            foreach (int item in view)
            {
                list.Add(item);
                view.Add(4); // modifies the list
            }
        }

        #endregion
    }
}
