using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Appointments.Queries.GetCalendarView
{
    public class GetCalendarViewQueryValidator:AbstractValidator<GetCalendarViewQuery>
    {
        public GetCalendarViewQueryValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.");
            RuleFor(x => x.From)
                .LessThan(x => x.To).WithMessage("Start date must be before end date.");
            RuleFor(x=>x.From)
                .LessThan(DateTime.Now).WithMessage("Start date must be in the past or present.");
            RuleFor(x=>x.To)
                .GreaterThan(DateTime.Now).WithMessage("End date must be in the future.");
        }
    }
}
