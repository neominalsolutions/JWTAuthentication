namespace JWTAuthentication.Services.Models
{
  public class TokenResponse
  {
    public string AccessToken { get; set; }
    public string? RefreshToken { get; set; } // Client Access Token Refleshler

  }
}
