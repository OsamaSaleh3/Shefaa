using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Queries.GetDoctorAppointments
{
    public class GetDoctorAppointmentsQueryValidator:AbstractValidator<GetDoctorAppointmentsQuery>
    {
        public GetDoctorAppointmentsQueryValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.");
            RuleFor(x => x.Date)
                .GreaterThan(DateTime.MinValue).WithMessage("Date must be a valid date.");
        }
    }

}
