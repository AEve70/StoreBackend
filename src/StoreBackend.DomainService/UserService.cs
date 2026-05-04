using System;
using StoreBackend.Domain.Entities;
using StoreBackend.Dto;
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

    public async Task<User> CreateUserAsync(CreateUserDto user)
    {
        if (await _userRepository.HasUserByUsernameAsync(user.Username)) throw new BadRequestResponseException("Username is already taken.");

        if (await _userRepository.HasUserByEmailAsync(user.Email)) throw new BadRequestResponseException("Email is already taken.");

        var entity = new User
        {
            ExternalId = Guid.NewGuid(),
            Name = user.Name,
            Username = user.Username,
            Email = user.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password)
        };

        return await _userRepository.CreateAsync(entity);

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
