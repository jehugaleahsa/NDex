using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the Grow methods.
    /// </summary>
    [TestClass]
    public class GrowTester
    {
        #region Real World Example

        /// <summary>
        /// Grow is useful for quickly populating a list with a single value.
        /// Use Fill when working with an array or an already populated list.
        /// </summary>
        [TestMethod]
        public void TestGrow_FillWithValue()
        {
            List<int> list = new List<int>(100);
            Sublist.Grow(list, 100, 4);
            Assert.AreEqual(100, list.Count);
            Assert.IsTrue(Sublist.TrueForAll(list.ToSublist(), i => i == 4), "The wrong values were added.");
        }

        /// <summary>
        /// Grow is useful for quickly populating a list with a generated value.
        /// Fill can do the same thing for arrays or lists that are already populated.
        /// </summary>
        [TestMethod]
        public void TestGrow_FillWithGeneratedValue()
        {
            Random fillRandom = new Random(1);

            List<int> list = new List<int>(100);
            Sublist.Grow(list, 100, () => fillRandom.Next());
            Assert.AreEqual(100, list.Count);

            Random compareRandom = new Random(1);
            Assert.IsTrue(Sublist.TrueForAll(list.ToSublist(), i => i == compareRandom.Next()), "The wrong values were added.");
        }

        /// <summary>
        /// Grow is useful for adding numbers in sequence.
        /// Fill can do the same thing for arrays or lists that are already populated.
        /// </summary>
        [TestMethod]
        public void TestGrow_FillWithMultiplesOfTwo()
        {
            List<int> list = new List<int>(100);
            Sublist.Grow(list, 100, i => i * 2);
            Assert.AreEqual(100, list.Count);
            Assert.IsTrue(Enumerable.Range(0, 100).Select(i => i * 2).SequenceEqual(list), "The wrong values were added.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGrow_NullList_Throws()
        {
            List<int> list = null;
            int size = 0;
            int value = 0;
            Sublist.Grow(list, size, value);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGrow_WithGenerator_NullList_Throws()
        {
            List<int> list = null;
            int size = 0;
            Func<int> generator = () => 0;
            Sublist.Grow(list, size, generator);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGrow_WithIndexedGenerator_NullList_Throws()
        {
            List<int> list = null;
            int size = 0;
            Func<int, int> generator = i => i;
            Sublist.Grow(list, size, generator);
        }

        /// <summary>
        /// An exception should be thrown if the size is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGrow_NegativeSize_Throws()
        {
            List<int> list = new List<int>();
            int size = -1;
            int value = 0;
            Sublist.Grow(list, size, value);
        }

        /// <summary>
        /// An exception should be thrown if the size is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGrow_WithGenerator_NegativeSize_Throws()
        {
            List<int> list = new List<int>();
            int size = -1;
            Func<int> generator = () => 0;
            Sublist.Grow(list, size, generator);
        }

        /// <summary>
        /// An exception should be thrown if the size is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGrow_WithIndexedGenerator_NegativeSize_Throws()
        {
            List<int> list = new List<int>();
            int size = -1;
            Func<int, int> generator = i => i;
            Sublist.Grow(list, size, generator);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGrow_NullGenerator_Throws()
        {
            List<int> list = new List<int>();
            int size = 0;
            Func<int> generator = null;
            Sublist.Grow(list, size, generator);
        }

        /// <summary>
        /// An exception should be thrown if the generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGrow_NullIndexedGenerator_Throws()
        {
            List<int> list = new List<int>();
            int size = 0;
            Func<int, int> generator = null;
            Sublist.Grow(list, size, generator);
        }

        #endregion

        /// <summary>
        /// We are allowed to pass a size that is smaller than the list. It simply
        /// does nothing.
        /// </summary>
        [TestMethod]
        public void TestGrow_SizeSmallerThanList_DoesNothing()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 };
            Sublist.Grow(list, 3, 0);
            Assert.AreEqual(5, list.Count, "The size of the list changed.");
        }

        /// <summary>
        /// We are allowed to pass a size that is smaller than the list. It simply
        /// does nothing.
        /// </summary>
        [TestMethod]
        public void TestGrow_WithGenerator_SizeSmallerThanList_DoesNothing()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 };
            Sublist.Grow(list, 3, () => 0);
            Assert.AreEqual(5, list.Count, "The size of the list changed.");
        }

        /// <summary>
        /// We are allowed to pass a size that is smaller than the list. It simply
        /// does nothing.
        /// </summary>
        [TestMethod]
        public void TestGrow_WithIndexedGenerator_SizeSmallerThanList_DoesNothing()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 };
            Sublist.Grow(list, 3, i => i);
            Assert.AreEqual(5, list.Count, "The size of the list changed.");
        }
    }
}
