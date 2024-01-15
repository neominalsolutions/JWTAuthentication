using JWTAuthentication.Features.Requests;
using JWTAuthentication.Services;
using JWTAuthentication.Services.Models;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace JWTAuthentication.Features.Handlers
{
  public class AkedasJwtHandler : IRequestHandler<AkedasTokenRequestDto, TokenResponse>
  {
    private IJwtService jwtService;

    public AkedasJwtHandler(IJwtService jwtService)
    {
      this.jwtService = jwtService;
    }

    public async Task<TokenResponse> Handle(AkedasTokenRequestDto request, CancellationToken cancellationToken)
    {

      if(request.UserName == "akedas" && request.Password == "123456")
      {
        // valid

        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim("Name", "akedas"));
        identity.AddClaim(new Claim("Role", "admin"));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()));

       var tokenResponse =  this.jwtService.CreateAccessToken(identity);

        return await Task.FromResult(tokenResponse);
      }
      else
      {
        throw new Exception("Kullanıcı veya parola bilgisi hatalı");
      }
     

     
    }
  }
}
