using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Contracts.Users
{
    public record UpdateUserRequest
    (
        string Id,
        string FirstName,
        string LastName,
        string? Specialization,
        string PhoneNumber
    );
}
