using ErrorOr;
using MediatR;
using Shefaa.Domain.Patients.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Patients.Commands.UpdatePatient
{
    public sealed record UpdatePatientCommand(
    Guid Id, 
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
        ) :IRequest<ErrorOr<Success>>;
}
