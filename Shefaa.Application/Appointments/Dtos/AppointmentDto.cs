using Shefaa.Domain.Appointments;
using Shefaa.Domain.Appointments.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Dtos
{
    public record AppointmentDto(
        Guid Id,
        Guid PatientId,
        string PatientName,
        string DoctorId,
        string DoctorName,
        string Specialization,
        DateTime AppointmentDate,
        string Status,
        int DurationMinutes,
        string? Notes = null
        );

}
