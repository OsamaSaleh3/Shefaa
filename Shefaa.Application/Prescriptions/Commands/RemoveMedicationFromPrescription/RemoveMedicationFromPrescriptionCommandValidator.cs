using FluentValidation;

namespace Shefaa.Application.Prescriptions.Commands.RemoveMedicationFromPrescription;

public class RemoveMedicationFromPrescriptionCommandValidator : AbstractValidator<RemoveMedicationFromPrescriptionCommand>
{
    public RemoveMedicationFromPrescriptionCommandValidator()
    {
        RuleFor(x => x.PrescriptionId).NotEmpty();
        RuleFor(x => x.MedicationName).NotEmpty();
    }
}