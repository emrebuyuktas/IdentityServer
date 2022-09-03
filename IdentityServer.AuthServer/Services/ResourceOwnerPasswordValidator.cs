using IdentityModel;
using IdentityServer.AuthServer.Repositories;
using IdentityServer4.Validation;

namespace IdentityServer.AuthServer.Services;

public class ResourceOwnerPasswordValidator: IResourceOwnerPasswordValidator
{
    private readonly ICustomUserRepository _userRepository;

    public ResourceOwnerPasswordValidator(ICustomUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var isUserValid = await _userRepository.Validate(context.UserName,context.Password);

        if (isUserValid)
        {
            var user = await _userRepository.FindUserByEmail(context.UserName);
            context.Result = new GrantValidationResult(user.Id.ToString(),OidcConstants.AuthenticationMethods.Password);
        }
    }
}