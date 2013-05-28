using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the CompareTo methods.
    /// </summary>
    [TestClass]
    public class CompareToTester
    {
        #region Real World Example

        /// <summary>
        /// We can use compare to sort lists, just like we'd sort strings.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_ItemByItemComparison()
        {
            Random random = new Random();

            var list1 = new List<int>(10);
            Sublist.Generate(10, i => random.Next(0, 10)).AddTo(list1.ToSublist());

            var list2 = new List<int>(10);
            Sublist.Generate(10, i => random.Next(0, 10)).AddTo(list2.ToSublist());

            // we know list1 and list2 should be equal to themselves
            Assert.AreEqual(0, list1.ToSublist().CompareTo(list1.ToSublist()), "The first list did not equal itself.");
            Assert.AreEqual(0, list2.ToSublist().CompareTo(list2.ToSublist()), "The second list did not equal itself.");

            // we can use mismatch to confirm that Comparer returned the correct value
            int result = list1.ToSublist().CompareTo(list2.ToSublist());
            int difference = list1.ToSublist().Mismatch(list2.ToSublist());
            if (difference == list1.Count)
            {
                Assert.AreEqual(0, result, "Mismatch found no differences, but CompareTo did not return zero.");
            }
            else
            {
                int first = list1[difference];
                int second = list2[difference];
                int expected = Comparer<int>.Default.Compare(first, second);
                Assert.AreEqual(expected, result, "The mismatching items did not compare the same as the result of CompareTo.");
            }
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompareTo_NullList1_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            list1.CompareTo(list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompareTo_WithComparer_NullList1_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IComparer<int> comparer = Comparer<int>.Default;
            list1.CompareTo(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompareTo_WithComparison_NullList1_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = null;
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.CompareTo(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompareTo_NullList2_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            list1.CompareTo(list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompareTo_WithComparer_NullList2_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list1.CompareTo(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompareTo_WithComparison_NullList2_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list1.CompareTo(list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompareTo_NullComparer_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list1.CompareTo(list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompareTo_NullComparison_Throws()
        {
            IExpandableSublist<List<int>, int> list1 = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> list2 = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list1.CompareTo(list2, comparison);
        }

        #endregion

        /// <summary>
        /// If both lists are empty, the result should zero.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_BothListsEmpty_ReturnsZero()
        {
            var list = TestHelper.Wrap(new List<int>());

            int result = list.CompareTo(list);

            Assert.AreEqual(0, result, "The result was not zero.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// If the items are all equal in both lists, but the first list is shorter, a negative should be returned.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_FirstListShorter_ReturnsNegative()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });

            int result = list1.CompareTo(list2);

            Assert.IsTrue(result < 0, "The result was not negative.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the items are all equal in both lists, but the first list is longer, a positive should be returned.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_FirstListLonger_ReturnsPositive()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });

            int result = list1.CompareTo(list2);

            Assert.IsTrue(result > 0, "The result was not positive.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If the items are all equal in both lists and both lists are the same size, a zero should be returned.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_ListsEqual_ReturnsZero()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });

            int result = list1.CompareTo(list2);

            Assert.AreEqual(0, result, "The result was not zero.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If an item in the first list is smaller than an item in the second list, a negative should be returned.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_ItemSmaller_ReturnsNegative()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 2 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });

            int result = list1.CompareTo(list2);

            Assert.IsTrue(result < 0, "The result was not negative.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If an item in the first list is larger than an item in the second list, a positive should be returned.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_ItemLarger_ReturnsPositive()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 4 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });

            int result = list1.CompareTo(list2);

            Assert.IsTrue(result > 0, "The result was not positive.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// We'll use a comparer to reverse the results.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_WithComparer()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 4 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });

            int result = list1.CompareTo(list2, Comparer<int>.Default);

            Assert.IsTrue(result > 0, "The result was not positive.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// We'll use a comparison to reverse the results.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_WithComparison_Reversed()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 4 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });

            int result = list1.CompareTo(list2, (x, y) => Comparer<int>.Default.Compare(y, x));

            Assert.IsTrue(result < 0, "The result was not negative.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }
    }
}
