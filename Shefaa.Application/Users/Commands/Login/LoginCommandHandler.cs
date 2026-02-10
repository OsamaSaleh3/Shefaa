using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Users.Commands.Login
{
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
            var userResult = await _userRepository.ValidateUserAsync(request.email, request.password);
            if (userResult.IsError)
            {
                return userResult.Errors;
            }

            var user = userResult.Value;

            var token=_jwtTokenGenerator.GenerateToken(user);

            var userDto=new UserDto
            (user.Id,
             user.FirstName,
             user.LastName,
             user.Email!,
             user.Role,
             user.Specialization);

            return new AuthenticationDto(userDto, token);
        }
    }
}
