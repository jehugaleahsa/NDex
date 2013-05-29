using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ItemAtInPlace methods.
    /// </summary>
    [TestClass]
    public class ItemAtInPlaceTester
    {
        #region Real World Example

        /// <summary>
        /// Sorting puts EVERY item in its appropriate location. When we only want to know
        /// what item is at a particular positon, it is often quicker to call ItemAt. We will
        /// use ItemAt to find 3rd place in a list of numbers.
        /// </summary>
        [TestMethod]
        public void TestItemAtInPlace_Find3rdPlace()
        {
            Random random = new Random();

            // build a list, leaving space for zero, one and two
            var list = new List<int>(97);
            Sublist.Generate(97, i => random.Next(3, 100)).AddTo(list.ToSublist());

            // insert 0-2 at random positions in the list
            list.Insert(random.Next(0, list.Count + 1), 0);
            list.Insert(random.Next(0, list.Count + 1), 1);
            list.Insert(random.Next(0, list.Count + 1), 2);

            // now find what item belongs in the second position, as if the list was sorted
            list.ToSublist().ItemAt(2).InPlace();
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
        public void TestItemAtInPlace_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            int index = 0;
            list.ItemAt(index);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAtInPlace_WithComparer_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            int index = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            list.ItemAt(index, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAtInPlace_WithComparison_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            int index = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.ItemAt(index, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAtInPlace_NegativeIndex_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            int index = -1;
            list.ItemAt(index);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAtInPlace_WithComparer_NegativeIndex_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            int index = -1;
            IComparer<int> comparer = Comparer<int>.Default;
            list.ItemAt(index, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAtInPlace_WithComparison_NegativeIndex_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            int index = -1;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.ItemAt(index, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAtInPlace_IndexTooBig_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            int index = 0;
            list.ItemAt(index);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAtInPlace_WithComparer_IndexTooBig_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            int index = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            list.ItemAt(index, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAtInPlace_WithComparison_IndexTooBig_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            int index = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.ItemAt(index, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAtInPlace_NullComparer_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>() { 1 }.ToSublist();
            int index = 0;
            IComparer<int> comparer = null;
            list.ItemAt(index, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAtInPlace_NullComparison_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>() { 1 }.ToSublist();
            int index = 0;
            Func<int, int, int> comparison = null;
            list.ItemAt(index, comparison);
        }

        #endregion

        /// <summary>
        /// We should be able to find the item in the front even if the list is reversed.
        /// </summary>
        [TestMethod]
        public void TestItemAtInPlace_Reversed_ItemInFront()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(100, i => 100 - i).AddTo(list);
            list.ItemAt(0, Comparer<int>.Default).InPlace();
            Assert.AreEqual(1, list[0], "The wrong item was moved to the front.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the item in the back even if the list is reversed.
        /// </summary>
        [TestMethod]
        public void TestItemAtInPlace_Reversed_ItemInBack()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(100, i => 100 - i).AddTo(list);
            list.ItemAt(list.Count - 1, Comparer<int>.Default).InPlace();
            Assert.AreEqual(100, list[list.Count - 1], "The wrong item was moved to the back.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the item in the middle even if the list is reversed.
        /// </summary>
        [TestMethod]
        public void TestItemAtInPlace_Reversed_ItemInMiddle()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(100, i => 100 - i).AddTo(list);
            list.ItemAt(49, Comparer<int>.Default).InPlace();
            Assert.AreEqual(50, list[49], "The wrong item was moved to the middle.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the item in the front even if the list is pipe-organed.
        /// </summary>
        [TestMethod]
        public void TestItemAtInPlace_PipeOrganed_ItemInFront()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(50, i => i * 2).AddTo(list);
            list = Sublist.Generate(50, i => 100 - (i * 2 + 1)).AddTo(list);
            list.ItemAt(0, Comparer<int>.Default.Compare).InPlace();
            Assert.AreEqual(0, list[0], "The wrong item was moved to the front.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the item in the back even if the list is pipe-organed.
        /// </summary>
        [TestMethod]
        public void TestItemAtInPlace_PipeOrganed_ItemInBack()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(50, i => i * 2).AddTo(list);
            list = Sublist.Generate(50, i => 100 - (i * 2 + 1)).AddTo(list);
            list.ItemAt(list.Count - 1, Comparer<int>.Default.Compare).InPlace();
            Assert.AreEqual(99, list[list.Count - 1], "The wrong item was moved to the back.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We should be able to find the item in the middle even if the list is pipe-organed.
        /// </summary>
        [TestMethod]
        public void TestItemAtInPlace_PipeOrganed_ItemInMiddle()
        {
            var list = TestHelper.Wrap(new List<int>());
            list = Sublist.Generate(50, i => i * 2).AddTo(list);
            list = Sublist.Generate(50, i => 100 - (i * 2 + 1)).AddTo(list);
            list.ItemAt(49, Comparer<int>.Default.Compare).InPlace();
            Assert.AreEqual(49, list[49], "The wrong item was moved to the middle.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
