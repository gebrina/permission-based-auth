using Auth.Application.Services;
using Auth.Domain.Dtos;
using Auth.Domain.Entities;

namespace Auth.Infrastructure.Services;

public class ProductService : IProductService
{
    public Task<bool> CreateProduct(CreateProductDto productDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}