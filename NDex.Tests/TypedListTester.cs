using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
#if NET45
    /// <summary>
    /// Tests the TypedList class.
    /// </summary>
    [TestClass]
    public class TypedListTester
    {
#region Typed
        /// <summary>
        /// We can make a typed interface around an array.
        /// </summary>
        [TestMethod]
        public void TestTyped_Array_WrapsArray()
        {
            Array array = Array.CreateInstance(typeof(int), 10);
            var typed = array.Typed<int>();
            Assert.AreSame(array, typed.List, "The array was not wrapped.");
        }

        /// <summary>
        /// We can make a typed interface around an ArrayList.
        /// </summary>
        [TestMethod]
        public void TestTyped_ArrayList_WrapsList()
        {
            ArrayList list = new ArrayList();
            var typed = list.Typed<int>();
            Assert.AreSame(list, typed.List, "The list was not wrapped.");
        }
#endregion

#region Ctor

    /// <summary>
    /// An exception should be thrown if the list to wrap is null.
    /// </summary>
    [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_NullList_Throws()
        {
            ArrayList list = null;
            new TypedList<ArrayList, int>(list);
        }

        /// <summary>
        /// Passing a list to the constructor should create a new TypedList
        /// with the list stored as a backing field.
        /// </summary>
        [TestMethod]
        public void TestCtor_SetsUnderlyingList()
        {
            ArrayList arrayList = new ArrayList() { 1, 2, 3, };
            TypedList<ArrayList, int> list = new TypedList<ArrayList, int>(arrayList);
            Assert.AreSame(arrayList, list.List, "The backing field was not set.");
            Assert.AreEqual(arrayList.Count, list.Count, "The counts did not match.");
            Assert.AreEqual(arrayList.IsReadOnly, ((ICollection<int>)list).IsReadOnly, "The read-only property didn't match.");
        }

#endregion

#region Indexer

        /// <summary>
        /// If the item at the index is of the correct type, it should cast without problem.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void TestIndexer_Getter_ItemWrongType_Throws()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { "1" });
            int value = list[0];
        }

        /// <summary>
        /// If the item at the index is of the correct type, it should cast without problem.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_ItemRightType()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1 });
            Assert.AreEqual(1, list[0], "The wrong item was selected.");
        }

        /// <summary>
        /// If the item at the index is of the correct type, it should cast without problem.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Setter_SetsValue()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1 });
            list[0] = 0;
            int[] expected = { 0 };
            Assert.AreEqual(0, list.List[0]);
        }

#endregion

#region Add

        /// <summary>
        /// Calling add should add the item to the end of the list.
        /// </summary>
        [TestMethod]
        public void TestAdd_AddsToEnd()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            list.Add(4);
            Assert.AreEqual(4, list.Count, "The count wasn't increased.");
            int[] expected = { 1, 2, 3, 4 };
            int[] actual = toTypedArray(list.List);
            Assert.IsTrue(expected.ToSublist().IsEqualTo(actual.ToSublist()), "The item wa not added as expected.");
        }

#endregion

#region Clear

        /// <summary>
        /// Clear should remove every item.
        /// </summary>
        [TestMethod]
        public void TestClear_Throws()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            Assert.AreEqual(3, list.Count, "The list had the wrong initial count.");
            list.Clear();
            Assert.AreEqual(0, list.Count, "The count was not zero after clearing");
        }

#endregion

