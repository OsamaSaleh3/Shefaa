using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Commands.CreateMedicalRecord
{
    public record CreateMedicalRecordCommand(
        
        Guid PatientId,
        string DoctorId,
        string ChiefComplaint,
        string Symptoms,
        string Diagnosed,
        string? BloodPressure,
        decimal? Temperature,
        int? Pulse,
        int? RespiratoryRate,
        decimal? Weight,
        decimal? Height,
        Guid? AppointmentId = null
        ) : IRequest<ErrorOr<Guid>>;
}
