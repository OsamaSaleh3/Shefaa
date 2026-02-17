namespace Shefaa.Application.Prescriptions.Dtos;

public record PrescriptionMedicationDto(
    string Name,
    string Dosage,
    string Frequency,
    string Duration,
    string? Instructions
);