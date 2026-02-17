namespace Shefaa.Contracts.Prescriptions;

public record AddMedicationToPrescriptionRequest(
    Guid PrescriptionId,
    string MedicationName,
    string Dosage,
    string Frequency,
    string Duration,
    string? Instructions
);
