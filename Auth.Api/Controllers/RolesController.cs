using Auth.Api.Utils;
using Auth.Application.Repository;
using Auth.Domain.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _roleRepo;
    private readonly IValidator<CreateRoleDto> _createRolevalidator;
    private readonly IValidator<RoleDto> _roleValidator;

    public RolesController(IRoleRepository roleRepo
    , IValidator<CreateRoleDto> createRoleValidator,
    IValidator<RoleDto> roleValidator)
    {
        _roleValidator = roleValidator;
        _createRolevalidator = createRoleValidator;
        _roleRepo = roleRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> ViewRoles()
    {
        var result = await _roleRepo.GetRolesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<RoleDto>>> ViewRole([FromRoute] string id)
    {
        if (id == null) return BadRequest("Invalid role id");
        var role = await _roleRepo.GetRoleByIdAsync(id);
        if (role == null) return NotFound();

        return Ok(role);
    }

    [HttpPut]
    public async Task<ActionResult> EditRole([FromBody] RoleDto roleDto)
    {
        var validationResult = await _roleValidator.ValidateAsync(roleDto);
        if (!validationResult.IsValid)
        {
            var formattedErrorResponse = FormatErrorMessage.Generate(validationResult.Errors);
            return BadRequest(formattedErrorResponse);
        }

        var isEdited = await _roleRepo.UpdateRoleAsync(roleDto);
        if (string.IsNullOrEmpty(isEdited.Item1) && isEdited.Item2)
        {
            return Ok();
        }
        else if (!string.IsNullOrEmpty(isEdited.Item1) && !isEdited.Item2)
        {
            return BadRequest(isEdited.Item1);
        }
        else
        {
            return StatusCode(500, isEdited.Item1);
        }

    }

    [HttpPost]
    public async Task<ActionResult> CreateRole(CreateRoleDto roleDto)
    {
        var validationResult = await _createRolevalidator.ValidateAsync(roleDto);
        if (!validationResult.IsValid)
        {
            var formattedErrorResponse = FormatErrorMessage.Generate(validationResult.Errors);
            return BadRequest(formattedErrorResponse);
        }
        
        if (await _roleRepo.CheckRoleExistanceAsync(roleDto.Name))
            return BadRequest("Role already registered.");

        var isCreated = await _roleRepo.CreateRoleAsync(roleDto);

        if (isCreated) return Created();

        return StatusCode(500);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole([FromRoute] string id)
    {
        var roleToBeDeleted = await _roleRepo.GetRoleByIdAsync(id);
        if (roleToBeDeleted == null) return BadRequest();

        await _roleRepo.DeleteRoleAsync(roleToBeDeleted);
        return NoContent();
    }
}
