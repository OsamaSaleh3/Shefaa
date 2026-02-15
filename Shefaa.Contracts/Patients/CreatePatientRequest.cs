using Shefaa.Domain.Patients.enums;

namespace Shefaa.Contracts.Patients;

public record CreatePatientRequest(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    Gender Gender,
    string PhoneNumber,
    string Address,
    string EmergencyContactName,
    string EmergencyContactPhone,
    string? Email,
    BloodType? BloodType,
    string? GeneralNotes
);
