using FluentValidation;

namespace Shefaa.Application.Prescriptions.Commands.UpdatePrescriptionNotes;

public class UpdatePrescriptionNotesCommandValidator : AbstractValidator<UpdatePrescriptionNotesCommand>
{
    public UpdatePrescriptionNotesCommandValidator()
    {
        RuleFor(x => x.PrescriptionId).NotEmpty();

        RuleFor(x => x.NewNotes)
            .MaximumLength(1000).WithMessage("Notes are too long.");
    }
}