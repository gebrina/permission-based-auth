
using Auth.Domain.Dtos;
using FluentValidation;

namespace Auth.Api.Validators;

public class RoleDtoValidator : AbstractValidator<RoleDto>
{
    public RoleDtoValidator()
    {
        RuleFor(role => role.Id).NotEmpty().WithMessage("Role id is required.");
        RuleFor(role => role.Name).NotEmpty().WithMessage("Rolename is required");
    }
}