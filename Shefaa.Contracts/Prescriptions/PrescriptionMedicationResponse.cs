using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Contracts.Prescriptions
{
    public record PrescriptionMedicationResponse
    (
        string Name,
        string Dosage,
        string Frequency,
        string Duration,
        string? Instructions
    );
}
