using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProtectedController : ControllerBase
  {

    [HttpGet]
    [Authorize]
    public IActionResult Protected()
    {
      return Ok("Identity Protected");
    }
  }
}
