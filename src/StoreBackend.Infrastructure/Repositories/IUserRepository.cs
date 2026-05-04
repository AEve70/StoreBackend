using System;
using StoreBackend.Domain.Entities;

namespace StoreBackend.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllUsers();
    Task<User> CreateAsync(User user);
    Task<bool> HasUserByUsernameAsync(string username);
    Task<bool> HasUserByEmailAsync(string email);

}
