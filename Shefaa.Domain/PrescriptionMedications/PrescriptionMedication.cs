using Shefaa.Domain.Prescriptions;
using System;
using System.Collections.Generic;

namespace Shefaa.Domain.PrescriptionMedications;

public partial class PrescriptionMedication: BaseEntity
{
    public int PrescriptionId { get; private set; }

    public string MedicationName { get; private set; } = null!;

    public string Dosage { get; private set; } = null!;

    public string Frequency { get; private set; } = null!;

    public string Duration { get; private set; } = null!;

    public string? Instructions { get; private set; }

    public virtual Prescription Prescription { get; private set; } = null!;

    public PrescriptionMedication(string medicationName, string dosage, string frequency, string duration,string? instructions = null)
    {
        if (string.IsNullOrWhiteSpace(medicationName))
        {

        }

        if (string.IsNullOrWhiteSpace(dosage))
        {

        }

        if (string.IsNullOrWhiteSpace(frequency))
        {

        }

        if (string.IsNullOrWhiteSpace(duration))
        {

        }

        MedicationName = medicationName;
        Dosage = dosage;
        Frequency = frequency;
        Duration = duration;
        Instructions = instructions;
    }

    public void UpdateDosage(string newDosage, string? reason)
    {
        if (string.IsNullOrWhiteSpace(newDosage)) throw new ArgumentException("the new dosage invalid");

        Dosage = newDosage;
        Instructions += $" [the Dosage updated for these reason : {reason}]";
        MarkAsUpdated();
    }
}
