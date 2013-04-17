using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    /// <summary>
    /// Tests the RemoveRange methods.
    /// </summary>
    [TestClass]
    public class RemoveRangeTester
    {
        #region Real World Examples

        /// <summary>
        /// RemoveRange is often used to eliminate unwanted items in a list. Calling Overwrite methods followed by
        /// RemoveRange is often more efficient than calling Remove methods, since removing results in items being
        /// shifted to the front of the list.
        /// </summary>
        [TestMethod]
        public void TestRemoveRange_RemoveUnwantedItems()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Grow(list, 100, () => random.Next(100));

            // overwrite if an odd number
            int index = Sublist.OverwriteIf(list.ToSublist(), i => i % 2 == 1);

            // now eliminate garbage at the end
            var remaining = list.ToSublist(index);
            Sublist.RemoveRange(remaining);
            Assert.AreEqual(0, remaining.Count, "The Sublist ");

            Assert.AreEqual(index, list.Count, "All of the items were removed.");
            Assert.IsTrue(Sublist.TrueForAll(list.ToSublist(), i => i % 2 == 0), "Not all odd numbers were removed.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemoveRange_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.RemoveRange(list);
        }

        #endregion

        /// <summary>
        /// If a Sublist encompasses an entire list, RemoveRange will completely clear the list.
        /// </summary>
        [TestMethod]
        public void TestRemoveRange_RemoveAllItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist();

            Sublist.RemoveRange(list);
            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            Assert.AreEqual(0, list.List.Count, "Items remain in the underlying list.");
        }

        /// <summary>
        /// If a Sublist encompasses the front of a list, RemoveRange will remove the front.
        /// </summary>
        [TestMethod]
        public void TestRemoveRange_RemoveFrontItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(0, 2);

            Sublist.RemoveRange(list);

            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            int[] expected = { 3, 4, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.List.ToSublist()), "The wrong items were removed.");
        }

        /// <summary>
        /// If a Sublist encompasses the back of a list, RemoveRange will remove the back.
        /// </summary>
        [TestMethod]
        public void TestRemoveRange_RemoveBackItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(3);

            Sublist.RemoveRange(list);

            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.List.ToSublist()), "The wrong items were removed.");
        }

        /// <summary>
        /// If a Sublist encompasses the middle of a list, RemoveRange will remove the middle.
        /// </summary>
        [TestMethod]
        public void TestRemoveRange_RemoveMiddleItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(1, 3);

            Sublist.RemoveRange(list);

            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            int[] expected = { 1, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.List.ToSublist()), "The wrong items were removed.");
        }

        /// <summary>
        /// If a reversed Sublist encompasses an entire list, RemoveRange will completely clear the list.
        /// </summary>
        [TestMethod]
        public void TestRemoveRange_Reversed_RemoveAllItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist().Reversed();
            Sublist.RemoveRange(list);
            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            Assert.AreEqual(0, list.List.Count, "Items remain in the underlying list.");
        }

        /// <summary>
        /// If a Sublist encompasses the front of a list, RemoveRange will remove the front.
        /// </summary>
        [TestMethod]
        public void TestRemoveRange_Reversed_RemoveFrontItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(0, 2).Reversed();

            Sublist.RemoveRange(list);

            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            int[] expected = { 3, 4, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.List.List.ToSublist()), "The wrong items were removed.");
        }

        /// <summary>
        /// If a Sublist encompasses the back of a list, RemoveRange will remove the back.
        /// </summary>
        [TestMethod]
        public void TestRemoveRange_Reversed_RemoveBackItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(3).Reversed();

            Sublist.RemoveRange(list);

            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.List.List.ToSublist()), "The wrong items were removed.");
        }

        /// <summary>
        /// If a Sublist encompasses the middle of a list, RemoveRange will remove the middle.
        /// </summary>
        [TestMethod]
        public void TestRemoveRange_Reversed_RemoveMiddleItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist(1, 3).Reversed();

            Sublist.RemoveRange(list);

            Assert.AreEqual(0, list.Count, "Items remain in the Sublist.");
            int[] expected = { 1, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.List.List.ToSublist()), "The wrong items were removed.");
        }
    }
}
