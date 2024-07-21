using FluentValidation;
using Auth.Domain.Entities;

namespace Auth.Api.Validators;

public class ProductDtoValidator : AbstractValidator<Product>
{

    public ProductDtoValidator()
    {
        RuleFor(product => product.Id).NotNull().NotEmpty()
        .WithMessage("Product id si required.");
        RuleFor(product => product.Name).NotEmpty().NotNull()
        .Matches("^[a-zA-z]+([0-9])*$").WithMessage("Product name must start with letters only");
        RuleFor(product => product.Price).GreaterThan(0).NotEmpty()
        .LessThan(1000);
        RuleFor(product => product.Image).NotEmpty().NotNull();
        RuleFor(product => product.Category).NotNull().NotEmpty()
        .WithMessage("Product category is required.");
    }
}