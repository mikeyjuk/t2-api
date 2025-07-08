using FastEndpoints;
using FluentValidation;

namespace T2LifestyleChecker.Api
{
    public class PatientDetailsValidator : Validator<PatientDetailsRequest>
    {
        public PatientDetailsValidator()
        {
            RuleFor(x => x.NhsNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("NHS Number invalid");
            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Surname is required")
                .MinimumLength(2)
                .WithMessage("Surname must be provided");
        }
    }
}
