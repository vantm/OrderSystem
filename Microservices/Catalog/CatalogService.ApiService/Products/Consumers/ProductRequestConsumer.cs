using CatalogService.ApiService.Products.Domain;
using CatalogService.Contracts.Products;

using MassTransit;

using SharedLib.Contracts;

namespace CatalogService.ApiService.Products.Consumers;

public class ProductRequestConsumer
    (IProductRepo repo) : IConsumer<ProductRequest>
{
    public async Task Consume(ConsumeContext<ProductRequest> context)
    {
        var product = await repo.QueryFindAsync(
            context.Message.Id,
            context.CancellationToken);

        if (product == null)
        {
            await context.RespondAsync(NotFound.Value);
        }
        else
        {
            var model = new ProductModel(
                product.Id,
                product.Name,
                product.CreatedAt,
                product.UpdatedAt);

            await context.RespondAsync(model);
        }
    }
}