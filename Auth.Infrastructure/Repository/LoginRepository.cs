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
        var response = new LoginResponseDto
        {
            Status = LoginStatus.REJECT,
            AccessToken = string.Empty,
            RefreshToken = string.Empty
        };

        var user = await _userService.GetUserByEmailAsync(loginRequest.Email);
        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, false, true);
            if (!result.Succeeded)
                response.Status = LoginStatus.FAIL;
            else
                response.Status = LoginStatus.SUCCESS;
        }
        
        return response;
    }
}