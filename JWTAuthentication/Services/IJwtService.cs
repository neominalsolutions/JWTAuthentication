using JWTAuthentication.Services.Models;
using System.Security.Claims;

namespace JWTAuthentication.Services
{
  public interface IJwtService
  {
    TokenResponse CreateAccessToken(ClaimsIdentity identity);
  }
}
