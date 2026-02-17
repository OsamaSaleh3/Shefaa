using ErrorOr;
using MediatR;
using Shefaa.Application.MedicalRecords.Dtos;

namespace Shefaa.Application.MedicalRecords.Queries.GetPatientMedicalHistory
{
    public record GetPatientMedicalHistoryQuery(
    Guid PatientId
) : IRequest<ErrorOr<List<MedicalRecordDto>>>;
}
