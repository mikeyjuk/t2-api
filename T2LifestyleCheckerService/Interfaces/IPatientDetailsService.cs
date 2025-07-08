namespace T2LifestyleChecker.Service
{
    public interface IPatientDetailsService
    {
        Task<String> GetPatientDetails(string requestUri, int nhsNumber, string surname, DateOnly dateOfBirth);
    }
}
