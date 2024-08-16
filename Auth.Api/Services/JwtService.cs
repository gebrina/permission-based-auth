using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Api.Settings;
using Auth.Application.Services;
using Auth.Domain.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api.Services;

public class JwtService:IJwtService
{
    private readonly JwtConfigSettings _jwtConfig;

    public JwtService(IOptions<JwtConfigSettings> options)
    {
        _jwtConfig = options.Value;
    }

    public (string accessToken, string refreshToken) GenerateTokens(UserDto user, IList<string> userClaims)
    {
        var byteSecretKeys = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);
        var securityKey = new SymmetricSecurityKey(byteSecretKeys);
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        IList<Claim> claims = [];
        foreach (var claim in userClaims)
        {
            claims.Add(
                new Claim("Permission", claim)
            );
        }

        var jwtSecurityToken = new JwtSecurityToken(
            audience: _jwtConfig.Audience,
            issuer: _jwtConfig.Issuer,
            expires: DateTime.Now.AddDays(2),
            claims: claims,
            signingCredentials: signingCredentials
        );
        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        string refreshToken = GenerateRerfreshToken(100);

        return (accessToken, refreshToken);
    }

    private static string GenerateRerfreshToken(int refreshTokenLength)
    {
        Random random = new();
        string refreshToken = "";
        while (refreshTokenLength > 0)
        {
            // Generate Upper Case letter's ascii code
            int charCode = random.Next(0, 26) + 65;
            Char character = Convert.ToChar(charCode);
            refreshToken += character;
            --refreshTokenLength;
        }
        return refreshToken;
    }
}