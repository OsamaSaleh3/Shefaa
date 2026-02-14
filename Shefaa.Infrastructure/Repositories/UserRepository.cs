using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.Users;
using Shefaa.Infrastructure.Common.Persistence;

namespace Shefaa.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ShefaaDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public UserRepository(
        ShefaaDbContext dbContext,
        UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<ErrorOr<User>> CreateUserAsync(
        string firstName,
        string lastName,
        string email,
        string password,
        UserRole role,
        string? specialization,
        string? phoneNumber)
    {
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser is not null)
            {
                return Error.Conflict(description: $"User with email '{email}' already exists.");
            }

            var user = new User
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Role = role,
                Specialization = specialization,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(e => Error.Failure(description: e.Description))
                    .Cast<Error>()
                    .ToList();

                return errors;
            }

            await _userManager.AddToRoleAsync(user, role.ToString());

            return user;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: $"An error occurred while creating the user: {ex.Message}");
        }
    }

    public async Task<ErrorOr<User>> ValidateUserAsync(string email, string password)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return Error.Unauthorized(description: "Invalid email or password.");
            }

            if (!user.IsActive)
            {
                return Error.Unauthorized(description: "User account is not active.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
            {
                return Error.Unauthorized(description: "Invalid email or password.");
            }

            return user;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: $"An error occurred while validating the user: {ex.Message}");
        }
    }
}
