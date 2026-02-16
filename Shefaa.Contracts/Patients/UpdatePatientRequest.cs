using Shefaa.Domain.Patients.enums;

namespace Shefaa.Contracts.Patients;

public record UpdatePatientRequest(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    Gender Gender,
    string Phone,
    string Address,
    string? Email,
    string EmergencyContactName,
    string EmergencyContactPhone,
    BloodType? BloodType,
    string? GeneralNotes
);
