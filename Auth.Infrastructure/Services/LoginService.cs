using Auth.Application.Services;
using Auth.Domain.Dtos;

namespace Auth.Infrastructure.Services;

public class LoginService : ILoginService
{
    public Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginRequest)
    {
        throw new NotImplementedException();
    }
}