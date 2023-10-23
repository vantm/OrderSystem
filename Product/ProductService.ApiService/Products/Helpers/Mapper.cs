using ProductService.ApiService.Products.Models;

namespace ProductService.ApiService.Products;

public static class Mapper
{
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto()
        {
            Id = product.Id,
            Name = product.Name,
            Image = product.Image,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
        };
    }
}