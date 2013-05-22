using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ReadOnlyList class.
    /// </summary>
    [TestClass]
    public class ReadOnlyListTester
    {
        #region Extensions

        /// <summary>
        /// If we call ReadOnly on an array, it should return a ReadOnlyList wrapping it.
        /// </summary>
        [TestMethod]
        public void TestReadOnly_Array_Wraps()
        {
            var array = new int[0];
            var readOnly = array.ReadOnly();
            Assert.AreSame(array, readOnly.List, "The underlying list was not set.");
        }

        /// <summary>
        /// If we call ReadOnly on a list, it should return a ReadOnlyList wrapping it.
        /// </summary>
        [TestMethod]
        public void TestReadOnly_List_Wraps()
        {
            var list = new List<int>();
            var readOnly = list.ReadOnly();
            Assert.AreSame(list, readOnly.List, "The underlying list was not set.");
        }

        /// <summary>
        /// If we call ReadOnly on a Collection, it should return a ReadOnlyList wrapping it.
        /// </summary>
        [TestMethod]
        public void TestReadOnly_Collection_WrapsCollection()
        {
            var collection = new Collection<int>();
            var readOnly = collection.ReadOnly();
            Assert.AreSame(collection, readOnly.List, "The underlying list was not set.");
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
            new ReadOnlyList<List<int>, int>(list);
        }

        /// <summary>
        /// The list passed to the constructor should be accessible from the List property,
        /// and the Count and IsReadOnly properties should be the same.
        /// </summary>
        [TestMethod]
        public void TestCtor_SetsList_Count_IsReadOnly_Properties()
        {
            List<int> list = new List<int>();
            var view = new ReadOnlyList<List<int>, int>(list);
            Assert.AreSame(list, view.List, "The List value was not set.");
            Assert.AreEqual(list.Count, view.Count, "The Count value was wrong.");
            Assert.IsTrue(view.IsReadOnly, "The IsReadOnly value was wrong.");
        }

        #endregion

        #region IndexOf

        /// <summary>
        /// IndexOf should return negative one if the given value isn't found.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_ValueNotFound_ReturnsNegativeOne()
        {
            var list = new List<int>() { 0, 1, 2 }.ReadOnly();
            int index = list.IndexOf(3);
            Assert.AreEqual(-1, index);
        }

        /// <summary>
        /// IndexOf should return the last index if the last item is a match.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_LastValue_ReturnsZero()
        {
            var list = new List<int>() { 0, 1, 2 }.ReadOnly();
            int index = list.IndexOf(2);
            Assert.AreEqual(list.Count - 1, index);
        }

        /// <summary>
        /// IndexOf should return 0 if the first item is a match.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_FirstValue_ReturnsCountMinusOne()
        {
            var list = new List<int>() { 0, 1, 2 }.ReadOnly();
            int index = list.IndexOf(0);
            Assert.AreEqual(0, index);
        }

        /// <summary>
        /// IndexOf should return the index of the expected value.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_MiddleValue_ReturnsExpectedIndex()
        {
            var list = new List<int>() { 0, 1, 2, 3 }.ReadOnly();
            int index = list.IndexOf(1);
            Assert.AreEqual(1, index);
        }

        /// <summary>
        /// IndexOf should return the index of the first occurrence of the value in the
        /// underlying list.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_MultipleOccurrences_ReturnsLastIndex()
        {
            var list = new List<int>() { 0, 1, 0, 1 }.ReadOnly();
            int index = list.IndexOf(1);
            Assert.AreEqual(1, index);
        }

        #endregion

        #region Insert

        /// <summary>
        /// An exception should be thrown if trying to insert.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestInsert_Throws()
        {
            var list = new List<int>() { 1, 2, 3 }.ReadOnly();
            list.Insert(-1, 4);
        }

        #endregion

        #region RemoveAt

        /// <summary>
        /// An exception should be thrown if trying to remove.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestRemoveAt_Throws()
        {
            var list = new List<int>() { 1, 2, 3 }.ReadOnly();
            list.RemoveAt(-1);
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
            var list = new List<int>() { 1, 2, 3, 4, }.ReadOnly();
            int value = list[-1];
        }

        /// <summary>
        /// Calling the setter should throw an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestIndexer_Setter_Throws()
        {
            var list = new List<int>() { 1, 2, 3, 4, }.ReadOnly();
            list[-1] = 0;
        }

        /// <summary>
        /// A negative index on the getter should throw an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Getter_IndexTooBig_Throws()
        {
            var list = new List<int>() { 1, 2, 3, }.ReadOnly();
            int value = list[3];
        }

        /// <summary>
        /// We should get the expected item at the given item.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_FirstIndex_GetsFirstItem()
        {
            var list = new List<int>() { 1, 2, 3, 4, }.ReadOnly();
            Assert.AreEqual(1, list[0], "The wrong item was selected.");
        }

        /// <summary>
        /// We should get the expected item at the given item.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_LastIndex_GetsLastItem()
        {
            var list = new List<int>() { 1, 2, 3, 4, }.ReadOnly();
            Assert.AreEqual(4, list[3], "The wrong item was selected.");
        }

        /// <summary>
        /// We should get the expected item at the given item.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_MiddleIndex_GetsMiddleItem()
        {
            var list = new List<int>() { 1, 2, 3, 4, }.ReadOnly();
            Assert.AreEqual(3, list[2], "The wrong item was selected.");
        }

        #endregion

        #region Add

        /// <summary>
        /// Calling add should throw an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestAdd_Throws()
        {
            var list = new List<int>() { 1, 2, 3 }.ReadOnly();
            list.Add(0);
        }

        #endregion

        #region Clear

        /// <summary>
        /// Clear should throw an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestClear_Throws()
        {
            var list = new List<int>() { 1, 2, 3 }.ReadOnly();
            list.Clear();
        }

        #endregion

        #region Contains

        /// <summary>
        /// Contains should return true if the item is in the list.
        /// </summary>
        [TestMethod]
        public void TestContains_ValueExists_ReturnsTrue()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ReadOnly();
            bool result = list.Contains(3);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Contains should return false if the item is not in the list.
        /// </summary>
        [TestMethod]
        public void TestContains_ValueDoesNotExists_ReturnsFalse()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ReadOnly();
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
            var list = new int[0].ReadOnly();
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
            var list = new int[0].ReadOnly();
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
            var list = new int[1].ReadOnly();
            int[] array = new int[1];
            int arrayIndex = 1;
            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copying should work.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_Copies()
        {
            var list = new int[] { 0, 1, 2, 3, 4, }.ReadOnly();
            int[] array = new int[5];
            int arrayIndex = 0;
            list.CopyTo(array, arrayIndex);
            int[] expected = { 0, 1, 2, 3, 4 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), array.ToSublist()), "The items were not copied as expected.");
        }

        /// <summary>
        /// Copying should work, starting at the arrayIndex.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_Copies_StartingAtArrayIndex()
        {
            var list = new int[] { 1, 2, 3, 4, 5 }.ReadOnly();
            int[] array = new int[9];
            int arrayIndex = 2;
            list.CopyTo(array, arrayIndex);
            int[] expected = { 0, 0, 1, 2, 3, 4, 5, 0, 0, };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), array.ToSublist()), "The items were not copied as expected.");
        }

        #endregion

        #region Remove

        /// <summary>
        /// Remove should throw an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestRemove_Throws()
        {
            var list = new List<int>() { 0, 1, 2 }.ReadOnly();
            list.Remove(3);
        }

        #endregion

        #region Implicit Conversion

        /// <summary>
        /// We should be able to create a ReadOnlyList implicitly from any ISublist.
        /// </summary>
        [TestMethod]
        public void TestImplicitConversion()
        {
            int[] list = { 1, 2, 3 };
            ReadOnlyList<int[], int> view = list;
            Assert.AreSame(list, view.List);
        }

        #endregion

        #region GetEnumerable

        /// <summary>
        /// Enumeration should move through the items.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_GetsItemsInReverse()
        {
            var view = new int[] { 0, 1, 2, 3 }.ReadOnly();
            var list = new List<int>();
            foreach (int item in view)
            {
                list.Add(item);
            }
            int[] expected = { 0, 1, 2, 3 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list.ToSublist()), "The values were not enumerated.");
        }

        /// <summary>
        /// Enumeration should move through the items.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_Implicit_StillEnumerates()
        {
            IEnumerable view = new int[] { 0, 1, 2, 3 }.ReadOnly();
            var list = new List<object>();
            foreach (int item in view)
            {
                list.Add(item);
            }
            object[] expected = { 0, 1, 2, 3 };
            Assert.IsTrue(Sublist.Equals(expected.ToSublist(), list.ToSublist()), "The values were not enumerated in reverse.");
        }

        #endregion
    }
}
