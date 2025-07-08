using T2LifestyleChecker.Service;

namespace T2LifestyleChecker.Tests
{
    [TestClass]
    public class GenerateRiskServiceDataTests
    {
        private GenerateRiskService _riskService;

        [TestInitialize]
        public void Setup()
        {
            _riskService = new GenerateRiskService();
        }

        [DataTestMethod]
        [DataRow(true, false, false, 18, 2)] // 18: smoker, no exercise
        [DataRow(true, true, false, 25, 7)]  // 25: smoker, drinker, no exercise
        [DataRow(false, true, false, 46, 4)] // 46: drinker, no exercise
        [DataRow(false, false, true, 70, 0)] // 70: no risk factors
        [DataRow(true, true, true, 16, 3)]   // 16: smoker, drinker
        [DataRow(true, true, true, 21, 3)]   // 21: smoker, drinker
        [DataRow(true, true, true, 22, 4)]   // 22: smoker, drinker
        [DataRow(true, true, true, 40, 4)]   // 40: smoker, drinker
        [DataRow(true, true, true, 41, 5)]   // 21: smoker, drinker
        [DataRow(true, true, true, 65, 5)]   // 21: smoker, drinker
        [DataRow(true, true, true, 66, 6)]   // 66: smoker, drinker
        public async Task GetRisk_DataDrivenTests(bool smoker, bool drinker, bool exercise, int age, int expectedScore)
        {
            // Arrange
            var dateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-age));

            // Act
            var riskScore = await _riskService.GetRisk(smoker, drinker, exercise, dateOfBirth);

            // Assert
            Assert.AreEqual(expectedScore, riskScore);
        }
    }
}