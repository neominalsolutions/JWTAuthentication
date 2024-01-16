using JWTAuthentication.Data.Identity;
using JWTAuthentication.Data.Migrations;
using JWTAuthentication.Features.Requests;
using JWTAuthentication.Services;
using JWTAuthentication.Services.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWTAuthentication.Features.Handlers
{
  public class AkedasJwtHandler : IRequestHandler<AkedasTokenRequestDto, TokenResponse>
  {
    private IJwtService jwtService;
    private UserManager<ApplicationUser> userManager;
    private RoleManager<ApplicationRole> roleManager;

    public AkedasJwtHandler(IJwtService jwtService, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
      this.jwtService = jwtService;
      this.userManager = userManager;
      this.roleManager = roleManager;
    }

    public async Task<TokenResponse> Handle(AkedasTokenRequestDto request, CancellationToken cancellationToken)
    {

      var user = await userManager.FindByNameAsync(request.UserName);

      if(user is not null)
      {
        var passwordConfirmed = await userManager.CheckPasswordAsync(user, request.Password);

        if(passwordConfirmed)
        {
          
     
          ////identity.AddClaim(new Claim(ClaimTypes.Name, "akedas"));

          //identity.AddClaim(new Claim("Name", "akedas"));
          //identity.AddClaim(new Claim("Role", "admin"));
          //identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()));
          var roles = await userManager.GetRolesAsync(user);
          var stringRoles = string.Join(",", roles);

          var userClaims = await userManager.GetClaimsAsync(user);
          var adminRole = await roleManager.FindByNameAsync("admin");
          var roleClaims = await roleManager.GetClaimsAsync(adminRole);

          var claims = new List<Claim>();


          claims.Add(new Claim("Name", user.UserName));
          claims.Add(new Claim("Role", stringRoles));

          foreach (var claim in userClaims)
          {
            claims.Add(new Claim(claim.Type, claim.Value));
          }

          foreach (var roleClaim in roleClaims)
          {
            claims.Add(new Claim(roleClaim.Type, roleClaim.Value));
          }

          var identity = new ClaimsIdentity(claims,"Bearer","Name","Role");


          var tokenResponse = this.jwtService.CreateAccessToken(identity);

          return await Task.FromResult(tokenResponse);
        }
        else
        {
          throw new Exception("Kullanıcı veya parola bilgisi hatalı");
        }

       
      }
      else
      {
        throw new Exception("Kullanıcı veya parola bilgisi hatalı");
      }
     

     
    }
  }
}
