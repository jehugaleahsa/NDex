using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the AddReplaced methods.
    /// </summary>
    [TestClass]
    public class AddReplacedTester
    {
        #region Real World Example

        /// <summary>
        /// If you had a list of numbers, and wanted to make sure that they were all positive,
        /// you could replace the negatives with their absolute values.
        /// </summary>
        [TestMethod]
        public void TestAddReplaced_AbsoluteValue()
        {
            Random random = new Random();

            // build a list of numbers
            var list = new List<int>(100);
            Sublist.Add(Enumerable.Range(0, 100).Select(i => random.Next(-49, 50)), list.ToSublist());

            var destination = new List<int>(100);

            Sublist.AddReplaced(list.ToSublist(), destination.ToSublist(), i => i < 0, i => -i);

            Assert.IsTrue(Sublist.TrueForAll(destination.ToSublist(), i => i >= 0), "Not all values were positive.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddReplaced_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            int replacement = 0;
            Func<int, bool> predicate = i => true;
            Sublist.AddReplaced(list, destination, predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddReplaced_WithGenerator_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => true;
            Sublist.AddReplaced(list, destination, predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddReplaced_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            int replacement = 0;
            Func<int, bool> predicate = i => true;
            Sublist.AddReplaced(list, destination, predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddReplaced_WithGenerator_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = i => true;
            Sublist.AddReplaced(list, destination, predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddReplaced_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            int replacement = 0;
            Func<int, bool> predicate = null;
            Sublist.AddReplaced(list, destination, predicate, replacement);
        }

        /// <summary>
        /// An exception should be thrown if the predicate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddReplaced_WithGenerator_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int> generator = i => i;
            Func<int, bool> predicate = null;
            Sublist.AddReplaced(list, destination, predicate, generator);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddReplaced_NullGenerator_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int> generator = null;
            Func<int, bool> predicate = i => true;
            Sublist.AddReplaced(list, destination, predicate, generator);
        }

        #endregion

        /// <summary>
        /// If the predicate is satified by an item, it should be replaced.
        /// </summary>
        [TestMethod]
        public void TestAddReplaced_ItemsMatchingPredicateAreReplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, });
            var destination = TestHelper.Wrap(new List<int>());
            int replacement = 3;
            Func<int, bool> predicate = i => i % 2 == 0;
            Sublist.AddReplaced(list, destination, predicate, replacement);
            int[] expected = { 1, 3, 3, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the predicate is satified by an item, it should be replaced.
        /// </summary>
        [TestMethod]
        public void TestAddReplaced_WithGenerator_ItemsMatchingPredicateAreReplaced()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 5, });
            var destination = TestHelper.Wrap(new List<int>());
            Func<int, int> generator = i => i + 1; // make odd by adding one
            Func<int, bool> predicate = i => i % 2 == 0;
            Sublist.AddReplaced(list, destination, predicate, generator);
            int[] expected = { 1, 3, 3, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The destination did not have the expected items.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
