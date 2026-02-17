using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Commands.UpdateMedicalRecordVitals
{
    public record UpdateMedicalRecordVitalsCommand(
    Guid Id, 
    string? BloodPressure,
    decimal? Temperature,
    int? Pulse,
    int? RespiratoryRate,
    decimal? Weight,
    decimal? Height
) : IRequest<ErrorOr<Success>>;
}
