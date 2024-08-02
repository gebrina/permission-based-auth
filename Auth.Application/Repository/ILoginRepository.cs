using Auth.Domain.Dtos;

namespace Auth.Application.Repository;

public interface ILoginRepository
{
    Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginRequest);
}