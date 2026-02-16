using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Commands.BookAppointment
{
    public sealed record BookAppointmentCommand(
        Guid PatientId,
        string DoctorId,
        DateTime AppointmentDate,
        int DurationMinutes ,
        string? Notes = null
        ):IRequest<ErrorOr<Guid>>;
}
