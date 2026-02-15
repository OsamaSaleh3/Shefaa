using ErrorOr;
using MediatR;

namespace Shefaa.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(string Id,
    string FirstName,
    string LastName,
    string? Specialization,
    string PhoneNumber
    ) : IRequest<ErrorOr<Updated>>;
