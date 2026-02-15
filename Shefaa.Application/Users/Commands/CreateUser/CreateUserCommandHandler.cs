using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Users.Commands.CreateUser;
using Shefaa.Application.Users.Dtos;
using Shefaa.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Users.Commands.CreatePatient
{
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
                return Error.Conflict(description: "Email already exists.");
            }
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Role = request.Role,
                UserName=request.Email

            };

            var isCreated = await _userRepository.CreateAsync(user, request.Password);

            if (!isCreated)
            {
                return Error.Failure(description: "Failed to create user.");
            }

            return new UserDto (user.Id,
                                user.FirstName,
                                user.LastName,
                                user.Email,
                                user.Role,
                                user.Specialization);
        }
    }
}
