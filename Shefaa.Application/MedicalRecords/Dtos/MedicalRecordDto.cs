using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Dtos
{
    public record MedicalRecordDto(
     Guid Id,
     Guid PatientId,
     string PatientName,
     string DoctorName, 
     DateTime VisitDate,
     string ChiefComplaint,
     string Diagnosis,
     string? BloodPressure,
     decimal? Temperature
     //to do
     //List<PrescriptionDto> Prescriptions
 );
}
