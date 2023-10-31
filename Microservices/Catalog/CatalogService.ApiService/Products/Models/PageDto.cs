using FluentValidation;

namespace CatalogService.ApiService.Products.Models;

public record PageDto(int? PageIndex, int? PageSize)
{
    public class Validator : AbstractValidator<PageDto>
    {
        public Validator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(10, 500);
        }
    }
}