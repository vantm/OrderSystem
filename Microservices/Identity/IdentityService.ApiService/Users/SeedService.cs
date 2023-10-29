namespace IdentityService.ApiService.Users;

public class SeedService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {


        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}