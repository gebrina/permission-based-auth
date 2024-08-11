using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Api.Settings;
using Auth.Domain.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api.Services;

public  class JwtService
{
    private readonly JwtConfigSettings _jwtConfig;

    public JwtService(IOptions<JwtConfigSettings> options)
    {
        _jwtConfig = options.Value;
    }

    public  (string acceessToken,string RefreshToken) GenerateTokens(UserDto user,IList<string> userClaims)
    {
        var byteSecretKeys = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);
        var securityKey = new SymmetricSecurityKey(byteSecretKeys);
        var signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
        IList<Claim> claims = [];

        foreach(var permission in user.Roles){}

        var jwtSecurityToken = new JwtSecurityToken(
            audience:_jwtConfig.Audience,
            issuer:_jwtConfig.Issuer,
            expires:DateTime.Today.AddDays(2)
            
        );

        return ("","");
    }
}