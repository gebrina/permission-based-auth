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
   private readonly IValidator<LoginRequestDto> _validator;

   public LoginController(ILoginService loginService,
   IValidator<LoginRequestDto> validator)
   {
      _validator = validator;
      _loginService = loginService;
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
      return new LoginResponseDto { };
   }
}