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
        if (await _userRepository.HasUserByUsernameAsync(user.Username))
        {
            throw new Exceptions.BadRequestResponseException("Username is already taken");
        }
        if (await _userRepository.HasUserByEmailAsync(user.Email))
        {
            throw new Exceptions.BadRequestResponseException("Email is already taken");
        }

        var entity = new User
        {
            ExternalId = Guid.NewGuid(),
            Name = user.Name,
            Username = user.Username,
            Email = user.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
        };

        return await _userRepository.CreateAsync(entity);

    }

    public Task<List<User>> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public Task<User?> GetByResourceIdAsync(Guid id)
    {
        return _userRepository.GetByIdAsync(id);
    }

    public async Task<User?> GetByUserAndPassword(AuthorizationRequestDto request)
    {
        var user = await _userRepository.GetByUsername(request.Username);
        if (user == null)
        {
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return null;
        }

        return user;
    }
}
