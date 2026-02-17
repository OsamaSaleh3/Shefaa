namespace Shefaa.Contracts.Prescriptions;

public record UpdatePrescriptionMedicationRequest(
    Guid PrescriptionId,
    string MedicationName,
    string NewDosage,
    string NewFrequency,
    string NewDuration,
    string? NewInstructions
);
