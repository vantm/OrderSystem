using CatalogService.ApiService.Products.Models;

using SharedLib.Domain;

namespace CatalogService.ApiService.Products.Domain;

public interface IProductRepo : IRepo<Product, Guid>
{
    Task<IEnumerable<ProductDto>> QueryAsync(
        int offset, int limit, CancellationToken ct = default);

    Task<ProductDto?> QueryFindAsync(Guid id, CancellationToken ct = default);
}