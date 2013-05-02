using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NDex.Test
{
    /// <summary>
    /// Tests the LowerAndUpperBoundResult class.
    /// </summary>
    [TestClass]
    public class LowerAndUpperBoundResultTester
    {
        #region ToString

        /// <summary>
        /// The LowerAndUpperBoundResult string should say what the min and max indexes were.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            LowerAndUpperBoundResult result = new LowerAndUpperBoundResult()
            {
                LowerBound = 0,
                UpperBound = 1,
            };
            string expected = "Lower Bound = 0, Upper Bound = 1";
            Assert.AreEqual(expected, result.ToString(), "The string was not the right value.");
        }

        #endregion
    }
}
