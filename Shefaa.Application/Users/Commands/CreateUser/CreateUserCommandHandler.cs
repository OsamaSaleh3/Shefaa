using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Users.Dtos;
using Shefaa.Domain.Users;

namespace Shefaa.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByEmailAsync(request.Email) is not null)
        {
            return Error.Conflict(
                code: "User.DuplicateEmail",
                description: "Email already exists.");
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email, 
            Role = request.Role,
            Specialization = request.Specialization
        };

        var (isSuccess, errors) = await _userRepository.CreateAsync(user, request.Password);

        if (!isSuccess)
        {
            return Error.Failure(
                code: "User.IdentityFailure",
                description: string.Join(", ", errors));
        }

        return new UserDto(
            user.Id, 
            user.FirstName,
            user.LastName,
            user.Email!, 
            user.Role,
            user.Specialization);
    }
}