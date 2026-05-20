using System;
using Microsoft.EntityFrameworkCore;
using StoreBackend.Domain.Entities;

namespace StoreBackend.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        return user;
    }

    public Task<List<User>> GetAllUsers()
    {
        return _context.Users.ToListAsync();
    }

    public async Task<bool> HasUserByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> HasUserByUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public Task<User?> GetByUsername(string username)
    {
        return _context.Users
         .Include(u => u.UserRoles)
         .ThenInclude(ur => ur.Role)
         .FirstOrDefaultAsync(u => u.Username == username);
    }


    public Task<User?> GetByIdAsync(Guid userId)
    {
        return _context.Users
         .Include(u => u.UserRoles)
         .ThenInclude(ur => ur.Role)
         .FirstOrDefaultAsync(u => u.ExternalId == userId);
    }
}



