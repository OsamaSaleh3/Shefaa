using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Commands.BookAppointment
{
    public class BookAppointmentCommandValidator:AbstractValidator<BookAppointmentCommand>
    {
        public BookAppointmentCommandValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Patient ID is required.");
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.");
            RuleFor(x => x.AppointmentDate)
                .GreaterThan(DateTime.Now).WithMessage("Appointment date must be in the future.");
            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0).WithMessage("Duration must be greater than zero.");
        }
    }
}
