using ErrorOr;
using MediatR;
using Shefaa.Application.Appointments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Queries.GetDoctorAppointments
{
    public record class GetDoctorAppointmentsQuery(
        string DoctorId,
        DateTime Date
        ):IRequest<ErrorOr<List<DoctorAppointmentDto>>>;
}
