using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Commands.RescheduleAppointment
{
    public class RescheduleAppointmentCommandHandler : IRequestHandler<RescheduleAppointmentCommand, ErrorOr<Success>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public RescheduleAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<ErrorOr<Success>> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
            if (appointment == null)
                return Error.NotFound("Appointment.NotFound", "The appointment with the specified ID was not found.");

            var isSlotBusy= await _appointmentRepository.IsSlotBusyAsync(appointment.DoctorId, request.NewDate,appointment.DurationMinutes);

            if(isSlotBusy)
                return Error.Conflict("Appointment.SlotBusy", "The doctor is not available at the requested time slot.");

            var rescheduleResult = appointment.Reschedule(request.NewDate);

            if (rescheduleResult.IsError)
                return rescheduleResult.Errors;
            
            await _appointmentRepository.UpdateAsync(appointment);
            return Result.Success;
        }
    }
}
