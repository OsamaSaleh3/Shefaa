using ErrorOr;
using MediatR;
using Shefaa.Application.Appointments.Dtos;
using Shefaa.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Queries.GetPatientAppointments
{
    internal class GetPatientAppointmentsQueryHandler : IRequestHandler<GetPatientAppointmentsQuery, ErrorOr<List<PatientAppointmentDto>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetPatientAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ErrorOr<List<PatientAppointmentDto>>> Handle(GetPatientAppointmentsQuery request, CancellationToken cancellationToken)
        {
            if(request.PatientId == Guid.Empty)
            {
                return Error.Validation("InvalidPatientId", "The provided patient ID is invalid.");
            }

            var appointments = await _appointmentRepository.GetAppointmentsByPatientIdAsync(request.PatientId);

            return appointments.Select(a => new PatientAppointmentDto(
                a.Id,
                $"{a.Doctor.FirstName} {a.Doctor.LastName}",
                a.Doctor.Specialization ?? "General",
                a.AppointmentDate,
                a.AppointmentDate.ToString("hh:mm tt"),
                a.Status.ToString()

                )).ToList();
        }
    }
}
