using Carter;
using Carter.ModelBinding;

using MediatR;

namespace BasketService.ApiService.Baskets;

public class Module : CarterModule
{
    public Module() : base("/baskets")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("", async (
            [AsParameters] GetBasket.Query query,
            HttpRequest req,
            ISender sender,
            CancellationToken ct) =>
        {
            var result = req.Validate(query);
            if (!result.IsValid)
            {
                return Results.ValidationProblem(
                    result.GetValidationProblems());
            }

            var basket = await sender.Send(query, ct);

            return TypedResults.Ok(basket);
        });
    }
}