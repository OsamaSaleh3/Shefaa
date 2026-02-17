using ErrorOr;
using MediatR;
using Shefaa.Application.MedicalRecords.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Queries.GetMedicalRecordByAppointment
{
    public record GetMedicalRecordByAppointmentQuery(
    Guid AppointmentId
    ) : IRequest<ErrorOr<MedicalRecordDto>>;
}
