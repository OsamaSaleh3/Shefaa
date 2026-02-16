using ErrorOr;
using MediatR;
using Shefaa.Application.Appointments.Dtos;
using Shefaa.Domain.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Queries.GetCalendarView
{
    public record GetCalendarViewQuery(DateTime From,DateTime To, string? DoctorId=null) :IRequest<ErrorOr<List<CalendarItemDto>>>;
}
