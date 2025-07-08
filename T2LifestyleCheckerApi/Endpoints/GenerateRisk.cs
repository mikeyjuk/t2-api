using FastEndpoints;
using T2LifestyleChecker.Service;

namespace T2LifestyleChecker.Api
{
    public class GenerateRisk : Endpoint<GenerateRiskRequest, GenerateRiskResponse>
    {

        private readonly IGenerateRiskService _generateRiskService;

        public GenerateRisk(IGenerateRiskService generateRiskService)
        {
            _generateRiskService = generateRiskService;
        }

        public override void Configure()
        {
            Get("/api/generaterisk/{Smoker}/{Drinker}/{Exercise}/{DateOfBirth}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GenerateRiskRequest req, CancellationToken ct)
        {
            var riskScore = _generateRiskService.GetRisk(req.Smoker, req.Drinker, req.Exercise, req.DateOfBirth).Result;

            await SendAsync(new()
            {
                RiskScore = riskScore

            });
        }
    }
}
