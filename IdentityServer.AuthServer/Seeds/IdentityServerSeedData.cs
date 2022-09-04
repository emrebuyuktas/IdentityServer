using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace IdentityServer.AuthServer.Seeds;

public static class IdentityServerSeedData
{
    public static void Seed(ConfigurationDbContext configurationDbContext)
    {
        if (!configurationDbContext.Clients.Any())
        {
            foreach (var client in Config.GetClients())
            {
                configurationDbContext.Clients.Add(client.ToEntity());
            }
        }

        if (!configurationDbContext.ApiResources.Any())
        {
            foreach (var resources in Config.GetApiResources())
            {
                configurationDbContext.ApiResources.Add(resources.ToEntity());
            }
        }
        
        
        if (!configurationDbContext.ApiScopes.Any())
        {
           Config.GetApiScopes().ToList().ForEach(x =>
           {
               configurationDbContext.ApiScopes.Add(x.ToEntity());
           });
        }
        
        if (!configurationDbContext.IdentityResources.Any())
        {
            Config.GetIdentityResources().ToList().ForEach(x =>
            {
                configurationDbContext.IdentityResources.Add(x.ToEntity());
            });
        }

        configurationDbContext.SaveChanges();
    }
}