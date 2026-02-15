using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Users.Commands.DeleteUser
{
    public sealed record DeleteUserCommand(string Id) : IRequest<ErrorOr<Deleted>>;
  
}
