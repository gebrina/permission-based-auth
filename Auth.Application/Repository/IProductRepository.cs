using Auth.Domain.Dtos;
using Auth.Domain.Entities;

namespace Auth.Application.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(string id);
    Task<(string message, bool created)> CreateProduct(CreateProductDto productDto);
    Task<(string message, bool updated)> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
}