namespace Shefaa.Contracts.Prescriptions;

public record UpdatePrescriptionMedicationRequest(
    string MedicationName,
    string NewDosage,
    string NewFrequency,
    string NewDuration,
    string? NewInstructions
);
