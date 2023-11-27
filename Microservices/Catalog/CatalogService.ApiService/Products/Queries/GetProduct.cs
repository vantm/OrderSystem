using CatalogService.ApiService.Products.Domain;
using CatalogService.ApiService.Products.Models;

using Microsoft.AspNetCore.Mvc;

using SharedLib.Contracts;

namespace CatalogService.ApiService.Products.Queries;

using Result = RequestResults<
    OkRequestResult<ProductDto>,
    NotFoundRequestResult
>;

public class GetProduct
{
    public sealed record Query([FromRoute] Guid Id) : IRequest<Result>;

    public class Handler(IProductRepo repo)
        : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(
            Query request,
            CancellationToken cancellationToken)
        {
            var product =
                await repo.QueryFindAsync(request.Id, cancellationToken);

            if (product == null)
            {
                return RequestResults.NotFound();
            }

            return RequestResults.Ok(product);
        }
    }
}