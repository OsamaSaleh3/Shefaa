using FluentValidation;

namespace Shefaa.Application.MedicalRecords.Commands.UpdateMedicalRecordVitals;

public class UpdateMedicalRecordVitalsCommandValidator : AbstractValidator<UpdateMedicalRecordVitalsCommand>
{
    public UpdateMedicalRecordVitalsCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Medical Record ID is required.");

        RuleFor(x => x.BloodPressure)
            .Matches(@"^\d{2,3}/\d{2,3}$")
            .WithMessage("Blood Pressure must be in format '120/80'.")
            .When(x => !string.IsNullOrWhiteSpace(x.BloodPressure)); 

        RuleFor(x => x.Temperature)
            .InclusiveBetween(30, 45)
            .WithMessage("Temperature must be between 30 and 45 Celsius.");

        RuleFor(x => x.Pulse)
            .InclusiveBetween(30, 220)
            .WithMessage("Pulse must be between 30 and 220 BPM.");

        RuleFor(x => x.RespiratoryRate)
            .InclusiveBetween(5, 60)
            .WithMessage("Respiratory Rate seems invalid (Valid range: 5-60).");

      
        RuleFor(x => x.Weight)
            .InclusiveBetween(1, 500)
            .WithMessage("Weight must be valid (1kg - 500kg).");

        RuleFor(x => x.Height)
            .InclusiveBetween(30, 250)
            .WithMessage("Height must be in cm (between 30cm and 250cm).");
    }
}