using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the AddTo methods.
    /// </summary>
    [TestClass]
    public class AddToTester
    {
        #region Real World Examples

        /// <summary>
        /// Needing to concatenate two lists is common.
        /// </summary>
        [TestMethod]
        public void TestAddTo_ConcatenateLists()
        {
            Random random = new Random();

            // build a list of values to concatenate.
            var list = new List<int>();
            Sublist.Generate(50, i => random.Next(0, 100)).AddTo(list.ToSublist());

            // build a destination list
            var destination = new List<int>();
            Sublist.Generate(50, i => random.Next(0, 100)).AddTo(destination.ToSublist());

            list.ToSublist().AddTo(destination.ToSublist());

            Assert.IsTrue(destination.ToSublist(50).IsEqualTo(list.ToSublist()), "The items were not added as expected.");
        }

        /// <summary>
        /// Needing to insert a range at the beginning of a list is also common. Note that inserting
        /// into the beginning of a list is inefficient.
        /// </summary>
        [TestMethod]
        public void TestAddTo_InsertAtBeginngin()
        {
            Random random = new Random();

            // build a list of values to concatenate.
            var list = new List<int>();
            Sublist.Generate(50, i => random.Next(0, 100)).AddTo(list.ToSublist());

            // build a destination list
            var destination = new List<int>();
            Sublist.Generate(50, i => random.Next(0, 100)).AddTo(destination.ToSublist());

            // destination.ToSublist(0,0) represents an empty range at the beginning
            list.ToSublist().AddTo(destination.ToSublist(0, 0));

            Assert.IsTrue(destination.ToSublist(0, 50).IsEqualTo(list.ToSublist()), "The items were not added as expected.");

            list.ToSublist().Zip(list.ToSublist(), (i, j) => i + j);
        }


        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddTo_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            list.AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddTo_NullCollection_Throws()
        {
            IEnumerable<int> collection = null;
            var sublist = new List<int>().ToSublist();
            collection.AddTo(sublist);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddTo_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            list.AddTo(destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddTo_IEnumerable_NullDestination_Throws()
        {
            IEnumerable<int> source = new List<int>();
            Sublist<List<int>, int> destination = null;
            source.AddTo(destination);
        }

        #endregion

        /// <summary>
        /// AddTo actually inserts. If there are trailing items, they should be unaffected.
        /// </summary>
        [TestMethod]
        public void TestAddTo_LeavesTrailingItemsAlone()
        {
            var list = TestHelper.Wrap(new List<int>() { 5, 6, 7, 8, 9 });
            var destination = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4 });
            destination = list.AddTo(destination);
            Assert.AreEqual(9, destination.Count, "The destination Sublist was not grown as expected.");
            int[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Assert.IsTrue(expected.ToSublist().IsEqualTo(destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// The items in the given collection should be added to the list.
        /// </summary>
        [TestMethod]
        public void TestAddTo_AddsItemsToBackOfList()
        {
            var source = new List<int>() { 4, 5, 6 };
            var destination = new List<int>() { 1, 2, 3 }.ToSublist();
            destination = source.AddTo(destination);
            Assert.AreEqual(6, destination.Count, "The size of the sublist was not adjusted.");
            var expected = new List<int>() { 1, 2, 3, 4, 5, 6 }.ToSublist();
            Assert.IsTrue(expected.IsEqualTo(destination), "The items were not added as expected.");
        }

        /// <summary>
        /// The items in the given collection should be added to the list. Since the size
        /// of the collection can't be determined up-front, a rotation will take place.
        /// </summary>
        [TestMethod]
        public void TestAddTo_IEnumerableSource_AddsItemsToBackOfList()
        {
            var source = Enumerable.Range(4, 3);
            var destination = new List<int>() { 1, 2, 3 }.ToSublist();
            destination = source.AddTo(destination);
            Assert.AreEqual(6, destination.Count, "The size of the sublist was not adjusted.");
            var expected = new List<int>() { 1, 2, 3, 4, 5, 6 }.ToSublist();
            Assert.IsTrue(expected.IsEqualTo(destination), "The items were not added as expected.");
        }

        /// <summary>
        /// The items in the given collection should be added to the middle of the underlying list.
        /// </summary>
        [TestMethod]
        public void TestAddTo_MiddleOfList_AddsItemsToBackOfList()
        {
            var source = new List<int>() { 3, 4 };
            var list = new List<int>() { 1, 2, 5, 6 };
            var destination = list.ToSublist(2, 0);
            destination = source.AddTo(destination);
            var expected = new List<int>() { 1, 2, 3, 4, 5, 6 }.ToSublist();
            Assert.AreEqual(6, list.Count, "The items were not added to the original list.");
            Assert.AreEqual(2, destination.Count, "The size of the sublist was not adjusted.");
            Assert.IsTrue(expected.IsEqualTo(list.ToSublist()), "The items were not added as expected.");
        }

        /// <summary>
        /// The items in the given collection should be added to the middle of the underlying list.
        /// Since the size of the collection can't be determined up-front, a rotation will take place.
        /// </summary>
        [TestMethod]
        public void TestAddTo_IEnumerableSource_MiddleOfList_AddsItemsToBackOfList()
        {
            var source = Enumerable.Range(3, 2);
            var list = new List<int>() { 1, 2, 5, 6 };
            var destination = list.ToSublist(2, 0);
            destination = source.AddTo(destination);
            var expected = new List<int>() { 1, 2, 3, 4, 5, 6 }.ToSublist();
            Assert.AreEqual(6, list.Count, "The items were not added to the original list.");
            Assert.AreEqual(2, destination.Count, "The size of the sublist was not adjusted.");
            Assert.IsTrue(expected.IsEqualTo(list.ToSublist()), "The items were not added as expected.");
        }
    }
}
