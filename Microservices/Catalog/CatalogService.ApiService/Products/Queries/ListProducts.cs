using CatalogService.ApiService.Products.Domain;
using CatalogService.ApiService.Products.Models;

using SharedLib.Contracts;

namespace CatalogService.ApiService.Products.Queries;

using Result = RequestResults<
    OkRequestResult<IEnumerable<ProductDto>>,
    ValidationProblemRequestResult
>;

public static class ListProducts
{
    public record Query(int? PageIndex, int? PageSize)
        : PageDto(PageIndex, PageSize), IRequest<Result>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).LessThanOrEqualTo(200_000);

            RuleFor(x => x.PageSize)
                .GreaterThan(0).LessThanOrEqualTo(200);
        }
    }

    public class Handler(IProductRepo repo) : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(Query request,
            CancellationToken cancellationToken)
        {
            var index = request.PageIndex ?? 1;
            var limit = request.PageSize ?? 20;
            var offset = (index - 1) * limit;

            var products = await repo.QueryAsync(
                offset, limit, cancellationToken);

            return RequestResults.Ok(products);
        }
    }
}