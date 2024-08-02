namespace Auth.Domain.Dtos;

public enum LoginStatus
{
  SUCCESS,
  REJECT,
  FAIL
}

public class LoginResponseDto
{
  public LoginStatus Status { get; set; }
  public string AccessToken { get; set; } = string.Empty;
  public string RefreshToken { get; set; } = string.Empty;
}