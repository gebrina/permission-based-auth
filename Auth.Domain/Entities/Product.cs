namespace Auth.Domain.Entities;

public class Product
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}