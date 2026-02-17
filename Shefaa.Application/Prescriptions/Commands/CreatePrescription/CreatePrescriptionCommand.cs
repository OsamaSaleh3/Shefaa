using ErrorOr;
using MediatR;

namespace Shefaa.Application.Prescriptions.Commands.CreatePrescription;

public record CreatePrescriptionCommand(
    Guid PatientId,
    string DoctorId,
    Guid MedicalRecordId,
    string? Notes
) : IRequest<ErrorOr<Guid>>;