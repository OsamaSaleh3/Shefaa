using ErrorOr;
using MediatR;
using Shefaa.Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Users.Commands.Login
{
    public record LoginCommand(string email,string password):IRequest<ErrorOr<AuthenticationDto>>;
}
