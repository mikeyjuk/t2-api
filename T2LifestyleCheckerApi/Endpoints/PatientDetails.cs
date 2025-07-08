using FastEndpoints;
using T2LifestyleChecker.Service;

namespace T2LifestyleChecker.Api
{
    public class PatientDetails : Endpoint<PatientDetailsRequest, PatientDetailsResponse>
    {

        private readonly IPatientDetailsService _patientDetailsService;
        
        private readonly IConfiguration Configuration;
        public PatientDetails(IPatientDetailsService patientDetailsService, IConfiguration configuration)
        {
            _patientDetailsService = patientDetailsService;
            Configuration = configuration;
        }

        public override void Configure()
        {
            Get("/api/patientdetails/{NhsNumber}/{Surname}/{DateOfBirth}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(PatientDetailsRequest req, CancellationToken ct)
        {

            var requestUri = Configuration["requestUri"];

            var response = _patientDetailsService.GetPatientDetails(requestUri, req.NhsNumber, req.Surname, req.DateOfBirth).Result;

            await SendAsync(new()
            {
                ResponseCode = response

            });
        }
    }
}


