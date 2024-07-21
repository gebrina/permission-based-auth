using Auth.Api.Utils;
using Auth.Application.Repository;
using Auth.Domain.Dtos;
using Auth.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepo;
    private readonly IValidator<CreateProductDto> _createProductValidator;
    private readonly IValidator<Product> _productValidator;

    public ProductsController(IProductRepository productRepo,
    IValidator<CreateProductDto> createProductValidator,
    IValidator<Product> productValidator)
    {
        _productRepo = productRepo;
        _productValidator = productValidator;
        _createProductValidator = createProductValidator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> ViewProducts()
    {
        return Ok(await _productRepo.GetProductsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> ViewProduct([FromRoute] string id)
    {
        var product = await _productRepo.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        var validationResult = await _createProductValidator.ValidateAsync(productDto);
        if (!validationResult.IsValid)
        {
            var formattedErrorResponse = FormatErrorMessage.Generate(validationResult.Errors);
            return BadRequest(formattedErrorResponse);
        }

        (string message, bool created) = await _productRepo.CreateProduct(productDto);

        if (!string.IsNullOrEmpty(message) && !created)
            return BadRequest(message);

        return Created();
    }

    [HttpPut]
    public async Task<ActionResult> EditProduct([FromBody] Product product)
    {
        var validationResult = await _productValidator.ValidateAsync(product);
        if (!validationResult.IsValid)
        {
            var formattedErrorResponse = FormatErrorMessage.Generate(validationResult.Errors);
            return BadRequest(formattedErrorResponse);
        }

        (string message, bool updated) = await _productRepo.UpdateProduct(product);

        if (!string.IsNullOrEmpty(message) && !updated)
            return BadRequest(message);

        return Ok();
    }
}