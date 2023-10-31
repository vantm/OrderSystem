using FluentValidation;

namespace CatalogService.ApiService.Products.Models;

public record NewProductDto(string Name)
{
    public class Validator : AbstractValidator<NewProductDto>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(200);
        }
    }
}