#region Contains

        /// <summary>
        /// Contains should return true if the item is in the list.
        /// </summary>
        [TestMethod]
        public void TestContains_ValueExists_ReturnsTrue()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3, 4, 5 });
            bool result = list.Contains(3);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Contains should return false if the item is not in the list.
        /// </summary>
        [TestMethod]
        public void TestContains_ValueDoesNotExists_ReturnsFalse()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3, 4, 5 });
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
            var list = new TypedList<ArrayList, int>(new ArrayList());
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
            var list = new TypedList<ArrayList, int>(new ArrayList());
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
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1 });
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
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3, 4, 5 });
            int[] array = new int[5];
            int arrayIndex = 0;
            list.CopyTo(array, arrayIndex);
            int[] expected = { 1, 2, 3, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(array.ToSublist()), "The items were not copied as expected.");
        }

        /// <summary>
        /// Copying should work, starting at the arrayIndex.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_Copies_StartingAtArrayIndex()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3, 4, 5 });
            int[] array = new int[9];
            int arrayIndex = 2;
            list.CopyTo(array, arrayIndex);
            int[] expected = { 0, 0, 1, 2, 3, 4, 5, 0, 0, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(array.ToSublist()), "The items were not copied as expected.");
        }

        /// <summary>
        /// Copying should fail if an item has an incompatible type.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void TestCopyTo_IncompatibleType_Throws()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, "3", 4, 5 });
            int[] array = new int[5];
            int arrayIndex = 0;
            list.CopyTo(array, arrayIndex);
        }

#endregion

#region IndexOf

        /// <summary>
        /// IndexOf should return negative one if the given value isn't found.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_ValueNotFound_ReturnsNegativeOne()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 0, 1, 2 });
            int index = list.IndexOf(3);
            Assert.AreEqual(-1, index);
        }

        /// <summary>
        /// IndexOf should return the last index if the last item is a match.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_LastValue_ReturnsZero()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            int index = list.IndexOf(3);
            Assert.AreEqual(list.Count - 1, index);
        }

        /// <summary>
        /// IndexOf should return 0 if the first item is a match.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_FirstValue_ReturnsCountMinusOne()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            int index = list.IndexOf(1);
            Assert.AreEqual(0, index);
        }

        /// <summary>
        /// IndexOf should return the index of the expected value.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_MiddleValue_ReturnsExpectedIndex()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            int index = list.IndexOf(2);
            Assert.AreEqual(1, index);
        }

        /// <summary>
        /// IndexOf should return the index of the first occurrence of the value in the
        /// underlying list.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_MultipleOccurrences_ReturnsLastIndex()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 0, 1, 2, 1, 3 });
            int index = list.IndexOf(1);
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
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            list.Insert(-1, 4);
        }

        /// <summary>
        /// An exception should be thrown if the index is larger than list.Count.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInsert_IndexTooBig_Throws()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            list.Insert(4, 0);
        }

        /// <summary>
        /// Adding an item to the back of a reversed list should add it to the front of the wrapped list.
        /// </summary>
        [TestMethod]
        public void TestInsert_IndexAtEnd_InsertsInFront()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            list.Insert(3, 4);
            int[] expected = { 1, 2, 3, 4 };
            int[] actual = toTypedArray(list.List);
            Assert.IsTrue(expected.ToSublist().IsEqualTo(actual.ToSublist()), "The item was not inserted as expected.");
        }

        /// <summary>
        /// Adding an item to the front of a reversed list should add it to the back of the wrapped list.
        /// </summary>
        [TestMethod]
        public void TestInsert_IndexAtBeginning_InsertsInBack()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            list.Insert(0, 0);
            int[] expected = { 0, 1, 2, 3 };
            int[] actual = toTypedArray(list.List);
            Assert.IsTrue(expected.ToSublist().IsEqualTo(actual.ToSublist()), "The item was not inserted as expected.");
        }

        /// <summary>
        /// Adding an item to the middle of a reversed list should add it to the middle of the wrapped list.
        /// </summary>
        [TestMethod]
        public void TestInsert_IndexInMiddle_InsertsInMiddle()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 4, 5 });
            list.Insert(2, 3);
            int[] expected = { 1, 2, 3, 4, 5 };
            int[] actual = toTypedArray(list.List);
            Assert.IsTrue(expected.ToSublist().IsEqualTo(actual.ToSublist()), "The item was not inserted as expected.");
        }

#endregion

