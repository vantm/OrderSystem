using Dapper;

using FluentValidation;

using MediatR;

using ProductService.ApiService.Products.Models;

using SharedLib.Data;

namespace ProductService.ApiService.Products.Queries;

public static class GetProduct
{
    public class Query : IRequest<ProductDto?>
    {
        public Guid Id { get; set; }
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
        private readonly OpenDbConnection _openDbConnection;
        private readonly ILogger<Handler> _logger;

        public Handler(OpenDbConnection openDbConnection,
            ILogger<Handler> logger)
        {
            _openDbConnection = openDbConnection;
            _logger = logger;
        }

        private const string Sql = """
                                   SELECT *
                                   FROM product
                                   WHERE Id = @id
                                   """;

        public async Task<ProductDto?> Handle(Query request,
            CancellationToken cancellationToken)
        {
            using var conn = _openDbConnection();

            var param = new { id = request.Id };

            ApiService.LoggerExtensions.LogSql(_logger, Sql, param);

            var product =
                await conn.QueryFirstOrDefaultAsync<ProductDto>(Sql, param);

            return product;
        }
    }
}