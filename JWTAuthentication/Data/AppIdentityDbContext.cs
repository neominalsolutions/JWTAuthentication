using JWTAuthentication.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthentication.Data
{
  public class AppIdentityDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,string>
  {

    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options):base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<ApplicationUser>().ToTable("Users");
      builder.Entity<ApplicationRole>().ToTable("Roles");

      base.OnModelCreating(builder);
    }
  }
}
