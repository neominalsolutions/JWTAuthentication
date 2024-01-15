using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTMvcClient.Controllers
{
  public class TestController : Controller
  {
  
    public IActionResult Index()
    {
     


      return View();
    }
  }
}
