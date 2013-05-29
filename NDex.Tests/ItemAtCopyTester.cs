using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ItemAtCopy methods.
    /// </summary>
    [TestClass]
    public class ItemAtCopyTester
    {
        #region Real World Example

        /// <summary>
        /// Sorting puts EVERY item in its appropriate location. When we only want to know
        /// what item is at a particular positon, it is quicker to call ItemAt. We will
        /// use ItemAt to find 3rd place in a list of numbers.
        /// </summary>
        [TestMethod]
        public void TestItemAtCopy_Find3rdPlace()
        {
            Random random = new Random();

            // build a list, leaving space for zero, one and two
            var list = new List<int>(97);
            Sublist.Generate(97, i => random.Next(3, 100)).AddTo(list.ToSublist());

            // insert 0-2 at random positions in the list
            list.Insert(random.Next(0, list.Count + 1), 0);
            list.Insert(random.Next(0, list.Count + 1), 1);
            list.Insert(random.Next(0, list.Count + 1), 2);

            var destination = new int[100];

            // now find what item belongs in the second position, as if the list was sorted
            list.ToSublist().ItemAt(2).CopyTo(destination.ToSublist());
            int actual = destination[2];
            Assert.AreEqual(2, actual, "The 2 was not moved to the second position.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAtCopy_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            int index = 0;
            list.ItemAt(index);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAtCopy_WithComparer_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            int index = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            list.ItemAt(index, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAtCopy_WithComparison_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            int index = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.ItemAt(index, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAtCopy_NegativeIndex_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int index = -1;
            list.ItemAt(index);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAtCopy_WithComparer_NegativeIndex_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int index = -1;
            IComparer<int> comparer = Comparer<int>.Default;
            list.ItemAt(index, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAtCopy_WithComparison_NegativeIndex_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int index = -1;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.ItemAt(index, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAtCopy_IndexTooBig_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int index = 0;
            list.ItemAt(index);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAtCopy_WithComparer_IndexTooBig_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int index = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            list.ItemAt(index, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestItemAtCopy_WithComparison_IndexTooBig_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            int index = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.ItemAt(index, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAtCopy_NullComparer_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>() { 1 }.ToSublist();
            int index = 0;
            IComparer<int> comparer = null;
            list.ItemAt(index, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAtCopy_NullComparison_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>() { 1 }.ToSublist();
            int index = 0;
            Func<int, int, int> comparison = null;
            list.ItemAt(index, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestItemAtCopy_DestinationNull_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>() { 1 }.ToSublist();
            int index = 0;
            IExpandableSublist<List<int>, int> destination = null;
            list.ItemAt(index).CopyTo(destination);
        }

        #endregion

        /// <summary>
        /// We should be able to find the item in the front even if the list is reversed.
        /// </summary>
        [TestMethod]
        public void TestItemAtCopy_Reversed_ItemInFront()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(100, i => 100 - i).AddTo(TestHelper.Populate(list));
            destination = Sublist.Generate(100, 0).AddTo(destination);
            var result = list.ItemAt(0).CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.AreEqual(1, destination[0], "The wrong item was moved to the front.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to find the item in the back even if the list is reversed.
        /// </summary>
        [TestMethod]
        public void TestItemAtCopy_Reversed_ItemInBack()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(100, i => 100 - i).AddTo(TestHelper.Populate(list));
            destination = Sublist.Generate(100, 0).AddTo(destination);
            var result = list.ItemAt(list.Count - 1, Comparer<int>.Default).CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.AreEqual(100, destination[destination.Count - 1], "The wrong item was moved to the back.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to find the item in the middle even if the list is reversed.
        /// </summary>
        [TestMethod]
        public void TestItemAtCopy_Reversed_ItemInMiddle()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(100, i => 100 - i).AddTo(TestHelper.Populate(list));
            destination = Sublist.Generate(100, 0).AddTo(destination);
            var result = list.ItemAt(49, Comparer<int>.Default).CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.AreEqual(50, destination[49], "The wrong item was moved to the middle.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to find the item in the front even if the list is pipe-organed.
        /// </summary>
        [TestMethod]
        public void TestItemAtCopy_PipeOrganed_ItemInFront()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(50, i => i * 2).AddTo(TestHelper.Populate(list));
            list = Sublist.Generate(50, i => 100 - (i * 2 + 1)).AddTo(TestHelper.Populate(list));
            destination = Sublist.Generate(100, 0).AddTo(destination);
            var result = list.ItemAt(0, Comparer<int>.Default.Compare).CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.AreEqual(0, destination[0], "The wrong item was moved to the front.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to find the item in the back even if the list is pipe-organed.
        /// </summary>
        [TestMethod]
        public void TestItemAtCopy_PipeOrganed_ItemInBack()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(50, i => i * 2).AddTo(TestHelper.Populate(list));
            list = Sublist.Generate(50, i => 100 - (i * 2 + 1)).AddTo(TestHelper.Populate(list));
            destination = Sublist.Generate(100, 0).AddTo(destination);
            var result = list.ItemAt(list.Count - 1, Comparer<int>.Default.Compare).CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.AreEqual(99, destination[destination.Count - 1], "The wrong item was moved to the back.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to find the item in the middle even if the list is pipe-organed.
        /// </summary>
        [TestMethod]
        public void TestItemAtCopy_PipeOrganed_ItemInMiddle()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(50, i => i * 2).AddTo(TestHelper.Populate(list));
            list = Sublist.Generate(50, i => 100 - (i * 2 + 1)).AddTo(TestHelper.Populate(list));
            destination = Sublist.Generate(100, 0).AddTo(destination);
            var result = list.ItemAt(49, Comparer<int>.Default.Compare).CopyTo(destination);

            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.AreEqual(49, destination[49], "The wrong item was moved to the middle.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is smaller than the source, but the desired item is smaller
        /// than the size, the algorithms assumes that we are only concerned with the front
        /// of the source.
        /// </summary>
        [TestMethod]
        public void TestItemAtCopy_DestinationSmallerThanSource_DestinationLargerThanIndex_RearrangesFrontOfSource()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(100, i => 99 - i).AddTo(TestHelper.Populate(list));
            destination = Sublist.Generate(50, 0).AddTo(destination);
            var result = list.ItemAt(49).CopyTo(destination);  // copies 99 -> 49, finds 49th position

            Assert.AreEqual(destination.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.AreEqual(99, destination[49], "The wrong item was moved to the middle.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is smaller than the source, and the desired item is past the
        /// end of the list, the algorithms simply performs a copy.
        /// </summary>
        [TestMethod]
        public void TestItemAtCopy_DestinationSmallerThanSource_DestinationSmallerThanIndex_NoRearrange()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            list = Sublist.Generate(100, i => 99 - i).AddTo(TestHelper.Populate(list));
            destination = Sublist.Generate(50, 0).AddTo(destination);
            var result = list.ItemAt(50).CopyTo(destination);  // copies 99 -> 49, but can't move item

            Assert.AreEqual(destination.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");
            Assert.IsTrue(destination.IsEqualTo(list.Nest(0, destination.Count)), "The items were not copied.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
