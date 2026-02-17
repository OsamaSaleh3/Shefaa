namespace Shefaa.Application.Prescriptions.Dtos;

public record PrescriptionDto(
    Guid Id,
    Guid MedicalRecordId,
    string DoctorName,
    string PatientName,
    DateTime Date,
    string? Notes,
    List<PrescriptionMedicationDto> Medications
);
