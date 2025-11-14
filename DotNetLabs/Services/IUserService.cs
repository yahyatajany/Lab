using System;
using DotNetLabs.Entities;

namespace DotNetLabs.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User?> CreateUserAsync(User user, string password);
    Task<bool> DeleteUserAsync(int id);
}
