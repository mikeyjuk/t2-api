using System.Globalization;
using System.Net.Http.Json;
using T2LifestyleChecker.Utils;

namespace T2LifestyleChecker.Service
{
     public class PatientDetailsService : IPatientDetailsService
    {

        private readonly HttpClient _httpClient;

        public PatientDetailsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<String> GetPatientDetails(string requestUri, int nhsNumber, string surname, DateOnly dateOfBirth)
        {

            requestUri  =  $"{requestUri}{nhsNumber}";
            var headers = new Dictionary<string, string>();

            // Placed the subscription key in an Environment variable - for production would
            // probably use a key vault depending on the hosting platform
            var subscriptionKey = Environment.GetEnvironmentVariable("API_SUBSCRIPTION_KEY");

            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }

                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var patientDetails = await response.Content.ReadFromJsonAsync<PatientDetails>();
                    return ValidateDetails(patientDetails, nhsNumber, surname, dateOfBirth);
                }
                else
                {
                    return Constants.NotFound;
                }

            }

        }

        private static string ValidateDetails(PatientDetails? patientDetails, int nhsNumber, string surname, DateOnly dateOfBirth)
        {

            var lastName = patientDetails?.name?.Split(',')[0]
                ;
            var culture = new CultureInfo("en-GB");
            var birthDate = String.IsNullOrEmpty(patientDetails?.born) 
                    ? DateOnly.FromDateTime(DateTime.Now)
                    : DateOnly.FromDateTime(DateTime.Parse(patientDetails.born, culture));
          
            var age = CheckerUtils.CalculateAge(birthDate);

            if (age < 16)
            {
                return Constants.NotEligible;
            }

            if (patientDetails?.nhsNumber == nhsNumber.ToString()
                && lastName?.ToLowerInvariant() == surname?.ToLowerInvariant()
                && birthDate == dateOfBirth)
            {
                return Constants.Matched;
            }

            return Constants.NotMatched;
        }

    }
}
