using ErrorOr;
using MediatR;

namespace Shefaa.Application.Prescriptions.Commands.UpdatePrescriptionNotes;

public record UpdatePrescriptionNotesCommand(
    Guid PrescriptionId,
    string? NewNotes
) : IRequest<ErrorOr<Success>>;