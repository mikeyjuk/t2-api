namespace T2LifestyleChecker.Api
{
    public class PatientDetailsRequest
    {
        public int NhsNumber { get; set; }
        public string? Surname { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
