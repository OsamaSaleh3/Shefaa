using ErrorOr;
using MediatR;
using Shefaa.Application.Appointments.Dtos;
using Shefaa.Domain.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Queries.GetAppointmentById
{
    public record GetAppointmentByIdQuery(Guid Id):IRequest<ErrorOr<AppointmentDto>>;
}
