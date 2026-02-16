using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Commands.CompleteAppointment
{
    public class CompleteAppointmentCommandHandler : IRequestHandler<CompleteAppointmentCommand, ErrorOr<Success>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CompleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ErrorOr<Success>> Handle(CompleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
            if (appointment == null)
            {
                return Error.NotFound("Appointment.NotFound", $"No appointment found with ID {request.Id}");
            }
            var CompleteResult = appointment.Complete();
            if (CompleteResult.IsError)
            {
                return CompleteResult.Errors;
            }

            await _appointmentRepository.UpdateAsync(appointment);
            return Result.Success;
        }
    }
}
