using JWTAuthentication.Features.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
  [ApiController]
  public class TokensController : ControllerBase
  {
    private readonly IMediator mediator;

    public TokensController(IMediator mediator)
    {
      this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> GetToken([FromBody] AkedasTokenRequestDto request)
    {
       var response = await  this.mediator.Send(request);

      return Ok(response);
    }

    [HttpPost("tedas")]
    public async Task<IActionResult> GetToken([FromBody] TedasTokenRequest request)
    { 
      await this.mediator.Send(request);

      return Ok();
    }

  }
}
