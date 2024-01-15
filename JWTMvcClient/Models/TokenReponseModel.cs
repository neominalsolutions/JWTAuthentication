namespace JWTMvcClient.Models
{
  public class TokenReponseModel
  {
    public string AccessToken { get; set; }
    public string? RefreshToken { get; set; }

  }
}
