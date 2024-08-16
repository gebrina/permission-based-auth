using Auth.Domain.Dtos;

namespace Auth.Application.Services;

public interface IJwtService
{
   (string accessToken,string refreshToken) GenerateTokens(UserDto user,IList<string> userPermissions);
}