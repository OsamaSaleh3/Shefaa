using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Dtos
{
    public record PatientAppointmentDto(
        Guid Id,
        string DoctorName,        
        string Specialization,    
        DateTime AppointmentDate,
        string Time,              
        string Status    
        );
}
