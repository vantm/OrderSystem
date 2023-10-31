using Carter;
using Carter.ModelBinding;

using CatalogService.ApiService.Products.Domain;
using CatalogService.ApiService.Products.Helpers;
using CatalogService.ApiService.Products.Models;

namespace CatalogService.ApiService.Products;

public sealed class Module : CarterModule
{
    public Module() : base("/products")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("",
                async ([AsParameters] PageDto page, IProductRepo repo,
                    HttpRequest req, CancellationToken ct) =>
                {
                    var result = req.Validate(page);
                    if (!result.IsValid)
                    {
                        return Results.ValidationProblem(
                            result.GetValidationProblems());
                    }

                    var index = page.PageIndex ?? 1;
                    var limit = page.PageSize ?? 20;
                    var offset = (index - 1) * limit;

                    var products = await repo.QueryAsync(
                        offset, limit, ct);
                    return TypedResults.Ok(products);
                })
            .WithName("ListProducts");

        app.MapGet("{id:guid}",
                async (Guid id, IProductRepo repo,
                    HttpRequest req, CancellationToken ct) =>
                {
                    var product = await repo.QueryFindAsync(id, ct);
                    return product == null
                        ? Results.NotFound()
                        : TypedResults.Ok(product);
                })
            .WithName("GetProduct");

        app.MapPost("",
                async (NewProductDto data, IProductRepo repo,
                    HttpRequest req, CancellationToken ct) =>
                {
                    var result = req.Validate(data);
                    if (!result.IsValid)
                    {
                        return Results.ValidationProblem(
                            result.GetValidationProblems());
                    }

                    var product = Product.Create(data.Name);

                    await repo.InsertAsync(product, ct);

                    var dto = Mapper.ToDto(product);

                    return TypedResults.CreatedAtRoute(dto, "GetProduct",
                        new { id = product.Id });
                })
            .WithName("CreateProduct");

        app.MapPut("{id:guid}",
                async (Guid id, NewProductDto data, IProductRepo repo,
                    HttpRequest req, CancellationToken ct) =>
                {
                    var result = req.Validate(data);
                    if (!result.IsValid)
                    {
                        return Results.ValidationProblem(
                            result.GetValidationProblems());
                    }

                    var product = await repo.FindAsync(id, ct);
                    if (product == null || product.IsDeleted)
                    {
                        return Results.NotFound();
                    }

                    product.Update(data.Name);

                    await repo.UpdateAsync(product, ct);

                    return Results.NoContent();
                })
            .WithName("UpdateProduct");

        app.MapDelete("{id:guid}",
                async (Guid id, IProductRepo repo,
                    CancellationToken ct) =>
                {
                    var product = await repo.FindAsync(id, ct);
                    if (product == null || product.IsDeleted)
                    {
                        return Results.NotFound();
                    }

                    product.Delete();

                    await repo.DeleteAsync(product, ct);

                    return Results.NoContent();
                })
            .WithName("DeleteProduct");
    }
}