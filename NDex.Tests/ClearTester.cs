using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the Clear methods.
    /// </summary>
    [TestClass]
    public class ClearTester
    {
        #region Real World Examples

        /// <summary>
        /// Clear is often used to eliminate unwanted items in a list. Calling Overwrite methods followed by
        /// Clear is often more efficient than calling Remove methods, since removing results in items being
        /// shifted to the front of the list.
        /// </summary>
        [TestMethod]
        public void TestClear_RemoveUnwantedItems()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list.ToSublist());

            // overwrite if an odd number
            int index = list.ToSublist().Where(i => i % 2 == 1).InPlace();

            // now eliminate garbage at the end
            var remaining = list.ToSublist(index);
            remaining = remaining.Clear();
            Assert.AreEqual(0, remaining.Count, "The Sublist ");

            Assert.AreEqual(index, list.Count, "All of the items were removed.");
            Assert.IsFalse(list.ToSublist().Find(i => i % 2 != 0), "Not all odd numbers were removed.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestClear_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            list.Clear();
        }

        #endregion

        /// <summary>
        /// If a Sublist encompasses an entire list, Clear will completely clear the list.
        /// </summary>
        [TestMethod]
        public void TestClear_RemoveAllItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist();

            list = list.Clear();
            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            Assert.AreEqual(0, list.List.Count, "Items remain in the underlying list.");
        }

        /// <summary>
        /// If a Sublist encompasses the front of a list, Clear will remove the front.
        /// </summary>
        [TestMethod]
        public void TestClear_RemoveFrontItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(0, 2);

            list = list.Clear();

            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            int[] expected = { 3, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()), "The wrong items were removed.");
        }

        /// <summary>
        /// If a Sublist encompasses the back of a list, Clear will remove the back.
        /// </summary>
        [TestMethod]
        public void TestClear_RemoveBackItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(3);

            list = list.Clear();

            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()), "The wrong items were removed.");
        }

        /// <summary>
        /// If a Sublist encompasses the middle of a list, Clear will remove the middle.
        /// </summary>
        [TestMethod]
        public void TestClear_RemoveMiddleItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(1, 3);

            list = list.Clear();

            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            int[] expected = { 1, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.ToSublist()), "The wrong items were removed.");
        }

        /// <summary>
        /// If a reversed Sublist encompasses an entire list, Clear will completely clear the list.
        /// </summary>
        [TestMethod]
        public void TestClear_Reversed_RemoveAllItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist().Reversed();
            list = list.Clear();
            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            Assert.AreEqual(0, list.List.Count, "Items remain in the underlying list.");
        }

        /// <summary>
        /// If a Sublist encompasses the front of a list, Clear will remove the front.
        /// </summary>
        [TestMethod]
        public void TestClear_Reversed_RemoveFrontItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(0, 2).Reversed();

            list = list.Clear();

            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            int[] expected = { 3, 4, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.List.ToSublist()), "The wrong items were removed.");
        }

        /// <summary>
        /// If a Sublist encompasses the back of a list, Clear will remove the back.
        /// </summary>
        [TestMethod]
        public void TestClear_Reversed_RemoveBackItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(3).Reversed();

            list = list.Clear();

            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.List.ToSublist()), "The wrong items were removed.");
        }

        /// <summary>
        /// If a Sublist encompasses the middle of a list, Clear will remove the middle.
        /// </summary>
        [TestMethod]
        public void TestClear_Reversed_RemoveMiddleItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(1, 3).Reversed();

            list = list.Clear();

            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            int[] expected = { 1, 5 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(list.List.List.ToSublist()), "The wrong items were removed.");
        }
    }
}
