using Auth.Domain.Dtos;
using Auth.Domain.Entities;

namespace Auth.Application.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(string id);
    Task<bool> CreateProduct(CreateProductDto productDto);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
}