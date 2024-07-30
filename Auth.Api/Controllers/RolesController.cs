using System.Reflection;
using Auth.Api.Utils;
using Auth.Application.Services;
using Auth.Domain.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IValidator<CreateRoleDto> _createRolevalidator;
    private readonly IValidator<RoleDto> _roleValidator;

    public RolesController(IRoleService roleService
    , IValidator<CreateRoleDto> createRoleValidator,
    IValidator<RoleDto> roleValidator)
    {
        _roleValidator = roleValidator;
        _createRolevalidator = createRoleValidator;
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> ViewRoles()
    {
        var result = await _roleService.GetRolesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<RoleDto>>> ViewRole([FromRoute] string id)
    {
        if (id == null) return BadRequest("Invalid role id");
        var role = await _roleService.GetRoleByIdAsync(id);
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

        var isEdited = await _roleService.UpdateRoleAsync(roleDto);
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

        if (await _roleService.CheckRoleExistanceAsync(roleDto.Name))
            return BadRequest("Role already registered.");
        roleDto.ActionNames = GetActionNames();
        var isRoleCreated = await _roleService.CreateRoleAsync(roleDto);
        if (isRoleCreated) return Created();
        return StatusCode(500, "Something went wrong.");
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole([FromRoute] string id)
    {
        var roleToBeDeleted = await _roleService.GetRoleByIdAsync(id);
        if (string.IsNullOrEmpty(roleToBeDeleted.Id)) return BadRequest("Invalid role's id.");

        await _roleService.DeleteRoleAsync(roleToBeDeleted);
        return NoContent();
    }

    [NonAction]
    private static List<string> GetActionNames()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var actionsControllerTypes = assembly.GetTypes()
        .Where(type => typeof(ControllerBase).IsAssignableFrom(type));

        var actionNames = actionsControllerTypes.SelectMany(t => t.GetMethods())
        .Where(method => (!method.IsDefined(typeof(NonActionAttribute))) &&
          (method.IsDefined(typeof(HttpPostAttribute)) ||
          method.IsDefined(typeof(HttpGetAttribute)) ||
          method.IsDefined(typeof(HttpPutAttribute)) ||
          method.IsDefined(typeof(HttpDeleteAttribute)))
        ).Select(method => method.Name).ToList();

        return actionNames;
    }
}
