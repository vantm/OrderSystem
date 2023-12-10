namespace IdentityService.ApiService.Contracts;

public sealed record UserReply(
    Guid Id,
    string UserName,
    bool IsActive);