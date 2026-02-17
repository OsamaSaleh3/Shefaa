using ErrorOr;
using MediatR;

namespace Shefaa.Application.Prescriptions.Commands.UpdatePrescriptionMedication;

public record UpdatePrescriptionMedicationCommand(
    Guid PrescriptionId,
    string MedicationName,
    string NewDosage,
    string NewFrequency,
    string NewDuration,
    string? NewInstructions
) : IRequest<ErrorOr<Success>>;