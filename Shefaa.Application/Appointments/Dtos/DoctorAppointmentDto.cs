using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Dtos
{
    public record DoctorAppointmentDto (
        Guid Id,
    string PatientName,
    int PatientAge,        
    string Gender,         
    string Status,
    string? Notes,       
    DateTime Time
        );
}
