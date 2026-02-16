using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Dtos
{
    public record CalendarItemDto(
        Guid Id,
        string Title,
        DateTime Start,
        DateTime End,
        string Status
        );
}
