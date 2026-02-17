namespace Shefaa.Contracts.MedicalRecords;

public record MedicalRecordResponse(
    Guid Id,
    Guid PatientId,
    string PatientName,
    string DoctorName,
    DateTime VisitDate,
    string ChiefComplaint,
    string Diagnosis,
    string? BloodPressure,
    decimal? Temperature
);
