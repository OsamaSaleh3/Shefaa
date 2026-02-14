using Shefaa.Domain.Users;

namespace Shefaa.Contracts.Users;

public record UserResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    UserRole Role,
    string? Specialization
);
