using Carter;

using CatalogService.ApiService.Products.Commands;
using CatalogService.ApiService.Products.Queries;

using SharedLib.Extensions;

namespace CatalogService.ApiService.Products;

public sealed class ProductModule : CarterModule
{
    public ProductModule() : base("/products")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("",
                async (
                    [AsParameters] ListProducts.Query query,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var (result, validationError) =
                        await sender.Send(query, cancellationToken);

                    return result?.ToHttpResult()
                           ?? validationError?.ToHttpResult()
                           ?? Results.StatusCode(StatusCodes
                               .Status500InternalServerError);
                })
            .WithName("ListProducts");

        app.MapGet("{id:guid}",
                async (
                    [AsParameters] GetProduct.Query query,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var (result, notFound) =
                        await sender.Send(query, cancellationToken);

                    return result?.ToHttpResult()
                           ?? notFound?.ToHttpResult()
                           ?? Results.StatusCode(StatusCodes
                               .Status500InternalServerError);
                })
            .WithName("GetProduct");

        app.MapPost("",
                async (CreateProduct.Command command, ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var (result, validationError) =
                        await sender.Send(command, cancellationToken);

                    var routeValues = new { result.Value.Id };

                    return result?.ToHttpResult("GetProduct", routeValues)
                           ?? validationError?.ToHttpResult()
                           ?? Results.StatusCode(StatusCodes
                               .Status500InternalServerError);
                })
            .WithName("CreateProduct");

        app.MapPut("{id:guid}",
                async (
                    [AsParameters] UpdateProduct.Command command,
                    ISender sender, CancellationToken cancellationToken) =>
                {
                    var (result, notFound, validationError) =
                        await sender.Send(command, cancellationToken);

                    return result?.ToHttpResult()
                           ?? notFound?.ToHttpResult()
                           ?? validationError?.ToHttpResult()
                           ?? Results.StatusCode(StatusCodes
                               .Status500InternalServerError);
                })
            .WithName("UpdateProduct");

        app.MapDelete("{id:guid}",
                async (
                    [AsParameters] DeleteProduct.Command command,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var (result, notFound) =
                        await sender.Send(command, cancellationToken);

                    return result?.ToHttpResult()
                           ?? notFound?.ToHttpResult()
                           ?? Results.StatusCode(StatusCodes
                               .Status500InternalServerError);
                })
            .WithName("DeleteProduct");
    }
}