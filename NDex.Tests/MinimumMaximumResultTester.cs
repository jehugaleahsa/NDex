using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Tests
{
    /// <summary>
    /// Tests the MinimumMaximumResult class.
    /// </summary>
    [TestClass]
    public class MinimumMaximumResultTester
    {
        #region ToString

        /// <summary>
        /// The MinimumMaximumResult string should say what the min and max indexes were.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            MinimumMaximumResult result = new MinimumMaximumResult()
            {
                MinimumIndex = 0,
                MaximumIndex = 1,
            };
            string expected = "Minimum Index = 0, Maximum Index = 1";
            Assert.AreEqual(expected, result.ToString(), "The string was not the right value.");
        }

        #endregion
    }
}
