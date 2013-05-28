using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the MakeSetAdd methods.
    /// </summary>
    [TestClass]
    public class MakeSetAddTester
    {
        #region Real World Example

        /// <summary>
        /// Regardless of the input, we can make it a set, so long as the type is sortable.
        /// </summary>
        [TestMethod]
        public void TestMakeSetAdd_FromRandomList()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next()).AddTo(list.ToSublist());
            var destination = new List<int>();

            // make it a set and remove trailing garbage
            list.ToSublist().MakeSet().AddTo(destination.ToSublist());

            // the set should be sorted and have all unique values
            Assert.IsTrue(destination.ToSublist().IsSorted(), "The list was not sorted.");
            Assert.IsFalse(destination.ToSublist().FindDuplicates(), "The list had duplicates.");
        }

        #endregion

        #region Argument Checking
        
        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSetAdd_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            list.MakeSet();
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSetAdd_WithComparer_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            IComparer<int> comparer = Comparer<int>.Default;
            list.MakeSet(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSetAdd_WithComparison_NullList_Throws()
        {
            IReadOnlySublist<List<int>, int> list = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.MakeSet(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSetAdd_NullComparer_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IComparer<int> comparer = null;
            list.MakeSet(comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSetAdd_NullComparison_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int, int> comparison = null;
            list.MakeSet(comparison);
        }

        /// <summary>
        /// An exception should be thrown if the desintation is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMakeSetAdd_DestinationNull_Throws()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>().ToSublist();
            IExpandableSublist<List<int>, int> destination = null;
            list.MakeSet().AddTo(destination);
        }

        #endregion

        /// <summary>
        /// Since an empty list is the same as an empty set, nothing should be done.
        /// </summary>
        [TestMethod]
        public void TestMakeSetAdd_EmptyList_DoesNothing()
        {
            var list = TestHelper.WrapReadOnly(new List<int>());
            var destination = TestHelper.Wrap(new List<int>());

            destination = list.MakeSet().AddTo(destination);

            Assert.AreEqual(0, destination.Count, "Somehow the list grew.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// Since a list with one item is a list, nothing should be done.
        /// </summary>
        [TestMethod]
        public void TestMakeSetAdd_ListOfOne_DoesNothing()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = list.MakeSet().AddTo(destination);

            Assert.AreEqual(1, destination.Count, "The wrong number of items were added.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to make a set out of randomized numbers.
        /// </summary>
        [TestMethod]
        public void TestMakeSetAdd_RandomizedItems_Sorts()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 5, 3, 2, 4, });
            var destination = TestHelper.Wrap(new List<int>());

            destination = list.MakeSet((x, y) => Comparer<int>.Default.Compare(y, x)).AddTo(destination);

            int[] expected = { 5, 4, 3, 2, 1 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The list was not sorted.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to make a set out of randomized numbers with duplicates.
        /// </summary>
        [TestMethod]
        public void TestMakeSetAdd_DuplicateItems_RemovesDuplicates()
        {
            var list = TestHelper.WrapReadOnly(new List<int>() { 1, 5, 3, 2, 4, 2, 1, 5, 4 });
            var destination = TestHelper.Wrap(new List<int>());

            destination = list.MakeSet(Comparer<int>.Default).AddTo(destination);
            int[] expected = { 1, 2, 3, 4, 5, };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The duplicates were not removed.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
