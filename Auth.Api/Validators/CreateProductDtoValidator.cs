using FluentValidation;
using Auth.Domain.Dtos;

namespace Auth.Api.Validators;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{

    public CreateProductDtoValidator()
    {
        RuleFor(product => product.Name).NotEmpty().NotNull()
        .Matches("^[a-zA-z]+([0-9])*$").WithMessage("Product name must start with letters only");
        RuleFor(product => product.Price).GreaterThan(0).NotEmpty()
        .LessThan(1000);
        RuleFor(product => product.Category).NotNull().NotEmpty()
        .WithMessage("Product category is required.");
    }
}