#region Remove

        /// <summary>
        /// Remove should return false if the given value isn't found.
        /// </summary>
        [TestMethod]
        public void TestRemove_ValueNotFound_ReturnsFalse()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 0, 1, 2 });
            bool result = list.Remove(3);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Remove should remove an item if it is a match.
        /// </summary>
        [TestMethod]
        public void TestRemove_FindsValue_ReturnsTrue()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 0, 1, 2 });
            bool result = list.Remove(0);
            Assert.IsTrue(result, "The value was not found.");
            int[] expected = { 1, 2, };
            int[] actual = toTypedArray(list.List);
            Assert.IsTrue(expected.ToSublist().IsEqualTo(actual.ToSublist()), "The item was not removed from the list.");
        }

        /// <summary>
        /// Remove should remove the last occurrence of the value.
        /// </summary>
        [TestMethod]
        public void TestRemove_MultipleOccurrences_RemovesFirstOccurrence()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 0, 1, 0, 1 });
            bool result = list.Remove(1);
            Assert.IsTrue(result, "The value was not found.");
            int[] expected = { 0, 0, 1 };
            int[] actual = toTypedArray(list.List);
            Assert.IsTrue(expected.ToSublist().IsEqualTo(actual.ToSublist()), "The item was not removed from the list.");
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
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            list.RemoveAt(-1);
        }

        /// <summary>
        /// An exception should be thrown if the index is larger than list.Count.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRemoveAt_IndexTooBig_Throws()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            list.RemoveAt(3);
        }

        /// <summary>
        /// Removing an item from the back of a reversed list should remove it from the front of the wrapped list.
        /// </summary>
        [TestMethod]
        public void TestRemoveAt_IndexAtEnd_RemovesFromFront()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            list.RemoveAt(2);
            int[] expected = { 1, 2 };
            int[] actual = toTypedArray(list.List);
            Assert.IsTrue(expected.ToSublist().IsEqualTo(actual.ToSublist()), "The item was not removed as expected.");
        }

        /// <summary>
        /// Removing an item from the front of a reversed list should remove it from the back of the wrapped list.
        /// </summary>
        [TestMethod]
        public void TestRemoveAt_IndexAtBeginning_RemovesFromBack()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            list.RemoveAt(0);
            int[] expected = { 2, 3 };
            int[] actual = toTypedArray(list.List);
            Assert.IsTrue(expected.ToSublist().IsEqualTo(actual.ToSublist()), "The item was not removed as expected.");
        }

        /// <summary>
        /// Removing an item from the middle of a reversed list should remove it from the middle of the wrapped list.
        /// </summary>
        [TestMethod]
        public void TestRemoveAt_IndexInMiddle_RemovesFromMiddle()
        {
            var list = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3, 4 });
            list.RemoveAt(2);
            int[] expected = { 1, 2, 4 };
            int[] actual = toTypedArray(list.List);
            Assert.IsTrue(expected.ToSublist().IsEqualTo(actual.ToSublist()), "The item was not removed as expected.");
        }

#endregion

#region GetEnumerable

        /// <summary>
        /// Enumeration should move through the items.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_GetsItems()
        {
            var view = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            var list = new List<int>();
            foreach (int item in view)
            {
                list.Add(item);
            }
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.ToSublist()), "The values were not enumerated.");
        }

        /// <summary>
        /// Enumeration should move through the items.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_Implicit_StillEnumerates()
        {
            IEnumerable view = new TypedList<ArrayList, int>(new ArrayList() { 1, 2, 3 });
            var list = new List<object>();
            foreach (object item in view)
            {
                list.Add(item);
            }
            object[] expected = { 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.ToSublist()), "The values were not enumerated.");
        }

        /// <summary>
        /// An exception should be thrown if an item in the list is incompatible.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void TestGetEnumerable_IncompatibleItem_Throws()
        {
            var view = new TypedList<ArrayList, int>(new ArrayList() { 1, "2", 3 });
            var list = new List<int>();
            foreach (int item in view)
            {
                list.Add(item);
            }
        }

#endregion

        private static int[] toTypedArray(ArrayList list)
        {
            int[] array = new int[list.Count];
            list.CopyTo(array);
            return array;
        }
    }
#endif
}
