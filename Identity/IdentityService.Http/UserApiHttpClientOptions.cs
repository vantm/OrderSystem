namespace IdentityService.Http;

public class UserApiHttpClientOptions
{
    public readonly string Name = "HttpUserApi";

    public string BaseUrl { get; set; } = string.Empty;
}