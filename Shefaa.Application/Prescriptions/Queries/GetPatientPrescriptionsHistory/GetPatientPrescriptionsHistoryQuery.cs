using ErrorOr;
using MediatR;
using Shefaa.Application.Prescriptions.Dtos;

namespace Shefaa.Application.Prescriptions.Queries.GetPatientPrescriptionsHistory;

public record GetPatientPrescriptionsHistoryQuery(Guid PatientId) : IRequest<ErrorOr<List<PrescriptionDto>>>;