using Auth.Application.Services;
using Auth.Domain.Data;
using Auth.Domain.Dtos;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _dbContext;

    public ProductService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(string message, bool created)> CreateProduct(CreateProductDto productDto)
    {
        var product = new Product
        {
            Id=Guid.NewGuid().ToString(),
            Name = productDto.Name,
            Price = productDto.Price,
            Image = productDto.Image,
            Category = productDto.Category
        };
        var state = await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        if (state.Entity.Id == null) return (
            message: "Something went wrong",
            created: false
        );

        return (
            message: string.Empty,
            created: true
        ); ;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(_ => _.Id == id);
        if (product == null) return false;

        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();
        return true;
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        var product = await _dbContext.Products.FirstAsync(_ => _.Id == id);
        return product;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var products = await _dbContext.Products.ToListAsync();
        return products;
    }

    public async Task<(string message, bool updated)> UpdateProduct(Product product)
    {
        var productInDb = await _dbContext.Products.FindAsync(product);
        if (productInDb == null) return (
            message: "Invalid product",
            updated: false
        );

        productInDb.Name = product.Name;
        productInDb.Category = product.Category;
        productInDb.Price = product.Price;
        productInDb.Image = product.Image;

        return (
            message: string.Empty,
            updated: true
        );
    }
}