namespace Auth.Api.Settings;

public class EmailSettings
{
    public string Server {get;set;} = string.Empty;
    public string EmailAddress { get; set; }=string.Empty;
    public string Port {get;set;} = string.Empty;
    public string Password {get;set;} = string.Empty;
}