using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Users.Dtos;

namespace Shefaa.Application.Users.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<AuthenticationDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.email);
        if (user is null)
        {
            return Error.Unauthorized(description: "invalid email or password");
        }

        if (!await _userRepository.CheckPasswordAsync(user, request.password))
        {
            return Error.Unauthorized(description: "invalid email or password");
        }

        if (!user.IsActive)
        {
            return Error.Unauthorized(description: "account is not active");
        }
        var token =_jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationDto(new UserDto
        (
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email!,
            user.Role,
            user.Specialization?? "no specialization"
        ),
        token);

    }
}
