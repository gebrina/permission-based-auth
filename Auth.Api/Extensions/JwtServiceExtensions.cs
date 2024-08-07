using System.Text;
using System.Text.Unicode;
using Auth.Api.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api.Extensions;

public static class JwtServiceExtensions
{
    public static IServiceCollection AddJwtAuthConfig(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        JwtConfigSettings jwtConfig = serviceProvider.GetRequiredService<JwtConfigSettings>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var secretKey = Encoding.UTF8.GetBytes(jwtConfig.SecretKey);
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = jwtConfig.Audience,
                ValidIssuer = jwtConfig.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey)
            };
        });
        return services;
    }
}