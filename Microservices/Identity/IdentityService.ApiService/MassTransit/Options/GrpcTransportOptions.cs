using System.ComponentModel.DataAnnotations;

namespace IdentityService.ApiService.MassTransit.Options;

public class GrpcTransportOptions
{
    public const string Name = "GrpcTransport";

    [Required] public Uri Host { get; set; } = new("http://localhost");

    [Required] public Uri[] Servers { get; set; } = Array.Empty<Uri>();
}