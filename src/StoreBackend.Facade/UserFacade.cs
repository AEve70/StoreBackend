using System;
using StoreBackend.DomainService;
using StoreBackend.Dto;
using StoreBackend.Facade.Mappers;
using StoreBackend.Infrastructure;

namespace StoreBackend.Facade;

public class UserFacade : IUserFacade
{
    private readonly IUserService _userService;
    private readonly AppDbContext context;

    public UserFacade(IUserService userService, AppDbContext dbContext)
    {
        _userService = userService;
        context = dbContext;
    }
    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return UserMapper.ToUserDto(users);
    }
}
