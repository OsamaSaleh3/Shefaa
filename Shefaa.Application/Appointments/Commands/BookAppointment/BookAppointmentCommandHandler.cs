using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Commands.BookAppointment
{
    public class BookAppointmentCommandHandler : IRequestHandler<BookAppointmentCommand, ErrorOr<Guid>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public BookAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IUserRepository userRepository, IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
            _patientRepository = patientRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
        {
            var patient=await _patientRepository.GetByIdAsync(request.PatientId);
            var doctor= await _userRepository.GetByIdAsync(request.DoctorId);

            if(patient is null)
                return Error.NotFound("Patient not found");
            if(doctor is null)
                return Error.NotFound("Doctor not found");

            var isSlotBusy = await _appointmentRepository.IsSlotBusyAsync(
                request.DoctorId,
                request.AppointmentDate,
                request.DurationMinutes
                );

            if(isSlotBusy)
                return Error.Conflict("The selected time slot is already booked. Please choose a different time.");

            var appointmentResult = Appointment.Create(
                request.PatientId,
                request.DoctorId,
                request.AppointmentDate,
                request.DurationMinutes,
                request.Notes
                );

            if(appointmentResult.IsError)
                return appointmentResult.Errors;

            await _appointmentRepository.AddAsync(appointmentResult.Value);
            return appointmentResult.Value.Id;
        }
    }
}
