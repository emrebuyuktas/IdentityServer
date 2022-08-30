using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer.ClientOne.Models;
using IdentityServer.ClientOne.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;

namespace IdentityServer.ClientOne.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IResourceHttpClient _client;
        public ProductController(IConfiguration configuration, IResourceHttpClient client)
        {
            _configuration = configuration;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = await _client.GetHttpClientAsync();
            // HttpClient client = new HttpClient();
            // var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            // client.SetBearerToken(accessToken);
            
            //var discovery = await client.GetDiscoveryDocumentAsync("https://localhost:7161");
            // if (discovery.IsError)
            // {
            //     Console.Write("Discovery Error");
            // }
            //
            // var clientCredentialRequest = new ClientCredentialsTokenRequest
            // {
            //     ClientId = _configuration["Client:ClientId"],
            //     ClientSecret = _configuration["Client:ClientSecret"],
            //     Address = discovery.TokenEndpoint
            // };
            //var token=await client.RequestClientCredentialsTokenAsync(clientCredentialRequest);
            // if (token.IsError)
            // {
            //     Console.Write("Token Error");
            // }
            // client.SetBearerToken(token.AccessToken);

            var response = await client.GetAsync("https://localhost:7018/api/Product/GetProducts");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<List<Product>>(data));
            }

            return RedirectToAction("Error","Home");
        }
    }
}