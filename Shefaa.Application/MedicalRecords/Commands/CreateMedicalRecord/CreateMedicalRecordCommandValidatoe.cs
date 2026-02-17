using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Commands.CreateMedicalRecord
{
    public class CreateMedicalRecordCommandValidatoe:AbstractValidator<CreateMedicalRecordCommand>
    {
        public CreateMedicalRecordCommandValidatoe() {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Patient ID is required.");

            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.");

            RuleFor(x => x.BloodPressure)
           .Matches(@"^\d{2,3}/\d{2,3}$")
           .WithMessage("Blood Pressure must be in format '120/80'.")
           .When(x => !string.IsNullOrWhiteSpace(x.BloodPressure));

            RuleFor(x => x.ChiefComplaint)
                .NotEmpty().WithMessage("Chief complaint is required.")
                .MaximumLength(500).WithMessage("Chief complaint must not exceed 500 characters.");

            RuleFor(x => x.Symptoms)
                .NotEmpty().WithMessage("Symptoms are required.")
                .MaximumLength(1000).WithMessage("Symptoms description is too long.");

            RuleFor(x => x.Diagnosed)
                .NotEmpty().WithMessage("Diagnosis is required.")
                .MaximumLength(1000).WithMessage("Diagnosis description is too long.");

            RuleFor(x => x.AppointmentId)
                .NotEqual(Guid.Empty).When(x => x.AppointmentId.HasValue)
                .WithMessage("Invalid Appointment ID.");
    
        }
    }
}
