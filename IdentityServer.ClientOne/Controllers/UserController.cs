using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace IdentityServer.ClientOne.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //var claims = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier);
            return View();
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }
        [HttpGet]
        public async Task<IActionResult> GetRefreshToken()
        {
            HttpClient httpClient = new HttpClient();
            var discovery = await httpClient.GetDiscoveryDocumentAsync("https://localhost:7161");
            
            var resfreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            
            var token=await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                ClientId = _configuration["ClientOne:ClientId"],
                ClientSecret = _configuration["ClientOne:ClientSecret"],
                RefreshToken = resfreshToken,
                Address = discovery.TokenEndpoint
                
            });

            if (token.IsError)
                return RedirectToAction("Error", "Home");
            
            var tokens = new List<AuthenticationToken>
            {
                new()
                {
                    Name = OpenIdConnectParameterNames.IdToken,
                    Value = token.IdentityToken
                },
                new()
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = token.AccessToken
                },
                new()
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = token.RefreshToken
                },
                new()
                {
                    Name = OpenIdConnectParameterNames.ExpiresIn,
                    Value = DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)
                }
            };

            var authResult = await HttpContext.AuthenticateAsync();
            var properties = authResult.Properties;
            
            properties.StoreTokens(tokens);
            await HttpContext.SignInAsync("Cookies",authResult.Principal,properties);
            return RedirectToAction("Index");
        }

        [Authorize(Roles="admin")]
        public IActionResult AdminAction()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles="customer")]
        public IActionResult CustomerAction()
        {
            return View();
        }
    }
}