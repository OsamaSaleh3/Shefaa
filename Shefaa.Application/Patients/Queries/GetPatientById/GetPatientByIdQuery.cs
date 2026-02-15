using ErrorOr;
using MediatR;
using Shefaa.Application.Patients.Dtos;

namespace Shefaa.Application.Patients.Queries.GetPatientById;

public sealed record GetPatientByIdQuery(Guid Id):IRequest<ErrorOr<PatientDto?>>;
