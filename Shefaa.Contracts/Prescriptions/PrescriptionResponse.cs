namespace Shefaa.Contracts.Prescriptions;

public record PrescriptionResponse(
    Guid Id,
    Guid MedicalRecordId,
    string DoctorName,
    string PatientName,
    DateTime Date,
    string? Notes,
    List<PrescriptionMedicationResponse> Medications
);
