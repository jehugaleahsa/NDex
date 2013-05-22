using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the SearchResult class.
    /// </summary>
    [TestClass]
    public class SearchResultTester
    {
        /// <summary>
        /// The result supports automatic conversion to a boolean, representing whether the value was found.
        /// </summary>
        [TestMethod]
        public void TestBooleanConversion()
        {
            SearchResult result1 = new SearchResult() { Exists = true };
            Assert.IsTrue(result1, "The value should have existed."); // implicit conversion
            SearchResult result2 = new SearchResult() { Exists = false };
            Assert.IsFalse(result2, "The value should not have existed."); // implicit conversion
        }

        /// <summary>
        /// The result supports automatic conversion to an integer, representing the first index where the value belongs.
        /// </summary>
        [TestMethod]
        public void TestIntegerConversion()
        {
            SearchResult result1 = new SearchResult() { Index = 0 };
            Assert.AreEqual<int>(result1.Index, result1, "The result converted to the wrong index."); // implicit conversion
            SearchResult result2 = new SearchResult() { Index = 2 };
            Assert.AreEqual<int>(result2.Index, result2, "The result converted to the wrong index."); // implicit conversion
        }

        /// <summary>
        /// The ToString method shows whether the value exists and its index.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            SearchResult result1 = new SearchResult() { Index = 0, Exists = true };
            Assert.AreEqual("Exists = True, Index = 0", result1.ToString(), "The wrong string representation was returned."); // implicit conversion
            SearchResult result2 = new SearchResult() { Index = 1, Exists = false };
            Assert.AreEqual("Exists = False, Index = 1", result2.ToString(), "The wrong string representation was returned."); // implicit conversion
        }
    }
}
