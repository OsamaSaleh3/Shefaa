using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Commands.CancelAppointment
{
    public sealed record CancelAppointmentCommand(Guid Id,string Reason):IRequest<ErrorOr<Success>>;
}
