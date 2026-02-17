namespace Shefaa.Contracts.MedicalRecords;

public record UpdateDiagnosisRequest(
    string NewDiagnosis,
    string? AdditionalNotes
);
