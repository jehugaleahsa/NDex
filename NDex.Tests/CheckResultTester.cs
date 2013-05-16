using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the CheckResult class.
    /// </summary>
    [TestClass]
    public class CheckResultTester
    {
        /// <summary>
        /// The result supports automatic conversion to a boolean, representing whether the value was found.
        /// </summary>
        [TestMethod]
        public void TestBooleanConversion()
        {
            CheckResult result1 = new CheckResult() { Success = true };
            Assert.IsTrue(result1, "The result should have evaluated to true."); // implicit conversion
            CheckResult result2 = new CheckResult() { Success = false };
            Assert.IsFalse(result2, "The result should have evaluated to false."); // implicit conversion
        }

        /// <summary>
        /// The result supports automatic conversion to an integer, representing the first index where the value belongs.
        /// </summary>
        [TestMethod]
        public void TestIntegerConversion()
        {
            CheckResult result1 = new CheckResult() { Index = 0 };
            Assert.AreEqual<int>(result1.Index, result1, "The result converted to the wrong index."); // implicit conversion
            CheckResult result2 = new CheckResult() { Index = 1 };
            Assert.AreEqual<int>(result2.Index, result2, "The result converted to the wrong index."); // implicit conversion
        }

        /// <summary>
        /// The ToString method shows whether the value exists and its index.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            CheckResult result1 = new CheckResult() { Index = 0, Success = true };
            Assert.AreEqual("Success = True, Index = 0", result1.ToString(), "The wrong string representation was returned."); // implicit conversion
            CheckResult result2 = new CheckResult() { Index = 1, Success = false };
            Assert.AreEqual("Success = False, Index = 1", result2.ToString(), "The wrong string representation was returned."); // implicit conversion
        }
    }
}
