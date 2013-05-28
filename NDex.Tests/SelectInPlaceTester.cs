using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the SelectInPlace methods.
    /// </summary>
    [TestClass]
    public class SelectInPlaceTester
    {
        #region Real World Example

        /// <summary>
        /// We can mutate all of the values in a list.
        /// </summary>
        [TestMethod]
        public void TestSelectInPlace_DoubleValues()
        {
            Random random = new Random();

            // build a list of numbers
            var numbers = new List<int>(100);
            Sublist.Generate(100, i => i).AddTo(numbers.ToSublist());

            // double the values
            numbers.ToSublist().Select(i => i * 2).InPlace();

            // build the expected values
            var expected = new List<int>(100);
            Sublist.Generate(100, i => i * 2).AddTo(expected.ToSublist());

            // verify that the values were doubled in-place
            Assert.IsTrue(expected.ToSublist().IsEqualTo(numbers.ToSublist()), "Could not convert between strings and numbers.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the source list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSelectInPlace_NullList_Throws()
        {
            IExpandableSublist<List<int>, int> list = null;
            Func<int, int> converter = i => i;
            list.Select(converter);
        }

        /// <summary>
        /// An exception should be thrown if the conversion delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSelectInPlace_NullConverter_Throws()
        {
            IExpandableSublist<List<int>, int> list = new List<int>().ToSublist();
            Func<int, int> converter = null;
            list.Select(converter);
        }

        #endregion
    }
}
