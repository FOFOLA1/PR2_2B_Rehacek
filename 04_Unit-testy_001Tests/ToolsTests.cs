namespace _03_OOP3_10_Unit_tests.Tests
{
    [TestClass()]
    public class ToolsTests
    {
        [TestMethod()]
        public void FindMinTest()
        {
            int[] nums = [1, 4, 18, -7, 31];
            Assert.AreEqual(-7, Tools.FindMin(nums));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FindMinTestEmptyInput()
        {
            int[] nums = [];
            Tools.FindMin(nums);
            Assert.Fail();
        }
    }
}