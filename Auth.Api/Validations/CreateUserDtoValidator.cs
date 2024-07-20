
using Auth.Domain.Dtos;
using FluentValidation;

namespace Auth.Api.Validations;


public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(user => user.Email).EmailAddress().NotEmpty()
        .NotNull();
        RuleFor(user => user.Password).NotNull().NotEmpty();
        RuleFor(user => user.UserName).NotNull().NotEmpty();
        RuleFor(user => user.Profession).Matches("^[a-zA-Z]+$")
        .WithMessage("Only letters are allowed for profession");
    }
}