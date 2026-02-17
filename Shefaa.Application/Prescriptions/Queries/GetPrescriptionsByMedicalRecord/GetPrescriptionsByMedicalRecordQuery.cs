using ErrorOr;
using MediatR;
using Shefaa.Application.Prescriptions.Dtos;

namespace Shefaa.Application.Prescriptions.Queries.GetPrescriptionsByMedicalRecord;

public record GetPrescriptionsByMedicalRecordQuery(Guid MedicalRecordId) : IRequest<ErrorOr<List<PrescriptionDto>>>;