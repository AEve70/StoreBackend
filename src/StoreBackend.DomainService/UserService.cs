using System;
using StoreBackend.Domain.Entities;
using StoreBackend.Exceptions;
using StoreBackend.Infrastructure.Repositories;

namespace StoreBackend.DomainService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();

        if (users == null || users.Count == 0)
        {
            throw new ResourceNotFoundException("No users found.");
        }
        return users;
    }
}
