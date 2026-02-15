using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Patients.Commands.UpdatePatient
{
    public class UpdatePatientCommandValidator:AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
        {
            RuleFor(x => x.FirstName)
           .NotEmpty().WithMessage("First name is required")
           .MaximumLength(50).WithMessage("First name must not exceed 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth cannot be in the future");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender value");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters");

            RuleFor(x => x.EmergencyContactName)
                .NotEmpty().WithMessage("Emergency contact name is required");

            RuleFor(x => x.EmergencyContactPhone)
                .NotEmpty().WithMessage("Emergency contact phone is required")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid emergency phone format");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.BloodType)
                .IsInEnum().WithMessage("Invalid blood type")
                .When(x => x.BloodType.HasValue);

            RuleFor(x => x.GeneralNotes)
                .MaximumLength(500).WithMessage("Notes must not exceed 500 characters")
                .When(x => !string.IsNullOrEmpty(x.GeneralNotes));
        }
    }
}
