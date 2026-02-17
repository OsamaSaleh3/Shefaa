using ErrorOr;
using Shefaa.Domain.Prescriptions;

namespace Shefaa.Domain.PrescriptionMedications;

public partial class PrescriptionMedication : BaseEntity
{
    public Guid PrescriptionId { get; private set; }

    public string MedicationName { get; private set; } = null!;

    public string Dosage { get; private set; } = null!;

    public string Frequency { get; private set; } = null!;

    public string Duration { get; private set; } = null!;

    public string? Instructions { get; private set; }

    public Prescription Prescription { get; private set; } = null!;

    internal PrescriptionMedication() { }

    private PrescriptionMedication(string medicationName, string dosage, string frequency, string duration, string? instructions = null)
    {
        MedicationName = medicationName;
        Dosage = dosage;
        Frequency = frequency;
        Duration = duration;
        Instructions = instructions;
    }

    internal static ErrorOr<PrescriptionMedication> Create(string medicationName, string dosage, string frequency, string duration, string? instructions = null)
    {
        if (string.IsNullOrWhiteSpace(medicationName))
            return PrescriptionMedicationErrors.EmptyMedicationName;

        if (string.IsNullOrWhiteSpace(dosage))
            return PrescriptionMedicationErrors.InvalidDosage;

        if (string.IsNullOrWhiteSpace(frequency))
            return PrescriptionMedicationErrors.InvalidFrequency;

        if (string.IsNullOrWhiteSpace(duration))
            return PrescriptionMedicationErrors.InvalidDuration;

        return new PrescriptionMedication(medicationName, dosage, frequency, duration, instructions);
    }

    internal ErrorOr<Success> UpdateDetails(string dosage, string frequency, string duration, string? instructions)
    {
        if (string.IsNullOrWhiteSpace(dosage)) return PrescriptionMedicationErrors.InvalidDosage;
        if (string.IsNullOrWhiteSpace(frequency)) return PrescriptionMedicationErrors.InvalidFrequency;
        if (string.IsNullOrWhiteSpace(duration)) return PrescriptionMedicationErrors.InvalidDuration;

        Dosage = dosage;
        Frequency = frequency;
        Duration = duration;
        Instructions = instructions;
        
        return Result.Success;
    }
}