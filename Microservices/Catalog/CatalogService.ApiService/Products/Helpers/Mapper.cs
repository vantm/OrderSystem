using CatalogService.ApiService.Products.Domain;
using CatalogService.ApiService.Products.Models;

namespace CatalogService.ApiService.Products.Helpers;

public static class Mapper
{
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto()
        {
            Id = product.Id,
            Name = product.Name,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
        };
    }
}