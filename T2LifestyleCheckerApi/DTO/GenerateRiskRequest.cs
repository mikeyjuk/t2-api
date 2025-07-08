namespace T2LifestyleChecker.Api
{
    public class GenerateRiskRequest
    {
        public bool Smoker { get; set; }
        public bool Drinker { get; set; }
        public bool Exercise { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
