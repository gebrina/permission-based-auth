using Auth.Application.Repository;
using Auth.Application.Services;
using Auth.Domain.Dtos;
using Auth.Domain.Entities;

namespace Auth.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private IProductService _service;

    public ProductRepository(IProductService service)
    {
        _service = service;
    }

    public async Task<(string message, bool created)> CreateProduct(CreateProductDto productDto)
    {
        return await _service.CreateProduct(productDto);
    }

    public async Task<bool> DeleteProduct(string id)
    {
        return await _service.DeleteProduct(id);
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _service.GetProductByIdAsync(id);
    }

    public Task<IEnumerable<Product>> GetProductsAsync()
    {
        return _service.GetProductsAsync();
    }

    public async Task<(string message, bool updated)> UpdateProduct(Product product)
    {
        return await _service.UpdateProduct(product);
    }
}