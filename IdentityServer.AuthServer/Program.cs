using System.Reflection;
using IdentityServer.AuthServer;
using IdentityServer.AuthServer.Models;
using IdentityServer.AuthServer.Repositories;
using IdentityServer.AuthServer.Seeds;
using IdentityServer.AuthServer.Services;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CustomDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"));
});

var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

// Add services to the container.
builder.Services.AddScoped<ICustomUserRepository, CustomUserRepository>();
builder.Services.AddControllersWithViews();
builder.Services.AddIdentityServer().AddInMemoryApiResources(Config.GetApiResources())
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"),
            sqloptions =>
            {
                sqloptions.MigrationsAssembly(assemblyName);
            });
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"),
            sqloptions =>
            {
                sqloptions.MigrationsAssembly(assemblyName);
            });
    })
    .AddInMemoryApiScopes(Config.GetApiScopes()).AddInMemoryClients(Config.GetClients())
    .AddDeveloperSigningCredential()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddProfileService<CustomProfileService>()
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
    //.AddTestUsers(Config.GetUsers().ToList());
using (var context=builder.Services.BuildServiceProvider().GetService<ConfigurationDbContext>())
{
    IdentityServerSeedData.Seed(context);
}

builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Logging.AddFilter("IdentityServer4", LogLevel.Debug);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
