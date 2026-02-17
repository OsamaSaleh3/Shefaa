using FluentValidation;
using Shefaa.Application.Prescriptions.Commands.AddMedicationToPrescription;

namespace Shefaa.Application.Prescriptions.Commands.AddMedication;

public class AddMedicationToPrescriptionCommandValidator : AbstractValidator<AddMedicationToPrescriptionCommand>
{
    public AddMedicationToPrescriptionCommandValidator()
    {
        RuleFor(x => x.PrescriptionId).NotEmpty();

        RuleFor(x => x.MedicationName)
            .NotEmpty().MaximumLength(200);

        RuleFor(x => x.Dosage)
            .NotEmpty().WithMessage("Dosage is required (e.g., 500mg).");

        RuleFor(x => x.Frequency)
            .NotEmpty().WithMessage("Frequency is required (e.g., 3 times a day).");

        RuleFor(x => x.Duration)
            .NotEmpty().WithMessage("Duration is required (e.g., 7 days).");

        RuleFor(x => x.Instructions)
            .MaximumLength(500);
    }
}