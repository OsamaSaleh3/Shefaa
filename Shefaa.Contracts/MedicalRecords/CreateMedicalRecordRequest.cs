namespace Shefaa.Contracts.MedicalRecords;

public record CreateMedicalRecordRequest(
    Guid PatientId,
    string DoctorId,
    string ChiefComplaint,
    string Symptoms,
    string Diagnosed,
    string? BloodPressure,
    decimal? Temperature,
    int? Pulse,
    int? RespiratoryRate,
    decimal? Weight,
    decimal? Height,
    Guid? AppointmentId
);
