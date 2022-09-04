using Microsoft.EntityFrameworkCore;

namespace IdentityServer.AuthServer.Models;

public class CustomDbContext:DbContext
{
    public DbSet<CustomUser> CustomUsers { get; set; }
    
    public CustomDbContext(DbContextOptions<CustomDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomUser>().HasData(
            new CustomUser
        {
            Id=1,
            UserName="emre",
            Email = "emrebtas@gmail.com",
            Password = "emre123",
            City="Adana"
        },
            new CustomUser
            {
                Id=2,
                UserName = "emre",
                Email = "emrebyk348@gmail.com",
                Password = "emre123",
                City = "Adana"
            });
    }
}