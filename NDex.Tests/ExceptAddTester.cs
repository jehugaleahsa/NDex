using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the ExceptAdd methods.
    /// </summary>
    [TestClass]
    public class ExceptAddTester
    {
        #region Real World Example

        /// <summary>
        /// Say we wanted to get a list of odd numbers that aren't divisible by three.
        /// </summary>
        [TestMethod]
        public void TestExceptAdd_EliminateSetIntersections()
        {
            Random random = new Random();

            // build a list of odd numbers
            var odds = new List<int>(100);
            Sublist.Generate(100, i => random.Next(0, 50) * 2 + 1).AddTo(odds.ToSublist());  // max of 99 (49 * 2 + 1)

            // build a list of all of the numbers divisible by three
            var threes = new List<int>(34);
            Sublist.Generate(34, i => i * 3).AddTo(threes.ToSublist());  // max of 99

            // sort and eliminate duplicates from odd list, to make it an ordered set
            odds.ToSublist(odds.ToSublist().MakeSet().InPlace()).Clear();

            // now remove the threes and make sure none remain
            var destination = new List<int>(100);

            odds.ToSublist().Except(threes.ToSublist()).AddTo(destination.ToSublist());
            Assert.IsFalse(destination.ToSublist().Find(i => i % 3 == 0), "Some numbers were still divisible by three.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptAdd_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            list1.Except(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptAdd_WithComparer_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Except(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptAdd_WithComparison_NullList1_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Except(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptAdd_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            list1.Except(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptAdd_WithComparer_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Except(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptAdd_WithComparison_NullList2_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Except(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptAdd_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            list1.Except(list2).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptAdd_WithComparer_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.Except(list2, comparer).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptAdd_WithComparison_NullDestination_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Except(list2, comparison).AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptAdd_NullComparer_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list1.Except(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptAdd_NullComparison_Throws()
        {
            IReadOnlySublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list1.Except(list2, comparison);
        }

        #endregion

        /// <summary>
        /// Nothing should be copied if the first and second list have the same items.
        /// </summary>
        [TestMethod]
        public void TestExceptAdd_ItemsInFirstNotInSecond_RemainingAdded()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>());
            destination = list1.Except(list2).AddTo(destination);
            int[] expected = { 3 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Nothing should be copied if the first and second list have the same items.
        /// </summary>
        [TestMethod]
        public void TestExceptAdd_WithComparer_DifferenceEqualsNullSet()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>());
            IComparer<int> comparer= Comparer<int>.Default;
            list1.Except(list2, comparer).AddTo(destination);
            int[] expected = { };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Nothing should be copied if the first and second list have the same items.
        /// </summary>
        [TestMethod]
        public void TestExceptAdd_WithComparison_DifferenceEqualsNullSet()
        {
            var list1 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3, });
            var list2 = TestHelper.WrapReadOnly(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>());
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.Except(list2, comparison).AddTo(destination);
            int[] expected = { };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
