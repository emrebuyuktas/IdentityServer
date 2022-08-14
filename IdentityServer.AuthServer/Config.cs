using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Test;

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
                Scopes = {"api1.read","api1.write","api1.update"},
                ApiSecrets = new []{new  Secret("secretapi1".Sha256())}
            },
            new ApiResource
            {
                Name = "resource_api_2",
                Scopes = {"api2.read","api2.write","api2.update"},
                ApiSecrets = new []{new  Secret("secretapi2".Sha256()) }
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
                AllowedScopes = {"api1.read","api1.update","api2.write","api2.update"}
            }
        };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources(){
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            
        };
    }
    public static IEnumerable<TestUser> GetUsers()
    {
        return new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "emrebtas@gmail",
                Password = "emre123",
                Claims = new List<Claim>{new Claim("given_name","Emre"),new Claim("family_name","büyüktaş")}
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "emrebyk@gmail",
                Password = "emre123",
                Claims = new List<Claim>{new Claim("given_name","Emreb"),new Claim("family_name","Büyüktaş")}
            }
        };
    }
}