using System.ComponentModel.DataAnnotations;

namespace CatalogService.ApiService.MassTransit;

public class MassTransitModuleOptions
{
    public const string Name = "MassTransitModule";

    [Required] public Uri Host { get; set; } = default!;
    [Required] public string Username { get; set; } = default!;
    [Required] public string Password { get; set; } = default!;
}