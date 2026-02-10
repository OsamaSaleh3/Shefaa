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
            var result = await _userRepository
               .CreateUserAsync(
               request.FirstName,
               request.LastName,
               request.Email,
               request.Password,
               request.Role,
               request.Specialization,
               request.PhoneNumber
               );

            if (result.IsError)
            {
                return result.Errors;
            }

            var user = result.Value;

            return new UserDto(
                user.Id, 
                user.FirstName,
                user.LastName,
                user.Email!,
                user.Role,
                user.Specialization
            );

        }
    }
}
