using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Shefaa.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Success>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if(request.Id is null)
            {
                return Error.Validation(code: "UserId", description: "UserId is required");
            }

            var user= await _userRepository.GetByIdAsync(request.Id);
            if(user is null)
            {
                return Error.NotFound(code: "UserNotFound", description: $"User with id {request.Id} not found");
            }

            await _userRepository.DeleteAsync(user);
            
           
            return Result.Deleted;
        }
    }
}
