namespace IdentityServer.ClientOne.Services;

public interface IResourceHttpClient
{
    Task<HttpClient> GetHttpClientAsync();
}