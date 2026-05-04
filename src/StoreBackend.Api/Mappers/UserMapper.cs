using System;
using StoreBackend.Api.Models.Requests;
using StoreBackend.Api.Models.Responses;
using StoreBackend.Dto;

namespace StoreBackend.Api.Mappers;

public class UserMapper
{
    public static List<UserResponseModel> toModel(List<UserDto> users)
    {
        return users.Select(u => toModel(u)).ToList();
    }

    public static UserResponseModel toModel(UserDto user)
    {
        return new UserResponseModel
        {
            ExternalId = user.ExternalId,
            Name = user.Name,
            Username = user.Username,
            Email = user.Email
        };
    }

    public static CreateUserDto ToDto(CreateUserRequestModel user)
    {
        return new CreateUserDto
        {
            Name = user.Name,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
        };
    }
}
