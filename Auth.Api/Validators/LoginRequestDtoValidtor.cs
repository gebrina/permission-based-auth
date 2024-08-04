using Auth.Domain.Dtos;
using FluentValidation;

namespace Auth.Api.Validators;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(login => login.Email).NotEmpty().NotNull().EmailAddress()
        .WithMessage("Invalid email address");
        RuleFor(login => login.Password).NotNull().NotEmpty();
    }
}