using ErrorOr;
using MediatR;
using Shefaa.Application.Users.Dtos;
using Shefaa.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,        
        string Password,
        UserRole Role,
        string? Specialization,
        string? PhoneNumber
    ) : IRequest<ErrorOr<UserDto>>;
}
