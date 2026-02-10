using ErrorOr;
using Shefaa.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<ErrorOr<User>> CreateUserAsync(string firstName, string lastName,string email, string password, UserRole role,string? Specialization,string? PhoneNumber);
        Task<ErrorOr<User>> ValidateUserAsync(string email, string password);
    }
}
