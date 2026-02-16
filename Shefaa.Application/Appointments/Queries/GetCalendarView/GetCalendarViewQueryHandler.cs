using ErrorOr;
using MediatR;
using Shefaa.Application.Appointments.Dtos;
using Shefaa.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Queries.GetCalendarView
{
    public class GetCalendarViewQueryHandler : IRequestHandler<GetCalendarViewQuery, ErrorOr<List<CalendarItemDto>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetCalendarViewQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ErrorOr<List<CalendarItemDto>>> Handle(GetCalendarViewQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAppointmentsByDateRangeAsync(request.From,request.To, request.DoctorId);

            return appointment.Select(a => new CalendarItemDto(
                a.Id,
                $"{a.Patient.FirstName} {a.Patient.LastName}",
                a.AppointmentDate,
                a.AppointmentDate.AddMinutes(a.DurationMinutes),
                a.Status.ToString()

                )).ToList();

        }
    }
}
