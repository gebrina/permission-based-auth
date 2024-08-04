namespace Auth.Api.Settings;

public class JwtConfigSettings
{
   public string Audience {get;set;} = string.Empty;
   public string Issuer {get;set;} = string.Empty;
   public string SecretKey {get;set;} = string.Empty;   
}