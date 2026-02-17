namespace Shefaa.Contracts.MedicalRecords;

public record UpdateMedicalRecordVitalsRequest(
    Guid Id,
    string? BloodPressure,
    decimal? Temperature,
    int? Pulse,
    int? RespiratoryRate,
    decimal? Weight,
    decimal? Height
);
