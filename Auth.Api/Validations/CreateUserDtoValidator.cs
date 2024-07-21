
using Auth.Domain.Dtos;
using FluentValidation;

namespace Auth.Api.Validations;


public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(user => user.Email).EmailAddress().NotEmpty()
        .NotNull();
        RuleFor(user => user.Password)
      .NotNull().NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$")
      .WithMessage("Password must contain atleast one number, unique character, uppercase letter, lowercase letter and must be 6 or more character length.");
        RuleFor(user => user.UserName).NotNull().NotEmpty();
        RuleFor(user => user.Profession).Matches("^[a-zA-Z]+$")
        .WithMessage("Only letters are allowed for profession");
    }
}