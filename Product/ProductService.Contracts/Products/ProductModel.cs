namespace ProductService.Contracts.Products;

public record Product(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    DateTime UpdatedAt);