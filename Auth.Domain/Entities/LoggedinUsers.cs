namespace Auth.Domain.Entities;

public class LoggedInUsers
{   
    public string UserId {get;set;} = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}