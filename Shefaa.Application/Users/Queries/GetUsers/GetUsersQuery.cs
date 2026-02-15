using ErrorOr;
using MediatR;
using Shefaa.Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Users.Queries.GetUsers
{
    public sealed record GetUsersQuery():IRequest<ErrorOr<List<UserDto>>>;
}
