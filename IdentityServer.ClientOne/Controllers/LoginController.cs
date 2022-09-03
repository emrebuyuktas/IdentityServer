using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer.ClientOne.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace IdentityServer.ClientOne.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            var client = new HttpClient();
            var discovery = await client.GetDiscoveryDocumentAsync("https://localhost:7161");

            if (discovery.IsError)
                return RedirectToAction("Error", "Home");

            var password = new PasswordTokenRequest();
            password.Address = discovery.TokenEndpoint;
            password.UserName = loginViewModel.Email;
            password.Password = loginViewModel.Password;
            password.ClientId = "Client1-ResourceOwner-Mvc";
            password.ClientSecret = "secret";
            
            var token=await client.RequestPasswordTokenAsync(password);
            
            if(token.IsError)
                return RedirectToAction("Error", "Home");

            var userInfoRequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = discovery.UserInfoEndpoint
            };
            var userInfo = await client.GetUserInfoAsync(userInfoRequest);
            
            if(userInfo.IsError)
                return RedirectToAction("Error", "Home");
            var claimsIdentity = new ClaimsIdentity(userInfo.Claims,CookieAuthenticationDefaults.AuthenticationScheme,
                "name","role");
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProps = new AuthenticationProperties();
            authProps.StoreTokens(new List<AuthenticationToken>
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
            });
            
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,claimPrincipal,authProps);
            
            return RedirectToAction("Index","User");
        }
    }
}