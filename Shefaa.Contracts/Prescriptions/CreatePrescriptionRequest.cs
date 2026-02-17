namespace Shefaa.Contracts.Prescriptions;

public record CreatePrescriptionRequest(
    Guid MedicalRecordId,
    Guid PatientId,
    string DoctorId,
    string? Notes
);
