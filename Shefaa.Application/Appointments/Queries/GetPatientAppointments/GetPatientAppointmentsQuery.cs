using ErrorOr;
using MediatR;
using Shefaa.Application.Appointments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Queries.GetPatientAppointments
{
    public record GetPatientAppointmentsQuery(Guid PatientId):IRequest<ErrorOr<List<PatientAppointmentDto>>>;
}
