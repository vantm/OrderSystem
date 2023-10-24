using Dapper;

using FluentValidation;

using MediatR;

using ProductService.ApiService.Products.Models;

using SharedLib.Data;

using LoggerExtensions = SharedLib.Logging.LoggerExtensions;

namespace ProductService.ApiService.Products.Queries;

public static class ListProducts
{
    public class Query : IRequest<IEnumerable<ProductDto>>
    {
        public int? PageIndex { get; set; } = 0;
        public int? PageSize { get; set; } = 20;
    }

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.PageSize)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo((int)Math.Floor(int.MaxValue / 100.0));
        }
    }

    internal class Handler : IRequestHandler<Query, IEnumerable<ProductDto>>
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
                                   WHERE Id NOT IN (
                                       SELECT Id
                                       FROM product
                                       ORDER BY Id
                                       LIMIT @offset
                                   )
                                   ORDER BY Id
                                   LIMIT @limit
                                   """;

        public async Task<IEnumerable<ProductDto>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            using var conn = _openDbConnection();

            var pageIndex = request.PageIndex ?? 1;
            var pageSize = request.PageSize ?? 20;

            var param = new
            {
                offset = (pageIndex - 1) * pageSize, limit = pageSize
            };

            LoggerExtensions.LogSql(_logger, Sql, param);

            var products = await conn.QueryAsync<ProductDto>(Sql, param);

            return products;
        }
    }
}