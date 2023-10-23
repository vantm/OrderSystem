﻿using Carter;
using Carter.ModelBinding;

using MediatR;

using ProductService.ApiService.Products.Domain;
using ProductService.ApiService.Products.Helpers;
using ProductService.ApiService.Products.Models;
using ProductService.ApiService.Products.Queries;

using SharedLib.Domain;

namespace ProductService.ApiService.Products;

public sealed class Module : CarterModule
{
    public Module() : base("/products")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("",
                async ([AsParameters] ListProducts.Query query, ISender sender,
                    HttpRequest req, CancellationToken ct) =>
                {
                    var result = req.Validate(query);
                    if (!result.IsValid)
                    {
                        return Results.ValidationProblem(
                            result.GetValidationProblems());
                    }

                    var products = await sender.Send(query, ct);
                    return TypedResults.Ok(products);
                })
            .WithName("ListProducts");

        app.MapGet("{id:guid}",
                async ([AsParameters] GetProduct.Query query, ISender sender,
                    HttpRequest req, CancellationToken ct) =>
                {
                    var result = req.Validate(query);
                    if (!result.IsValid)
                    {
                        return Results.ValidationProblem(
                            result.GetValidationProblems());
                    }

                    var product = await sender.Send(query, ct);

                    return product == null
                        ? Results.NotFound()
                        : TypedResults.Ok(product);
                })
            .WithName("GetProduct");

        app.MapPost("",
                async (NewProductDto data, IRepo<Product, Guid> repo,
                    HttpRequest req, CancellationToken ct) =>
                {
                    var result = req.Validate(data);
                    if (!result.IsValid)
                    {
                        return Results.ValidationProblem(
                            result.GetValidationProblems());
                    }

                    var product = Product.Create(
                        data.Name,
                        data.ImageBuffer()
                    );

                    await repo.InsertAsync(product, ct);

                    var dto = Mapper.ToDto(product);

                    return TypedResults.CreatedAtRoute(dto, "GetProduct",
                        new { id = product.Id });
                })
            .WithName("CreateProduct");

        app.MapPut("{id:guid}",
                async (Guid id, NewProductDto data, IRepo<Product, Guid> repo,
                    HttpRequest req, CancellationToken ct) =>
                {
                    var result = req.Validate(data);
                    if (!result.IsValid)
                    {
                        return Results.ValidationProblem(
                            result.GetValidationProblems());
                    }

                    var product = await repo.FindAsync(id, ct);
                    if (product == null)
                    {
                        return Results.NotFound();
                    }

                    product.Update(
                        data.Name,
                        data.ImageBuffer());

                    await repo.UpdateAsync(product, ct);

                    return Results.NoContent();
                })
            .WithName("UpdateProduct");
    }
}