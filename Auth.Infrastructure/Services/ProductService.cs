using Auth.Application.Repository;
using Auth.Application.Services;
using Auth.Domain.Dtos;
using Auth.Domain.Entities;

namespace Auth.Infrastructure.Services;

public class ProductService : IProductService
{
    private IProductRepository _productRepo;

    public ProductService(IProductRepository productRepository)
    {
        _productRepo = productRepository;
    }

    public async Task<(string message, bool created)> CreateProduct(CreateProductDto productDto)
    {
        return await _productRepo.CreateProduct(productDto);
    }

    public async Task<bool> DeleteProduct(string id)
    {
        return await _productRepo.DeleteProduct(id);
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _productRepo.GetProductByIdAsync(id);
    }

    public Task<IEnumerable<Product>> GetProductsAsync()
    {
        return _productRepo.GetProductsAsync();
    }

    public async Task<(string message, bool updated)> UpdateProduct(Product product)
    {
        return await _productRepo.UpdateProduct(product);
    }
}