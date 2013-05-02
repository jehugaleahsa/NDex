using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    /// <summary>
    /// Tests the AddIf methods.
    /// </summary>
    [TestClass]
    public class AddIfTester
    {
        #region Real World Examples

        /// <summary>
        /// We will use copy if to remove items we don't want from a list.
        /// </summary>
        [TestMethod]
        public void TestAddIf_CopyEvenItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6 };
            var destination = new List<int>();

            // only keep the even items
            Sublist.AddIf(list.ToSublist(), destination.ToSublist(), item => item % 2 == 0);

            int[] expected = { 2, 4, 6 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination.ToSublist()), "The items were not where they were expected.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the source list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddIf_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, bool> predicate = i => true; // always true
            Sublist.AddIf(list, destination, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the destination list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddIf_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, bool> predicate = i => true; // always true
            Sublist.AddIf(list, destination, predicate);
        }

        /// <summary>
        /// An exception should be thrown if the predicate delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddIf_NullPredicate_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, bool> predicate = null; // always true
            Sublist.AddIf(list, destination, predicate);
        }

        #endregion

        /// <summary>
        /// If the destination is bigger than the source, the result should appear in the middle of the destination.
        /// </summary>
        [TestMethod]
        public void TestAddIf_CopyEvenItems_AddsToDestination()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, });
            var destination = TestHelper.Wrap(new List<int>());
            Func<int, bool> predicate = i => i % 2 == 0; // always true
            destination = Sublist.AddIf(list, destination, predicate);
            int[] expected = { 2 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not added as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}
