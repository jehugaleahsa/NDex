using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    /// <summary>
    /// Tests the Add methods.
    /// </summary>
    [TestClass]
    public class AddTester
    {
        #region Real World Examples

        /// <summary>
        /// Needing to concatenate two lists is common.
        /// </summary>
        [TestMethod]
        public void TestAdd_ConcatenateLists()
        {
            Random random = new Random();

            // build a list of values to concatenate.
            var list = new List<int>();
            Sublist.AddGenerated(list.ToSublist(), 50, i => random.Next(0, 100));

            // build a destination list
            var destination = new List<int>();
            Sublist.AddGenerated(destination.ToSublist(), 50, i => random.Next(0, 100));

            Sublist.Add(list.ToSublist(), destination.ToSublist());

            Assert.IsTrue(Sublist.AreEqual(destination.ToSublist(50), list.ToSublist()), "The items were not added as expected.");
        }

        /// <summary>
        /// Needing to insert a range at the beginning of a list is also common. Note that inserting
        /// into the beginning of a list is inefficient.
        /// </summary>
        [TestMethod]
        public void TestAdd_InsertAtBeginngin()
        {
            Random random = new Random();

            // build a list of values to concatenate.
            var list = new List<int>();
            Sublist.AddGenerated(list.ToSublist(), 50, i => random.Next(0, 100));

            // build a destination list
            var destination = new List<int>();
            Sublist.AddGenerated(destination.ToSublist(), 50, i => random.Next(0, 100));

            // destination.ToSublist(0,0) represents an empty range at the beginning
            Sublist.Add(list.ToSublist(), destination.ToSublist(0, 0));

            Assert.IsTrue(Sublist.AreEqual(destination.ToSublist(0, 50), list.ToSublist()), "The items were not added as expected.");
        }


        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAdd_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.Add(list, destination);
        }

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAdd_NullCollection_Throws()
        {
            IEnumerable<int> collection = null;
            var sublist = new List<int>().ToSublist();
            Sublist.Add(collection, sublist);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAdd_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.Add(list, destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAdd_IEnumerable_NullDestination_Throws()
        {
            IEnumerable<int> source = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.Add(source, destination);
        }

        #endregion

        /// <summary>
        /// Add actually inserts. If there are trailing items, they should be unaffected.
        /// </summary>
        [TestMethod]
        public void TestAdd_LeavesTrailingItemsAlone()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 6, 7, 8, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });
            destination = Sublist.Add(list, destination);
            Assert.AreEqual(9, destination.Count, "The destination Sublist was not grown as expected.");
            int[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// The items in the given collection should be added to the list.
        /// </summary>
        [TestMethod]
        public void TestAdd_AddsItemsToBackOfList()
        {
            var source = new List<int>() { 4, 5, 6 };
            var destination = new List<int>() { 1, 2, 3 }.ToSublist();
            destination = Sublist.Add(source, destination);
            Assert.AreEqual(6, destination.Count, "The size of the sublist was not adjusted.");
            var expected = new List<int>() { 1, 2, 3, 4, 5, 6 }.ToSublist();
            Assert.IsTrue(Sublist.AreEqual(expected, destination), "The items were not added as expected.");
        }

        /// <summary>
        /// The items in the given collection should be added to the list. Since the size
        /// of the collection can't be determined up-front, a rotation will take place.
        /// </summary>
        [TestMethod]
        public void TestAdd_IEnumerableSource_AddsItemsToBackOfList()
        {
            var source = Enumerable.Range(4, 3);
            var destination = new List<int>() { 1, 2, 3 }.ToSublist();
            destination = Sublist.Add(source, destination);
            Assert.AreEqual(6, destination.Count, "The size of the sublist was not adjusted.");
            var expected = new List<int>() { 1, 2, 3, 4, 5, 6 }.ToSublist();
            Assert.IsTrue(Sublist.AreEqual(expected, destination), "The items were not added as expected.");
        }

        /// <summary>
        /// The items in the given collection should be added to the middle of the underlying list.
        /// </summary>
        [TestMethod]
        public void TestAdd_MiddleOfList_AddsItemsToBackOfList()
        {
            var source = new List<int>() { 3, 4 };
            var list = new List<int>() { 1, 2, 5, 6 };
            var destination = list.ToSublist(2, 0);
            destination = Sublist.Add(source, destination);
            var expected = new List<int>() { 1, 2, 3, 4, 5, 6 }.ToSublist();
            Assert.AreEqual(6, list.Count, "The items were not added to the original list.");
            Assert.AreEqual(2, destination.Count, "The size of the sublist was not adjusted.");
            Assert.IsTrue(Sublist.AreEqual(expected, list.ToSublist()), "The items were not added as expected.");
        }

        /// <summary>
        /// The items in the given collection should be added to the middle of the underlying list.
        /// Since the size of the collection can't be determined up-front, a rotation will take place.
        /// </summary>
        [TestMethod]
        public void TestAdd_IEnumerableSource_MiddleOfList_AddsItemsToBackOfList()
        {
            var source = Enumerable.Range(3, 2);
            var list = new List<int>() { 1, 2, 5, 6 };
            var destination = list.ToSublist(2, 0);
            destination = Sublist.Add(source, destination);
            var expected = new List<int>() { 1, 2, 3, 4, 5, 6 }.ToSublist();
            Assert.AreEqual(6, list.Count, "The items were not added to the original list.");
            Assert.AreEqual(2, destination.Count, "The size of the sublist was not adjusted.");
            Assert.IsTrue(Sublist.AreEqual(expected, list.ToSublist()), "The items were not added as expected.");
        }
    }
}
