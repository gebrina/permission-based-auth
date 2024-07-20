using Auth.Application.Repository;
using Auth.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepo;

    public UsersController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerator<UserDto>>> ViewUsers()
    {
        var users = await _userRepo.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> ViewUser(string id)
    {
        var user = await _userRepo.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
}