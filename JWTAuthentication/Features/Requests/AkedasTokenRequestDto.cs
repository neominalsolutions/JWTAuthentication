using JWTAuthentication.Services.Models;
using MediatR;

namespace JWTAuthentication.Features.Requests
{
    public class AkedasTokenRequestDto:IRequest<TokenResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
