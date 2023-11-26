using CatalogService.ApiService.Products.Domain;
using CatalogService.ApiService.Products.Models;

using SharedLib.Results;

namespace CatalogService.ApiService.Products.Commands;

using CommandResult = Result<
    OkResultValue<ProductDto>,
    ValidationResultError,
    NotFoundResultError
>;

public static class CreateProduct
{
    public sealed record Command(string Name) : IRequest<CommandResult>;

    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(200);
        }
    }

    public class Handler : IRequestHandler<Command, CommandResult>
    {
        private readonly IProductRepo _repo;

        public Handler(IProductRepo repo)
        {
            _repo = repo;
        }

        public async Task<CommandResult> Handle(
            Command request,
            CancellationToken cancellationToken)
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
        }
    }
}