using Microsoft.AspNetCore.Identity;

namespace JWTAuthentication.Data.Identity
{
  public class ApplicationUser:IdentityUser
  {
    public string WebSiteUrl { get; set; }

  }
}
