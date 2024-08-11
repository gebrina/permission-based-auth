using Auth.Api.Services;
using Auth.Api.Utils;
using Auth.Application.Services;
using Auth.Domain.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
   private readonly ILoginService _loginService;
   private readonly IUserService _userService;
   private readonly IValidator<LoginRequestDto> _validator;
   private readonly JwtService _jwtService;

   public LoginController(ILoginService loginService,
   IValidator<LoginRequestDto> validator,
   IUserService userService,
   JwtService jwtService)
   {
      _validator = validator;
      _loginService = loginService;
      _userService = userService;
      _jwtService = jwtService;
   }

   [HttpPost]
   [AllowAnonymous]
   public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
   {
      var validationResult = await _validator.ValidateAsync(loginRequest);

      if (!validationResult.IsValid)
      {
         var errorResponse = FormatErrorMessage.Generate(validationResult.Errors);
         return BadRequest(errorResponse);
      }

      var result = await _loginService.AuthenticateAsync(loginRequest);

      if (result.Status == LoginStatus.REJECT) return BadRequest("You are rejected. use valid credentials.");
      if (result.Status == LoginStatus.FAIL) return BadRequest("Login failed, invalid email or password.");

      var user = await _userService.GetUserByEmailAsync(loginRequest.Email);
      var userDto = new UserDto
      {
         Id = user.Id,
         UserName = user.UserName ?? "",
         Email = user.Email ?? "",
         FirstName = user.FirstName,
         LastName = user.LastName
      };

      var userClaims = await _userService.GetUserRoleClaimsAsync(userDto);
      (string accessToken, string refreshToken) = _jwtService.GenerateTokens(userDto, userClaims);
      
      return new LoginResponseDto
      {
         Status = LoginStatus.SUCCESS,
         AccessToken = accessToken,
         RefreshToken = refreshToken
      };
   }
}