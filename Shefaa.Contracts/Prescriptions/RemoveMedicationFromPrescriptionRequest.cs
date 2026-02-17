namespace Shefaa.Contracts.Prescriptions;

public record RemoveMedicationFromPrescriptionRequest(
    Guid PrescriptionId,
    string MedicationName
);
