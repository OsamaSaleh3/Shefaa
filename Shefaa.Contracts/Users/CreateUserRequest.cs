using System.ComponentModel.DataAnnotations;
using Shefaa.Domain.Users;

namespace Shefaa.Contracts.Users;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    UserRole Role,
    string? Specialization,
    string? PhoneNumber
);
