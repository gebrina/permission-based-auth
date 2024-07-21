
using Auth.Domain.Dtos;
using FluentValidation;

namespace Auth.Api.Validators;


public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(user => user.Email).EmailAddress().NotEmpty()
        .NotNull();
        RuleFor(user => user.Id).NotNull().NotEmpty();
        RuleFor(user => user.UserName).NotNull().NotEmpty();
        RuleFor(user => user.Profession).Matches("^[a-zA-Z]+$")
        .WithMessage("Only letters are allowed for profession");
    }
}