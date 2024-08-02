using Auth.Application.Repository;
using Auth.Application.Services;
using Auth.Domain.Dtos;
using Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure.Repository;

public class LoginRepository : ILoginRepository
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserService _userService;

    public LoginRepository(SignInManager<ApplicationUser> signInManager,
    IUserService userService)
    {
        _userService = userService;
        _signInManager = signInManager;
    }

    public async Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginRequest)
    {
        return new LoginResponseDto
        {
            Status = LoginStatus.REJECT,
            AccessToken = null,
            RefreshToken = null
        };
    }
}