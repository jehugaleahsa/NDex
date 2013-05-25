using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the UpperBound methods.
    /// </summary>
    [TestClass]
    public class UpperBoundTester
    {
        #region Real World Example

        /// <summary>
        /// UpperBound is useful for finding the index past a value in a sorted list.
        /// We'll use UpperBound to build a set.
        /// </summary>
        [TestMethod]
        public void TestUpperBound_BuildSet()
        {
            Random random = new Random();

            // build a list
            var list = new List<int>(100);
            Sublist.Generate(100, i => random.Next(100)).AddTo(list.ToSublist());

            // only add unique items in sorted order
            var set = new List<int>();
            foreach (int value in list)
            {
                int index = set.ToSublist().UpperBound(value);
                if (index == 0 || set[index - 1] != value)
                {
                    set.Insert(index, value);
                }
            }

            // check that all items are present, sorted and unique
            list.ToSublist().Sort().InPlace();
            Assert.IsTrue(set.ToSublist().IsSorted(), "The set is not sorted.");
            bool hasValues = set.ToSublist().IsSubset(list.ToSublist());
            Assert.IsTrue(hasValues, "Not all of the values were copied.");
            Assert.IsFalse(set.ToSublist().FindDuplicates(), "A duplicate was found.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpperBound_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            list.UpperBound(value);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpperBound_WithComparer_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            list.UpperBound(value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpperBound_WithComparison_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            int value = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            list.UpperBound(value, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpperBound_NullComparer_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int value = 0;
            IComparer<int> comparer = null;
            list.UpperBound(value, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpperBound_NullComparison_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            int value = 0;
            Func<int, int, int> comparison = null;
            list.UpperBound(value, comparison);
        }

        #endregion

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestUpperBound_InMiddle_Exists()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            decimal value = 2;
            int index = list.UpperBound(value, (i, d) => Comparer<decimal>.Default.Compare(i, d));
            Assert.AreEqual(2, index, "The value was not found at the expected index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestUpperBound_InMiddle_Missing()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            decimal value = 2.5m;
            int index = list.UpperBound(value, (i, d) => Comparer<decimal>.Default.Compare(i, d));
            Assert.AreEqual(2, index, "The value was found or expected at the wrong index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestUpperBound_AtStart_Exists()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            decimal value = 1;
            int index = list.UpperBound(value, (i, d) => Comparer<decimal>.Default.Compare(i, d));
            Assert.AreEqual(1, index, "The value was not found at the expected index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestUpperBound_AtStart_Missing()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            decimal value = .5m;
            int index = list.UpperBound(value, (i, d) => Comparer<decimal>.Default.Compare(i, d));
            Assert.AreEqual(0, index, "The value was found or expected at the wrong index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestUpperBound_AtEnd_Exists()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            int value = 3;
            int index = list.UpperBound(value, Comparer<int>.Default);
            Assert.AreEqual(3, index, "The value was not found at the expected index.");
            TestHelper.CheckHeaderAndFooter(list);
        }

        /// <summary>
        /// We can find a value even if it is a different type than the items in the list.
        /// </summary>
        [TestMethod]
        public void TestUpperBound_WithComparer_AtEnd_Missing()
        {
            var list = TestHelper.Wrap(new List<int> { 1, 2, 3 });
            int value = 4;
            int index = list.UpperBound(value, Comparer<int>.Default);
            Assert.AreEqual(3, index, "The value was found or expected at the wrong index.");
            TestHelper.CheckHeaderAndFooter(list);
        }
    }
}
