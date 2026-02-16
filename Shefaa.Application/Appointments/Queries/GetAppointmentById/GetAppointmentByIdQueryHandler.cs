using ErrorOr;
using MediatR;
using Shefaa.Application.Appointments.Dtos;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.Appointments;
using Shefaa.Domain.Appointments.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQueryHandler:IRequestHandler<GetAppointmentByIdQuery, ErrorOr<AppointmentDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentByIdQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ErrorOr<AppointmentDto>> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            if(request.Id == Guid.Empty)
                return Error.Validation("Appointment.Id", "Appointment ID cannot be empty.");

            var appointment =await _appointmentRepository.GetByIdAsync(request.Id);

            if(appointment is null)
                return Error.NotFound("Appointment.NotFound", $"No appointment found with ID {request.Id}");

            return new AppointmentDto(
                appointment.Id,
                appointment.PatientId,
                $"{appointment.Patient.FirstName} {appointment.Patient.LastName}",
                appointment.DoctorId,
                $"{appointment.Doctor.FirstName} {appointment.Doctor.LastName}",
                appointment.Doctor.Specialization??"General",
                appointment.AppointmentDate,
                appointment.Status.ToString(),
                appointment.DurationMinutes,
                appointment.Notes
                );
        }
    }
}
