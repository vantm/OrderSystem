using CatalogService.ApiService.Products.Domain;
using CatalogService.ApiService.Products.Models;

using SharedLib.Contracts;

namespace CatalogService.ApiService.Products.Commands;

using Result = RequestResults<
    CreatedRequestResult<ProductDto>,
    ValidationProblemRequestResult
>;

public static class CreateProduct
{
    public sealed record Command(string Name) : IRequest<Result>;

    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MinimumLength(10).MaximumLength(200);
        }
    }

    public class Handler(IProductRepo repo)
        : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(
            Command request,
            CancellationToken cancellationToken)
        {
            var product = Product.Create(request.Name);

            await repo.InsertAsync(product, cancellationToken);

            var dto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };

            return RequestResults.Created(dto);
        }
    }
}