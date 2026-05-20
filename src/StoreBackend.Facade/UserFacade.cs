using System;
using StoreBackend.Domain.Entities;
using StoreBackend.DomainService;
using StoreBackend.Dto;
using StoreBackend.Exceptions;
using StoreBackend.Facade.Mappers;
using StoreBackend.Infrastructure;


namespace StoreBackend.Facade;

public class UserFacade : IUserFacade
{
    private readonly IUserService _userService;
    private readonly IRoleService roleService;
    private readonly AppDbContext context;

    public UserFacade(IUserService userService, IRoleService roleService, AppDbContext dbContext)
    {
        _userService = userService;
        this.roleService = roleService;
        context = dbContext;
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto user)
    {
        var entity = await _userService.CreateUserAsync(user);
        await context.SaveChangesAsync();
        return UserMapper.ToUserDto(entity);
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return UserMapper.ToUserDto(users);
    }

    public async Task<UserRolesDto> GetUserRolesAsync(Guid userId)
    {
        var user = await _userService.GetByResourceIdAsync(userId);

        if (user == null)
        {
            throw new ResourceNotFoundException();
        }

        return UserMapper.ToUserRolesDto(user);
    }

    public async Task<UserRolesDto> UpdateUserRolesAsync(Guid userId, UpdateRolesDto dto)
    {
        List<Role>? allRoles = null;

        if (dto.Roles?.Count > 0)
        {
            allRoles = await roleService.GetAllAsync();

            if (dto.Roles.Any(role => !allRoles.Any(e => e.Name.Equals(role))))
            {
                throw new BadRequestResponseException("One or more roles do not exist.");
            }
        }

        var user = await _userService.GetByResourceIdAsync(userId);

        if (user == null)
        {
            throw new ResourceNotFoundException();
        }

        user.ClearRoles();

        if (dto.Roles?.Count > 0)
        {
            allRoles ??= await roleService.GetAllAsync();
            var matchedRoles = allRoles.Where(r => dto.Roles.Any(role => r.Name.Equals(role))).ToList();

            var userRoles = matchedRoles.Select(role => new UserRole
            {
                User = user,
                Role = role,
            }).ToList();

            user.UserRoles.AddRange(userRoles);
        }

        await context.SaveChangesAsync();

        return UserMapper.ToUserRolesDto(user);
    }

    public async Task DeleteUserRolesAsync(Guid userId)
    {
        var user = await _userService.GetByResourceIdAsync(userId);

        if (user == null)
        {
            throw new ResourceNotFoundException();
        }

        user.ClearRoles();

        await context.SaveChangesAsync();
    }
}
