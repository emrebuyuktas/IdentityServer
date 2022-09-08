using System.Threading.Tasks;
using IdentityModel;
using IdentityServer.IdentityApi.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.IdentityApi.Services
{
    public class IdentityResourceOwnerPasswordValidator: IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _userManager.FindByEmailAsync(context.UserName);
            if(existUser==null) return;

            var passWordCheck = await _userManager.CheckPasswordAsync(existUser,context.Password);

            if (!passWordCheck) return;

            context.Result = new GrantValidationResult(existUser.Id.ToString(),
                OidcConstants.AuthenticationMethods.Password);
        }
    }
}