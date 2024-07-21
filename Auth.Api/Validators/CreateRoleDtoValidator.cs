
using Auth.Domain.Dtos;
using FluentValidation;

namespace Auth.Api.Validators;

public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleDtoValidator()
    {
        RuleFor(role => role.Name).NotEmpty().WithMessage("Rolename is required");
    }
}