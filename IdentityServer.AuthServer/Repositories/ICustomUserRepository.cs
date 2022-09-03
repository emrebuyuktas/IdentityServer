using IdentityServer.AuthServer.Models;

namespace IdentityServer.AuthServer.Repositories;

public interface ICustomUserRepository
{
    Task<bool> Validate(string email, string password);
    Task<CustomUser> FindUserById(int id);
    Task<CustomUser> FindUserByEmail(string email);
}