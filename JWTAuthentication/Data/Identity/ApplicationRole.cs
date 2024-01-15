using Microsoft.AspNetCore.Identity;

namespace JWTAuthentication.Data.Identity
{
  public class ApplicationRole:IdentityRole
  {
    public string Description { get; set; }

  }
}
