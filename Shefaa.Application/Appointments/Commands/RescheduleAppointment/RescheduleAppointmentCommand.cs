using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Commands.RescheduleAppointment
{
    public sealed record RescheduleAppointmentCommand(Guid Id,DateTime NewDate):IRequest<ErrorOr<Success>>;
}
