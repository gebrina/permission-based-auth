using Auth.Api.Utils;
using Auth.Application.Services;
using Auth.Domain.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<CreateUserDto> _createUserDtoValidator;
    private readonly IValidator<UserDto> _userDtoValidator;
    private readonly IEmailService _emailService;

    public UsersController(IUserService userService,
    IValidator<CreateUserDto> createUserDtoValidator,
    IValidator<UserDto> userDtoValidator,
    IEmailService emailService)
    {
        _userService = userService;
        _createUserDtoValidator = createUserDtoValidator;
        _userDtoValidator = userDtoValidator;
        _emailService = emailService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerator<UserDto>>> ViewUsers([FromQuery] PagingFilterRequest request)
    {
        var users = await _userService.GetUsersAsync(request);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> ViewUser([FromRoute] string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var validtionResult = await _createUserDtoValidator.ValidateAsync(userDto);

        if (!validtionResult.IsValid)
        {
            var formattedErros = FormatErrorMessage.Generate(validtionResult.Errors);
            return BadRequest(formattedErros);
        }

        var isCreated = await _userService.CreateUserAsync(userDto);
        if (!isCreated) return StatusCode(500);

        string confirmationToken = await _userService.GetEmailConfirmationToken(userDto.Email);
        string to = userDto.Email;
        string subject = "Email Confimation";
        string confirmationLink = $"http://localhost:3000/confirm-email?email={userDto.Email}&token={confirmationToken}";
        string body = $"<div><h1>Email Confrimation</h1><p>Confirm your email by <a href='{confirmationLink}'>Clicking here</a>.</p></div>";

        await _emailService.SendEmailAsync(to, subject, body, true);
        return Created();
    }


    [HttpPut]
    public async Task<ActionResult> EditUser([FromBody] UserDto userDto)
    {
        var validtionResult = await _userDtoValidator.ValidateAsync(userDto);
        if (!validtionResult.IsValid)
        {
            var formattedErros = FormatErrorMessage.Generate(validtionResult.Errors);
            return BadRequest(formattedErros);
        }

        var isUpdated = await _userService.UpdateUserAsync(userDto);
        if (!isUpdated) return StatusCode(500);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();

        var isDeleted = await _userService.DeleteUserAsync(user);
        if (!isDeleted) return StatusCode(500);

        return NoContent();
    }
}