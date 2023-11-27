using CatalogService.ApiService.Products.Domain;

using Microsoft.AspNetCore.Mvc;

using SharedLib.Contracts;

namespace CatalogService.ApiService.Products.Commands;

using Result = RequestResults<
    NoContentRequestResult,
    NotFoundRequestResult
>;

public static class DeleteProduct
{
    public sealed record Command([FromRoute] Guid Id) : IRequest<Result>;

    public class Handler(IProductRepo repo)
        : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(
            Command request,
            CancellationToken cancellationToken)
        {
            var product = await repo.FindAsync(request.Id, cancellationToken);
            if (product == null)
            {
                return RequestResults.NotFound();
            }

            await repo.DeleteAsync(product, cancellationToken);

            return RequestResults.NoContent();
        }
    }
}