using Auth.Domain.Dtos;

namespace Auth.Application.Services;

public interface ILoginService
{
    Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginRequest);
}