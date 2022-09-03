using IdentityServer.AuthServer.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.AuthServer.Repositories;

public class CustomUserRepository: ICustomUserRepository
{
    private readonly CustomDbContext _context;

    public CustomUserRepository(CustomDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Validate(string email, string password)
    {
        return await _context.CustomUsers.AnyAsync(x => x.Email == email && x.Password==password);
    }

    public async Task<CustomUser> FindUserById(int id)
    {
        return await _context.CustomUsers.FindAsync(id);
    }

    public async Task<CustomUser> FindUserByEmail(string email)
    {
        return await _context.CustomUsers.FirstOrDefaultAsync(x => x.Email == email);
    }
}