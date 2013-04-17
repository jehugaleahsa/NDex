using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDex;
using System.Collections.Generic;

namespace NDex.Test
{
    /// <summary>
    /// Tests the ContainsSequence methods.
    /// </summary>
    [TestClass]
    public class ContainsSequenceTester
    {
        #region Real World Example

        /// <summary>
        /// We'll look within a randomly generated string for a randomly generated sequence.
        /// </summary>
        [TestMethod]
        public void TestContainsSequence_SearchText()
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

            Assert.IsTrue(Sublist.ContainsSequence(list1.ToSublist(), list2.ToSublist()), "Should have found the subsequence.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsSequence_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist.ContainsSequence(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsSequence_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.ContainsSequence(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsSequence_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.ContainsSequence(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsSequence_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist.ContainsSequence(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsSequence_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sublist.ContainsSequence(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsSequence_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;
            Sublist.ContainsSequence(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsSequence_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IEqualityComparer<int> comparer = null;
            Sublist.ContainsSequence(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestContainsSequence_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, bool> comparison = null;
            Sublist.ContainsSequence(list1, list2, comparison);
        }

        #endregion

        /// <summary>
        /// If the sequence does not exist, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestContainsSequence_DoesNotExist_ReturnsFalse()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 0, 1, 0, 1, 2, 0, 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4 });
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;

            bool result = Sublist.ContainsSequence(list1, list2, comparison);
            Assert.IsFalse(result, "Incorrectly found the sequence.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the sequence appears at the beginning, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestContainsSequence_InFront_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4 });
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;

            bool result = Sublist.ContainsSequence(list1, list2, comparer);
            Assert.IsTrue(result, "Did not find the sequence.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the sequence appears at the end, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestContainsSequence_InBack_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            Func<int, int, bool> comparison = EqualityComparer<int>.Default.Equals;

            bool result = Sublist.ContainsSequence(list1, list2);
            Assert.IsTrue(result, "Did not find the sequence.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the sequence appears in the middle, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestContainsSequence_InMiddle_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 0, 1, 2, 3, 4, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });

            bool result = Sublist.ContainsSequence(list1, list2);
            Assert.IsTrue(result, "Did not find the sequence.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the lists are equal, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestContainsSequence_ListsEqual_ReturnsTrue()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });

            bool result = Sublist.ContainsSequence(list, list);
            Assert.IsTrue(result, "Did not find the sequence.");

            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
