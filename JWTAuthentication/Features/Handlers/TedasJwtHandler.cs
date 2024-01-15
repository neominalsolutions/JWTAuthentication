using JWTAuthentication.Features.Requests;
using JWTAuthentication.Services.Models;
using MediatR;

namespace JWTAuthentication.Features.Handlers
{
  public class TedasJwtHandler : IRequestHandler<TedasTokenRequest, TokenResponse>
  {
    public Task<TokenResponse> Handle(TedasTokenRequest request, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
