using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the IndexOfSequence methods.
    /// </summary>
    [TestClass]
    public class IndexOfSequenceTester
    {
        #region Real World Example

        /// <summary>
        /// We'll look within a randomly generated string for a randomly generated sequence.
        /// </summary>
        [TestMethod]
        public void TestIndexOfSequence_SearchText()
        {
            Random random = new Random();
            char[] keys = { 'A', 'C', 'G', 'T' };

            // build large, random list
            var list1 = new List<char>(1000);
            Sublist.Grow(list1, 1000, () => keys[random.Next(4)]);

            // build small search pattern
            var list2 = new char[5];
            Sublist.Fill(list2.ToSublist(), () => keys[random.Next(4)]);

            // force a find
            Sublist.Add(list2.ToSublist(), list1.ToSublist(random.Next(list1.Count + 1), 0));

            int index = Sublist.IndexOfSequence(list1.ToSublist(), list2.ToSublist());
            Assert.AreNotEqual(list1.Count, index, "The sequence was not found.");
            Assert.IsTrue(list1.Count - index > list2.Length, "The index was too close to the end.");

            var actual = list1.ToSublist(index, list2.Length);
            Assert.IsTrue(Sublist.AreEqual(list2.ToSublist(), actual), "The index was not pointing to the beginning of a matching sequence.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfSequence_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist.IndexOfSequence(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfSequence_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.IndexOfSequence(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfSequence_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.IndexOfSequence(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfSequence_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist.IndexOfSequence(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfSequence_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.IndexOfSequence(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfSequence_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.IndexOfSequence(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfSequence_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.IndexOfSequence(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIndexOfSequence_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.IndexOfSequence(list1, list2, comparison);
        }

        #endregion

        /// <summary>
        /// If the sequence does not exist, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOfSequence_DoesNotExist_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 0, 1, 0, 1, 2, 0, 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4 });
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;

            int index = Sublist.IndexOfSequence(list1, list2, comparison);
            Assert.AreEqual(list1.Count, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the sequence appears at the beginning, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOfSequence_InFront_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4 });
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;

            int index = Sublist.IndexOfSequence(list1, list2, comparer);
            Assert.AreEqual(0, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the sequence appears at the end, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOfSequence_InBack_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;

            int index = Sublist.IndexOfSequence(list1, list2, comparison);
            Assert.AreEqual(1, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the sequence appears in the middle, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOfSequence_InMiddle_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });

            int index = Sublist.IndexOfSequence(list1, list2);
            Assert.AreEqual(1, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the lists are equal, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestIndexOfSequence_ListsEqual_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });

            int index = Sublist.IndexOfSequence(list, list);
            Assert.AreEqual(0, index, "The index was wrong.");

            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
