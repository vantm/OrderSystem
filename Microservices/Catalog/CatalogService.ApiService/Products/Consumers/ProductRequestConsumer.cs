using CatalogService.ApiService.Products.Queries;

using MassTransit;

using MediatR;

using CatalogService.Contracts.Products;

using SharedLib.Contracts;

namespace CatalogService.ApiService.Products.Consumers;

public class ProductRequestConsumer : IConsumer<ProductRequest>
{
    private readonly ISender _sender;

    public ProductRequestConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<ProductRequest> context)
    {
        var query = new GetProduct.Query(context.Message.Id);
        var product = await _sender.Send(query, context.CancellationToken);

        if (product == null)
        {
            await context.RespondAsync(NotFound.Value);
        }
        else
        {
            await context.RespondAsync(product);
        }
    }
}