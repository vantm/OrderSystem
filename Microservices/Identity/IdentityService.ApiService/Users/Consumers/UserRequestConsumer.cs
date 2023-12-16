using IdentityService.ApiService.Contracts;
using IdentityService.ApiService.Users.Domain;

using MassTransit;

using SharedLib.Contracts;

namespace IdentityService.ApiService.Users.Consumers;

public class UserRequestConsumer : IConsumer<UserRequest>
{
    private readonly IUserRepo _repo;

    public UserRequestConsumer(IUserRepo repo)
    {
        _repo = repo;
    }

    public async Task Consume(ConsumeContext<UserRequest> context)
    {
        var userId = context.Message.Id;
        var user = await _repo.FindAsync(userId, context.CancellationToken);

        if (user == null)
        {
            await context.RespondAsync(NotFoundContract.Value);
        }
        else
        {
            var reply = new UserReply(
                user.Id,
                user.UserName.Value,
                user.IsActive);

            await context.RespondAsync(reply);
        }
    }
}