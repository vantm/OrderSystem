using Carter;

namespace InventoryService.ApiService.Batches;

public class BatchModule : CarterModule
{
    public BatchModule() : base("/batches")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("", () =>
        {

        });
    }
}