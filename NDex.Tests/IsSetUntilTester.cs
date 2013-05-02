using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    /// <summary>
    /// Tests the ISetUntil methods.
    /// </summary>
    [TestClass]
    public class IsSetUntilTester
    {
        #region Real World Example

        /// <summary>
        /// If we have a partially ordered, unique set of numbers, we may want to know where to start
        /// adding items.
        /// </summary>
        [TestMethod]
        public void TestIsSetUntil_BuildSet()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.AddGenerated(list.ToSublist(), 100, i => random.Next());

            // build a set in place - could call QuickSort then RemoveDuplicates, or MakeSet.
            int index = Sublist.IsSetUntil(list.ToSublist());
            for (int next = index; next < list.Count; ++next)
            {
                var set = list.ToSublist(0, index);
                int value = list[next];
                int location = Sublist.BinarySearch(set, value);
                // if the value is unique
                if (location < 0)
                {
                    location = ~location;
                    // shift everything over one and stick in the value
                    int size = index - location;
                    var backend = list.ToSublist(location, size);
                    var offset = list.ToSublist(location + 1, size);
                    Sublist.Copy(backend.Reversed(), offset.Reversed());
                    //copyBackward(backend, offset);
                    list[location] = value;
                    ++index; // the set grew
                }
            }
            Sublist.RemoveRange(list.ToSublist(index)); // removes dangling items
            Assert.IsTrue(Sublist.IsSet(list.ToSublist()), "Did not build a valid set.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSetUntil_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist.IsSetUntil(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSetUntil_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.IsSetUntil(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSetUntil_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.IsSetUntil(list, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSetUntil_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            IComparer<int> comparer = null;
            Sublist.IsSetUntil(list, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsSetUntil_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.IsSetUntil(list, comparison);
        }

        #endregion

        /// <summary>
        /// An empty list is a valid set.
        /// </summary>
        [TestMethod]
        public void TestIsSetUntil_EmptyList_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>());
            int index = Sublist.IsSetUntil(list);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// An list with one item is a valid set.
        /// </summary>
        [TestMethod]
        public void TestIsSetUntil_SizeOfOne_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1 });
            int index = Sublist.IsSetUntil(list);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// An list with two unique items in sorted order is a valid set.
        /// </summary>
        [TestMethod]
        public void TestIsSetUntil_SizeOfTwo()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2 });
            int index = Sublist.IsSetUntil(list, Comparer<int>.Default);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we build a set in reverse, it should still be a set with the appropriate comparison.
        /// </summary>
        [TestMethod]
        public void TestIsSetUntil_Reversed()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 4, 3, 2, 1 });
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            int index = Sublist.IsSetUntil(list, comparison);
            Assert.AreEqual(list.Count, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we build a list that is not a set, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsSetUntil_ContainsDuplicates_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 3, });
            int index = Sublist.IsSetUntil(list);
            Assert.AreEqual(3, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If we build a list that is not a set, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestIsSetUntil_OutOfOrder_ReturnsFalse()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 4, 3, }); // 3's the trouble maker, not 4!
            int index = Sublist.IsSetUntil(list);
            Assert.AreEqual(3, index, "The wrong index was returned.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
