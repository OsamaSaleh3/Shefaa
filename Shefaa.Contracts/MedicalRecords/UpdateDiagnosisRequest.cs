namespace Shefaa.Contracts.MedicalRecords;

public record UpdateDiagnosisRequest(
    Guid Id,
    string NewDiagnosis,
    string? AdditionalNotes
);
