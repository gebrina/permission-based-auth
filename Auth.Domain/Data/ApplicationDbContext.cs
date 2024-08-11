using Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Domain.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<LoggedInUsers>()
        .HasNoKey()
        .HasIndex(login => login.UserId)
        .IsUnique();

        builder.Entity<ApplicationUser>()
        .HasOne<LoggedInUsers>();
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<LoggedInUsers> LoggedInUsers { get; set; }
}