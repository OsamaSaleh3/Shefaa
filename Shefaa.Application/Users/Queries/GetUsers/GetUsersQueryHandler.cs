using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<List<UserDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users=await _userRepository.GetAllAsync();
            if(users.Count<1)
            {
                return Error.NotFound("Users.NotFound","No Users Found");
            }
            var userDtos = users.Select(u => new UserDto(
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email!,
                u.Role,
                u.Specialization
            )).ToList();
            
            return userDtos;
        }
    }
}
