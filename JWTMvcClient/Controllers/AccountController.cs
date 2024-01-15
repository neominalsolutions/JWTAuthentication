using JWTMvcClient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTMvcClient.Controllers
{
  public class AccountController : Controller
  {
    private readonly HttpClient httpClient;

    public AccountController(IHttpClientFactory httpClientFactory)
    {
      this.httpClient = httpClientFactory.CreateClient("IdentityService");
    }


    [HttpGet]
    public IActionResult Login()
    {
      return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken] // CSRF ataklarına karşı bu attribute koruma sağlar.
    public async Task<IActionResult> Login(LoginInputModel payload)
    {

      try
      {
        //var payload = new LoginInputModel
        //{
        //  UserName = "akedas",
        //  Password = "123456"
        //};

        // Serialize our concrete class into a JSON String
        var stringPayload = System.Text.Json.JsonSerializer.Serialize(payload);

        var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");



        var response = await httpClient.PostAsync("Tokens", httpContent);


        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
          var token = await response.Content.ReadFromJsonAsync<TokenReponseModel>();

          // encoded token jwt kütüphanesi üzerinden decode ediyoruz.
          var handler = new JwtSecurityTokenHandler();
          var jwtSecurityToken = handler.ReadJwtToken(token.AccessToken);

          // claim yönetimi ClaimsPrincipal nesnesi üzerinde yapılır
          var principle = new ClaimsPrincipal();
          var claimsIdentity = new ClaimsIdentity(jwtSecurityToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "Name","Role");
          principle.AddIdentity(claimsIdentity);

          var authProps = new AuthenticationProperties();
          authProps.ExpiresUtc = DateTimeOffset.Now.AddHours(1);

          // token biyerde saklamak için kullanılan yöntem
          List<AuthenticationToken> tokens = new List<AuthenticationToken>();
          var authenticationToken = new AuthenticationToken();
          authenticationToken.Name = "AccessToken";
          authenticationToken.Value = token.AccessToken;
          tokens.Add(authenticationToken);
          authProps.StoreTokens(tokens);
          authProps.IsPersistent = true;

          // token sessionda olabilir

          // Mvc uygulaması API üzerinden authenticated oldu.
          
          
          
          await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, authProps);


          return Redirect("/");
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError("Login", ex.Message);
      }


      return View();
    }
  
    public async Task<IActionResult> LogOut()
    {
      await HttpContext.SignOutAsync("Bearer");
      return RedirectToAction("Login", "Account");
    }
  
  }
}
