using System;
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
            Username = user.Username,
            Email = user.Email
        };
    }
}
