using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the FindSequence methods.
    /// </summary>
    [TestClass]
    public class FindSequenceTester
    {
        #region Real World Example

        /// <summary>
        /// We'll look within a randomly generated string for a randomly generated sequence.
        /// </summary>
        [TestMethod]
        public void TestFindSequence_SearchText()
        {
            Random random = new Random();
            char[] keys = { 'A', 'C', 'G', 'T' };

            // build large, random list
            var list1 = new List<char>(1000);
            Sublist.AddGenerated(list1.ToSublist(), 1000, i => keys[random.Next(4)]);

            // build small search pattern
            var list2 = new char[5];
            Sublist.CopyGenerated(list2.ToSublist(), () => keys[random.Next(4)]);

            // force a find
            Sublist.AddTo(list2.ToSublist(), list1.ToSublist(random.Next(list1.Count + 1), 0));

            var result = Sublist.FindSequence(list1.ToSublist(), list2.ToSublist());
            Assert.IsTrue(result.Exists, "The sequence was not found.");
            Assert.IsTrue(list1.Count - result.Index > list2.Length, "The index was too close to the end.");

            var actual = list1.ToSublist(result.Index, list2.Length);
            Assert.IsTrue(Sublist.AreEqual(list2.ToSublist(), actual), "The index was not pointing to the beginning of a matching sequence.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindSequence_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist.FindSequence(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindSequence_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.FindSequence(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindSequence_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.FindSequence(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindSequence_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist.FindSequence(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindSequence_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.FindSequence(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindSequence_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.FindSequence(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindSequence_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.FindSequence(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFindSequence_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.FindSequence(list1, list2, comparison);
        }

        #endregion

        /// <summary>
        /// If the sequence does not exist, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestFindSequence_DoesNotExist_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 0, 1, 0, 1, 2, 0, 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4 });
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;

            var result = Sublist.FindSequence(list1, list2, comparison);
            Assert.AreEqual(list1.Count, result.Index, "The index was wrong.");
            Assert.IsFalse(result.Exists, "The sequence should not have been found.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the sequence appears at the beginning, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestFindSequence_InFront_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4 });
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;

            var result = Sublist.FindSequence(list1, list2, comparer);
            Assert.AreEqual(0, result.Index, "The index was wrong.");
            Assert.IsTrue(result.Exists, "The sequence should have been found.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the sequence appears at the end, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestFindSequence_InBack_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;

            var result = Sublist.FindSequence(list1, list2, comparison);
            Assert.AreEqual(1, result.Index, "The index was wrong.");
            Assert.IsTrue(result.Exists, "The sequence should have been found.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the sequence appears in the middle, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestFindSequence_InMiddle_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });

            var result = Sublist.FindSequence(list1, list2);
            Assert.AreEqual(1, result.Index, "The index was wrong.");
            Assert.IsTrue(result.Exists, "The sequence should have been found.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the lists are equal, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestFindSequence_ListsEqual_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });

            var result = Sublist.FindSequence(list, list);
            Assert.AreEqual(0, result.Index, "The index was wrong.");
            Assert.IsTrue(result.Exists, "The sequence should have been found.");

            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
