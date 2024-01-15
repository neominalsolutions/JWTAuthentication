using JWTAuthentication.Services.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.Services
{
  public class MicrosoftJwtBearerService : IJwtService
  {
    public TokenResponse CreateAccessToken(ClaimsIdentity identity)
    {
      var key = Encoding.ASCII.GetBytes(JwtSettings.SecretKey);
      var tokenHandler = new JwtSecurityTokenHandler();
      // Expire Date, Subject, Claims bilgileri SecurityTokenDescriptor descriptior sınıfı içerisinde tanımlanmıştır.
      var descriptor = new SecurityTokenDescriptor
      {
        Subject = identity,
        Expires = DateTime.Now.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
      };

      var token = tokenHandler.CreateToken(descriptor);
      var accessToken = tokenHandler.WriteToken(token);

      return new TokenResponse {
        AccessToken = accessToken
      };

    }
  }
}
