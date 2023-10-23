using Carter;

namespace ProductService.ApiService.Inventory;

public class Module : CarterModule
{
    public Module() : base("/product-items")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
    }
}