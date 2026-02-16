using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Commands.RescheduleAppointment
{
    public class RescheduleAppointmentCommandValidator:AbstractValidator<RescheduleAppointmentCommand>
    {
        public RescheduleAppointmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Appointment ID is required.");
            RuleFor(x => x.NewDate)
                .GreaterThan(DateTime.Now).WithMessage("The new appointment date must be in the future.");
           
        }

    }
}
