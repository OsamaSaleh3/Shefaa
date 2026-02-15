using ErrorOr;
using MediatR;
using Shefaa.Application.Patients.Dtos;

namespace Shefaa.Application.Patients.Queries.GetPatients;

public sealed record GetPatientsQuery():IRequest<ErrorOr<List<PatientDto>>>;
