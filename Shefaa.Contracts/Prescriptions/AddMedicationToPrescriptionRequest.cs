namespace Shefaa.Contracts.Prescriptions;

public record AddMedicationToPrescriptionRequest(
    string MedicationName,
    string Dosage,
    string Frequency,
    string Duration,
    string? Instructions
);
