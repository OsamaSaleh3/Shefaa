using ErrorOr;
using MediatR;
using Shefaa.Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Users.Queries.GetUserById
{
    public sealed record GetUserByIdQuery(string id):IRequest<ErrorOr<UserDto>>;
}
