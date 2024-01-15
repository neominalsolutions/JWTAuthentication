using JWTAuthentication.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;
    // MVC uygulamaları için ise login işlemlerini yönetecek SignInManager
    private readonly SignInManager<ApplicationUser> signInManager;

    public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
    {
      this.userManager = userManager;
      this.roleManager = roleManager;
      this.signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> CreateUser()
    {
      ApplicationUser user = new ApplicationUser();
      user.UserName = "akedas";
      user.Email = "test@akedas.com";
      user.WebSiteUrl = "https://www.akedas.com";
 
      var result = await userManager.CreateAsync(user,"P@ssword34");

      var role = new ApplicationRole();
      role.Name = "admin";
      role.Description = "Yönetici";

      await roleManager.CreateAsync(role);

      await userManager.AddToRoleAsync(user, "admin");

      await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("ApplicationName.UsersController", "CreateUser"));

      var existingRole = await roleManager.FindByNameAsync("admin");
      await roleManager.AddClaimAsync(existingRole, new System.Security.Claims.Claim("ApplicationName.TokensController", "GetToken"));

      if (result.Succeeded)
      {
        return Ok("Kullanıcı oluştu");
      }


     

      return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
      var user = await userManager.FindByNameAsync("akedas");
      await this.signInManager.SignInAsync(user,true); // Cookie based Authentication
      // default 20 dk.
      // formdaki remember me ayarı isPersistent
      await this.signInManager.SignOutAsync();

      return Ok();
    }
  }
}
