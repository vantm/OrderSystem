using Carter;

namespace InventoryService.ApiService.Batches;

public class Module : CarterModule
{
    public Module() : base("/batches")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("", () =>
        {

        });
    }
}