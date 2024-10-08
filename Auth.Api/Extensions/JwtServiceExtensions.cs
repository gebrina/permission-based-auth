using System.Text;
using Auth.Api.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api.Extensions;

public static class JwtServiceExtensions
{
    public static IServiceCollection AddJwtAuthConfig(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        JwtConfigSettings jwtConfig = serviceProvider.GetRequiredService<IOptions<JwtConfigSettings>>().Value;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var secretKey = Encoding.UTF8.GetBytes(jwtConfig.SecretKey);
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = jwtConfig.Audience,
                ValidIssuer = jwtConfig.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey)
            };
        }).AddBearerToken();
        return services;
    }

    private static Func<JwtBearerChallengeContext, Task> async()
    {
        throw new NotImplementedException();
    }
}