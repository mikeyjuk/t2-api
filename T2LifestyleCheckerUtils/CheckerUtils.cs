namespace T2LifestyleChecker.Utils
{
    public static class CheckerUtils
    {

        public static int CalculateAge(DateOnly dateOfBirth)
        {
            var dob = dateOfBirth.ToDateTime(TimeOnly.MinValue);
            DateTime now = DateTime.Today;
            int age = now.Year - dob.Year;
            if (dob.AddYears(age) > now)
            {
                age--;
            }
            return age;

        }

    }
}
