using JWTAuthentication.Services.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Features.Requests
{
    public class TedasTokenRequest:IRequest<TokenResponse>
    {
        [EmailAddress(ErrorMessage = "Email formatında giriniz")]
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
