using ErrorOr;
using MediatR;
using Shefaa.Application.Prescriptions.Dtos;

namespace Shefaa.Application.Prescriptions.Queries.GetPrescriptionById;

public record GetPrescriptionByIdQuery(Guid Id) : IRequest<ErrorOr<PrescriptionDto>>;