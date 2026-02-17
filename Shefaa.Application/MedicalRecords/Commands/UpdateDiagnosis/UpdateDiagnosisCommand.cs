using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Commands.UpdateDiagnosis
{
    public record UpdateDiagnosisCommand(
    Guid Id,
    string NewDiagnosis,
    string? AdditionalNotes
) : IRequest<ErrorOr<Success>>;
}
