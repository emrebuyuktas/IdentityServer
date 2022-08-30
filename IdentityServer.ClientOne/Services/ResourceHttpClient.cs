using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace IdentityServer.ClientOne.Services;

public class ResourceHttpClient:IResourceHttpClient
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly HttpClient _client;
    
    public ResourceHttpClient(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
        _client = new HttpClient();
    }

    public async Task<HttpClient> GetHttpClientAsync()
    {
        var accessToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        _client.SetBearerToken(accessToken);
        return _client;
    }
}