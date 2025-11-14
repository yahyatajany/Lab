using System;
using DotNetLabs.Entities;

namespace DotNetLabs.Services;

public interface IAuthService
{
    Task<string?> LoginAsync(string username, string password);
    Task<User?> RegisterAsync(string username, string password, string role = "User");

}
