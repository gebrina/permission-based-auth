using Auth.Application.Repository;
using Auth.Domain.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepo;
    private readonly IValidator<CreateUserDto> _createUserDtoValidator;
    private readonly IValidator<UserDto> _userDtoValidator;
    public UsersController(IUserRepository userRepo,
    IValidator<CreateUserDto> createUserDtoValidator,
    IValidator<UserDto> userDtoValidator)
    {
        _userRepo = userRepo;
        _createUserDtoValidator = createUserDtoValidator;
        _userDtoValidator = userDtoValidator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerator<UserDto>>> ViewUsers()
    {
        var users = await _userRepo.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> ViewUser([FromRoute] string id)
    {
        var user = await _userRepo.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var validtionResult = await _createUserDtoValidator.ValidateAsync(userDto);

        if (!validtionResult.IsValid)
            return BadRequest(validtionResult.Errors);

        var isCreated = await _userRepo.CreateUserAsync(userDto);
        if (!isCreated) return StatusCode(500);

        return Created();
    }


    [HttpPut]
    public async Task<ActionResult> EditUser([FromBody] UserDto userDto)
    {
        var validtionResult = await _userDtoValidator.ValidateAsync(userDto);
        if (!validtionResult.IsValid) return BadRequest(validtionResult.Errors);

        var isUpdated = await _userRepo.UpdateUserAsync(userDto);
        if (!isUpdated) return StatusCode(500);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

        var user = await _userRepo.GetUserByIdAsync(id);
        if (user == null) return NotFound();

        var isDeleted = await _userRepo.DeleteUserAsync(user);
        if (!isDeleted) return StatusCode(500);

        return NoContent();
    }
}