using ErrorOr;
using MediatR;
using Shefaa.Domain.Patients.enums;

namespace Shefaa.Application.Patients.Commands.CreatePatient;

public sealed record CreatePatientCommand(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    Gender Gender,
    string PhoneNumber,
    string Address,
    string EmergencyContactName,
    string EmergencyContactPhone,

    string ? Email,
    BloodType? BloodType,
    string ? GeneralNotes
    )
    :IRequest<ErrorOr<Guid>>;
