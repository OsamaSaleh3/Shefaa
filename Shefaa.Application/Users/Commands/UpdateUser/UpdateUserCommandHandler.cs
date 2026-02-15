using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<Updated>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<Updated>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if(user is null)
            {
                return Error.NotFound("User.NotFound", $"No user found with ID {request.Id}");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Specialization = request.Specialization;
            user.PhoneNumber = request.PhoneNumber;

            await _userRepository.UpdateAsync(user);

            return Result.Updated;

        }
    }
}
