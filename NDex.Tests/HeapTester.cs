﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the heap methods.
    /// </summary>
    [TestClass]
    public class HeapTester
    {
        #region Real World Example

        /// <summary>
        /// Since all of the heap methods work together, I'll just create one example for all of them.
        /// I will simulate a priority queue.
        /// </summary>
        [TestMethod]
        public void TestHeap_PriorityQueue()
        {
            Random random = new Random();

            // build a random list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list.ToSublist());

            // make a heap
            list.ToSublist().MakeHeap().InPlace();
            Assert.IsTrue(list.ToSublist().IsHeap(), "The list is not a heap."); // confirm we have a heap

            // let's push a value onto the heap and make it the highest priority
            list.Add(100);
            list.ToSublist().HeapAdd();
            Assert.AreEqual(100, list[0], "The value was not moved to the top of the heap.");
            Assert.IsTrue(list.ToSublist().IsHeap(), "The list is not a heap.");

            // now let's remove it
            list.ToSublist().HeapRemove();
            Assert.AreEqual(100, list[list.Count - 1], "The value not moved to the bottom of the heap.");
            Assert.AreEqual(list.Count - 1, list.ToSublist().IsHeap(), "Could not find the end of the heap.");
            list.RemoveAt(list.Count - 1);
            Assert.IsTrue(list.ToSublist().IsHeap(), "The list is not a heap.");

            // we can sort a heap
            list.ToSublist().HeapSort();
            Assert.IsTrue(list.ToSublist().IsSorted(), "The list was not sorted.");
        }

        #endregion
    }
}
