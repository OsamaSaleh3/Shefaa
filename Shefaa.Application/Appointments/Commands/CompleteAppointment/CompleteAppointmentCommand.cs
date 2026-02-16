using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Commands.CompleteAppointment
{
    public sealed record CompleteAppointmentCommand(Guid Id):IRequest<ErrorOr<Success>>;
}
