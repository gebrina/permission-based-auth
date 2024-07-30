using Auth.Api.Utils;
using Auth.Application.Services;
using Auth.Domain.Dtos;
using Auth.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IValidator<CreateProductDto> _createProductValidator;
    private readonly IValidator<Product> _productValidator;

    public ProductsController(IProductService productService,
    IValidator<CreateProductDto> createProductValidator,
    IValidator<Product> productValidator)
    {
        _productService = productService;
        _productValidator = productValidator;
        _createProductValidator = createProductValidator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> ViewProducts()
    {
        return Ok(await _productService.GetProductsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> ViewProduct([FromRoute] string id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct(
    [FromForm] CreateProductDto productDto, IFormFile productImage)
    {
        var validationResult = await _createProductValidator.ValidateAsync(productDto);
        if (!validationResult.IsValid)
        {
            var formattedErrorResponse = FormatErrorMessage.Generate(validationResult.Errors);
            return BadRequest(formattedErrorResponse);
        }
        else if (productImage?.Length <= 0 || productImage?.FileName == null)
        {
            return BadRequest("Prodcut image is required field.");
        }

        var fileName = string.Concat(Guid.NewGuid(), productImage?.FileName.Replace(" ", "-"));
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
        using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            await productImage.CopyToAsync(stream);
        }

        var fileUrl = string.Concat(
             HttpContext.Request.GetDisplayUrl()
            .Replace(HttpContext.Request.Path, ""),
            "/uploads/", fileName);
        productDto.Image = fileUrl;
        (string message, bool created) = await _productService.CreateProduct(productDto);

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

        (string message, bool updated) = await _productService.UpdateProduct(product);

        if (!string.IsNullOrEmpty(message) && !updated)
            return BadRequest(message);

        return Ok();
    }
}