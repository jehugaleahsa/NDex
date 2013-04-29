using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NDex;
using System.Linq;

namespace NDex.Test
{
    /// <summary>
    /// Tests the ForEach methods.
    /// </summary>
    [TestClass]
    public class ForEachTester
    {
        #region Real World Example

        /// <summary>
        /// Using a closure, we can use ForEach to sum the values in a list.
        /// </summary>
        [TestMethod]
        public void TestForEach_FindAverage()
        {
            Random random = new Random();

            // build the list 
            var list = new List<int>(10);
            Sublist.Add(Enumerable.Range(0, 10).Select(i => random.Next(10)), list.ToSublist());

            decimal sum = 0m;
            Sublist.ForEach(list.ToSublist(), i => sum += i);
            decimal average = sum / list.Count;

            sum = 0m;
            foreach (int i in list)
            {
                sum += i;
            }
            decimal expected = sum / list.Count;

            Assert.AreEqual(expected, average, "The wrong average was calculated.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestForEach_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Action<int> action = i => { };
            Sublist.ForEach(list, action);
        }

        /// <summary>
        /// An exception should be thrown if the action delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestForEach_NullAction_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Action<int> action = null;
            Sublist.ForEach(list, action);
        }

        #endregion

        /// <summary>
        /// We'll simulate AddIf using a closure.
        /// </summary>
        [TestMethod]
        public void TestForEach_AddEvenNumbers()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            var destination = new List<int>();
            Action<int> action = i =>
            {
                if (i % 2 == 0)
                {
                    destination.Add(i);
                }
            };
            Sublist.ForEach(list, action);
            int[] expected = { 2, 4, 6, 8, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination.ToSublist()), "Not all items were added to the destination.");
        }
    }
}
