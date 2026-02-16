using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Commands.CancelAppointment
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, ErrorOr<Success>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CancelAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ErrorOr<Success>> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment=await _appointmentRepository.GetByIdAsync(request.Id);
            if (appointment == null)
            {
                return Error.NotFound("Appointment.NotFound", $"No appointment found with ID {request.Id}");
            }
            var cancelResult = appointment.Cancel(request.Reason);
            if (cancelResult.IsError)
            {
                return cancelResult.Errors;
            }

            await _appointmentRepository.UpdateAsync(appointment);
            return Result.Success;
        }
    }
}
