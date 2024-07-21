
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

    public RolesController(IRoleRepository roleRepo
    , IValidator<CreateRoleDto> createRoleValidator)
    {
        _createRolevalidator = createRoleValidator;
        _roleRepo = roleRepo;
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

        var isCreated = await _roleRepo.CreateRoleAsync(roleDto);

        if (isCreated) return Created();

        return StatusCode(500);
    }
}
