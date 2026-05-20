using StoreBackend.Api.Models.Requests;
using StoreBackend.Api.Models.Responses;
using StoreBackend.Dto;

namespace StoreBackend.Api.Mappers;

public static class AuthorizationMapper
{
    public static AuthorizationRequestDto ToDto(this AuthorizationRequestModel model)
    {
        return new AuthorizationRequestDto
        {
            Username = model.Username,
            Password = model.Password,
        };
    }

    public static AuthorizationResponse ToResponse(this AuthorizationResponseDto dto)
    {
        return new AuthorizationResponse
        {
            BearerToken = dto.BearerToken,
            ExpiresIn = dto.ExpiresIn,
        };
    }
}