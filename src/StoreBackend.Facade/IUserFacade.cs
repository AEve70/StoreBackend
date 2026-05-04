using System;
using StoreBackend.Dto;

namespace StoreBackend.Facade;

public interface IUserFacade
{
    Task<List<UserDto>> GetAllUsers();
    Task<UserDto> CreateUserAsync(CreateUserDto user);

}
