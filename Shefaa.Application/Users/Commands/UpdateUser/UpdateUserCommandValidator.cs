using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator:AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
              .NotEmpty().WithMessage("User ID is required");

           RuleFor(x => x.FirstName)
             .NotEmpty().WithMessage("First name is required")
             .Length(2, 50).WithMessage("First name must be between 2 and 50 characters");

            RuleFor(x => x.LastName)
              .NotEmpty().WithMessage("Last name is required")
              .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters");

            RuleFor(x => x.PhoneNumber)
              .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number format is invalid")
              .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

        }
    }
}
