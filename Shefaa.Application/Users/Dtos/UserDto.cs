using Shefaa.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Users.Dtos
{
    public record UserDto(
        string Id,
        string FirstName,
        string LastName,
        string Email,
        UserRole Role,
        string? Specialization
    );
}
