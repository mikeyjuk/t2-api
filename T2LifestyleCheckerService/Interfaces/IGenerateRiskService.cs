namespace T2LifestyleChecker.Service
{
    public interface IGenerateRiskService
    {
        Task<int> GetRisk(bool smoker, bool drinker, bool exercise, DateOnly dateOfBirth);
    }
}
