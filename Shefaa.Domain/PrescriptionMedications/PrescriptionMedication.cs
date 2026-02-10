using ErrorOr;
using Shefaa.Domain.Prescriptions;
using System;
using System.Collections.Generic;

namespace Shefaa.Domain.PrescriptionMedications;

public partial class PrescriptionMedication: BaseEntity
{
    public Guid PrescriptionId { get; private set; }

    public string MedicationName { get; private set; } = null!;

    public string Dosage { get; private set; } = null!;

    public string Frequency { get; private set; } = null!;

    public string Duration { get; private set; } = null!;

    public string? Instructions { get; private set; }

    public virtual Prescription Prescription { get; private set; } = null!;

    private PrescriptionMedication(string medicationName, string dosage, string frequency, string duration,string? instructions = null)
    {
        MedicationName = medicationName;
        Dosage = dosage;
        Frequency = frequency;
        Duration = duration;
        Instructions = instructions;
    }

    public static ErrorOr<PrescriptionMedication>Create(string medicationName, string dosage, string frequency, string duration, string? instructions = null)
    {
        if (string.IsNullOrWhiteSpace(medicationName))
        {
            return PrescriptionMedicationErrors.EmptyMedicationName;
        }

        if (string.IsNullOrWhiteSpace(dosage))
        {
            return PrescriptionMedicationErrors.InvalidDosage;
        }

        if (string.IsNullOrWhiteSpace(frequency))
        {
            return PrescriptionMedicationErrors.InvalidFrequency;
        }

        if (string.IsNullOrWhiteSpace(duration))
        {
            return PrescriptionMedicationErrors.InvalidDuration;
        }

        return new PrescriptionMedication(medicationName, dosage, frequency, duration, instructions);
    }

    public ErrorOr<Success> UpdateDosage(string newDosage, string? reason)
    {
        if (string.IsNullOrWhiteSpace(newDosage))
        {
            return PrescriptionMedicationErrors.InvalidDosage;
        }

        Dosage = newDosage;
        Instructions += $" [the Dosage updated for these reason : {reason}]";
        MarkAsUpdated();
        return Result.Success;
    }
}
