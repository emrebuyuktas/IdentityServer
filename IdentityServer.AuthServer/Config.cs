using System.Security.Claims;
using IdentityServer4;
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
                AllowedScopes = {"api1.read"}
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
            },
            new Client
            {
                ClientId = "Client1-Mvc",
                RequirePkce=false,
                ClientName="Client 1 app",
                ClientSecrets=new[] {new Secret("secret".Sha256())},
                AllowedGrantTypes= GrantTypes.Hybrid,
                RedirectUris=new  List<string>{"https://localhost:7169/signin-oidc"},
                PostLogoutRedirectUris = new  List<string>{"https://localhost:7169/signout-callback-oidc"},
                AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.Profile, "api1.read",
                    IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"},
                AccessTokenLifetime = (int)(DateTime.Now.AddHours(2)-DateTime.Now).TotalSeconds,
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.ReUse,
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                RequireConsent = true
                
            },
            new Client
            {
                ClientId = "Client2-Mvc",
                RequirePkce=false,
                ClientName="Client 2 app",
                ClientSecrets=new[] {new Secret("secret".Sha256())},
                AllowedGrantTypes= GrantTypes.Hybrid,
                RedirectUris=new  List<string>{"https://localhost:7118/signin-oidc"},
                PostLogoutRedirectUris = new  List<string>{"https://localhost:7118/signout-callback-oidc"},
                AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read",
                    IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"},
                AccessTokenLifetime = (int)(DateTime.Now.AddHours(2)-DateTime.Now).TotalSeconds,
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.ReUse,
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                RequireConsent = true
                
            },
            new Client
            {
                ClientId = "Client1-ResourceOwner-Mvc",
                ClientName="Client 1 app",
                ClientSecrets=new[] {new Secret("secret".Sha256())},
                AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.Profile, "api1.read",
                    IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"},
                AccessTokenLifetime = (int)(DateTime.Now.AddHours(2)-DateTime.Now).TotalSeconds,
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.ReUse,
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                
            }
        };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources(){
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "CountryAndCity", DisplayName = "Country and city", Description = "Country and city informations", UserClaims = new []{"country","city"}
            },
            new IdentityResource
            {
                Name="Roles",DisplayName="Roles", Description="User Roles", UserClaims=new [] { "role"}
            }
            
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
                Claims = new List<Claim>{new ("given_name","Emre"), new ("family_name","büyüktaş"),
                    new ("country","Turkey"), new ("city","Adana"),new ("role","admin")}
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "emrebyk@gmail",
                Password = "emre123",
                Claims = new List<Claim>{new ("given_name","Emreb"),new ("family_name","Büyüktaş"), 
                    new ("country","Turkey"), new ("city","Adana"),new ("role","customer")}
            }
        };
    }
}