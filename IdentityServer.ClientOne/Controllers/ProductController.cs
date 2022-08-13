using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer.ClientOne.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityServer.ClientOne.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var discovery = await client.GetDiscoveryDocumentAsync("https://localhost:7161");
            if (discovery.IsError)
            {
                Console.Write("Discovery Error");
            }

            var clientCredentialRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _configuration["Client:ClientId"],
                ClientSecret = _configuration["Client:ClientSecret"],
                Address = discovery.TokenEndpoint
            };
            var token=await client.RequestClientCredentialsTokenAsync(clientCredentialRequest);
            if (token.IsError)
            {
                Console.Write("Token Error");
            }
            client.SetBearerToken(token.AccessToken);

            var response = await client.GetAsync("https://localhost:7018/api/Product/GetProducts");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return View(JsonConvert.DeserializeObject<List<Product>>(data));
            }

            return null;
        }
    }
}