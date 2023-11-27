using CatalogService.ApiService.Products.Domain;

using Microsoft.AspNetCore.Mvc;

using SharedLib.Contracts;

namespace CatalogService.ApiService.Products.Commands;

using Result = RequestResults<
    NoContentRequestResult,
    NotFoundRequestResult,
    ValidationProblemRequestResult
>;

public static class UpdateProduct
{
    public sealed record Command(
        [FromRoute] Guid Id,
        [FromBody] Body Body) : IRequest<Result>;

    public sealed record Body(string Name);

    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Body.Name)
                .NotEmpty().MinimumLength(10).MaximumLength(200)
                .OverridePropertyName(nameof(Body.Name));
        }
    }

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

            await repo.UpdateAsync(product, cancellationToken);

            return RequestResults.NoContent();
        }
    }
}