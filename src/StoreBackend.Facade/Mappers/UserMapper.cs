using System;
using StoreBackend.Domain.Entities;
using StoreBackend.Dto;

namespace StoreBackend.Facade.Mappers;

public class UserMapper
{

    public static List<UserDto> ToUserDto(List<User> users)
    {
        return users.Select(u => ToUserDto(u)).ToList();
    }

    public static UserDto ToUserDto(User user)
    {
        return new UserDto
        {
            ExternalId = user.ExternalId,
            Name = user.Name,
            Username = user.Username,
            Email = user.Email

        };
    }
}
