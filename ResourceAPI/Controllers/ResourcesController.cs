using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResourceAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ResourcesController : ControllerBase
  {
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public IActionResult Get()
    {
      return Ok("Protected Resource");
    }
  }
}
