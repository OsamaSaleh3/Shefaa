using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ErrorOr<UserDto>>
    {
        private readonly IUserRepository _userRepository;
public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user=await _userRepository.GetByIdAsync(request.id);
            if (user is null)
            {
                return Error.NotFound("User.NotFound", $"User with id {request.id} not found.");
            }

            return new UserDto(
                Id: user.Id,
                FirstName: user.FirstName,
                LastName: user.LastName,
                Email: user.Email!,
                Role: user.Role,
                Specialization: user.Specialization
                );
        }
    }
}
