using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the Substring class.
    /// </summary>
    [TestClass]
    public class SubstringTester
    {
        #region Real World Example

        /// <summary>
        /// Substring allows us to perform read-only operations on a string, provided by the List class.
        /// We will demonstrate this by counting the occurrences of a character.
        /// </summary>
        [TestMethod]
        public void TestSubstring_CountingOccurrencesOfLetters()
        {
            // build a string of 100 random characters
            Random random = new Random();

            List<char> characters = new List<char>(1000);
            Sublist.Add(Enumerable.Range(0, 1000).Select(i => (char)random.Next(0x41, 0x7A)), characters.ToSublist());
            string value = new String(characters.ToArray());

            // count the occurrences of the letter 'e'
            int count = Sublist.CountIf(value.ToSubstring(), c => c == 'e');

            // count the occurrences with the string functions
            int index = 0;
            int oldCount = 0;
            while (index != -1)
            {
                index = value.IndexOf('e', index);
                if (index != -1)
                {
                    ++oldCount;
                    ++index; // otherwise we'll just find the same character again
                }
            }

            Assert.AreEqual(oldCount, count, "The wrong number of characters were counted.");
        }

        #endregion

        #region ToSubstring

        /// <summary>
        /// If we call ToSubstring on a string with no additional parameters, the entire string
        /// should be wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSubstring_WrapsEntireString()
        {
            string value = "testing";
            Substring substring = value.ToSubstring();
            Assert.AreSame(value, substring.Value, "The underlying string was not set.");
            Assert.AreEqual(0, substring.Offset, "The offset was not zero.");
            Assert.AreEqual(value.Length, substring.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSubstring with an offset, the rest of the string is wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSubstring_WithOffset_WrapsRemaining()
        {
            string value = "testing";
            var substring = value.ToSubstring(1);
            Assert.AreSame(value, substring.Value, "The underlying string was not set.");
            Assert.AreEqual(1, substring.Offset, "The offset was not zero.");
            Assert.AreEqual(6, substring.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSubstring with an offset and count, that portion of the string should be wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSubstring_WithOffsetAndCount_WrapsRemaining()
        {
            string value = "testing";
            var substring = value.ToSubstring(1, 1);
            Assert.AreSame(value, substring.Value, "The underlying string was not set.");
            Assert.AreEqual(1, substring.Offset, "The offset was not zero.");
            Assert.AreEqual(1, substring.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSubstring a null sublist, it throws an exception/
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToSubstring_NullSublist_Throws()
        {
            IReadOnlySublist<StringAdapter, char> sublist = null;
            sublist.ToSubstring();
        }

        /// <summary>
        /// Some times when manipulating a Substring, it gets converted to an IReadOnlySublist.
        /// We can call ToSubstring to restore it back to Substring.
        /// </summary>
        [TestMethod]
        public void TestToSubstring_IReadOnlySublist_Restores()
        {
            Substring substring = "Hello".ToSubstring(1, 3);
            var reversed = substring.Reversed().Reversed();
            var actual = reversed.ToSubstring();
            Assert.AreSame(substring.Value, actual.Value, "The original string was not wrapped.");
            Assert.AreEqual(substring.Offset, actual.Offset, "The offset was not restored.");
            Assert.AreEqual(substring.Count, actual.Count, "The count was not restored.");
        }

        #endregion

        #region Ctor

        /// <summary>
        /// If we pass a null string to the constructor, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_NullString_ThrowsException()
        {
            new Substring((string)null);
        }

        /// <summary>
        /// An exception should be thrown if the string is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_WithOffset_NullString_Throws()
        {
            string value = null;
            int offset = 0;
            Substring substring = new Substring(value, offset);
        }

        /// <summary>
        /// An exception should be thrown if the string is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_WithOffsetAndCount_NullString_Throws()
        {
            string value = null;
            int offset = 0;
            int count = 0;
            Substring substring = new Substring(value, offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_NegativeOffset_Throws()
        {
            string value = "testing";
            int offset = -1;
            Substring sublist = new Substring(value, offset);
        }

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_WithCount_NegativeOffset_Throws()
        {
            string value = String.Empty;
            int offset = -1;
            int count = 0;
            Substring substring = new Substring(value, offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the offset too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_OffsetTooBig_Throws()
        {
            string value = String.Empty;
            int offset = 1;
            Substring substring = new Substring(value, offset);
        }

        /// <summary>
        /// An exception should be thrown if the offset too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_WithCount_OffsetTooBig_Throws()
        {
            string value = String.Empty;
            int offset = 1;
            int count = 0;
            Substring substring = new Substring(value, offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the count is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_NegativeCount_Throws()
        {
            string value = String.Empty;
            int offset = 0;
            int count = -1;
            Substring substring = new Substring(value, offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the count is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_CountTooBig_Throws()
        {
            string value = String.Empty;
            int offset = 0;
            int count = 1;
            Substring substring = new Substring(value, offset, count);
        }

        /// <summary>
        /// The ctor just taking a string, should wrap the entire string.
        /// </summary>
        [TestMethod]
        public void TestCtor_WrapsEntireString()
        {
            string value = "test";
            var substring = new Substring(value);
            Assert.AreSame(value, substring.Value, "The string was not set as a backing field.");
            Assert.AreEqual(value.Length, substring.Count, "The substring had the wrong count.");
            Assert.AreEqual(0, substring.Offset, "The substring had the wrong offset.");
            char[] expected = { 't', 'e', 's', 't' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), substring), "The substring did not contain the expected items.");
        }

        /// <summary>
        /// The ctor taking an offset defaults the count to the remaining number of items.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithOffset_WrapsRemainingString()
        {
            string value = "test";
            var substring = new Substring(value, 1);
            Assert.AreSame(value, substring.Value, "The string was not set as a backing field.");
            Assert.AreEqual(value.Length - 1, substring.Count, "The substring had the wrong count.");
            Assert.AreEqual(1, substring.Offset, "The substring had the wrong offset.");
            char[] expected = { 'e', 's', 't' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), substring), "The substring did not contain the expected items.");
        }

        /// <summary>
        /// The ctor taking a count limits the number of items to a splice of the underlying string.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithOffsetAndCount_CreatesSplice()
        {
            string value = "test";
            var substring = new Substring(value, 1, 2);
            Assert.AreSame(value, substring.Value, "The string was not set as a backing field.");
            Assert.AreEqual(2, substring.Count, "The substring had the wrong count.");
            Assert.AreEqual(1, substring.Offset, "The substring had the wrong offset.");
            char[] expected = { 'e', 's' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), substring), "The substring did not contain the expected items.");
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
            var substring = String.Empty.ToSubstring();
            int offset = -1;
            substring.Nest(offset);
        }

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_WithCount_NegativeOffset_Throws()
        {
            var substring = String.Empty.ToSubstring();
            int offset = -1;
            int count = 0;
            substring.Nest(offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the offset is past the end of the substring.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_OffsetTooBig_Throws()
        {
            var substring = String.Empty.ToSubstring();
            int offset = 1;
            substring.Nest(offset);
        }

        /// <summary>
        /// An exception should be thrown if the offset is past the end of the substring.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_WithCount_OffsetTooBig_Throws()
        {
            var substring = String.Empty.ToSubstring();
            int offset = 1;
            int count = 0;
            substring.Nest(offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the count is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_NegativeCount_Throws()
        {
            var substring = String.Empty.ToSubstring();
            int offset = 0;
            int count = -1;
            substring.Nest(offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the count is greater than the remaining items.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_CountTooBig_Throws()
        {
            var substring = "Sammy".ToSubstring();
            int offset = 1;
            int count = 5; // one too big
            substring.Nest(offset, count);
        }

        /// <summary>
        /// If we just change the offset and there's no room to the right, we're essentially popping off the front.
        /// </summary>
        [TestMethod]
        public void TestNest_OffsetToPopFront()
        {
            var substring = "Sammy".ToSubstring();
            var nested = substring.Nest(1);
            char[] expected = { 'a', 'm', 'm', 'y' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we just change the offset and there's no room to the right, we're essentially popping off the front.
        /// </summary>
        [TestMethod]
        public void TestNest_ReadOnly_OffsetToPopFront()
        {
            IReadOnlySublist<StringAdapter, char> substring = "Sammy".ToSubstring();
            var nested = substring.Nest(1);
            char[] expected = { 'a', 'm', 'm', 'y' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we nest a nested Sublist, the count should be adjusted as expected.
        /// </summary>
        [TestMethod]
        public void TestNest_DoublyNested_AdjustsCount()
        {
            var list = "Sammy".ToSubstring();
            var nested = list.Nest(1).Nest(1);
            char[] expected = { 'm', 'm', 'y' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_CountToPopBack()
        {
            var list = "Sammy".ToSubstring();
            var nested = list.Nest(0, list.Count - 1);
            char[] expected = { 'S', 'a', 'm', 'm' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the last item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_ReadOnly_CountToPopBack()
        {
            IReadOnlySublist<StringAdapter, char> list = "Sammy".ToSubstring();
            var nested = list.Nest(0, list.Count - 1);
            char[] expected = { 'S', 'a', 'm', 'm' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the last item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_ShiftAndShrink()
        {
            var list = "Sammy".ToSubstring();
            var nested = list.Nest(1, list.Count - 2); // we want to remove the front and back, two items
            char[] expected = { 'a', 'm', 'm' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the last item.");
        }

        #endregion

        #region Offset

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestOffset_NegativeOffset_Throws()
        {
            var substring = String.Empty.ToSubstring();
            substring.Offset = -1;
        }

        /// <summary>
        /// An exception should be thrown if the offset is greater than the size of the list.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestOffset_OffsetTooBig_Throws()
        {
            var substring = String.Empty.ToSubstring();
            substring.Offset = 1;
        }

        /// <summary>
        /// If we shift the offset to the right, the whole splice should shift.
        /// </summary>
        [TestMethod]
        public void TestOffset_ShiftOffsetRight_CountShrinks()
        {
            var substring = "bob".ToSubstring(0, 2);
            substring.Offset = 1;
            Assert.AreEqual(2, substring.Count, "The count did not shrink.");
            char[] expected = { 'o', 'b' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), substring), "The string was not shifted to the right.");
        }

        /// <summary>
        /// If we shift the offset to the right, the count should shrink automatically, if needed.
        /// </summary>
        [TestMethod]
        public void TestOffset_ShiftOffsetRight_ShiftsSplice()
        {
            var substring = "bob".ToSubstring();
            substring.Offset = 1;
            Assert.AreEqual(2, substring.Count, "The count did not shrink.");
            char[] expected = { 'o', 'b' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), substring), "The string was not shifted to the right.");
        }

        #endregion

        #region Count

        /// <summary>
        /// An exception should be thrown if the count is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCount_Setter_NegativeCount_Throws()
        {
            var substring = String.Empty.ToSubstring();
            substring.Count = -1;
        }

        /// <summary>
        /// An exception should be thrown if the count is larger than the size of the underlying string.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCount_Setter_CountTooBig_Throws()
        {
            var substring = String.Empty.ToSubstring();
            substring.Count = 1;
        }

        /// <summary>
        /// The count can be modified to grow or shink the substring.
        /// </summary>
        [TestMethod]
        public void TestCount_Setter_CountGrowing_GrowsSplice()
        {
            var substring = "bob".ToSubstring();
            substring.Count = 1;
            char[] expected1 = { 'b' };
            Assert.IsTrue(Sublist.AreEqual(expected1.ToSublist(), substring), "A substring of size 1 was wrong.");
            substring.Count = 2;
            char[] expected2 = { 'b', 'o' };
            Assert.IsTrue(Sublist.AreEqual(expected2.ToSublist(), substring), "A substring of size 2 was wrong.");
            substring.Count = 3;
            char[] expected3 = { 'b', 'o', 'b' };
            Assert.IsTrue(Sublist.AreEqual(expected3.ToSublist(), substring), "A substring of size 3 was wrong.");
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
            var substring = String.Empty.ToSubstring();
            int value = substring[-1];
        }

        /// <summary>
        /// An exception should be thrown if the index is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Getter_IndexEqualsCount_Throws()
        {
            var substring = "bob".ToSubstring(0, 2);
            int value = substring[substring.Count];
        }

        /// <summary>
        /// The indexer should add the given index to the substring's offset to
        /// grab the value from the underlying string.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_GetsValueAtIndexPlusOffset()
        {
            var substring = "bob".ToSubstring(1);
            char value = substring[1];
            Assert.AreEqual('b', value);
        }

        #endregion

        #region GetEnumerator

        /// <summary>
        /// Enumeration should move through the items in the substring.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_GetsItemsInReverse()
        {
            var view = "Josh".ToSubstring(1, 2);
            var list = new List<char>();
            foreach (char item in view)
            {
                list.Add(item);
            }
            char[] expected = { 'o', 's' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.ToSublist()), "The correct values were not enumerated.");
        }

        /// <summary>
        /// Enumeration should move through the items in the substring.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_Implicit_StillEnumerates()
        {
            IEnumerable view = "Josh".ToSubstring(1, 2);
            var list = new List<char>();
            foreach (char item in view)
            {
                list.Add(item);
            }
            char[] expected = { 'o', 's' };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.ToSublist()), "The correct values were not enumerated.");
        }

        #endregion

        #region ToString

        /// <summary>
        /// As a convenience, calling ToString on a Substring will return the actual substring.
        /// </summary>
        [TestMethod]
        public void TestToString_ReturnsSubstring()
        {
            Substring value = "Hello".ToSubstring(1);
            string result = value.ToString();
            Assert.AreEqual("ello", result, "The wrong substring was returned.");
        }

        #endregion

        #region Implicit Conversion

        /// <summary>
        /// We should be able to directly create a Substring from a string.
        /// </summary>
        [TestMethod]
        public void TestConversion_Implicit()
        {
            const string value = "Hello";
            Substring substring = value;
            Assert.AreEqual(value, substring.Value, "The substring did not wrap the string.");
            Assert.AreEqual(0, substring.Offset, "The offset was wrong.");
            Assert.AreEqual(value.Length, substring.Count, "The count was wrong.");
        }

        #endregion
    }
}
