using CatalogService.ApiService.Products.Models;

using Dapper;

using FluentValidation;

using MediatR;

using SharedLib.Data;

using LoggerExtensions = SharedLib.Logging.LoggerExtensions;

namespace CatalogService.ApiService.Products.Queries;

public static class GetProduct
{
    public record Query(Guid Id) : IRequest<ProductDto?>
    {
    }

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    internal class Handler : IRequestHandler<Query, ProductDto?>
    {
        private readonly CreateDbConnection _createDbConnection;
        private readonly ILogger<Handler> _logger;

        public Handler(CreateDbConnection createDbConnection,
            ILogger<Handler> logger)
        {
            _createDbConnection = createDbConnection;
            _logger = logger;
        }

        private const string Sql = """
                                   SELECT *
                                   FROM [Catalog].[Product]
                                   WHERE [Id] = @id AND [IsDeleted] = 0
                                   """;

        public async Task<ProductDto?> Handle(Query request,
            CancellationToken cancellationToken)
        {
            using var conn = _createDbConnection();

            var param = new { id = request.Id };

            LoggerExtensions.LogSql(_logger, Sql, param);

            var product =
                await conn.QueryFirstOrDefaultAsync<ProductDto>(Sql, param);

            return product;
        }
    }
}