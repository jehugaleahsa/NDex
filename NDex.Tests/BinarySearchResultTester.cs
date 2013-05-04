using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the BinarySearchResult class.
    /// </summary>
    [TestClass]
    public class BinarySearchResultTester
    {
        /// <summary>
        /// BinarySearchResult supports automatic conversion to a boolean, representing whether the value was found.
        /// </summary>
        [TestMethod]
        public void TestBooleanConversion()
        {
            int[] values = { 1 };
            BinarySearchResult result1 = Sublist.BinarySearch(values.ToSublist(), 1);
            Assert.IsTrue(result1, "The value should have existed."); // implicit conversion
            BinarySearchResult result2 = Sublist.BinarySearch(values.ToSublist(), 2);
            Assert.IsFalse(result2, "The value should not have existed."); // implicit conversion
        }

        /// <summary>
        /// BinarySearchResult supports automatic conversion to an integer, representing the first index where the value belongs.
        /// </summary>
        [TestMethod]
        public void TestIntegerConversion()
        {
            int[] values = { 1 };
            BinarySearchResult result1 = Sublist.BinarySearch(values.ToSublist(), 1);
            Assert.AreEqual<int>(0, result1, "The result converted to the wrong index."); // implicit conversion
            BinarySearchResult result2 = Sublist.BinarySearch(values.ToSublist(), 2);
            Assert.AreEqual<int>(1, result2, "The result converted to the wrong index."); // implicit conversion
        }

        /// <summary>
        /// BinarySearchResult ToString method shows whether the value exists and its index.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            int[] values = { 1 };
            BinarySearchResult result1 = Sublist.BinarySearch(values.ToSublist(), 1);
            Assert.AreEqual("Exists = True, Index = 0", result1.ToString(), "The wrong string representation was returned."); // implicit conversion
            BinarySearchResult result2 = Sublist.BinarySearch(values.ToSublist(), 2);
            Assert.AreEqual("Exists = False, Index = 1", result2.ToString(), "The wrong string representation was returned."); // implicit conversion
        }
    }
}
