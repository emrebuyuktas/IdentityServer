using IdentityServer4.Models;

namespace IdentityServer.AuthServer;

public static class Config
{
    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
        {
            new ApiResource
            {
                Name = "resource_api_1",
                Scopes = {"api1.read","api1.write","api1.update"}
            },
            new ApiResource
            {
                Name = "resource_api_2",
                Scopes = {"api2.read","api2.write","api2.update"}
            }
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("api1.read", "read permission for api1"),
            new ApiScope("api1.write", "write permission for api1"),
            new ApiScope("api1.update", "update permission for api1"),
            new ApiScope("api2.read", "read permission for api2"),
            new ApiScope("api2.write", "write permission for api2"),
            new ApiScope("api2.update", "update permission for api2")
        };
    }

    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "Client1",
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                ClientName = "ClientOne",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"api1.read",}
            },
            new Client
            {
                ClientId = "Client2",
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                ClientName = "ClientTwo",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"api1.read","api2.write","api2.update"}
            }
        };
    }
}