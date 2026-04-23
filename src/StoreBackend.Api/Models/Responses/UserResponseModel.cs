using System;

namespace StoreBackend.Api.Models.Responses;

public class UserResponseModel
{
    public Guid ExternalId { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
}
