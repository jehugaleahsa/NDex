using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    /// <summary>
    /// Tests the StringAdapter helper class.
    /// </summary>
    [TestClass]
    public class StringAdapterTester
    {
        #region Ctor & Count & IsReadOnly

        /// <summary>
        /// Whatever string we pass to the ctor should be set.
        /// </summary>
        [TestMethod]
        public void TestCtor_SetsUnderlyingString()
        {
            const string value = "Hello";
            StringAdapter list = new StringAdapter(value);
            Assert.AreSame(value, list.Value, "The backing field was not set.");
            Assert.AreEqual(value.Length, list.Count, "The counts did not match.");
            Assert.IsTrue(((ICollection<char>)list).IsReadOnly, "The list should have been read-only.");
        }

        #endregion

        #region Indexer

        /// <summary>
        /// If the item at the index is of the correct type, it should cast without problem.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_ReturnsCharacter()
        {
            string value = "Hello";
            StringAdapter list = new StringAdapter(value);
            Assert.AreEqual(list[2], value[2], "The index returned the wrong value.");
        }

        /// <summary>
        /// We cannot set the value at an index (strings are immutable).
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestIndexer_Setter_Throws()
        {
            string value = "Hello";
            StringAdapter list = new StringAdapter(value);
            list[0] = 'a';
        }

        #endregion

        #region Add

        /// <summary>
        /// We cannot add characters to a string, since it is immutable.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestAdd_Throws()
        {
            IList<char> list = new StringAdapter("Hello");
            list.Add('a');
        }

        #endregion

        #region Clear

        /// <summary>
        /// We cannot clear out a string, since it is immutable.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestClear_Throws()
        {
            IList<char> list = new StringAdapter("Hello");
            list.Clear();
        }

        #endregion

        #region Contains

        /// <summary>
        /// Contains should return true if the character is in the list.
        /// </summary>
        [TestMethod]
        public void TestContains_ValueExists_ReturnsTrue()
        {
            StringAdapter list = new StringAdapter("Hello");
            bool result = list.Contains('e');
            Assert.IsTrue(result, "The character should have been found.");
        }

        /// <summary>
        /// Contains should return false if the character is not in the list.
        /// </summary>
        [TestMethod]
        public void TestContains_ValueDoesNotExists_ReturnsFalse()
        {
            StringAdapter list = new StringAdapter("Hello");
            bool result = list.Contains('r');
            Assert.IsFalse(result, "The character should not have been found.");
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Copying should work.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_Copies()
        {
            StringAdapter list = new StringAdapter("Hello");
            char[] output = new char[list.Count];
            list.CopyTo(output, 0);
            Assert.AreEqual("Hello", new String(output), "The values were not copied.");
        }

        /// <summary>
        /// Copying should work, starting at the arrayIndex.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_Copies_StartingAtArrayIndex()
        {
            StringAdapter list = new StringAdapter("Hello");
            char[] output = new char[list.Count + 2];
            output[0] = 'a';
            output[1] = 'a';
            list.CopyTo(output, 2);
            Assert.AreEqual("aaHello", new String(output), "The values were not copied.");
        }

        #endregion

        #region IndexOf

        /// <summary>
        /// IndexOf should return negative one if the given character isn't found.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_ValueNotFound_ReturnsNegativeOne()
        {
            StringAdapter list = new StringAdapter("Hello");
            int index = list.IndexOf('r');
            Assert.AreEqual(-1, index, "The wrong index was returned.");
        }

        /// <summary>
        /// IndexOf should return the index of the first occurrence of the value.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_ValueFound_ReturnsFirst()
        {
            StringAdapter list = new StringAdapter("Hello, Yellow");
            int index = list.IndexOf('e');
            Assert.AreEqual(1, index, "The wrong index was returned.");
        }

        #endregion

        #region Insert

        /// <summary>
        /// Inserting an item in a string is invalid, since strings are immutable.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestInsert_Throws()
        {
            IList<char> list = new StringAdapter("Hello");
            list.Insert(0, 'a');
        }

        #endregion

        #region Remove

        /// <summary>
        /// Remove should throw an exception since strings are immutable.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestRemove_Throws()
        {
            IList<char> list = new StringAdapter("Hello");
            list.Remove('e');
        }

        #endregion

        #region RemoveAt

        /// <summary>
        /// Removing should thrown an exception since string are immutable.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestRemoveAt_Throws()
        {
            IList<char> list = new StringAdapter("Hello");
            list.RemoveAt(0);
        }

        #endregion

        #region GetEnumerable

        /// <summary>
        /// Enumeration should move through the items.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_GetsItems()
        {
            StringAdapter list = new StringAdapter("Hello");
            IEnumerator<char> enumerator = list.GetEnumerator();
            moveAndCheck(enumerator, 'H');
            moveAndCheck(enumerator, 'e');
            moveAndCheck(enumerator, 'l');
            moveAndCheck(enumerator, 'l');
            moveAndCheck(enumerator, 'o');
            Assert.IsFalse(enumerator.MoveNext(), "There should not have been any more items.");
        }

        private void moveAndCheck(IEnumerator<char> enumerator, char expected)
        {
            Assert.IsTrue(enumerator.MoveNext(), "Ran out of items. Expecting {0}.", expected);
            Assert.AreEqual(expected, enumerator.Current, "The wrong value was returned. Expecting {0}.", expected);
        }

        /// <summary>
        /// Enumeration should move through the items.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_Implicit_StillEnumerates()
        {
            IEnumerable list = new StringAdapter("Hello");
            IEnumerator enumerator = list.GetEnumerator();
            moveAndCheck(enumerator, 'H');
            moveAndCheck(enumerator, 'e');
            moveAndCheck(enumerator, 'l');
            moveAndCheck(enumerator, 'l');
            moveAndCheck(enumerator, 'o');
            Assert.IsFalse(enumerator.MoveNext(), "There should not have been any more items.");
        }

        private void moveAndCheck(IEnumerator enumerator, char expected)
        {
            Assert.IsTrue(enumerator.MoveNext(), "Ran out of items. Expecting {0}.", expected);
            Assert.AreEqual(expected, enumerator.Current, "The wrong value was returned. Expecting {0}.", expected);
        }

        #endregion
    }
}
