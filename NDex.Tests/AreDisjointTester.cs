using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the AreDisjoint methods.
    /// </summary>
    [TestClass]
    public class AreDisjointTester
    {
        #region Realistic Example

        /// <summary>
        /// We should be able to verify that even and odd numbers are mutually exclusive sets.
        /// </summary>
        [TestMethod]
        public void TestAreDisjoint_EvensAndOdds_AreDisjoint()
        {
            Random random = new Random();

            // build even set
            var evens = new List<int>();
            Sublist.Generate(100, i => random.Next(49) * 2).AddTo(evens.ToSublist()); // can't exceed 98
            
            // build odd set
            var odds = new List<int>();
            Sublist.Generate(100, i => random.Next(49) * 2 + 1).AddTo(odds.ToSublist()); // can't exceed 99

            // make sure sets are sorted using the same comparison
            Sublist.Clear(evens.ToSublist(Sublist.MakeSet(odds.ToSublist())));
            Sublist.Clear(odds.ToSublist(Sublist.MakeSet(odds.ToSublist())));

            // check that they are disjoint
            bool result = Sublist.IsDisjoint(evens.ToSublist(), odds.ToSublist());
            Assert.IsTrue(result, "Incorrectly found overlap among even and odd numbers.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreDisjoint_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist.IsDisjoint(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreDisjoint_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.IsDisjoint(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreDisjoint_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.IsDisjoint(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreDisjoint_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist.IsDisjoint(list1, list2);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreDisjoint_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.IsDisjoint(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreDisjoint_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.IsDisjoint(list1, list2, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreDisjoint_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            IComparer<int> comparer = null;
            Sublist.IsDisjoint(list1, list2, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAreDisjoint_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.IsDisjoint(list1, list2, comparison);
        }

        #endregion

        /// <summary>
        /// We want to ensure that lists containing equivilent items are not disjoint.
        /// </summary>
        [TestMethod]
        public void TestAreDisjoint_DifferentTypes_ShareItems_ReturnsFalse()
        {
            var odds = TestHelper.Wrap(new List<int> { 1, 3, 5, });
            var primes = TestHelper.Wrap(new List<int> { 2, 3, 5 });
            bool result = Sublist.IsDisjoint(odds, primes, Comparer<int>.Default.Compare);
            Assert.IsFalse(result, "The lists should have shared items.");
            TestHelper.CheckHeaderAndFooter(odds);
            TestHelper.CheckHeaderAndFooter(primes);
        }

        /// <summary>
        /// If either list is empty, the result should be true.
        /// </summary>
        [TestMethod]
        public void TestAreDisjoint_List1Empty_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int> { });
            var list2 = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            bool result = Sublist.IsDisjoint(list1, list2, Comparer<int>.Default);
            Assert.IsTrue(result, "Items cannot be shared with an empty list.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }

        /// <summary>
        /// If either list is empty, the result should be true.
        /// </summary>
        [TestMethod]
        public void TestAreDisjoint_List2Empty_ReturnsTrue()
        {
            var list1 = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int> { });
            bool result = Sublist.IsDisjoint(list1, list2);
            Assert.IsTrue(result, "Items cannot be shared with an empty list.");
            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
        }
    }
}
