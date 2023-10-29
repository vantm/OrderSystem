using Carter;
using Carter.ModelBinding;

using IdentityService.ApiService.Users.Domain;
using IdentityService.Contracts;

namespace IdentityService.ApiService.Users;

public class UserModule : CarterModule
{
    public UserModule() : base("/users")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("{id:guid}", async (
            Guid id, IUserRepo userRepo, CancellationToken cancellationToken) =>
        {
            var entity = await userRepo.FindAsync(id, cancellationToken);
            if (entity == null)
            {
                return Results.NotFound();
            }

            var model = Mapper.MapUserEntityToModel(entity);

            return TypedResults.Ok(model);
        }).WithName("GetUser");

        app.MapPost("", async (
            NewUserInfo newInfo, IUserRepo userRepo,
            HttpRequest req, CancellationToken cancellationToken) =>
        {
            var result = req.Validate(newInfo);
            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.GetValidationProblems());
            }

            var hashedPassword = HashedPassword.Create(newInfo.Password);

            var entity = User.New(
                new UserName(newInfo.UserName),
                hashedPassword,
                new FullName(newInfo.FullName),
                new EmailAddress(newInfo.EmailAddress),
                newInfo.IsActive);

            await userRepo.InsertAsync(entity, cancellationToken);

            var model = Mapper.MapUserEntityToModel(entity);

            return TypedResults.CreatedAtRoute(
                model,
                "GetUser",
                new { id = model.Id }
            );
        });
    }
}