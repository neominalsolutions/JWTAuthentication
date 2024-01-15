using JWTMvcClient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JWTMvcClient.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    [Authorize]
    //[Route("/home")]
    public async Task<IActionResult> Index()
    {
      //var userId = User.Claims.FirstOrDefault(x => x.Type == "sub");

      //if(userId is not null)
      //{

      //}


      //var result =  await HttpContext.AuthenticateAsync("MyCookie");

      //var token = await HttpContext.GetTokenAsync("MyCookie", "AccessToken");

      return View();
    }

    // [Authorize(Roles = "admin")] // sadece bu method admin rolüne sahibi kişi görebilir.
    // [Authorize(Policy = "OnlyAdminManager")]  // sadece Manager ve Admin olmalıdır
    [Authorize(Roles = "admin,Manager")] // bu veyalı hali
    
    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}