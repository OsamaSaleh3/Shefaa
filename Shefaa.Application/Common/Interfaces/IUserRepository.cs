using Shefaa.Domain.Users;

namespace Shefaa.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(string id);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<bool> CreateAsync(User user, string password);
    Task DeleteAsync(User user);
    Task UpdateAsync(User user);
    Task<List<User>> GetAllAsync();
}