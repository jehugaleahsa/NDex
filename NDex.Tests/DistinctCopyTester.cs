﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the DistinctCopy methods.
    /// </summary>
    [TestClass]
    public class DistinctCopyTester
    {
        #region Real World Example

        /// <summary>
        /// Often when working with set operations, you must first create a set from a list. This can be done
        /// by guaranteeing the uniqueness of each field.
        /// </summary>
        [TestMethod]
        public void TestDistinctCopy_CreateSet()
        {
            Random random = new Random();

            // build a list of random numbers
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list.ToSublist());

            // unique requires that elements be sorted
            list.ToSublist().Sort().InPlace();

            // now we create a set from the list
            var destination = new List<int>(100);
            Sublist.Generate(100, 0).AddTo(destination.ToSublist());
            int result = list.ToSublist().Distinct().CopyTo(destination.ToSublist());
            destination.RemoveRange(result, destination.Count - result); // remove dangling elements

            // check that we have a valid set
            bool isSet = destination.ToSublist().IsSet();
            Assert.IsTrue(isSet, "The destinatin was not a valid set.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDistinctCopy_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            list.Distinct();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDistinctCopy_WithComparer_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list.Distinct(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDistinctCopy_WithComparison_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list.Distinct(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDistinctCopy_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            list.Distinct().CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDistinctCopy_WithComparer_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            list.Distinct(comparer).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDistinctCopy_WithComparison_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            list.Distinct(comparison).CopyTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDistinctCopy_NullComparer_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IEqualityComparer<int> comparer = null;
            list.Distinct(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDistinctCopy_NullComparison_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int, bool> comparison = null;
            list.Distinct(comparison);
        }

        #endregion

        /// <summary>
        /// If all of the items are the same, then only one value should go to the destination.
        /// </summary>
        [TestMethod]
        public void TestDistinctCopy_AllValuesTheSame_OnlyCopiesOne()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 1, 1, 1, 1, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            var result = list.Distinct(EqualityComparer<int>.Default.Equals).CopyTo(destination);
            Assert.AreEqual(list.Count, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(1, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = { 1, 0, 0, 0, 0 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The values were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If there are more unique values than space in the destination, the copy ends prematurely.
        /// </summary>
        [TestMethod]
        public void TestDistinctCopy_DestinationTooSmallForUniqueValues_StopsPrematurely()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, });

            var result = list.Distinct(EqualityComparer<int>.Default).CopyTo(destination);
            Assert.AreEqual(2, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            int[] expected = { 1, 2 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The values were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the destination is empty, then nothing should happen.
        /// </summary>
        [TestMethod]
        public void TestDistinctCopy_DestinationEmpty_DoesNothing()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, });
            var destination = TestHelper.Wrap(new List<int>());

            var result = list.Distinct().CopyTo(destination);
            Assert.AreEqual(0, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the list is empty, then nothing should happen.
        /// </summary>
        [TestMethod]
        public void TestDistinctCopy_ListEmpty_DoesNothing()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            var result = list.Distinct().CopyTo(destination);
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
        public void TestDistinctCopy_DestinationTooSmall_FindsNextUniqueValue()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 2, 2, 3, 4 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0 });

            var result = list.Distinct().CopyTo(destination);
            Assert.AreEqual(4, result.SourceOffset, "The source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The destination offset was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
