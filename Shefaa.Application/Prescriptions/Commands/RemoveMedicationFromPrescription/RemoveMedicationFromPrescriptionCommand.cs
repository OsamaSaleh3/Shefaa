using ErrorOr;
using MediatR;

namespace Shefaa.Application.Prescriptions.Commands.RemoveMedicationFromPrescription;

public record RemoveMedicationFromPrescriptionCommand(
    Guid PrescriptionId,
    string MedicationName
) : IRequest<ErrorOr<Success>>;