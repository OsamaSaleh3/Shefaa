using ErrorOr;
using MediatR;
using Shefaa.Application.MedicalRecords.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Queries.GetMedicalRecordById
{
    public record GetMedicalRecordByIdQuery(
    Guid Id
    ) : IRequest<ErrorOr<MedicalRecordDto>>;
}
