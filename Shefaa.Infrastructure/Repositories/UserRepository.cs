using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.Users;
using Shefaa.Infrastructure.Common.Persistence;
using System;
using System.Threading.Tasks;

namespace Shefaa.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<(bool IsSuccess, string[] Errors)> CreateAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description).ToArray());
            }
            var roleResult = await _userManager.AddToRoleAsync(user, user.Role.ToString());
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return (false, roleResult.Errors.Select(e => e.Description).ToArray());
            }
            return (true, Array.Empty<string>());
        }

        public async Task DeleteAsync(User user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userManager.Users
                .Where(u=>!u.IsDeleted&&u.IsActive)
                .ToListAsync();
        }
    }
}