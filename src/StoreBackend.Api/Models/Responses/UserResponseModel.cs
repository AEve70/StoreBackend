using System;

namespace StoreBackend.Api.Models.Responses;

public class UserResponseModel
{
    public Guid ExternalId { get; set; }
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
}
