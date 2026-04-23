using System;
using StoreBackend.Domain.Entities;

namespace StoreBackend.DomainService;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
}
