using FluentValidation;

namespace Shefaa.Application.Prescriptions.Commands.UpdatePrescriptionMedication;

public class UpdatePrescriptionMedicationCommandValidator : AbstractValidator<UpdatePrescriptionMedicationCommand>
{
    public UpdatePrescriptionMedicationCommandValidator()
    {
        RuleFor(x => x.PrescriptionId).NotEmpty();
        RuleFor(x => x.MedicationName).NotEmpty();

        RuleFor(x => x.NewDosage).NotEmpty();
        RuleFor(x => x.NewFrequency).NotEmpty();
        RuleFor(x => x.NewDuration).NotEmpty();

        RuleFor(x => x.NewInstructions).MaximumLength(500);
    }
}