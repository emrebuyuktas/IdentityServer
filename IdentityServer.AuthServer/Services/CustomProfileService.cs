using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityServer.AuthServer.Repositories;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityServer.AuthServer.Services;

public class CustomProfileService: IProfileService
{
    private readonly ICustomUserRepository _userRepository;

    public CustomProfileService(ICustomUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var userId = context.Subject.GetSubjectId();
        var user = await _userRepository.FindUserById(int.Parse(userId));

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("name", user.UserName),
            new Claim("city", user.City)
        };

        if (user.Id == 1)
        {
            claims.Add(new Claim("role","admin"));
        }
        else
        {
            claims.Add(new Claim("role","customer"));
        }
        context.AddRequestedClaims(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var userId = context.Subject.GetSubjectId();
        var user = await _userRepository.FindUserById(int.Parse(userId));
        context.IsActive = user != null;
    }
}