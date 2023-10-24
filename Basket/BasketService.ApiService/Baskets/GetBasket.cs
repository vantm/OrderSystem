using System.Data;

using Dapper;

using FluentValidation;

using MediatR;

using SharedLib.Data;
using SharedLib.Logging;

namespace BasketService.ApiService.Baskets;

public static class GetBasket
{
    public record Query(Guid UserId) : IRequest<BasketDto>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }

    public record Dto(Guid Id, Guid UserId);

    public class Handler(OpenDbConnection openDbConnection,
        ILogger<Handler> logger) : IRequestHandler<Query, BasketDto>
    {
        public async Task<BasketDto> Handle(Query request,
            CancellationToken cancellationToken)
        {
            using var conn = openDbConnection();

            var basket = await FindByUserId(request.UserId, conn);

            if (basket != null)
            {
                return basket;
            }

            var entity = await InsertNewAsync(request.UserId, conn);

            var dto = new BasketDto(
                entity.Id,
                entity.UserId,
                entity.Items);

            return dto;
        }

        private async Task<BasketDto?> FindByUserId(Guid userId,
            IDbConnection conn)
        {
            const string sql = """
                               SELECT *
                               FROM basket
                               WHERE UserId = @userId
                               ORDER BY CreatedAt DESC
                               LIMIT 1
                               """;

            var param = new { userId };

            logger.LogSql(sql, param);

            var basket = await
                conn.QueryFirstOrDefaultAsync<Dto>(sql, new { userId });

            if (basket != null)
            {
                const string sql2 = """
                                    SELECT *
                                    FROM basket_item
                                    WHERE BasketId = @id
                                    """;
                var param2 = new { id = basket.Id };
                logger.LogSql(sql2, param2);

                var items = await conn.QueryAsync<BasketItem>(
                    sql2, param2);

                return new(basket.Id, basket.UserId, items.ToArray());
            }

            return null;
        }

        private async Task<Basket> InsertNewAsync(Guid userId,
            IDbConnection conn)
        {
            var basket = Basket.Create(userId);

            const string sql = """
                               INSERT INTO basket (Id, UserId, CreatedAt, UpdatedAt)
                               VALUES (@Id, @UserId, @CreatedAt, @CreatedAt)
                               """;

            var param = new { basket.Id, basket.UserId, basket.CreatedAt };

            logger.LogSql(sql, param);

            await conn.ExecuteAsync(sql, param);

            return basket;
        }
    }
}