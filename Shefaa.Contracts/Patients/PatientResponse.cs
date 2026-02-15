namespace Shefaa.Contracts.Patients;

public record PatientResponse(
    Guid Id,
    string FileNumber,
    string FirstName,
    string LastName,
    string FullName,
    DateOnly DateOfBirth,
    int Age,
    string Gender,
    string Phone,
    string? Email,
    string Address,
    string? BloodType,
    string EmergencyContactName,
    string EmergencyContactPhone,
    string? GeneralNotes
);
