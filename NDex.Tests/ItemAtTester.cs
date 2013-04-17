using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the ItemAt methods.
    /// </summary>
    [TestClass]
    public class ItemAtTester
    {
        #region Real World Example

        /// <summary>
        /// Sorting puts EVERY item in its appropriate location. When we only want to know
        /// what item is at a particular positon, it is often quicker to call ItemAt. We will
        /// use ItemAt to find 3rd place in a list of numbers.
        /// </summary>
        [TestMethod]
        public void TestItemAt_Find3rdPlace()
        {
            Random random = new Random();

            // build a list, leaving space for zero, one and two
            var list = new List<int>(97);
            Sublist.Grow(list, 97, () => random.Next(3, 100));

            // insert 0-2 at random positions in the list
            list.Insert(random.Next(0, list.Count + 1), 0);
            list.Insert(random.Next(0, list.Count + 1), 1);
            list.Insert(random.Next(0, list.Count + 1), 2);

            // now find what item belongs in the second position, as if the list was sorted
            Sublist.ItemAt(list.ToSublist(), 2);
            int actual = list[2];
            Assert.AreEqual(2, actual, "The 2 was not moved to the second position.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAt_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int index = 0;
            Sublist.ItemAt(list, index);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAt_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int index = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.ItemAt(list, index, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAt_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int index = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.ItemAt(list, index, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAt_NegativeIndex_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int index = -1;
            Sublist.ItemAt(list, index);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAt_WithComparer_NegativeIndex_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int index = -1;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.ItemAt(list, index, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAt_WithComparison_NegativeIndex_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int index = -1;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.ItemAt(list, index, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAt_IndexTooBig_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int index = 0;
            Sublist.ItemAt(list, index);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAt_WithComparer_IndexTooBig_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int index = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.ItemAt(list, index, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAt_WithComparison_IndexTooBig_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int index = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.ItemAt(list, index, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAt_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>() { 1 };
            int index = 0;
            IComparer<int> comparer = null;
            Sublist.ItemAt(list, index, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAt_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>() { 1 };
            int index = 0;
            Func<int, int, int> comparison = null;
            Sublist.ItemAt(list, index, comparison);
        }

        #endregion

        /// <summary>
        /// We should be able to find the item in the front even if the list is reversed.
        /// </summary>
        [TestMethod]
        public void TestItemAt_Reversed_ItemInFront()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.Grow(list, 100, i => 100 - i);
            Sublist.ItemAt(list, 0, Comparer<int>.Default);
            Assert.AreEqual(1, list[0], "The wrong item was moved to the front.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the item in the back even if the list is reversed.
        /// </summary>
        [TestMethod]
        public void TestItemAt_Reversed_ItemInBack()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.Grow(list, 100, i => 100 - i);
            Sublist.ItemAt(list, list.Count - 1, Comparer<int>.Default);
            Assert.AreEqual(100, list[list.Count - 1], "The wrong item was moved to the back.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the item in the middle even if the list is reversed.
        /// </summary>
        [TestMethod]
        public void TestItemAt_Reversed_ItemInMiddle()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.Grow(list, 100, i => 100 - i);
            Sublist.ItemAt(list, 49, Comparer<int>.Default);
            Assert.AreEqual(50, list[49], "The wrong item was moved to the middle.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the item in the front even if the list is pipe-organed.
        /// </summary>
        [TestMethod]
        public void TestItemAt_PipeOrganed_ItemInFront()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.Grow(list, 50, i => i * 2);
            Sublist.Grow(list, 100, i => 100 - ((i - 50) * 2 + 1));
            Sublist.ItemAt(list, 0, Comparer<int>.Default.Compare);
            Assert.AreEqual(0, list[0], "The wrong item was moved to the front.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the item in the back even if the list is pipe-organed.
        /// </summary>
        [TestMethod]
        public void TestItemAt_PipeOrganed_ItemInBack()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.Grow(list, 50, i => i * 2);
            Sublist.Grow(list, 100, i => 100 - ((i - 50) * 2 + 1));
            Sublist.ItemAt(list, list.Count - 1, Comparer<int>.Default.Compare);
            Assert.AreEqual(99, list[list.Count - 1], "The wrong item was moved to the back.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the item in the middle even if the list is pipe-organed.
        /// </summary>
        [TestMethod]
        public void TestItemAt_PipeOrganed_ItemInMiddle()
        {
            var list = TestHelper.Wrap(new List<int>());
            Sublist.Grow(list, 50, i => i * 2);
            Sublist.Grow(list, 100, i => 100 - ((i - 50) * 2 + 1));
            Sublist.ItemAt(list, 49, Comparer<int>.Default.Compare);
            Assert.AreEqual(49, 49, "The wrong item was moved to the middle.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
