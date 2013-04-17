using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;

namespace NDex.Test
{
    /// <summary>
    /// Tests the CopyUnique methods.
    /// </summary>
    [TestClass]
    public class CopyUniqueTester
    {
        #region Real World Example

        /// <summary>
        /// Often when working with set operations, you must first create a set from a list. This can be done
        /// by guaranteeing the uniqueness of each field.
        /// </summary>
        [TestMethod]
        public void TestCopyUnique_CreateSet()
        {
            Random random = new Random();

            // build a list of random numbers
            var list = new List<int>(100);
            Sublist.Grow(list, 100, () => random.Next(100));

            // unique requires that elements be sorted
            Sublist.QuickSort(list.ToSublist());

            // now we create a set from the list
            var destination = new List<int>(100);
            Sublist.Grow(destination, 100, 0);
            int result = Sublist.CopyUnique(list.ToSublist(), destination.ToSublist());
            destination.RemoveRange(result, destination.Count - result); // remove dangling elements

            // check that we have a valid set
            bool isSet = Sublist.IsSet(destination.ToSublist());
            Assert.IsTrue(isSet, "The destinatin was not a valid set.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnique_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopyUnique(list, destination);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnique_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.CopyUnique(list, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnique_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.CopyUnique(list, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnique_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.CopyUnique(list, destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnique_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.CopyUnique(list, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnique_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.CopyUnique(list, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnique_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.CopyUnique(list, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyUnique_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.CopyUnique(list, destination, comparison);
        }

        #endregion

        /// <summary>
        /// If all of the items are the same, then only one value should go to the destination.
        /// </summary>
        [TestMethod]
        public void TestCopyUnique_AllValuesTheSame_OnlyCopiesOne()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 1, 1, 1, 1, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            CopyResult result = Sublist.CopyUnique(list, destination, EqualityComparer<int>.Default.Equals);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(1, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = { 1, 0, 0, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If there are more unique values than space in the destination, the copy ends prematurely.
        /// </summary>
        [TestMethod]
        public void TestCopyUnique_DestinationTooSmallForUniqueValues_StopsPrematurely()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, });

            CopyResult result = Sublist.CopyUnique(list, destination, EqualityComparer<int>.Default);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = { 1, 2 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The values were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is empty, then nothing should happen.
        /// </summary>
        [TestMethod]
        public void TestCopyUnique_DestinationEmpty_DoesNothing()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, });
            var destination = TestHelper.Wrap(new List<int>());

            CopyResult result = Sublist.CopyUnique(list, destination);
            Assert.AreEqual(0, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the list is empty, then nothing should happen.
        /// </summary>
        [TestMethod]
        public void TestCopyUnique_ListEmpty_DoesNothing()
        {
            var list = TestHelper.Wrap(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            CopyResult result = Sublist.CopyUnique(list, destination);
            Assert.AreEqual(0, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we run out of space in the destination, the algorithm should keep searching for the next
        /// unique value. This way the source offset of the result can be used to start the next search.
        /// </summary>
        [TestMethod]
        public void TestCopyUnique_DestinationTooSmall_FindsNextUniqueValue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 2, 2, 3, 4 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });

            CopyResult result = Sublist.CopyUnique(list, destination);
            Assert.AreEqual(4, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
